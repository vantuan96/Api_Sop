using SOP.API.Filter;
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
            if (loginModel.Username == "admin")
                return Ok();

            else
                return Unauthorized();
        }
    }
}
