
using ManageStock.Models;
using Microsoft.EntityFrameworkCore;

namespace ManageStock.Data.Services.Fournisseur
{
    public class FournisseurService : IFournisseurService
    {

        private readonly ManageStockContext _context;

        public FournisseurService(ManageStockContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task Add(Models.Fournisseur fournisseur)
        {
            if (fournisseur == null)
            {
                throw new ArgumentNullException(nameof(fournisseur));
            }
            _context.Fournisseurs.Add(fournisseur);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var fournisseur = await _context.Fournisseurs.FindAsync(id);
            if (fournisseur == null)
            {
                throw new KeyNotFoundException($"Le Fournisseur avec id {id} introuvable.");
            }
            _context.Fournisseurs.Remove(fournisseur);
            await _context.SaveChangesAsync().ContinueWith(t => t.Result > 0);
        }

        public async Task<bool> Exists(int id)
        {
            return await _context.Fournisseurs.AnyAsync(c => c.IdFournisseur == id);
        }

        public async Task<IEnumerable<Models.Fournisseur>> GetAll()
        {
            return await _context.Fournisseurs.ToListAsync();
        }

        public Task<Models.Fournisseur?> GetById(int id)
        {
            var fournisseur = _context.Fournisseurs.Find(id);
            if (fournisseur == null)
            {
                return Task.FromResult<Models.Fournisseur?>(null);
            }
            return Task.FromResult<Models.Fournisseur?>(fournisseur);
        }

        public async Task Update(int id, Models.Fournisseur fournisseur)
        {
            _context.Entry(fournisseur).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
