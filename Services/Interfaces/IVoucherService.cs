using LoginEkrani.Models.Admin;

namespace LoginEkrani.Services.Interfaces
{
    public interface IVoucherService
    {
        Task<List<FisTabloModel>> GetAllVouchers();
        Task<FisTabloModel> GetVoucherByNumber(string fisNo);
        Task<bool> CreateVoucher(FisTabloModel model);
        Task<bool> UpdateVoucher(FisTabloModel model);
        Task<bool> DeleteVoucher(string fisNo);
        Task<List<FisHaraketleriCariModel>> GetVoucherMovements(string fisNo);
        Task<bool> CreateCariVoucher(JsonStok json);
        Task<bool> UpdateCariVoucher(JsonStok json);
        Task<bool> CreateDepoVoucher(DoubleModel model);
        Task<bool> UpdateDepoVoucher(DoubleModel model);
    }
} 