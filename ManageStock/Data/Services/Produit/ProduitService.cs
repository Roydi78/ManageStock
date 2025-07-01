
using ManageStock.Models;
using Microsoft.EntityFrameworkCore;

namespace ManageStock.Data.Services.Produit
{
    public class ProduitService : IProduitService
    {
        private readonly ManageStockContext _context;

        public ProduitService(ManageStockContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task Add(Models.Produit produit)
        {
            if (produit == null)
            {
                throw new ArgumentNullException(nameof(produit));
            }
            _context.Produits.Add(produit);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var produit = await _context.Produits.FindAsync(id);
            if (produit == null)
            {
                throw new KeyNotFoundException($"Le Produit avec id {id} est introuvable.");
            }
            _context.Produits.Remove(produit);
            await _context.SaveChangesAsync();
        }

        public async Task<Models.Produit?> DetailProduit(int id)
        {
            return  await _context.Produits
                .Include(f => f.IdCategorieNavigation)
                .Include(f => f.IdFournisseurNavigation)
                .FirstOrDefaultAsync(m => m.IdProduit == id);

        }

        public async Task<bool> Exists(int id)
        {
            return await _context.Produits.AnyAsync(c => c.IdProduit == id);
        }

        public async Task<Models.Produit?> FirstorDefault(int id)
        {
            return  await _context.Produits.FirstOrDefaultAsync(m => m.IdProduit == id);      
        }

        public async Task<IEnumerable<Models.Produit>> GetAll()
        {
            return await _context.Produits.ToListAsync();
        }

        public async Task<Models.Produit?> GetById(int id)
        {
            return await _context.Produits.FindAsync(id);    
        }

        public async Task Update(Models.Produit produit)
        {
            _context.Entry(produit).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
