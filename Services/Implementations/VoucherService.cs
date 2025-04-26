using LoginEkrani.Models.Admin;
using LoginEkrani.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LoginEkrani.Services.Implementations
{
    public class VoucherService : IVoucherService
    {
        private readonly Database _db;

        public VoucherService(Database db)
        {
            _db = db;
        }

        public async Task<List<FisTabloModel>> GetAllVouchers()
        {
            return await _db.kpsft_fistablo_cari
                .Include(f => f.Cariislem)
                .Include(f => f.Depo)
                .ToListAsync();
        }

        public async Task<FisTabloModel> GetVoucherByNumber(string fisNo)
        {
            return await _db.kpsft_fistablo_cari
                .Include(f => f.Cariislem)
                .Include(f => f.Depo)
                .FirstOrDefaultAsync(f => f.kpsft_FisNo == fisNo);
        }

        public async Task<bool> CreateVoucher(FisTabloModel model)
        {
            _db.kpsft_fistablo_cari.Add(model);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateVoucher(FisTabloModel model)
        {
            var mevcutFis = await _db.kpsft_fistablo_cari
                .FirstOrDefaultAsync(f => f.kpsft_FisNo == model.kpsft_FisNo);

            if (mevcutFis != null)
            {
                _db.Entry(mevcutFis).CurrentValues.SetValues(model);
                await _db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteVoucher(string fisNo)
        {
            var fis = await _db.kpsft_fistablo_cari
                .FirstOrDefaultAsync(f => f.kpsft_FisNo == fisNo);

            if (fis != null)
            {
                _db.kpsft_fistablo_cari.Remove(fis);
                await _db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<FisHaraketleriCariModel>> GetVoucherMovements(string fisNo)
        {
            return await _db.kpsft_fisharaketi_cari
                .Include(f => f.StokKartı)
                    .ThenInclude(s => s.birimModel)
                .Include(f => f.fistablocariModel)
                    .ThenInclude(f => f.Cariislem)
                .Include(f => f.fistablocariModel)
                    .ThenInclude(f => f.Depo)
                .Where(f => f.fistablocariModel.kpsft_FisNo == fisNo)
                .ToListAsync();
        }

        public async Task<bool> CreateCariVoucher(JsonStok json)
        {
            try
            {
                var fisTablo = new FisTabloCariModel
                {
                    kpsft_FisNo = json.fisNo,
                    kpsft_tarih = DateTime.Now,
                    kpsft_FisTuru = json.fisTuru,
                    kpsft_cari = json.cariKodu,
                    kpsft_depo = json.depoKodu
                };

                _db.kpsft_fistablo_cari.Add(fisTablo);
                await _db.SaveChangesAsync();

                foreach (var stok in json.stoklar)
                {
                    var fisHareket = new FisHaraketleriCariModel
                    {
                        kpsft_FisNo = json.fisNo,
                        kpsft_StokKod = stok.stokKodu,
                        kpsft_Miktar = stok.miktar,
                        kpsft_BirimFiyat = stok.birimFiyat
                    };

                    _db.kpsft_fisharaketi_cari.Add(fisHareket);
                }

                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateCariVoucher(JsonStok json)
        {
            try
            {
                var fisTablo = await _db.kpsft_fistablo_cari
                    .FirstOrDefaultAsync(f => f.kpsft_FisNo == json.fisNo);

                if (fisTablo != null)
                {
                    fisTablo.kpsft_cari = json.cariKodu;
                    fisTablo.kpsft_depo = json.depoKodu;
                    _db.kpsft_fistablo_cari.Update(fisTablo);

                    var mevcutHareketler = await _db.kpsft_fisharaketi_cari
                        .Where(f => f.kpsft_FisNo == json.fisNo)
                        .ToListAsync();

                    _db.kpsft_fisharaketi_cari.RemoveRange(mevcutHareketler);

                    foreach (var stok in json.stoklar)
                    {
                        var fisHareket = new FisHaraketleriCariModel
                        {
                            kpsft_FisNo = json.fisNo,
                            kpsft_StokKod = stok.stokKodu,
                            kpsft_Miktar = stok.miktar,
                            kpsft_BirimFiyat = stok.birimFiyat
                        };

                        _db.kpsft_fisharaketi_cari.Add(fisHareket);
                    }

                    await _db.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> CreateDepoVoucher(DoubleModel model)
        {
            try
            {
                var fisTablo = new FisTabloDepoModel
                {
                    kpsft_FisNo = model.fisNo,
                    kpsft_tarih = DateTime.Now,
                    kpsft_FisTuru = model.fisTuru,
                    kpsft_kaynakDepo = model.kaynakDepo,
                    kpsft_hedefDepo = model.hedefDepo
                };

                _db.kpsft_fistablo_depo.Add(fisTablo);
                await _db.SaveChangesAsync();

                foreach (var stok in model.stoklar)
                {
                    var fisHareket = new FisHaraketleriDepoModel
                    {
                        kpsft_FisNo = model.fisNo,
                        kpsft_StokKod = stok.stokKodu,
                        kpsft_Miktar = stok.miktar
                    };

                    _db.kpsft_fisharaketi_depo.Add(fisHareket);
                }

                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateDepoVoucher(DoubleModel model)
        {
            try
            {
                var fisTablo = await _db.kpsft_fistablo_depo
                    .FirstOrDefaultAsync(f => f.kpsft_FisNo == model.fisNo);

                if (fisTablo != null)
                {
                    fisTablo.kpsft_kaynakDepo = model.kaynakDepo;
                    fisTablo.kpsft_hedefDepo = model.hedefDepo;
                    _db.kpsft_fistablo_depo.Update(fisTablo);

                    var mevcutHareketler = await _db.kpsft_fisharaketi_depo
                        .Where(f => f.kpsft_FisNo == model.fisNo)
                        .ToListAsync();

                    _db.kpsft_fisharaketi_depo.RemoveRange(mevcutHareketler);

                    foreach (var stok in model.stoklar)
                    {
                        var fisHareket = new FisHaraketleriDepoModel
                        {
                            kpsft_FisNo = model.fisNo,
                            kpsft_StokKod = stok.stokKodu,
                            kpsft_Miktar = stok.miktar
                        };

                        _db.kpsft_fisharaketi_depo.Add(fisHareket);
                    }

                    await _db.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
} 