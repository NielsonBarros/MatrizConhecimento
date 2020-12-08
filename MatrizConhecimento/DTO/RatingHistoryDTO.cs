using System;
using System.ComponentModel.DataAnnotations;

namespace MatrizConhecimento.DTO
{
    public class RatingHistoryDTO
    {
        public int Id { get; set; }

        [Required]
        public int TopicId { get; set; }

        [Required]
        public int MatterId { get; set; }

        [Required]
        public DateTime? RatingDate { get; set; }

        [Required]
        public Int16? Score { get; set; }
    }
}
