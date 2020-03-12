using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.User
{
    public class UserViewModel
    {
        public UserModel Data { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public int LoginAttempt { get; set; }
    }

    public class UserModel
    {

        public long UserID { get; set; }
        public string UserName { get; set; }
        public string UserEmpID { get; set; }
        public string UserEID { get; set; }
        public Nullable<long> UserNationalityID { get; set; }
        public string UserEmail { get; set; }
        public string UserMobile { get; set; }
        public Nullable<long> UserTypeID { get; set; }
        public Nullable<long> UserDesignationID { get; set; }
        public string UserDeviceUDID { get; set; }
        public Nullable<long> UserOUID { get; set; }
        public Nullable<long> UserCompanyID { get; set; }
        public byte[] UserSignature { get; set; }
        public byte[] ProfileImage { get; set; }
        public Nullable<bool> UserStatus { get; set; }
        public Nullable<long> BusinessSystemID { get; set; }
        public string DesignationEn { get; set; }
        public string UserDisplayName { get; set; }
        public string DesignationAr { get; set; }
        public string CountryNameEn { get; set; }
        public string CountryNameAr { get; set; }
        public string BusinessSystemNameEn { get; set; }
        public string BusinessSystemNameAr { get; set; }
        public string UserTypeEn { get; set; }
        public string UserTypeAr { get; set; }
        public string OUNameAr { get; set; }
        public string OUNameEn { get; set; }
        public long CountryID { get; set; }
        public Nullable<long> ActionGroupID { get; set; }
        public string AccessToken { get; set; }
        public string CreationDate { get; set; }
        public string SuspensionDate { get; set; }
        public string Password { get; set; }
        public DateTime LinkExp { get; set; }
        public int LoginAttempt { get; set; }
        public int LastLoginDayCount { get; set; }
        public bool IsClicked { get; set; }
        public string UserDispalyStatus { get; set; }
        public List<int> RoleIDs { get; set; }
        public List<long> BusinessSystemIDs { get; set; }
        public long? GovEntityID { get; set; }
    }

    public class MainUserModel
    {

        public long UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UserEmpID { get; set; }
        public string DisplayName { get; set; }
        public string UserEID { get; set; }
        public Nullable<long> UserNationalityID { get; set; }
        public string UserEmail { get; set; }
        public string UserMobile { get; set; }
        public Nullable<long> UserTypeID { get; set; }
        public Nullable<long> UserDesignationID { get; set; }
        public string UserDeviceUDID { get; set; }
        public Nullable<long> UserOUID { get; set; }
        public string UUID { get; set; }
        public Nullable<long> UserCompanyID { get; set; }
        public byte[] UserSignature { get; set; }
        public Nullable<long> BusinessSystemID { get; set; }
        public bool UserStatus { get; set; }
        public byte[] ProfileImage { get; set; }
        public Nullable<long> ActionGroupID { get; set; }
        //public List<RolesList> RolesList { get; set; }
        //public List<UserBusinessSystemList> BusinessSystemsList { get; set; }
    }
}
