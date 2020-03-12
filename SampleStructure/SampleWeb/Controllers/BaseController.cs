using Constant;
using Logging;
using SampleWeb.CommonLogic;
using SampleWeb.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using ViewModel.User;
using static Utilities.Utility;
using static Utility.Utility;

namespace SampleWeb.Controllers
{
    //[CustomAuthorization]
    public class BaseController : Controller
    {
        public readonly AuthenticationBH _AuthenticationBH;
        BL objBL = new BL();
        public readonly ILogger _logger;

        public BaseController(ILogger logger/*ISharedManager sharedManager, ICacheProvider cacheProvider, IMeetingManager meetingManager*/)
        {

            _logger = logger;
            _AuthenticationBH = new AuthenticationBH();
        }

        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            if (Request.IsAuthenticated && SessionWrapper.CurrentSession.User_Id == 0)
            {
                if (Session["UName"] != null && Session["PWord"] != null)
                {
                    var mdbUser = _AuthenticationBH.Login(Session["UName"].ToString(), Session["PWord"].ToString());
                    SetSessionData(mdbUser);
                }
            }
            else
            {
                if (Request.Headers != null && Request.Headers[Utility.Constants.Authorization] != null)
                {
                    var mdbUser = _AuthenticationBH.GetInfoByUserID(Request.Headers[Utility.Constants.Authorization]);
                    if (mdbUser.StatusCode == (int)ExternalService.StatusType.Success)
                    {
                        SetSessionData(mdbUser);
                    }
                }
                if (Request.Headers[Utility.Constants.SELECTED_LANGUAGE] != null)
                {
                    Session[Utility.Constants.SELECTED_LANGUAGE] = Request.Headers[Utility.Constants.SELECTED_LANGUAGE];
                }
            }

            if (Session[Utility.Constants.SELECTED_LANGUAGE] == null)
            {
                Session.Add(Utility.Constants.SELECTED_LANGUAGE, "en");
            }
            else
            {
                string lang = Session[Utility.Constants.SELECTED_LANGUAGE] as string;

                switch (lang)
                {
                    case "ar":
                        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(Constant.LanguageCulture.UAEArabicCulture);
                        Thread.CurrentThread.CurrentUICulture = new CultureInfo(Constant.LanguageCulture.UAEArabicCulture);
                        Thread.CurrentThread.CurrentCulture.DateTimeFormat = new CultureInfo(Constant.LanguageCulture.UAEArabicCulture).DateTimeFormat;
                        break;
                    case "en":
                        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(Constant.LanguageCulture.USEnglishCulture);
                        Thread.CurrentThread.CurrentUICulture = new CultureInfo(Constant.LanguageCulture.USEnglishCulture);
                        Thread.CurrentThread.CurrentCulture.DateTimeFormat = new CultureInfo(Constant.LanguageCulture.USEnglishCulture).DateTimeFormat;
                        break;
                    default:
                        break;
                }

            }

            return base.BeginExecuteCore(callback, state);
        }

        protected string GetErrorFromModelStat(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(v => v.Errors).ToList();
            if (errors.Any())
            {
                StringBuilder errorBuilder = new StringBuilder();
                errorBuilder.Append("<ui class='text-left'>");
                foreach (var error in errors)
                    errorBuilder.Append($"<li>{error.ErrorMessage}</li>");
                errorBuilder.Append("</ui>");

                return errorBuilder.ToString();
            }
            return string.Empty;
        }

        public void SetSessionData(UserViewModel mdbUser)
        {

            if (mdbUser != null && mdbUser.Data != null)
            {
                Session["checksession"] = mdbUser.Data.UserID;
                if (SessionWrapper.CurrentSession.User_Id == 0)
                    SessionWrapper.CurrentSession.User_Id = mdbUser.Data.UserID;

                if (SessionWrapper.CurrentSession.UserName == null)
                    //SessionWrapper.CurrentSession.UserName =Decrypt(mdbUser.Data.UserDisplayName);
                    SessionWrapper.CurrentSession.UserName = mdbUser.Data.UserDisplayName;

                if (SessionWrapper.CurrentSession.BusinessSystemID == 0)
                    SessionWrapper.CurrentSession.BusinessSystemID = Convert.ToInt64(mdbUser.Data.BusinessSystemID);

                if (SessionWrapper.CurrentSession.OrganizationUnitID == 0)
                    SessionWrapper.CurrentSession.OrganizationUnitID = Convert.ToInt64(mdbUser.Data.UserOUID);

                if (string.IsNullOrEmpty(SessionWrapper.CurrentSession.OrganizationUnitNameEn))
                {
                    SessionWrapper.CurrentSession.OrganizationUnitNameEn = mdbUser.Data.OUNameEn;
                    SessionWrapper.CurrentSession.OrganizationUnitNameAr = mdbUser.Data.OUNameAr;

                }
                if (string.IsNullOrEmpty(SessionWrapper.CurrentSession.Mobile))
                {
                    if (mdbUser.Data.UserMobile != null)
                    {
                        SessionWrapper.CurrentSession.Mobile = Decrypt(mdbUser.Data.UserMobile);
                    }

                }
                if (SessionWrapper.CurrentSession.BusinessSystemsList == null)
                {
                    SessionWrapper.CurrentSession.BusinessSystemsList = mdbUser.Data.BusinessSystemIDs;
                }

                if (mdbUser.Data.ProfileImage != null)
                {
                    if (mdbUser.Data.ProfileImage.Length > 0)
                    {
                        MemoryStream ms = new MemoryStream(mdbUser.Data.ProfileImage, 0, mdbUser.Data.ProfileImage.Length);
                        ms.Write(mdbUser.Data.ProfileImage, 0, mdbUser.Data.ProfileImage.Length);
                        Image returnImage = Image.FromStream(ms, true);//Exception occurs here
                                                                       //returnImage.Save("output.jpg", ImageFormat.Jpeg)
                        Bitmap bitmap;
                        bitmap = new Bitmap(returnImage);
                        Session["bitmap"] = bitmap;

                    }
                }


                if (mdbUser.Data.UserSignature != null)
                {
                    SessionWrapper.CurrentSession.UserSignature = mdbUser.Data.UserSignature;
                    MemoryStream ms = new MemoryStream(mdbUser.Data.UserSignature);
                    System.Drawing.Bitmap returnImage = (System.Drawing.Bitmap)System.Drawing.Bitmap.FromStream(ms);
                    System.Drawing.Bitmap newImage = Transparent2Color(returnImage, Color.White);
                    Session["Signature"] = newImage;
                }
                else
                    SessionWrapper.CurrentSession.UserSignature = null;

                if (SessionWrapper.CurrentSession.UserNationalityID == 0)
                    SessionWrapper.CurrentSession.UserNationalityID = Convert.ToInt64(mdbUser.Data.UserNationalityID);

                if (string.IsNullOrEmpty(SessionWrapper.CurrentSession.UserMobile))
                {
                    if (mdbUser.Data.UserMobile != null)
                        SessionWrapper.CurrentSession.UserMobile = Decrypt(mdbUser.Data.UserMobile);
                }

                if (string.IsNullOrEmpty(SessionWrapper.CurrentSession.UserEID))
                {
                    if (mdbUser.Data.UserEID != null)
                        SessionWrapper.CurrentSession.UserEID = Decrypt(mdbUser.Data.UserEID);

                }

                if (string.IsNullOrEmpty(SessionWrapper.CurrentSession.FullName))
                {
                    SessionWrapper.CurrentSession.FullName = mdbUser.Data.UserDisplayName;
                }



                if (string.IsNullOrEmpty(SessionWrapper.CurrentSession.Email))
                {
                    if (mdbUser.Data.UserEmail != null)
                        SessionWrapper.CurrentSession.Email = Decrypt(mdbUser.Data.UserEmail);

                }
                if (SessionWrapper.CurrentSession.UserCompanyID == 0)
                {
                    SessionWrapper.CurrentSession.UserCompanyID = Convert.ToInt64(mdbUser.Data.UserCompanyID);

                }

                if (SessionWrapper.CurrentSession.UserTypeID == 0)
                {
                    SessionWrapper.CurrentSession.UserTypeID = Convert.ToInt64(mdbUser.Data.UserTypeID);

                }

                SessionWrapper.CurrentSession.LastLoginDayCount = mdbUser.Data.LastLoginDayCount;

                if (SessionWrapper.CurrentSession.RolesID == null)
                {
                    SessionWrapper.CurrentSession.RolesID = mdbUser.Data.RoleIDs;
                }

                if (SessionWrapper.CurrentSession.GovtEntityID == null)
                    SessionWrapper.CurrentSession.GovtEntityID = mdbUser.Data.GovEntityID;

                if (SessionWrapper.CurrentSession.ActionGroupID == 0)
                    SessionWrapper.CurrentSession.ActionGroupID = Convert.ToInt64(mdbUser.Data.ActionGroupID);

                if (string.IsNullOrEmpty(SessionWrapper.CurrentSession.DesignationEn))
                    SessionWrapper.CurrentSession.DesignationEn = mdbUser.Data.DesignationEn;

                if (string.IsNullOrEmpty(SessionWrapper.CurrentSession.DesignationAr))
                    SessionWrapper.CurrentSession.DesignationAr = mdbUser.Data.DesignationAr;

                if (SessionWrapper.CurrentSession.EntityID == 0)
                {
                    try
                    {
                        var a = 0;//API will be here _registrationManger.GetCurrentActiveEnity((Convert.ToInt32(mdbUser.Data.UserID))).Data;
                        if (a != 0)
                        {
                            if (a > 0)
                                SessionWrapper.CurrentSession.EntityID = a;
                        }
                    }
                    catch (Exception ex)
                    {
                        SessionWrapper.CurrentSession.EntityID = 0;
                    }
                }
                //string userId = Encrypt(SessionWrapper.CurrentSession.User_Id.ToString());
                //RolesAndPermissionsViewModel objModel = objBL.GetRoleandPermision(userId, AuthorizationToken());
                //if (objModel.StatusCode == 200)
                //{
                //    SessionWrapper.CurrentSession.Permissions = objModel.Data;
                //}
            }
        }

        public ActionResult Error(Exception exception)
        {
            string message;
            if (HttpContext.IsDebuggingEnabled)
                message = "There is something wrong. Please contact your administrator.";
            else
                message = exception.Message;

            if (string.IsNullOrEmpty(exception.StackTrace))
                message += exception.StackTrace;

            @ViewBag.Error = message;

            return View("Error");
        }

        public string AuthorizationToken()
        {
            DateTime epochStart = new DateTime(1970, 01, 01, 0, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan timeSpan = DateTime.UtcNow - epochStart;
            string requestTimeStamp = Convert.ToUInt64(timeSpan.TotalSeconds).ToString();
            string usertoken = Utility.Utility.Encrypt(SessionWrapper.CurrentSession.User_Id.ToString());
            return usertoken + "$" + requestTimeStamp + "$" + System.Environment.MachineName;
        }

        public Bitmap Transparent2Color(Bitmap bmp1, Color target)
        {
            Bitmap bmp2 = new Bitmap(bmp1.Width, bmp1.Height);
            Rectangle rect = new Rectangle(Point.Empty, bmp1.Size);
            using (Graphics G = Graphics.FromImage(bmp2))
            {
                G.Clear(target);
                G.DrawImageUnscaledAndClipped(bmp1, rect);
            }
            return bmp2;
        }

        protected string GetBaseUrl()
        {
            var request = HttpContext.Request;
            var appUrl = HttpRuntime.AppDomainAppVirtualPath;
            if (appUrl != "/")
                appUrl = "/" + appUrl;
            var baseUrl = string.Format("{0}://{1}{2}", request.Url.Scheme, request.Url.Authority, appUrl);
            return baseUrl;
        }

        public ActionResult ChangeCulture(string lng, string returnUrl)
        {
            try
            {
                //SessionWrapper.CurrentSession.Language = lng.ToString();
                Session.Remove(Utility.Constants.SELECTED_LANGUAGE);

                Session.Add(Utility.Constants.SELECTED_LANGUAGE, lng);

                Session["Culture"] = new CultureInfo(lng);

                if (lng == "en")
                {
                    SessionWrapper.CurrentSession.Language = "English";
                }
                else
                {
                    SessionWrapper.CurrentSession.Language = "Arabic";
                }
                return Redirect(returnUrl);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void LogException(Exception ex)
        {
            _logger.Error(ex);
        }
    }
}