using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ManageStock.Data;
using ManageStock.Models;
using ManageStock.Data.Services.Stock;

namespace ManageStock.Controllers
{
    public class StocksController : Controller
    {
        private readonly IStockService _StockService;

        public StocksController(IStockService StockService)
        {
            _StockService = StockService;
        }

        // GET: Stocks
        public async Task<IActionResult> Index()
        {
            var stocks = await _StockService.GetAll();
            return View(stocks);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stock = await _StockService.Detail(id.Value);

            if (stock == null)
            {
                return NotFound();
            }

            return View(stock);
        }
    }
}
