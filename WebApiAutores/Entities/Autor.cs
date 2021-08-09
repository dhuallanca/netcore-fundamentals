using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiAutores.Entities
{
    public class Autor: IValidatableObject
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Campo requerido")]
        [StringLength(60, ErrorMessage ="Maximo 60 caracteres")]
        [ValidatorsModel.CapitalizarNombre]
        public string Nombre { get; set; }
        //property of navigation to libro
        public List<Libro> Libros { get; set; }
        [NotMapped]
        [Range(18,60, ErrorMessage = "Solo para adultos mayores de 18 años")]
        public int Edad { get; set; }
        [Url]
        [NotMapped]
        public string Url { get; set; }
        public int MayorQue { get; set; }
        public int MenorQue { get; set; }

        // implementación del IValidatableObject para validar el modelo
        // estas validaciones se ejecutan después de las validaciones por atributo
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(!string.IsNullOrEmpty(Nombre))
            {
                var firstLetter = Nombre[0].ToString();
                if (firstLetter != firstLetter.ToUpper())
                {
                    // You use a yield return statement to return each element one at a time.
                    // Using yield to define an iterator removes the need for an explicit extra class IEnumerator/Ienumerable
                    // yield inserta un elemento en el IEnumeable validationResult
                    yield return new ValidationResult("Debe Iniciar con mayúscula", new string[] { nameof(Nombre) });
                }
            }
            
            if(MenorQue > MayorQue)
            {
                yield return new ValidationResult("No puede ser mayor a MayorQue", new string[] { nameof(MenorQue) });
            }
        }
    }
}
