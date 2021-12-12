using SOP.API.LibaryHelper;
using SOP.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SOP.API.Service
{
    public class RateService
    {

        public static bool CreatObj(RatingResult value)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Insert into RatingResult([RatingResult_ UserId] ,RatingResult_CreatedOn,RatingResult_RatingId) Values ");
            sb.AppendLine(string.Format(" ('{0}' ,'{1}','{2}')", value.RatingResult_UserId, value.RatingResult_CreatedOn, value.RatingResult_RatingId));
            var result = SqlHelper.Execute(sb.ToString());
            return (result);
        }
    }
}