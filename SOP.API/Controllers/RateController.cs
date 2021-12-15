using SOP.API.Filter;
using SOP.API.LibaryHelper;
using SOP.API.Service;
using SOP.Shared.Models;
using System;
using System.Collections.Generic;
using System.Data;

using System.Globalization;

using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SOP.API.Controllers
{

    [BasicAuthentication]
    public class RateController : ApiController
    {
        [Route("rating")]
        [HttpPost]
        public MessageReport Rating(RatingResult model)
        {
            var result = new MessageReport(false, "Có lỗi xảy ra");
            model.RatingResult_CreatedOn = DateTime.Now.ToString("yyy-MM-dd HH:mm:ss");
            bool check = RateService.CreatObj(model);
            if (check)
            {
                return result = new MessageReport(check, "thêm thành công");
            }

            else
            {
                return result = new MessageReport(false, "Lỗi");
            }
        }

        [Route("getuserbyname")]
        [HttpPost]
        public User_SOP GetUserByName(User_SOP_RequestByUserNameModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Username)) return new User_SOP();

            var dt = UserService.GetByUsername(model.Username);

            if (dt != null && dt.Rows.Count > 0)
            {
                var result = dt.AsEnumerable()
                    .Select(row => new User_SOP()
                    {
                        User_Id = row.Field<int>("User_Id"),
                        User_UserName = row.Field<string>("User_UserName"),
                        User_Email = row.Field<string>("User_Email"),
                        User_FullName = row.Field<string>("User_FullName")
                    })
                    .FirstOrDefault();

                return result ?? new User_SOP() { User_Id = 0 };
            }
            else
            {
                return new User_SOP() { User_Id = 0 };
            }
        }

        [Route("listuser")]
        [HttpGet]
        public List<User_SOP> GetListUsers(User_SOP_RequestByListIdModel model)
        {
            var lst = new List<User_SOP>();

            DataTable dt = UserService.GetDtUser();

            if (dt != null)
            {
                var lstObj = SqlHelper.ConvertTo<User_SOP>(dt);

                foreach (var item in lstObj)
                {
                    foreach (var item1 in model.ListIds)
                    {
                        if (item.User_Id == item1)
                        {
                            lst.Add(item);
                        }
                    }
                }
            }

            return lst;
        }
    }
}
