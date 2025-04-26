using LoginEkrani.Models.Admin;
using LoginEkrani.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoginEkrani.Controllers
{
    [Authorize]
    public class StockController : Controller
    {
        private readonly IStockService _stockService;
        private readonly IDefinitionService _definitionService;

        public StockController(IStockService stockService, IDefinitionService definitionService)
        {
            _stockService = stockService;
            _definitionService = definitionService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var stokKartlar = await _stockService.GetAllStocks();
            return View(stokKartlar);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new MultipleModel();
            model.BirimModel = await _definitionService.GetAllUnits();
            model.AlisKdvOraniModel = await _definitionService.GetAllPurchaseVatRates();
            model.SatisKdvOraniModel = await _definitionService.GetAllSalesVatRates();
            model.GrupModel = await _definitionService.GetAllGroupCodes();
            model.GumrukKoduModel = await _definitionService.GetAllCustomsCodes();
            model.KaliteModel = await _definitionService.GetAllQualities();
            model.StokTipiModel = await _definitionService.GetAllStockTypes();
            model.StokKartıModel = new StokKartıModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(StokKartıModel model)
        {
            var result = await _stockService.AddStock(model);
            if (result)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var model = await _stockService.GetStockDetails(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(StokKartıModel model)
        {
            var result = await _stockService.UpdateStock(model);
            if (result)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _stockService.DeleteStock(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            var model = await _stockService.GetStockDetails(id);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Movements(string id)
        {
            var hareketler = await _stockService.GetStockMovements(id);
            return View(hareketler);
        }
    }
} 