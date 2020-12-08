using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MatrizConhecimento.Models
{
    public class Topic
    {
        [Key]
        public int Id{ get; set; }

        [DisplayName("Área de Conhecimento")]
        [Required]
        public string Name { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public bool Active { get; set; }

        #region References
        public ICollection<Matter> Matters { get; set; }
        public User User { get; set; }
        public ICollection<Rating> Ratings { get; set; }
        public ICollection<RatingHistory> RatingHistories { get; set; }
        #endregion
    }
}
