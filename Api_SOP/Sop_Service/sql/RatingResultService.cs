using Api_SOP.LibaryHelper;
using Api_SOP.Sop_model;
using Kztek_Data.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_SOP.Sop_Service.sql
{
    public class RatingResultService : IRatingResultService
    {
        private IRatingResultRepository _RatingResultRepository;
        public RatingResultService(IRatingResultRepository _RatingResultRepository)
        {
            this._RatingResultRepository = _RatingResultRepository;
        }
        public async Task<ActionResult<MessageReport>> Create(RatingResult value)
        {
            return await _RatingResultRepository.Add(value);
        }

        public async Task<User> GetByUsername(string username)
        {
            var connect = AppSettingHelper.GetStringFromFileJson("connectstring", "ConnectionStrings:DefaultConnection").Result;

            var sb = new StringBuilder();
            sb.AppendLine("Select * from [User] ");
            sb.AppendLine(string.Format("Where User_UserName = '{0}'", username));
            var obj = SqlHelper.ExcuteCommandToModel<User>(connect, sb.ToString());
            return await Task.FromResult(obj);
        }
    }
}
