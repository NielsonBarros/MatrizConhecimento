using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MatrizConhecimento.DTO
{
    public class RatingDTO
    {
        [Required]
        public int TopicId { get; set; }

        [Required]
        public int MatterId { get; set; }

        public int RatingHistoryId { get; set; }

        public DateTime RatingDate { get; set; }

        public Int16 Score { get; set; }
    }
}
