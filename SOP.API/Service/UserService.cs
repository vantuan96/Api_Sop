using SOP.API.LibaryHelper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace SOP.API.Service
{
    public class UserService
    {
        private static string connStr = ConfigurationManager.ConnectionStrings["SOPEntities"].ConnectionString;
        public static DataTable GetByUsername(string username)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Select * from [User] ");
            sb.AppendLine(string.Format("Where User_UserName = '{0}'", username));
            var obj = SqlHelper.GetTable( sb.ToString(), connStr ,false);
            return (obj);
        }

        //public static object GetLstUser(int item)
        //{
        //    var sb = new StringBuilder();
        //    sb.AppendLine("Select * from [User] ");
        //    sb.AppendLine(string.Format("Where User_Id = '{0}'", item));
        //}

        public static DataTable GetDtUser()
        {

            var sb = new StringBuilder();
            sb.AppendLine("Select * from [User] ");
            DataTable dt = SqlHelper.GetTable(sb.ToString(), connStr, false);
            return dt;
        }
    }
}