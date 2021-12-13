using Api_SOP.BasicAuth.AuthAttribute;
using Api_SOP.LibaryHelper;
using Api_SOP.Sop_model;
using Api_SOP.Sop_Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_SOP.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class API_LoginController : ControllerBase
    {
        //private IUserService _UserService;

        //public API_LoginController(IUserService _UserService)
        //{
        //    this._UserService = _UserService;
        //}

        /// <summary>
        /// Api đăng nhập
        /// </summary>
        /// Author          Date            Summary
        /// TrungNQ         12/12/2019      Thêm mới tính năng
        /// <param name="value">{ Username, Password }</param>
        /// <returns>{ isSuccess, Message }</returns>
        [HttpPost]
       
        public async Task<MessageReport> Post([FromBody] AuthModel_LowSecurity model)
        {
            


         
            var result = new MessageReport(false, "Có lỗi xảy ra");

            try
            {
                //Kiểm tra tks
                var objUser = await GetByUsername(model.Username);
                var pass = FunctionHelper.Encrypt(model.Password,true);
                if (objUser == null)
                {
                    result = new MessageReport(false, "Tài khoản không tồn tại");
                    return await Task.FromResult(result);
                }





                if (objUser.User_PassWord != pass)
                {
                    result = new MessageReport(false, "Mật khẩu không khớp");
                    return await Task.FromResult(result);
                }


                result = new MessageReport(true, "Thành công");
            }
            catch (System.Exception ex)
            {
                result = new MessageReport(false, string.Format("Message: {0} - Details: {1}", ex.Message, ex.InnerException != null ? ex.InnerException.Message : ""));
            }

            return await Task.FromResult(result);



        }
       
    }
}
