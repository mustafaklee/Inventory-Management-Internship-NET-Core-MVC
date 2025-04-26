using LoginEkrani.Models.Admin;
using LoginEkrani.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoginEkrani.Controllers
{
    [Authorize]
    public class VoucherController : Controller
    {
        private readonly IVoucherService _voucherService;
        private readonly IDefinitionService _definitionService;

        public VoucherController(IVoucherService voucherService, IDefinitionService definitionService)
        {
            _voucherService = voucherService;
            _definitionService = definitionService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var fisler = await _voucherService.GetAllVouchers();
            return View(fisler);
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
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] JsonStok json)
        {
            var result = await _voucherService.CreateCariVoucher(json);
            return Json(new { success = result });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var fis = await _voucherService.GetVoucherByNumber(id);
            var hareketler = await _voucherService.GetVoucherMovements(id);
            var model = new MultipleModel
            {
                FisTabloModel = fis,
                FisHaraketleriCari = hareketler
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromBody] JsonStok json)
        {
            var result = await _voucherService.UpdateCariVoucher(json);
            return Json(new { success = result });
        }

        [HttpGet]
        public async Task<IActionResult> Transfer()
        {
            var model = new MultipleModel();
            model.BirimModel = await _definitionService.GetAllUnits();
            model.AlisKdvOraniModel = await _definitionService.GetAllPurchaseVatRates();
            model.SatisKdvOraniModel = await _definitionService.GetAllSalesVatRates();
            model.GrupModel = await _definitionService.GetAllGroupCodes();
            model.GumrukKoduModel = await _definitionService.GetAllCustomsCodes();
            model.KaliteModel = await _definitionService.GetAllQualities();
            model.StokTipiModel = await _definitionService.GetAllStockTypes();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Transfer(DoubleModel model)
        {
            var result = await _voucherService.CreateDepoVoucher(model);
            return Json(new { success = result });
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            var fis = await _voucherService.GetVoucherByNumber(id);
            var hareketler = await _voucherService.GetVoucherMovements(id);
            var model = new MultipleModel
            {
                FisTabloModel = fis,
                FisHaraketleriCari = hareketler
            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Movements(string id)
        {
            var hareketler = await _voucherService.GetVoucherMovements(id);
            return View(hareketler);
        }
    }
} 