using SOP.API.Filter;
using SOP.API.LibaryHelper;
using SOP.API.Models;
using SOP.API.Service;
using SOP.Shared.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SOP.API.Controllers
{

    [BasicAuthentication]
    public class RateController : ApiController
    {
        
       
        [Route("Create")]
        [HttpPost]
        public MessageReport Post( RatingResult model)
        {
            var result = new MessageReport(false, "Có lỗi xảy ra");
                    bool check = RateService.CreatObj(model);
            if (check)
            {
                return result = new MessageReport(check, "thêm thành công");
            }
            //syyyy
            else
            {
                return result = new MessageReport(false, "Lỗi");
            }
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
