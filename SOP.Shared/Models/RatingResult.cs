using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOP.Shared.Models
{
   public class RatingResult
    {     
        public int RatingResult_UserId { get; set; } // id của User

        public string RatingResult_CreatedOn { get; set; } // Ngày tạo đánh giá

        public int RatingResult_RatingId { get; set; } // id của ratingresult      
    }
}
