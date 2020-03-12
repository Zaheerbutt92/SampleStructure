using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using static Constant.ExternalService;
using static Utilities.Utility;

namespace Utilities
{
    public static class Utility
    {
        [Serializable]
        public class SessionWrapper
        {
            private SessionWrapper()
            {
            }
            private static SessionWrapper sessionWrapper = new SessionWrapper();

            public static SessionWrapper CurrentSession
            {
                get
                {
                    if (HttpContext.Current == null)
                    {
                        return sessionWrapper;
                    }
                    if (HttpContext.Current.Session == null)
                    {
                        return null;
                    }


                    if (HttpContext.Current.Session["SessionInfo"] == null)
                    {
                        HttpContext.Current.Session["SessionInfo"] = new SessionWrapper();
                    }
                    return HttpContext.Current.Session["SessionInfo"] as SessionWrapper;
                }
            }

            public string UserName { get; set; }
            public string Email { get; set; }
            public long User_Id { get; set; }
            public Nullable<long> User_Type_Id { get; set; }
            public string FullName { get; set; }
            public string UserMobile { get; set; }
            public string UserEID { get; set; }
            public long UserNationalityID { get; set; }
            public long ActionGroupID { get; set; }
            public string Language { get; set; }
            public long OTPAttempCount { get; set; }
            public long BusinessSystemID { get; set; }
            public long OrganizationUnitID { get; set; }
            public Bitmap ProfileImageUrl { get; set; }
            public string OrganizationUnitNameEn { get; set; }
            public string Mobile { get; set; }
            public string OrganizationUnitNameAr { get; set; }
            public long UserRoleID { get; set; }
            public List<int> RolesID { get; set; }
            public long UserTypeID { get; set; }
            public long UserCompanyID { get; set; }
            public long EntityID { get; set; }
            public string DesignationEn { get; set; }
            public string DesignationAr { get; set; }
            //public EntityModificationVM ModificationRequestEntity { get; set; }
            public Boolean ModificationRequest { get; set; }
            //public List<RolePermissionsModel> Permissions { get; set; }
            public List<long> BusinessSystemsList { get; set; }
            //public UserMenuPermissionsVM UserMenuPermissions { get; set; }
            public byte[] UserSignature { get; set; }
            public int LastLoginDayCount { get; set; }
            public long? GovtEntityID { get; set; }
        }
    }
}


namespace Utility
{
    public static class Utility
    {
        public static string GetApibaseUrl()
        {
            string ApibasePath = ConfigurationManager.AppSettings["ApiPath"].ToString();
            return ApibasePath;
        }
        public static string GetEcomplaintApibaseUrl()
        {
            string ApibasePath = ConfigurationManager.AppSettings["EcomplaintApiPath"].ToString();
            return ApibasePath;
        }
        public static string GetSharedApibaseUrl()
        {
            string ApibasePath = ConfigurationManager.AppSettings["SharedApiPath"].ToString();
            return ApibasePath;
        }

        public static string GetLicensingAndRegisterationApibaseUrl()
        {
            string ApibasePath = ConfigurationManager.AppSettings["LicensingAndRegisterationApiPath"].ToString();
            return ApibasePath;

        }
        public static string Getlongdatestring(DateTime date)
        {
            string strdate = string.Empty;
            try
            {
                if (date != null)
                {
                    strdate = date.ToString("DD/mmm/yyyy");
                }
            }
            catch (Exception ex)
            {

            }
            return strdate;
        }

        public static string Encrypt(string plainSourceStringToEncrypt)
        {
            //Set up the encryption objects
            string strMessage = string.Empty;
            try
            {
                string passPhrase = ConfigurationManager.AppSettings["EncryptionKey"].ToString();
                using (AesCryptoServiceProvider acsp = GetProvider(Encoding.UTF8.GetBytes(passPhrase)))
                {
                    byte[] sourceBytes = Encoding.ASCII.GetBytes(plainSourceStringToEncrypt);
                    ICryptoTransform ictE = acsp.CreateEncryptor();

                    //Set up stream to contain the encryption
                    MemoryStream msS = new MemoryStream();

                    //Perform the encrpytion, storing output into the stream
                    CryptoStream csS = new CryptoStream(msS, ictE, CryptoStreamMode.Write);
                    csS.Write(sourceBytes, 0, sourceBytes.Length);
                    csS.FlushFinalBlock();

                    //sourceBytes are now encrypted as an array of secure bytes
                    byte[] encryptedBytes = msS.ToArray(); //.ToArray() is important, don't mess with the buffer

                    //return the encrypted bytes as a BASE64 encoded string
                    strMessage = Convert.ToBase64String(encryptedBytes);
                    strMessage = strMessage.Replace("+", "secret");
                }
            }
            catch (Exception ex)
            {
                strMessage = Convert.ToString(StatusType.Error);
            }
            return strMessage;
        }
        public static DateTime GetStandardDateTime()
        {

            DateTime epochStart = new DateTime(1970, 01, 01, 0, 0, 0, 0, DateTimeKind.Utc);
            return epochStart;
        }
        public static string Decrypt(string base64StringToDecrypt)
        {
            base64StringToDecrypt = base64StringToDecrypt.Replace("secret", "+");
            //Set up the encryption objects
            string strMessage = string.Empty;
            try
            {
                string passphrase = ConfigurationManager.AppSettings["EncryptionKey"].ToString();
                using (AesCryptoServiceProvider acsp = GetProvider(Encoding.UTF8.GetBytes(passphrase)))
                {
                    byte[] RawBytes = Convert.FromBase64String(base64StringToDecrypt);
                    //byte[] RawBytes = System.Text.UTF8Encoding.ASCII.GetBytes(base64StringToDecrypt);

                    ICryptoTransform ictD = acsp.CreateDecryptor();

                    //RawBytes now contains original byte array, still in Encrypted state

                    //Decrypt into stream
                    MemoryStream msD = new MemoryStream(RawBytes, 0, RawBytes.Length);
                    CryptoStream csD = new CryptoStream(msD, ictD, CryptoStreamMode.Read);
                    //csD now contains original byte array, fully decrypted

                    //return the content of msD as a regular string
                    strMessage = (new StreamReader(csD)).ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                strMessage = Convert.ToString(StatusType.Error);
            }
            return strMessage;
        }
        private static AesCryptoServiceProvider GetProvider(byte[] key)
        {
            AesCryptoServiceProvider result = new AesCryptoServiceProvider();
            result.BlockSize = 128;
            result.KeySize = 128;
            result.Mode = CipherMode.CBC;
            result.Padding = PaddingMode.PKCS7;

            result.GenerateIV();
            result.IV = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            byte[] RealKey = GetKey(key, result);
            result.Key = RealKey;
            // result.IV = RealKey;
            return result;
        }
        private static byte[] GetKey(byte[] suggestedKey, SymmetricAlgorithm p)
        {
            byte[] kRaw = suggestedKey;
            List<byte> kList = new List<byte>();

            for (int i = 0; i < p.LegalKeySizes[0].MinSize; i += 8)
            {
                kList.Add(kRaw[(i / 8) % kRaw.Length]);
            }
            byte[] k = kList.ToArray();
            return k;
        }
        public static bool ErrorLogging(Exception ex)
        {
            string ErrorDetail = ("\n Exception Title: " + ex.Message + "\r\n Exception Detail:" + ex.StackTrace);
            string path = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["ErrorLoggingPath"].ToString());
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            path = path + DateTime.Now.ToString("MM-dd-yyyy") + ".txt";
            if (File.Exists(path))
            {
                var fileInfo = new FileInfo(path);
                if (fileInfo.Length > 200000)
                {
                    File.Delete(path);

                }
            }
            if (!File.Exists(path))
            {
                File.Create(path).Dispose();
                using (TextWriter tw = new StreamWriter(path))
                {

                    tw.WriteLine("...................................." + DateTime.Now + "............................................");
                    tw.WriteLine("");
                    tw.WriteLine(ErrorDetail);
                    tw.WriteLine("................................................................................");
                    tw.WriteLine("");
                    tw.Close();
                }
            }
            else
            {

                using (StreamWriter outputFile = new StreamWriter(path, true))
                {
                    outputFile.WriteLine("...................................." + DateTime.Now + "............................................");
                    outputFile.WriteLine("");
                    outputFile.WriteLine(ErrorDetail);
                    outputFile.WriteLine("................................................................................");
                    outputFile.WriteLine("");
                    outputFile.Close();
                }
            }

            return true;
        }

        //public static bool CheckUserPermission(int actionID)
        //{
        //    return (SessionWrapper.CurrentSession.UserTypeID == (int)RegisterUserTypes.SuperAdmin ||
        //            SessionWrapper.CurrentSession.Permissions.Where(p => p.Actions.Where(a => a.ActionID == actionID).Count() > 0).Count() > 0);
        //}

        //public static bool IsUserInternalRole()
        //{
        //    var permissions = SessionWrapper.CurrentSession.Permissions;
        //    return (permissions.Where(x => x.RoleID == (int)LCRoles.Requester).ToList().Count > 0);
        //}
    }
}