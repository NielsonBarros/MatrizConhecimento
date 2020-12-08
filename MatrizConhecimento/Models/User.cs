using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MatrizConhecimento.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o usuário")]
        [Display(Name = "Usuário")]
        public string UserName { get; set; }

        [Required(ErrorMessage ="O nome de usuário é requerido.")]
        [DisplayName("Nome")]
        public string Name { get; set; }

        [Required(ErrorMessage = "A data de nascimento é requerida.")]
        [DisplayName("Nascimento")]
        public DateTime BirthDay { get; set; }

        [Required(ErrorMessage = "O CPF é requerido.")]
        [DisplayName("CPF")]
        public string CPF { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("Senha")]
        [Required(ErrorMessage = "A senha é necessário.")]
        [MinLength(6, ErrorMessage = "A senha precisar ter 6 caracteres")]
        public string Password { get; set; }

        #region references
        public ICollection<Topic> Topics { get; set; }
        public ICollection<Matter> Matters { get; set; }
        public ICollection<Rating> Ratings { get; set; }
        public ICollection<RatingHistory> RatingHistories { get; set; }
        #endregion
    }
}
