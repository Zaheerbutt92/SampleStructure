using Constant;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using ViewModel.Administration;
using ViewModel.User;

namespace SampleWeb.CommonLogic
{
    public class AuthenticationBH
    {
        public UserViewModel Login(string UserName, string Password)
        {
            UserViewModel objUserViewModel = new UserViewModel();
            try
            {
                // ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                LoginModel objLoginModel = new LoginModel();
                objLoginModel.UserName = UserName;
                objLoginModel.Password = Password;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Utility.Utility.GetSharedApibaseUrl());
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + "testauth");
                //HttpResponseMessage response = client.PostAsJsonAsync("Login", objLoginModel).Result;

                var json = JsonConvert.SerializeObject(objLoginModel);
                var httpContent = new StringContent(json, Encoding.Unicode, HttpContentType.ContentTypeJson);
                HttpResponseMessage response = client.PostAsync("Login", httpContent).Result;

                if (response.IsSuccessStatusCode)
                {
                    var Response = response.Content.ReadAsStringAsync().Result;
                    objUserViewModel = JsonConvert.DeserializeObject<UserViewModel>(Response);
                }
            }
            catch (Exception ex)
            {
                Utility.Utility.ErrorLogging(ex);
            }

            return objUserViewModel;
        }

        //public PublicUserViewModel RegisterPublicUser(PublicUserModel UserMode)
        //{
        //    PublicUserViewModel objPublicUserViewModel = new PublicUserViewModel();
        //    try
        //    {
        //        HttpClient client = new HttpClient();
        //        client.BaseAddress = new Uri(IA.LC.Utility.Utility.GetSharedApibaseUrl());
        //        client.DefaultRequestHeaders.Accept.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + "testauth");
        //        HttpResponseMessage response = client.PostAsJsonAsync("RegisterPublicUser", UserMode).Result;
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var Response = response.Content.ReadAsStringAsync().Result;
        //            objPublicUserViewModel = JsonConvert.DeserializeObject<PublicUserViewModel>(Response);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        IA.LC.Utility.Utility.ErrorLogging(ex);
        //    }

        //    return objPublicUserViewModel;
        //}

        //public ForgotViewModel ForgotPassword(string Email)
        //{
        //    ForgotViewModel objForgotViewModel = new ForgotViewModel();
        //    try
        //    {

        //        HttpClient client = new HttpClient();
        //        client.BaseAddress = new Uri(IA.LC.Utility.Utility.GetSharedApibaseUrl());
        //        client.DefaultRequestHeaders.Accept.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + "testauth");
        //        HttpResponseMessage response = client.GetAsync("ForgetPassword?Email=" + Email).Result;
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var Response = response.Content.ReadAsStringAsync().Result;
        //            objForgotViewModel = JsonConvert.DeserializeObject<ForgotViewModel>(Response);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    return objForgotViewModel;
        //}

        //public ForgotViewModel UpdatePassword(string Email, string Password)
        //{
        //    ForgotViewModel objForgotViewModel = new ForgotViewModel();
        //    try
        //    {

        //        HttpClient client = new HttpClient();
        //        client.BaseAddress = new Uri(IA.LC.Utility.Utility.GetSharedApibaseUrl());
        //        client.DefaultRequestHeaders.Accept.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + "testauth");
        //        HttpResponseMessage response = client.GetAsync("UpdatePassword?Email=" + Email + "&Password=" + Password).Result;
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var Response = response.Content.ReadAsStringAsync().Result;
        //            objForgotViewModel = JsonConvert.DeserializeObject<ForgotViewModel>(Response);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    return objForgotViewModel;
        //}

        public UserViewModel GetInfoByUserID(string UserID)
        {
            UserViewModel objUserViewModel = new UserViewModel();
            objUserViewModel.Data = new UserModel();
            try
            {

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Utility.Utility.GetSharedApibaseUrl());
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + "testauth");
                HttpResponseMessage response = client.GetAsync("GetInfoByUserID?UserID=" + UserID).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Response = response.Content.ReadAsStringAsync().Result;
                    objUserViewModel = JsonConvert.DeserializeObject<UserViewModel>(Response);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objUserViewModel;
        }

        //public GenericResponseViewModel GenerateOTP(string Email)
        //{
        //    GenericResponseViewModel objForgotViewModel = new GenericResponseViewModel();
        //    try
        //    {

        //        HttpClient client = new HttpClient();
        //        client.BaseAddress = new Uri(IA.LC.Utility.Utility.GetSharedApibaseUrl());
        //        client.DefaultRequestHeaders.Accept.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + "testauth");
        //        HttpResponseMessage response = client.GetAsync("GenerateOTP?Email=" + Email).Result;
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var Response = response.Content.ReadAsStringAsync().Result;
        //            objForgotViewModel = JsonConvert.DeserializeObject<GenericResponseViewModel>(Response);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    return objForgotViewModel;
        //}

        //public GenericResponseViewModel VerifyOTP(string Email, string OTP)
        //{
        //    GenericResponseViewModel objForgotViewModel = new GenericResponseViewModel();
        //    try
        //    {

        //        HttpClient client = new HttpClient();
        //        client.BaseAddress = new Uri(IA.LC.Utility.Utility.GetSharedApibaseUrl());
        //        client.DefaultRequestHeaders.Accept.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + "testauth");
        //        HttpResponseMessage response = client.GetAsync("VerifyOTP?Email=" + Email + "&OTP=" + OTP).Result;
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var Response = response.Content.ReadAsStringAsync().Result;
        //            objForgotViewModel = JsonConvert.DeserializeObject<GenericResponseViewModel>(Response);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    return objForgotViewModel;
        //}

        //public GenericResponseViewModel UserLoggedOut(string UserID)
        //{
        //    GenericResponseViewModel objForgotViewModel = new GenericResponseViewModel();
        //    try
        //    {

        //        HttpClient client = new HttpClient();
        //        client.BaseAddress = new Uri(IA.LC.Utility.Utility.GetSharedApibaseUrl());
        //        client.DefaultRequestHeaders.Accept.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + "testauth");
        //        HttpResponseMessage response = client.GetAsync("UpdateLogoutAudit?TrailKey=" + UserID).Result;
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var Response = response.Content.ReadAsStringAsync().Result;
        //            objForgotViewModel = JsonConvert.DeserializeObject<GenericResponseViewModel>(Response);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    return objForgotViewModel;
        //}
    }
}