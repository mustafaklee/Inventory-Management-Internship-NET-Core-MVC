using LoginEkrani.Models.Admin;
using LoginEkrani.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LoginEkrani.Services.Implementations
{
    public class DefinitionService : IDefinitionService
    {
        private readonly Database _db;

        public DefinitionService(Database db)
        {
            _db = db;
        }

        public async Task<bool> AddUnit(BirimModel model)
        {
            _db.kpsft_birimler.Add(model);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddStockType(StokTipi model)
        {
            _db.kpsft_stoktipi.Add(model);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddQuality(KaliteModel model)
        {
            _db.kpsft_kalite.Add(model);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddGroupCode(GrupModel model)
        {
            _db.kpsft_grup_kodu.Add(model);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddPurchaseVatRate(AlısKdvOraniModel model)
        {
            _db.kpsft_alis_kdv_orani.Add(model);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddSalesVatRate(SatisKdvOrani model)
        {
            _db.kpsft_satis_kdv_orani.Add(model);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddCustomsCode(GumrukKoduModel model)
        {
            _db.kpsft_gumruk_kodu.Add(model);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddTaxOffice(VergiDairesiModel model)
        {
            _db.kpsft_vergidairesi.Add(model);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<List<BirimModel>> GetAllUnits()
        {
            return await _db.kpsft_birimler.ToListAsync();
        }

        public async Task<List<StokTipi>> GetAllStockTypes()
        {
            return await _db.kpsft_stoktipi.ToListAsync();
        }

        public async Task<List<KaliteModel>> GetAllQualities()
        {
            return await _db.kpsft_kalite.ToListAsync();
        }

        public async Task<List<GrupModel>> GetAllGroupCodes()
        {
            return await _db.kpsft_grup_kodu.ToListAsync();
        }

        public async Task<List<AlısKdvOraniModel>> GetAllPurchaseVatRates()
        {
            return await _db.kpsft_alis_kdv_orani.ToListAsync();
        }

        public async Task<List<SatisKdvOrani>> GetAllSalesVatRates()
        {
            return await _db.kpsft_satis_kdv_orani.ToListAsync();
        }

        public async Task<List<GumrukKoduModel>> GetAllCustomsCodes()
        {
            return await _db.kpsft_gumruk_kodu.ToListAsync();
        }

        public async Task<List<VergiDairesiModel>> GetAllTaxOffices()
        {
            return await _db.kpsft_vergidairesi.ToListAsync();
        }
    }
} 