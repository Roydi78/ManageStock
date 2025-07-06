using ManageStock.Data.Services.Stock;
using Microsoft.AspNetCore.Mvc;

namespace ManageStock.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IStockService _StockService;

        public DashboardController(IStockService StockService)
        {
            _StockService = StockService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetStockChart()
        {
            var produits = await _StockService.GetAll(); // ex: liste de produits avec Nom et Quantité


            var chartData = new
            {
                labels = produits.Select(p => p.IdProduitNavigation.Nom).ToArray(),
                datasets = new[]
                {
            new {
                data = produits.Select(p => p.QuantitéDisponible).ToArray(),
                backgroundColor = produits.Select(p => GetRandomColor()).ToArray()
                }
            }
            };

            return Json(chartData);
        }

        private string GetRandomColor()
        {
            var rand = new Random();
            return $"#{rand.Next(0x1000000):X6}";
        }
    }
}
