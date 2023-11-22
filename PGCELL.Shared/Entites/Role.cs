using System.ComponentModel.DataAnnotations;

namespace PGCELL.Shared.Entites
{
    public class Role
    {
        public int Id { get; set; }

        [Display(Name = "Rol")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; } = null!;

        //public ICollection<UserRol> UserRoles { get; set; } // Relación muchos a muchos con usuarios

    }
}
