using LoginEkrani.Models.Admin;
using LoginEkrani.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LoginEkrani.Services.Implementations
{
    public class StockService : IStockService
    {
        private readonly Database _db;

        public StockService(Database db)
        {
            _db = db;
        }

        public async Task<List<StokKartıModel>> GetAllStocks()
        {
            return await _db.kpsft_stok_karti
                .Include(s => s.durum)
                .Include(s => s.birimModel)
                .Include(s => s.grupModel)
                .Include(s => s.gumrukKoduModel)
                .Include(s => s.kaliteModel)
                .Include(s => s.satisKdvOrani)
                .Include(s => s.alisKdvOraniModel)
                .Include(s => s.stokTipi)
                .ToListAsync();
        }

        public async Task<StokKartıModel> GetStockByCode(string stokKodu)
        {
            return await _db.kpsft_stok_karti
                .Include(s => s.alisKdvOraniModel)
                .Include(sh => sh.birimModel)
                .Include(sc => sc.grupModel)
                .Include(sm => sm.gumrukKoduModel)
                .Include(sk => sk.kaliteModel)
                .Include(sl => sl.satisKdvOrani)
                .Include(so => so.stokTipi)
                .Include(sn => sn.durum)
                .FirstOrDefaultAsync(sa => sa.kpsft_stokKodu == stokKodu);
        }

        public async Task<bool> UpdateStock(StokKartıModel model)
        {
            var mevcutStokKarti = await _db.kpsft_stok_karti
                .FirstOrDefaultAsync(s => s.kpsft_stokKodu == model.kpsft_stokKodu);

            if (mevcutStokKarti != null)
            {
                mevcutStokKarti.kpsft_stokKodu = model.kpsft_stokKodu;
                mevcutStokKarti.kpsft_stokAdi = model.kpsft_stokAdi;
                mevcutStokKarti.kpsft_birim = model.kpsft_birim;
                mevcutStokKarti.kpsft_grupKodu = model.kpsft_grupKodu;
                mevcutStokKarti.kpsft_aciklama = model.kpsft_aciklama;
                mevcutStokKarti.kpsft_durum = model.kpsft_durum;
                mevcutStokKarti.kpsft_stokKartTipi = model.kpsft_stokKartTipi;
                mevcutStokKarti.kpsft_alisKdvOrani = model.kpsft_alisKdvOrani;
                mevcutStokKarti.kpsft_satisKdvOrani = model.kpsft_satisKdvOrani;
                mevcutStokKarti.kpsft_kalite = model.kpsft_kalite;
                mevcutStokKarti.kpsft_kritikMiktar = model.kpsft_kritikMiktar;
                mevcutStokKarti.kpsft_lotBuyuklugu = model.kpsft_lotBuyuklugu;
                mevcutStokKarti.kpsft_gtipKodu = model.kpsft_gtipKodu;
                mevcutStokKarti.kpsft_boyut1 = model.kpsft_boyut1;
                mevcutStokKarti.kpsft_boyut2 = model.kpsft_boyut2;
                mevcutStokKarti.kpsft_boyut3 = model.kpsft_boyut3;
                mevcutStokKarti.kpsft_boyut4 = model.kpsft_boyut4;
                mevcutStokKarti.kpsft_boyut5 = model.kpsft_boyut5;
                mevcutStokKarti.kpsft_uretim_parti_buyuklugu = model.kpsft_uretim_parti_buyuklugu;

                _db.kpsft_stok_karti.Update(mevcutStokKarti);
                await _db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteStock(string stokKodu)
        {
            var relatedStokItems = _db.kpsft_stok_karti.Where(s => s.kpsft_stokKodu == stokKodu);
            _db.kpsft_stok_karti.RemoveRange(relatedStokItems);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddStock(StokKartıModel model)
        {
            _db.kpsft_stok_karti.Add(model);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<MultipleModel> GetStockDetails(string stokKodu)
        {
            var model = new MultipleModel();
            model.StokKartıModel = await GetStockByCode(stokKodu);
            model.BirimModel = await _db.kpsft_birimler.ToListAsync();
            model.AlisKdvOraniModel = await _db.kpsft_alis_kdv_orani.ToListAsync();
            model.SatisKdvOraniModel = await _db.kpsft_satis_kdv_orani.ToListAsync();
            model.GrupModel = await _db.kpsft_grup_kodu.ToListAsync();
            model.GumrukKoduModel = await _db.kpsft_gumruk_kodu.ToListAsync();
            model.KaliteModel = await _db.kpsft_kalite.ToListAsync();
            model.StokTipiModel = await _db.kpsft_stoktipi.ToListAsync();
            return model;
        }

        public async Task<List<StokKartıModel>> GetStockMovements(string stokKodu)
        {
            return await _db.kpsft_stok_karti
                .Include(s => s.durum)
                .Include(s => s.birimModel)
                .Include(s => s.grupModel)
                .Include(s => s.gumrukKoduModel)
                .Include(s => s.kaliteModel)
                .Include(s => s.satisKdvOrani)
                .Include(s => s.alisKdvOraniModel)
                .Include(s => s.stokTipi)
                .Where(s => s.kpsft_stokKodu == stokKodu)
                .ToListAsync();
        }
    }
} 