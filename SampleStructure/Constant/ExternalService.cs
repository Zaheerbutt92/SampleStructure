using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constant
{
    public class ExternalService
    {
        #region Email APIs
        public const string SendEmailApi = "{0}/SendEmail";

        #endregion

        #region SMS APIs

        public const string SendSmsapi = "{0}/Api/SharedComponentAPI/SendMessage";

        #endregion

        #region External Enum
        public enum StatusType
        {
            Success = 200,
            Empty = 201,
            Error = 203,
            Duplicate = 204,
            InvalidCredentials = 205,
            NoActiveEmailExists = 206,
            EmailSent = 207,
            AccountBlocked = 208,
            InvalidUserName = 209,
            InvalidPassworde = 210,
            UserNameAlreadyExists = 211,
            UserEmailAlreadyExists = 212,
            UserEIDAlreadyExists = 213,
            UserMobileAlreadyExists = 214,
            Failure = 215,
            AccountIsInactive = 216,
            UnAuthorizedRequest = 217
        }

        #endregion

        #region External Constant

        #region SMS Templates
        public class SMSTemplate
        {
            public const string SmsLogin = "LOGIN";
            public const string Changepassword = "CHANGE PASSWORD";
            public const string Forgetpassword = "FORGET PASSWORD";
            public const string Registration = "REGISTRATION";
        }

        #endregion

        #endregion

        #region External Shared Constant

        public const string UrlGetSharedUsersApi = "{0}/GetAllUsers?BusinessSystemID={1}";
        public const string UrlGetUserByIdApi = "{0}/GetInfoByUserID?UserID={1}";
        public const string UrlGetDepartmentsApi = "{0}/GetDepartments";
        #endregion

        #region Users

        public const string GetUserById = "{0}/GetInfoByUserID?UserID={1}";
        public const string GetUsersByRoleId = "{0}/GetUsersByRoleID?RoleID={1}";
        public const string GetUsersByRoleIds = "{0}/GetUsersByRolesID";
        public const string GetUserSuperviserByRoleIds = "{0}/GetUserSuperviserByRoleIDsAndUserTypeID";
        public const string GetUserSuperviserByUserId = "{0}/GetUserSuperviserByRoleIDsAndUserTypeID";
        public const string GetUsersByCompanyID = "{0}/GetUsersByCompanyID?CompanyID={1}";
        public const string GetUsersByDepartmentID = "{0}/GetUsersByOUID?OUID={1}";
        public const string GetScreenListByUserID = "{0}/GetScreenList_Mobile?UserID={1}";
        public const string GetUsersByBusinessSystemId = "{0}GetAllUsers?BusinessSystemID={1}";
        //public const string GetAvailableUsersByBusinessSystemIdAndRoleIds = "{0}GetAvailableUsersByBusinessSystemIdAndRoleIds";
        public const string GetAvailableUsersByBusinessSystemIdAndRoleIds = "{0}GetAvailableUsers_LC_ByBusinessSystemIdAndRoleIds";


        //Meeting
        public const string GetOrgUnits = "{0}/GetOrgUnitsList";
        public const string GetActionGroupByOrgUnitID = "{0}/GetActionGroupByOUID?OUID={1}";
        public const string GetOrgUnitsListLinkedwithGovEntity = "{0}/GetOrgUnitsListLinkedwithGovEntity?key={1}";
        public const string GetActiveAttendeeParty = "{0}/GetActiveAttendeeParty?key={1}";

        public const string GetUsersByActionGroupId = "{0}/GetUsersByActionGroupID?ActionGroupID={1}&BusinessSystemID=0";
        public const string GetGovernmentEntityList = "{0}/GetGovernmentEntityList";
        public const string GetOrgUnitsListByEntityID = "{0}/GetOrgUnitsListByEntityID?EntityID={1}";



        #endregion

        #region Roles

        public const string GetRolesByBusinessSystemId = "{0}GetRolesListByBusinessID?BusinessID={1}";
        public const string GetRolesListByUserID = "{0}GetRolesListByUserID?UserID={1}";

        #endregion

        #region Countries

        public const string GetCountries = "{0}/GetCountriesList";

        #endregion

        #region Emirates

        public const string GetEmirates = "{0}/GetEmiratesList";

        #endregion

        #region SystemConfig

        public const string GetSystemConfig = "{0}/GetSystemConfig?BusinessSystemID={1}";

        #endregion

        #region Insurance Company

        public const string GetInsuranceCompany = "{0}/GetRegisterInsuranceCompany";

        #endregion

        #region Entity Type

        public const string GetEntityType = "{0}/GetAllEntityTypes";

        #endregion

        #region Email templates
        public const string GetEmailTemplatebyTypeID = "{0}/GetEmailTemplatebyTypeID?TemplateTypeID={1}";
        public const string GetEmailTemplatebyActionCode = "{0}/GetEmailTemplatebyActionCode?ActionCode={1}";
        #endregion

        #region Reminder

        public const string GetReminderConfigurationByTemplatebyTypeID = "{0}/GetReminderConfigurationByTemplatebyTypeID?TemplateTypeID={1}";

        #endregion
    }
}
