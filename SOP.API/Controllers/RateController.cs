using SOP.API.Filter;
using SOP.API.LibaryHelper;
using SOP.API.Models;
using SOP.API.Service;
using SOP.Shared.Models;
using System;
using System.Collections.Generic;
<<<<<<< HEAD
using System.Data;
=======
using System.Globalization;
>>>>>>> a85aa601b81d45303653188451021ea8ddd7613a
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
        public MessageReport Post(RatingResult model)
        {
            var result = new MessageReport(false, "Có lỗi xảy ra");

            try
            {
                model.RatingResult_CreatedOn = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                bool check = RateService.CreatObj(model);
                if (check)
                {
                    result = new MessageReport(check, "thêm thành công");
                }
            }
            catch { }

            return result;
        }
        [Route("listuser")]
        [HttpGet]
        public List<User_SOP> GetListUsers(List<int>  userids)
        {
            var lst = new List<User_SOP>();
            
            // lấy datatbale user 
            DataTable dt = UserService.GetDtUser();
            // Convert to list
            if (dt != null)
            {
                var lstObj = SqlHelper.ConvertTo<User_SOP>(dt);
                foreach (var item in lstObj)
                {
                    foreach (var item1 in userids)
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
