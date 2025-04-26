using System.ComponentModel.DataAnnotations;

namespace LoginEkrani.Models.Admin
{
    public class MultipleModel
    {
        [Display(Name = "Stok Kartı")]
        public StokKartıModel StokKartıModel { get; set; }

        [Display(Name = "Alış KDV Oranları")]
        public List<AlısKdvOraniModel> AlisKdvOraniModel { get; set; }

        [Display(Name = "Gruplar")]
        public List<GrupModel> GrupModel { get; set; }

        [Display(Name = "Gümrük Kodları")]
        public List<GumrukKoduModel> GumrukKoduModel { get; set; }

        [Display(Name = "Kalite Bilgileri")]
        public List<KaliteModel> KaliteModel { get; set; }

        [Display(Name = "Satış KDV Oranları")]
        public List<SatisKdvOrani> SatisKdvOraniModel { get; set; }

        [Display(Name = "Stok Tipleri")]
        public List<StokTipi> StokTipiModel { get; set; }

        [Display(Name = "Birimler")]
        public List<BirimModel> BirimModel { get; set; }

        [Display(Name = "Fiş Bilgileri")]
        public FisTabloModel FisTabloModel { get; set; }

        [Display(Name = "Cari Bilgileri")]
        public List<CariModel> CariModel { get; set; }

        [Display(Name = "Depo Bilgileri")]
        public List<DepoModel> DepoModel { get; set; }
    }
}
