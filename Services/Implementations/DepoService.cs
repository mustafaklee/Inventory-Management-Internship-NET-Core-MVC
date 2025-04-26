using LoginEkrani.Controllers;
using LoginEkrani.Models.Admin;
using LoginEkrani.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LoginEkrani.Services.Implementations
{
    public class DepoService : IDepoService
    {
        private readonly ILogger<AdminController> _logger;
        private readonly Database _db;
        public DepoService(ILogger<AdminController> logger, Database db)
        {
            _logger = logger;
            _db = db;
        }

        public async Task<IActionResult> CreateDepoAsync(DepoModel model)
        {
            model.kpsft_depoTuru = kpsft_depoTuru;

            _db.kpsft_depobilgisi.Add(model);
            await _db.SaveChangesAsync();
        }

        public async Task<IActionResult> DeleteDepoAsync(string depoKodu)
        {
            var relatedStokItems = _db.kpsft_depobilgisi.Where(s => s.kpsft_depoKod == depoKodu);
            _db.kpsft_depobilgisi.RemoveRange(relatedStokItems);
            await _db.SaveChangesAsync();
        }

        public async Task<List<DepoModel>> GetAllDepoAsync()
        {
            var depoBilgisi = await _db.kpsft_depobilgisi.
               Include(s => s.depoTuru)
               .ToListAsync();
            return depoBilgisi;
        }

        public async Task<IActionResult> UpdateDepoAsync(DepoModel model)
        {
            // Veritabanında mevcut olan kaydı buluyoruz
            var mevcutDepoKarti = await _db.kpsft_depobilgisi
                .FirstOrDefaultAsync(s => s.kpsft_depoKod == model.kpsft_depoKod);

            if (mevcutDepoKarti != null)
            {
                mevcutDepoKarti.kpsft_depoKod = model.kpsft_depoKod;
                mevcutDepoKarti.kpsft_depoAd = model.kpsft_depoAd;
                mevcutDepoKarti.kpsft_depoTuru = model.kpsft_depoTuru;
                mevcutDepoKarti.kpsft_durum = model.kpsft_durum;
                mevcutDepoKarti.kpsft_genislik = model.kpsft_genislik;
                mevcutDepoKarti.kpsft_yukseklik = model.kpsft_yukseklik;
                mevcutDepoKarti.kpsft_uzunluk = model.kpsft_uzunluk;
                mevcutDepoKarti.kpsft_alan = model.kpsft_alan;
                mevcutDepoKarti.kpsft_depoSorumlusu = model.kpsft_depoSorumlusu;
                mevcutDepoKarti.kpsft_aciklama = model.kpsft_aciklama;

                _db.kpsft_depobilgisi.Update(mevcutDepoKarti);
            }
            else
            {
                _db.kpsft_depobilgisi.Add(model);
                await _db.SaveChangesAsync();
            }
        }
    }
}
