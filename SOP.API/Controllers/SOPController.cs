using SOP.API.Filter;
using SOP.API.LibaryHelper;
using SOP.API.Service;
using SOP.Shared.Models;
using SOP.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;


namespace SOP.API.Controllers
{
    [AllowAnonymous]
 
    public class SOPController : ApiController
    {
        [Route("test")]
        [HttpGet]
        public IHttpActionResult Test()
        {
            return Ok();
        }

        [BasicAuthentication]
        [Route("testauth")]
        [HttpGet]
        public IHttpActionResult TestAuth()
        {
            return Ok();
        }
        
        [Route("login")]
        [HttpPost]
        public IHttpActionResult Login(LoginModel loginModel)
        {

            var result = new MessageReport(false, "Có lỗi xảy ra");

            ////Kiểm tra tks
            var dt = UserService.GetByUsername(loginModel.Username);

            var pass = FunctionHelper.Encrypt(loginModel.Password, true);
           

            var usre_pass = dt.Rows[0]["User_PassWord"].ToString();

            if (usre_pass == pass)
                return Ok();

            return Unauthorized();
        }






    }
}
