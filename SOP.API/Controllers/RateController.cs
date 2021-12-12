using SOP.API.Filter;
using SOP.API.Service;
using SOP.Shared.Models;
using System;
using System.Collections.Generic;
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

    }
}
