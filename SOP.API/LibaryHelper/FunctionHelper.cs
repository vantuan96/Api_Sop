using SOP.API.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace SOP.API.LibaryHelper
{
    public class FunctionHelper
    {
        public static string Encrypt(string pass)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(pass.Trim(), "MD5");
        }
        public static bool LoginExist(string UserName, string PassWord)
        {

            bool check = false;
            ////Kiểm tra tks
            var dt = UserService.GetByUsername(UserName);
            if (dt != null)
            {
                var pass = FunctionHelper.Encrypt(PassWord);
                var usre_pass = dt.Rows[0]["User_PassWord"].ToString();
                if (usre_pass == pass)
                    return check = true;
                else

                    return check = false;

            }
            return check;



        }
    }
}