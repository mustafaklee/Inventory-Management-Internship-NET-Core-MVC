using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoginEkrani.Models.Login
{ 
    public class Role
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_kpsft { get; set; }

        [Required(ErrorMessage = "Rol adı zorunludur")]
        [StringLength(50, ErrorMessage = "Rol adı en fazla 50 karakter olabilir")]
        [Display(Name = "Rol Adı")]
        public string kpsft_rol { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
    }
}
