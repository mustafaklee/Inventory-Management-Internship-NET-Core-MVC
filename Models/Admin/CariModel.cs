using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoginEkrani.Models.Admin
{
    public class CariModel
    {
        [Key]
        [Required(ErrorMessage = "Cari kodu zorunludur")]
        [StringLength(45, ErrorMessage = "Cari kodu en fazla 45 karakter olabilir")]
        [Display(Name = "Cari Kodu")]
        public string kpsft_cariKod { get; set; }

        [Required(ErrorMessage = "Cari adı zorunludur")]
        [StringLength(100, ErrorMessage = "Cari adı en fazla 100 karakter olabilir")]
        [Display(Name = "Cari Adı")]
        public string kpsft_cariAd { get; set; }

        [Required(ErrorMessage = "Cari tipi zorunludur")]
        [Display(Name = "Cari Tipi")]
        public int kpsft_cariTipi { get; set; }

        [Display(Name = "Vergi No")]
        [StringLength(20, ErrorMessage = "Vergi numarası en fazla 20 karakter olabilir")]
        public string? kpsft_vergiNo { get; set; }

        [Display(Name = "Vergi Dairesi")]
        [StringLength(100, ErrorMessage = "Vergi dairesi en fazla 100 karakter olabilir")]
        public string? kpsft_vergiDairesi { get; set; }

        [Display(Name = "Adres 1")]
        [StringLength(200, ErrorMessage = "Adres en fazla 200 karakter olabilir")]
        public string? kpsft_adres1 { get; set; }

        [Display(Name = "Adres 2")]
        [StringLength(200, ErrorMessage = "Adres en fazla 200 karakter olabilir")]
        public string? kpsft_adres2 { get; set; }

        [Display(Name = "Cari Adı 2")]
        [StringLength(100, ErrorMessage = "Cari adı en fazla 100 karakter olabilir")]
        public string? kpsft_cariAd2 { get; set; }

        [Display(Name = "Adres 3")]
        [StringLength(200, ErrorMessage = "Adres en fazla 200 karakter olabilir")]
        public string? kpsft_adres3 { get; set; }

        [Display(Name = "Telefon 1")]
        [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz")]
        [StringLength(20, ErrorMessage = "Telefon numarası en fazla 20 karakter olabilir")]
        public string? kpsft_telefon1 { get; set; }

        [Display(Name = "Telefon 2")]
        [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz")]
        [StringLength(20, ErrorMessage = "Telefon numarası en fazla 20 karakter olabilir")]
        public string? kpsft_telefon2 { get; set; }

        [Display(Name = "E-posta")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz")]
        [StringLength(100, ErrorMessage = "E-posta adresi en fazla 100 karakter olabilir")]
        public string? kpsft_email { get; set; }

        [Display(Name = "IBAN")]
        [StringLength(50, ErrorMessage = "IBAN en fazla 50 karakter olabilir")]
        public string? kpsft_ıban { get; set; }

        [Display(Name = "Açıklama")]
        [StringLength(500, ErrorMessage = "Açıklama en fazla 500 karakter olabilir")]
        public string? kpsft_aciklama { get; set; }

        [Display(Name = "Yurtiçi/Dışı")]
        public int? kpsft_yurticiDisi { get; set; }

        [Display(Name = "Web Sitesi")]
        [Url(ErrorMessage = "Geçerli bir web sitesi adresi giriniz")]
        [StringLength(200, ErrorMessage = "Web sitesi adresi en fazla 200 karakter olabilir")]
        public string? kpsft_web_sitesi { get; set; }

        [Display(Name = "Durum")]
        public int? kpsft_durum { get; set; }

        [Display(Name = "Fason Üretim")]
        public int? kpsft_fason_uretim { get; set; }
    }
}
