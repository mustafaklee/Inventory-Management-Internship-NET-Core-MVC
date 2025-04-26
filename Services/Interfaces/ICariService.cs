using LoginEkrani.Models.Admin;
using Microsoft.AspNetCore.Mvc;

namespace LoginEkrani.Services.Interfaces
{
    public interface ICariService
    {
        Task<List<CariModel>> GetAllCariAsync();
        Task<IActionResult> CreateCariAsync();
        Task<CariModel> UpdateCariAsync(CariGrupModel model1);
        Task<IActionResult> DeleteCariAsync();

    }
}
