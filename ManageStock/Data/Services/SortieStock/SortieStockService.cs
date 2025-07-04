using ManageStock.Models;
using Microsoft.EntityFrameworkCore;

namespace ManageStock.Data.Services.SortieStock
{
    public class SortieStockService : ISortieStockService
    {
        private readonly ManageStockContext _context;

        public SortieStockService(ManageStockContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task Add(Sortiestock Sortiestock)
        {
            if (Sortiestock == null)
            {
                throw new ArgumentNullException(nameof(Sortiestock));
            }
            _context.Sortiestocks.Add(Sortiestock);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var Sortiestock = await _context.Sortiestocks.FindAsync(id);
            if (Sortiestock == null)
            {
                throw new KeyNotFoundException($"Le stock avec id {id} est introuvable.");
            }
            _context.Sortiestocks.Remove(Sortiestock);
            await _context.SaveChangesAsync();
        }

        public async Task<Sortiestock?> Detail(int id)
        {
            return await _context.Sortiestocks
                .Include(f => f.IdEntrepotNavigation)
                .Include(f => f.IdProduitNavigation)
                .FirstOrDefaultAsync(m => m.IdSortie == id);
        }

        public async Task<bool> Exists(int id)
        {
            return await _context.Sortiestocks.AnyAsync(c => c.IdSortie == id);
        }

        public async Task<Sortiestock?> FirstorDefault(int id)
        {
            var sortiestock = await _context.Sortiestocks
                                    .Include(f => f.IdEntrepotNavigation)
                                    .Include(f => f.IdProduitNavigation)
                                    .FirstOrDefaultAsync(m => m.IdSortie == id);
            return sortiestock;
        }

        public async Task<IEnumerable<Sortiestock>> GetAll()
        {
            return await _context.Sortiestocks
                    .Include(f => f.IdEntrepotNavigation)
                    .Include(f => f.IdProduitNavigation)
                    .ToListAsync();
        }

        

        public async Task Update(Sortiestock Sortiestock)
        {
            _context.Entry(Sortiestock).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
