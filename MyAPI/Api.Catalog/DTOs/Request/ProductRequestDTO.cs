using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCatalog.Core.DTOs.Request
{
    public class ProductRequestDTO : IValidatableObject
    {

        [Required(ErrorMessage = "O nome é obrigatório")]
        public string Name { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória")]
        public string Description { get; set; }

        [Required(ErrorMessage = "O preço é obrigatório")]
        [Range(0, double.MaxValue, ErrorMessage = "O preço deve ser maior ou igual a 0")]
        public float Price { get; set; }

        public string? ImageUrl { get; set; }

        [Required(ErrorMessage = "O estoque é obrigatório")]
        [Range(0, 1000000, ErrorMessage = "O estoque deve estar entre 0 e 1.000.000")]
        public float Stock { get; set; }

        [Required(ErrorMessage = "A data de registro é obrigatória")]
        [DataType(DataType.Date, ErrorMessage = "A data de registro deve ser válida")]
        public DateTime DateRegister { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DateRegister > DateTime.Now)
            {
                yield return new ValidationResult("A data de registro não pode estar no futuro", new[] { nameof(DateRegister) });
            }
        }
    }
}
