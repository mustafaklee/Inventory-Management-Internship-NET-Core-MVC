using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoginEkrani.Models.Login
{
    public class LoginPageModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_kpsft { get; set; }

        [Required(ErrorMessage = "Kullanıcı adı zorunludur")]
        [StringLength(45, ErrorMessage = "Kullanıcı adı en fazla 45 karakter olabilir")]
        [Display(Name = "Kullanıcı Adı")]
        public string kpsft_kullaniciAdi { get; set; }

        [Required(ErrorMessage = "Ad alanı zorunludur")]
        [StringLength(45, ErrorMessage = "Ad en fazla 45 karakter olabilir")]
        [Display(Name = "Ad")]
        public string kpsft_ad { get; set; }

        [Required(ErrorMessage = "Soyad alanı zorunludur")]
        [StringLength(45, ErrorMessage = "Soyad en fazla 45 karakter olabilir")]
        [Display(Name = "Soyad")]
        public string kpsft_soyad { get; set; }

        [Required(ErrorMessage = "Şifre zorunludur")]
        [StringLength(150, ErrorMessage = "Şifre en fazla 150 karakter olabilir")]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        public string kpsft_sifre { get; set; }

        [Required(ErrorMessage = "TC Kimlik numarası zorunludur")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "TC Kimlik numarası 11 haneli olmalıdır")]
        [Display(Name = "TC Kimlik No")]
        public int kpsft_tcKimlik { get; set; }

        [Required(ErrorMessage = "E-posta adresi zorunludur")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz")]
        [StringLength(100, ErrorMessage = "E-posta adresi en fazla 100 karakter olabilir")]
        [Display(Name = "E-posta")]
        public string kpsft_mailAdrress { get; set; }

        public ICollection<UserRole>? UserRoles { get; set; }
    }
}
