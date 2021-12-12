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

        public static string Encrypt(string toEncrypt, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            string key = "17032008";
            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                hashmd5.Clear();
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            tdes.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        public static bool LoginExist(string UserName, string PassWord)
        {

            bool check = false;
            ////Kiểm tra tks
            var dt = UserService.GetByUsername(UserName);
            if (dt != null)
            {
                var pass = FunctionHelper.Encrypt(PassWord, true);
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