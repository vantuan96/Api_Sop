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
  
    public class Api_RatingResultController : ControllerBase
    { 
      
       
        /// <summary>
        /// Api thêm mới bản ghi
        /// </summary>
        /// Author          Date            Summary
        /// TrungNQ         14/12/2019      Thêm mới
        /// <param name="value"> Model camera thêm mới </param>
        /// <returns></returns>
        [HttpPost]
      
        public async Task<ActionResult<MessageReport>> Post([FromBody] RatingResult model)
        {
            var result = new MessageReport(false, "Có lỗi xảy ra");


            try
            {
                //Kiểm tra tks
                var objUser = await GetByUsername(model.Username);
                var pass = FunctionHelper.Encrypt(model.Password, true);
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

                if (objUser.User_PassWord == pass)
                {
                    result = await CreatObj(model);
                }
               
            }
            catch (System.Exception ex)
            {
                result = new MessageReport(false, string.Format("Message: {0} - Details: {1}", ex.Message, ex.InnerException != null ? ex.InnerException.Message : ""));
            }

          
          
            return result;
        }

        private async Task<User> GetByUsername(string username)
        {
            var connect = AppSettingHelper.GetStringFromFileJson("connectstring", "ConnectionStrings:DefaultConnection").Result;

            var sb = new StringBuilder();
            sb.AppendLine("Select * from [User] ");
            sb.AppendLine(string.Format("Where User_UserName = '{0}'", username));
            var obj = SqlHelper.ExcuteCommandToModel<User>(connect, sb.ToString());
            return await Task.FromResult(obj);
        }

        private async Task<MessageReport> CreatObj(RatingResult value)
        {
            var connect = AppSettingHelper.GetStringFromFileJson("connectstring", "ConnectionStrings:DefaultConnection").Result;

            var sb = new StringBuilder();
            sb.AppendLine("Insert into RatingResult([RatingResult_ UserId] ,RatingResult_CreatedOn,RatingResult_RatingId) Values ");
            sb.AppendLine(string.Format(" ('{0}' ,'{1}','{2}')", value.RatingResult_UserId,value.RatingResult_CreatedOn,value.RatingResult_RatingId));
          
            var result = SqlHelper.ExcuteCommandToMesageReport(connect, sb.ToString());
            return await Task.FromResult(result);
        }
    }
}
