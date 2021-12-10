using Api_SOP.LibaryHelper;
using Api_SOP.Sop_model;
using Kztek_Data.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
