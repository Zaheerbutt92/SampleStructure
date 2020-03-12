using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using ViewModel.User;

namespace SampleWeb.CommonLogic
{
    public class BL
    {
        //public UserModel GetUserInfo(string AuthorizationToken)
        //{
        //    UserModel UserModel = new UserModel();
        //    try
        //    {
        //        UserViewModel UserVM = new UserViewModel();
        //        HttpClient client = new HttpClient();
        //        client.BaseAddress = new Uri(Utility.Utility.GetSharedApibaseUrl());
        //        client.DefaultRequestHeaders.Accept.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", AuthorizationToken);
        //        HttpResponseMessage response = client.GetAsync("Login?UserName=1&Password=2").Result;

        //        if (response.IsSuccessStatusCode)
        //        {
        //            var answer = response.Content.ReadAsStringAsync().Result;
        //            UserVM = JsonConvert.DeserializeObject<UserViewModel>(answer);
        //            if (UserVM.StatusCode == Convert.ToInt32((long)Constant.ExternalService.StatusType.Success))
        //            {
        //                UserModel = UserVM.Data;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return UserModel;
        //}

        //public RolesAndPermissionsViewModel GetRoleandPermision(string UserID, string AuthorizationToken)
        //{
        //    RolesAndPermissionsViewModel objRolesAndPermissionsViewModel = new RolesAndPermissionsViewModel();
        //    try
        //    {
        //        HttpClient client = new HttpClient();
        //        client.BaseAddress = new Uri(Utility.Utility.GetSharedApibaseUrl());
        //        client.DefaultRequestHeaders.Accept.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", AuthorizationToken);
        //        HttpResponseMessage response = client.GetAsync("GetScreenList_mobile?UserID=" + UserID.ToString()).Result;

        //        if (response.IsSuccessStatusCode)
        //        {
        //            var answer = response.Content.ReadAsStringAsync().Result;
        //            objRolesAndPermissionsViewModel = JsonConvert.DeserializeObject<RolesAndPermissionsViewModel>(answer);
        //        }


        //        return objRolesAndPermissionsViewModel;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public AuditTrailViewModel SaveAuditTrail(AuditTrailModel objAudit, string AuthorizationToken)
        //{
        //    AuditTrailViewModel objAuditTrailViewModel = new AuditTrailViewModel();
        //    try
        //    {
        //        HttpClient client = new HttpClient();
        //        client.BaseAddress = new Uri(Utility.Utility.GetSharedApibaseUrl());
        //        client.DefaultRequestHeaders.Accept.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", AuthorizationToken);
        //        HttpResponseMessage response = client.PostAsJsonAsync("SaveAuditTrail", objAudit).Result;

        //        if (response.IsSuccessStatusCode)
        //        {
        //            var answer = response.Content.ReadAsStringAsync().Result;
        //            objAuditTrailViewModel = JsonConvert.DeserializeObject<AuditTrailViewModel>(answer);
        //        }


        //        return objAuditTrailViewModel;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

    }
}