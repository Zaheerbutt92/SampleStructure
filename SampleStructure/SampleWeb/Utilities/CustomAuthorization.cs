using Constant;
using SampleWeb.CommonLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using static Utilities.Utility;
using static Utility.Utility;

namespace SampleWeb.Utilities
{
    public class CustomAuthorization : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            AuthenticationBH _AuthenticationBH = new AuthenticationBH();
            if (!HttpContext.Current.Request.IsAuthenticated)
            {
                if (HttpContext.Current.Request.Headers != null && HttpContext.Current.Request.Headers[Utility.Constants.Authorization] != null)
                {
                    var mdbUser = _AuthenticationBH.GetInfoByUserID(HttpContext.Current.Request.Headers[Utility.Constants.Authorization]);
                    if (mdbUser.StatusCode != (int)ExternalService.StatusType.Success)
                    {
                        filterContext.Result = new RedirectResult(System.Web.Security.FormsAuthentication.LoginUrl + "?ReturnUrl=" +
                        filterContext.HttpContext.Server.UrlEncode(filterContext.HttpContext.Request.RawUrl));
                    }
                }
                else if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.HttpContext.Response.StatusCode = 401; // session timed out. Unauthorized request
                    filterContext.HttpContext.Response.End();
                    // For now redirect to login page in case of ajax call when session expires
                    filterContext.Result = new RedirectResult(System.Web.Security.FormsAuthentication.LoginUrl + "?ReturnUrl=" +
                    filterContext.HttpContext.Server.UrlEncode(filterContext.HttpContext.Request.RawUrl));
                    return;
                }
                else
                {
                    filterContext.Result = new RedirectResult(System.Web.Security.FormsAuthentication.LoginUrl + "?ReturnUrl=" +
                    filterContext.HttpContext.Server.UrlEncode(filterContext.HttpContext.Request.RawUrl));
                    return;
                }
            }
            else if (HttpContext.Current.Request.IsAuthenticated && SessionWrapper.CurrentSession.User_Id == 0) //means session state has expired
            {
                FormsAuthentication.SignOut();
                filterContext.Result = new RedirectResult(System.Web.Security.FormsAuthentication.LoginUrl + "?ReturnUrl=" +
                filterContext.HttpContext.Server.UrlEncode(filterContext.HttpContext.Request.RawUrl));
                return;
            }

        }
    }
}