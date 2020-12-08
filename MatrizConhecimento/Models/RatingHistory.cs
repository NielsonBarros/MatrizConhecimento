using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MatrizConhecimento.Models
{
    public class RatingHistory
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public int TopicId { get; set; }

        public int MatterId { get; set; }

        public DateTime RatingDate { get; set; }

        public Int16 Score { get; set; }

        #region Reference
        public User User { get; set; }
        public Topic Topic { get; set; }
        public Matter Matter { get; set; }
        public ICollection<Rating> Ratings { get; set; }
        #endregion
    }
}
