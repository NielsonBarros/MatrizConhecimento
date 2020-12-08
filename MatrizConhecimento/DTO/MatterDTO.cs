using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MatrizConhecimento.DTO
{
    public class MatterDTO
    {
        public int Id { get; set; }

        [Required]
        public int TopicId { get; set; }

        [DisplayName("Item de Conhecimento")]
        [Required]
        public string Name { get; set; }

        [Required]
        public bool Active { get; set; }
    }
}
