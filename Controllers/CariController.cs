using LoginEkrani.Models.Admin;
using LoginEkrani.Services.Implementations;
using LoginEkrani.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LoginEkrani.Controllers
{
    public class CariController : Controller
    {
        private readonly ICariService _cariService;
        public CariController(ICariService cariService)
        {
            _cariService = cariService;
        }

        [HttpGet]
        public async Task<IActionResult> CariTanım(DepoModel model)
        {
            return View(await _cariService.GetAllCariAsync());
        }


        [HttpPost]
        public async Task<IActionResult> CariDuzenle(CariGrupModel model1)
        {
            _cariService.UpdateCariAsync(model1);
            return RedirectToAction("CariTanım");
        }
    }
}
