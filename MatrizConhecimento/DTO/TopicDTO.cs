using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MatrizConhecimento.DTO
{
    public class TopicDTO
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Área de Conhecimento")]
        [Required]
        public string Name { get; set; }

        [Required]
        public bool Active { get; set; }
    }
}
