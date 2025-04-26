using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoginEkrani.Models.Admin
{
    public class StokKartıModel
    {
        [Key]
        [Required(ErrorMessage = "Stok kodu zorunludur")]
        [StringLength(45, ErrorMessage = "Stok kodu en fazla 45 karakter olabilir")]
        [Display(Name = "Stok Kodu")]
        public string kpsft_stokKodu { get; set; }

        [Required(ErrorMessage = "Stok adı zorunludur")]
        [StringLength(100, ErrorMessage = "Stok adı en fazla 100 karakter olabilir")]
        [Display(Name = "Stok Adı")]
        public string kpsft_stokAdi { get; set; }

        [Required(ErrorMessage = "Birim seçimi zorunludur")]
        [Display(Name = "Birim")]
        public string kpsft_birim { get; set; }

        [Display(Name = "Grup Kodu")]
        public string kpsft_grupKodu { get; set; }

        [Display(Name = "Alış KDV Oranı")]
        public string kpsft_alisKdvOrani { get; set; }

        [Display(Name = "Satış KDV Oranı")]
        public string kpsft_satisKdvOrani { get; set; }

        [Display(Name = "Kalite")]
        public string kpsft_kalite { get; set; }

        [Display(Name = "Stok Kart Tipi")]
        public string kpsft_stokKartTipi { get; set; }

        [Display(Name = "Gümrük Kodu")]
        public string kpsft_gumrukKodu { get; set; }

        [Display(Name = "Kritik Miktar")]
        public int? kpsft_kritikMiktar { get; set; }

        [Display(Name = "Lot Büyüklüğü")]
        public int? kpsft_lotBuyuklugu { get; set; }

        [Display(Name = "Durum")]
        public string kpsft_durum { get; set; }

        [Display(Name = "Açıklama")]
        [StringLength(500, ErrorMessage = "Açıklama en fazla 500 karakter olabilir")]
        public string kpsft_aciklama { get; set; }

        // Navigation Properties
        [ForeignKey("kpsft_birim")]
        public BirimModel birimModel { get; set; }

        [ForeignKey("kpsft_grupKodu")]
        public GrupModel grupModel { get; set; }

        [ForeignKey("kpsft_alisKdvOrani")]
        public AlısKdvOraniModel alisKdvOraniModel { get; set; }

        [ForeignKey("kpsft_satisKdvOrani")]
        public SatisKdvOrani satisKdvOrani { get; set; }

        [ForeignKey("kpsft_kalite")]
        public KaliteModel kaliteModel { get; set; }

        [ForeignKey("kpsft_stokKartTipi")]
        public StokTipi stokTipi { get; set; }

        [ForeignKey("kpsft_gumrukKodu")]
        public GumrukKoduModel gumrukKoduModel { get; set; }

        [ForeignKey("kpsft_durum")]
        public DurumModel durum { get; set; }
    }
}
