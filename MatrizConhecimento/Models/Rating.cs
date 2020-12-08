using System;
using System.ComponentModel.DataAnnotations;

namespace MatrizConhecimento.Models
{
    public class Rating
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int TopicId { get; set; }

        [Required]
        public int MatterId { get; set; }

        public int? RatingHistoryId { get; set; }

        public DateTime? RatingDate { get; set; }

        public Int16? Score { get; set; }

        #region Reference
        public User User { get; set; }
        public Topic Topic { get; set; }
        public Matter Matter { get; set; }
        public RatingHistory RatingHistory { get; set; }
        #endregion
    }
}
