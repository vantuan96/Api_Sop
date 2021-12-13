using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Api_SOP.Sop_model
{
    
    public class RatingResult
    {
       
        public int RatingResult_Id { get; set; }

        public string RatingResult_UserId { get; set; }

        public string RatingResult_CreatedOn { get; set; }

        public string RatingResult_RatingId { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}
