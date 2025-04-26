using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoginEkrani.Models.Login
{
    public class UserRole
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        public int id_kpsft { get; set; }

        [Required]
        public int kpsft_rol_id { get; set; }

        [ForeignKey("id_kpsft")]
        public LoginPageModel User { get; set; }

        [ForeignKey("kpsft_rol_id")]
        public Role Role { get; set; }
    }
}
