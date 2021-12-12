using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOP.Shared.Models
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
