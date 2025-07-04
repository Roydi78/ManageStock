using ManageStock.Models;
using Microsoft.EntityFrameworkCore;

namespace ManageStock.Data.Services.EntreeStock
{
    public class EntreeStockService : IEntreeStockService
    {
        private readonly ManageStockContext _context;

        public EntreeStockService(ManageStockContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task Add(Entreestock Entreestock)
        {
            if (Entreestock == null)
            {
                throw new ArgumentNullException(nameof(Entreestock));
            }
            _context.Entreestocks.Add(Entreestock);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var Entreestock = await _context.Entreestocks.FindAsync(id);
            if (Entreestock == null)
            {
                throw new KeyNotFoundException($"Le stock avec id {id} est introuvable.");
            }
            _context.Entreestocks.Remove(Entreestock);
            await _context.SaveChangesAsync();
        }

        public async Task<Entreestock?> DetailEntreestock(int id)
        {
            return await _context.Entreestocks
                .Include(f => f.IdEntrepotNavigation)
                .Include(f => f.IdProduitNavigation)
                .Include(f => f.IdFournisseurNavigation)
                .FirstOrDefaultAsync(m => m.IdEntree == id);
        }

        public async Task<bool> Exists(int id)
        {
            return await _context.Entreestocks.AnyAsync(c => c.IdEntree == id);
        }

        public async Task<Entreestock?> FirstorDefault(int id)
        {
            var Entreestock = await _context.Entreestocks
                                    .Include(f => f.IdEntrepotNavigation)
                                    .Include(f => f.IdProduitNavigation)
                                    .Include(f => f.IdFournisseurNavigation)
                                    .FirstOrDefaultAsync(m => m.IdEntree == id);
            return Entreestock;
        }

        public async Task<IEnumerable<Entreestock>> GetAll()
        {
            return await _context.Entreestocks
                    .Include(f => f.IdEntrepotNavigation)
                    .Include(f => f.IdProduitNavigation)
                    .Include(f => f.IdFournisseurNavigation)
                    .ToListAsync();
        }

        public async Task<Entreestock?> GetById(int id)
        {
            return await _context.Entreestocks.FindAsync(id);
        }

        public async Task Update(Entreestock Entreestock)
        {
            _context.Entry(Entreestock).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
