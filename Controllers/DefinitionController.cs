using LoginEkrani.Models.Admin;
using LoginEkrani.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoginEkrani.Controllers
{
    [Authorize]
    public class DefinitionController : Controller
    {
        private readonly IDefinitionService _definitionService;

        public DefinitionController(IDefinitionService definitionService)
        {
            _definitionService = definitionService;
        }

        [HttpGet]
        public IActionResult Unit()
        {
            return View(new BirimModel());
        }

        [HttpPost]
        public async Task<IActionResult> Unit(BirimModel model)
        {
            var result = await _definitionService.AddUnit(model);
            return RedirectToAction("Create", "Stock");
        }

        [HttpGet]
        public IActionResult StockType()
        {
            return View(new StokTipi());
        }

        [HttpPost]
        public async Task<IActionResult> StockType(StokTipi model)
        {
            var result = await _definitionService.AddStockType(model);
            return RedirectToAction("Create", "Stock");
        }

        [HttpGet]
        public IActionResult Quality()
        {
            return View(new KaliteModel());
        }

        [HttpPost]
        public async Task<IActionResult> Quality(KaliteModel model)
        {
            var result = await _definitionService.AddQuality(model);
            return RedirectToAction("Create", "Stock");
        }

        [HttpGet]
        public IActionResult GroupCode()
        {
            return View(new GrupModel());
        }

        [HttpPost]
        public async Task<IActionResult> GroupCode(GrupModel model)
        {
            var result = await _definitionService.AddGroupCode(model);
            return RedirectToAction("Create", "Stock");
        }

        [HttpGet]
        public IActionResult PurchaseVatRate()
        {
            return View(new AlısKdvOraniModel());
        }

        [HttpPost]
        public async Task<IActionResult> PurchaseVatRate(AlısKdvOraniModel model)
        {
            var result = await _definitionService.AddPurchaseVatRate(model);
            return RedirectToAction("Create", "Stock");
        }

        [HttpGet]
        public IActionResult SalesVatRate()
        {
            return View(new SatisKdvOrani());
        }

        [HttpPost]
        public async Task<IActionResult> SalesVatRate(SatisKdvOrani model)
        {
            var result = await _definitionService.AddSalesVatRate(model);
            return RedirectToAction("Create", "Stock");
        }

        [HttpGet]
        public IActionResult CustomsCode()
        {
            return View(new GumrukKoduModel());
        }

        [HttpPost]
        public async Task<IActionResult> CustomsCode(GumrukKoduModel model)
        {
            var result = await _definitionService.AddCustomsCode(model);
            return RedirectToAction("Create", "Stock");
        }

        [HttpGet]
        public IActionResult TaxOffice()
        {
            return View(new VergiDairesiModel());
        }

        [HttpPost]
        public async Task<IActionResult> TaxOffice(VergiDairesiModel model)
        {
            var result = await _definitionService.AddTaxOffice(model);
            return RedirectToAction("Create", "Stock");
        }
    }
} 