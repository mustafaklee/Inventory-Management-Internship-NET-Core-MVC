using LoginEkrani.Models.Admin;
using Microsoft.AspNetCore.Mvc;

namespace LoginEkrani.Services.Interfaces
{
    public interface IDepoService
    {
        Task<List<CariModel>> GetAllDepoAsync(DepoModel model);
        Task<IActionResult> CreateDepoAsync(DepoModel model);
        Task<CariModel> UpdateDepoAsync(DepoModel model);
        Task<IActionResult> DeleteDepoAsync(string depokodu);
    }
}
