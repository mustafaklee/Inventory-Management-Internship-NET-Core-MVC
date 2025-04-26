using LoginEkrani.Models.Admin;
using LoginEkrani.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Renci.SshNet.Messages.Authentication;

namespace LoginEkrani.Services.Implementations
{
    public class CariService : ICariService
    {
        private readonly Database _db;

        public CariService(Database db)
        {
            _db = db;
        }

        public Task<IActionResult> CreateCariAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> DeleteCariAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<CariModel>> GetAllCariAsync()
        {
            return await _db.kpsft_cariler.ToListAsync();
        }

        public async Task<CariModel> UpdateCariAsync(CariGrupModel model1)
        {
            var mevcutCariKarti = await _db.kpsft_cariler
                .FirstOrDefaultAsync(s => s.kpsft_cariKod == model1.cariModel.kpsft_cariKod);

            var model = model1.cariModel;
            if (mevcutCariKarti != null)
            {
                mevcutCariKarti.kpsft_cariKod = model.kpsft_cariKod;
                mevcutCariKarti.kpsft_cariAd = model.kpsft_cariAd;
                mevcutCariKarti.kpsft_cariTipi = model.kpsft_cariTipi;
                mevcutCariKarti.kpsft_vergiNo = model.kpsft_vergiNo;
                mevcutCariKarti.kpsft_vergiDairesi = model.kpsft_vergiDairesi;
                mevcutCariKarti.kpsft_adres1 = model.kpsft_adres1;
                mevcutCariKarti.kpsft_adres2 = model.kpsft_adres2;
                mevcutCariKarti.kpsft_cariAd2 = model.kpsft_cariAd2;
                mevcutCariKarti.kpsft_adres3 = model.kpsft_adres3;
                mevcutCariKarti.kpsft_telefon1 = model.kpsft_telefon1;
                mevcutCariKarti.kpsft_telefon2 = model.kpsft_telefon2;
                mevcutCariKarti.kpsft_email = model.kpsft_email;
                mevcutCariKarti.kpsft_ıban = model.kpsft_ıban;
                mevcutCariKarti.kpsft_aciklama = model.kpsft_aciklama;
                mevcutCariKarti.kpsft_yurticiDisi = model.kpsft_yurticiDisi;
                mevcutCariKarti.kpsft_web_sitesi = model.kpsft_web_sitesi;
                mevcutCariKarti.kpsft_durum = model.kpsft_durum;
                mevcutCariKarti.kpsft_fason_uretim = model.kpsft_fason_uretim;



                _db.kpsft_cariler.Update(mevcutCariKarti);
                await _db.SaveChangesAsync();
                return mevcutCariKarti;
            }
            else
            {
                return null;
            }
        }
    }
}
