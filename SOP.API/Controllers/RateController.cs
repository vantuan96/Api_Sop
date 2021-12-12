using SOP.API.Filter;
using SOP.API.Service;
using SOP.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SOP.API.Controllers
{
    [AllowAnonymous]
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
            else
            {
                return result = new MessageReport(false, "Lỗi");
            }
        }

    }
}
