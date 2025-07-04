
using Microsoft.EntityFrameworkCore;

namespace ManageStock.Data.Services.Stock
{
    public class StockService : IStockService
    {
        private readonly ManageStockContext _context;

        public StockService(ManageStockContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Models.Stock?>> Detail(int id)
        {
            var stock = await _context.Stocks
                .Include(f => f.IdEntrepotNavigation)
                .Include(f => f.IdProduitNavigation)
                .Where(m => m.IdProduit == id)
                .ToListAsync();

            if (stock == null || !stock.Any())
            {
                return null;
            }

            return stock;
        }

        public async Task<IEnumerable<Models.Stock>> GetAll()
        {
            var stockParProduit = await _context.Stocks
                                .Include(s => s.IdProduitNavigation)
                                .Include(s => s.IdEntrepotNavigation)
                                .ToListAsync();

            var result = stockParProduit
                .GroupBy(s => s.IdProduit)
                .Select(g => new Models.Stock
                {
                    IdProduit = g.Key,
                    IdProduitNavigation = g.First().IdProduitNavigation,
                    QuantitéDisponible = g.Sum(s => s.QuantitéDisponible)
                })
                .OrderBy(s => s.IdProduitNavigation.Nom)
                .ToList();

            return result;
        }

        public async Task<IEnumerable<Models.Stock?>> GetAllNoGrp()
        {
            var listebrute = await _context.Stocks
                                 .Include(s => s.IdProduitNavigation)
                                 .Include(s => s.IdEntrepotNavigation)
                                 .Distinct()
                                 .OrderBy(s => s.IdProduitNavigation.Nom)
                                 .ToListAsync();

            return listebrute;
        }
    }
}
