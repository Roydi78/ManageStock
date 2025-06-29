
using Microsoft.EntityFrameworkCore;

namespace ManageStock.Data.Services.Categorie
{
    public class CategorieService : ICategorieService
    {
        private readonly ManageStockContext _context;

        public CategorieService(ManageStockContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task Add(Models.Categorie categorie)
        {
            if (categorie == null)
            {
                throw new ArgumentNullException(nameof(categorie));
            }
            _context.Categories.Add(categorie);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var categorie = await _context.Categories.FindAsync(id);
            if (categorie == null)
            {
                throw new KeyNotFoundException($"La Catégorie avec id {id} introuvable.");
            }
            _context.Categories.Remove(categorie);
            await _context.SaveChangesAsync().ContinueWith(t => t.Result > 0);

        }

        public async Task<bool> Exists(int id)
        {
            return await _context.Categories.AnyAsync(c => c.IdCategorie == id);
        }

        public async Task<IEnumerable<Models.Categorie>> GetAll()
        {
           return await _context.Categories.ToListAsync();
        }

        public Task<Models.Categorie?> GetById(int id)
        {
            var categorie = _context.Categories.Find(id);
            if (categorie == null)
            {
                return Task.FromResult<Models.Categorie?>(null);
            }
            return Task.FromResult<Models.Categorie?>(categorie);
            
        }

        public async Task Update(int id, Models.Categorie categorie)
        {
           
            _context.Entry(categorie).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            
        }

    }
}
