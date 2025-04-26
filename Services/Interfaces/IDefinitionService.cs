using LoginEkrani.Models.Admin;

namespace LoginEkrani.Services.Interfaces
{
    public interface IDefinitionService
    {
        Task<bool> AddUnit(BirimModel model);
        Task<bool> AddStockType(StokTipi model);
        Task<bool> AddQuality(KaliteModel model);
        Task<bool> AddGroupCode(GrupModel model);
        Task<bool> AddPurchaseVatRate(AlısKdvOraniModel model);
        Task<bool> AddSalesVatRate(SatisKdvOrani model);
        Task<bool> AddCustomsCode(GumrukKoduModel model);
        Task<bool> AddTaxOffice(VergiDairesiModel model);
        Task<List<BirimModel>> GetAllUnits();
        Task<List<StokTipi>> GetAllStockTypes();
        Task<List<KaliteModel>> GetAllQualities();
        Task<List<GrupModel>> GetAllGroupCodes();
        Task<List<AlısKdvOraniModel>> GetAllPurchaseVatRates();
        Task<List<SatisKdvOrani>> GetAllSalesVatRates();
        Task<List<GumrukKoduModel>> GetAllCustomsCodes();
        Task<List<VergiDairesiModel>> GetAllTaxOffices();
    }
} 