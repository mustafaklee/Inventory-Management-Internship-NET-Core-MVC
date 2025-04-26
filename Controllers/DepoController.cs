using LoginEkrani.Models.Admin;
using LoginEkrani.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LoginEkrani.Controllers
{
    public class DepoController : Controller
    {

        private readonly ILogger<AdminController> _logger;
        private readonly Database _db;
        private readonly IDepoService _depoService;
        public DepoController(ILogger<AdminController> logger, Database db , IDepoService depoService)
        {
            _logger = logger;
            _db = db;
            _depoService = depoService;
        }


        [HttpGet]
        public async Task<IActionResult> DepoTanım(DepoModel model)
        {

            var depoBilgisi = _depoService.GetAllDepoAsync(model);

            return View(depoBilgisi);
        }

        [HttpGet]
        public async Task<IActionResult> YeniDepoEkle()
        {
            ViewBag.DepoTurler = _db.kpsft_depo_turu.ToList();
            ViewBag.DepoSorumlusu = _db.kpsft_boskay.ToList();
            DepoModel model = new DepoModel();
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> YeniDepoEkle(DepoModel model, int kpsft_depoTuru)
        {
            _depoService.CreateDepoAsync(model);
            return RedirectToAction("DepoTanım");
        }

        [HttpGet]
        public async Task<IActionResult> DepoKartiDuzenle(string depoKodu)
        {
            ViewBag.DepoTurler = ViewBag.DepoTurler = _db.kpsft_depo_turu.ToList();
            DepoModel model = new DepoModel();
            model = await _db.kpsft_depobilgisi
               .FirstOrDefaultAsync(sa => sa.kpsft_depoKod == depoKodu);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DepoKartiDuzenle(DepoModel model)
        {
            _depoService.UpdateDepoAsync(model);
            return RedirectToAction("DepoTanım");
        }

        public async Task<IActionResult> depoKartıSil(string depoKodu)
        {
            _depoService.DeleteDepoAsync(depoKodu);
            return RedirectToAction("DepoTanım");
        }
    }
}
