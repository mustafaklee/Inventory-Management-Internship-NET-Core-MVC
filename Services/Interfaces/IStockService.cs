using LoginEkrani.Models.Admin;

namespace LoginEkrani.Services.Interfaces
{
    public interface IStockService
    {
        Task<List<StokKartıModel>> GetAllStocks();
        Task<StokKartıModel> GetStockByCode(string stokKodu);
        Task<bool> UpdateStock(StokKartıModel model);
        Task<bool> DeleteStock(string stokKodu);
        Task<bool> AddStock(StokKartıModel model);
        Task<MultipleModel> GetStockDetails(string stokKodu);
        Task<List<StokKartıModel>> GetStockMovements(string stokKodu);
    }
} 