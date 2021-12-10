using Api_SOP.LibaryHelper;
using Api_SOP.Sop_model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_SOP.Sop_Service
{
    public interface IRatingResultService
    {
        Task<ActionResult<MessageReport>> Create(RatingResult value);
    }
}
