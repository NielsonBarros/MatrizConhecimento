using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MatrizConhecimento.Models
{
    public class Matter
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int TopicId { get; set; }

        [DisplayName("Item de Conhecimento")]
        [Required]
        public string Name { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public bool Active { get; set; }

        #region references
        public Topic Topic { get; set; }
        public User User { get; set; }
        public ICollection<Rating> Ratings { get; set; }
        public ICollection<RatingHistory> RatingHistories { get; set; }
        #endregion
    }
}
