
using ManageStock.Models;
using Microsoft.EntityFrameworkCore;

namespace ManageStock.Data.Services.Entrepot
{
    public class EntrepotService : IEntrepotService
    {

        private readonly ManageStockContext _context;

        public EntrepotService(ManageStockContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task Add(Models.Entrepot ent)
        {
            if (ent == null)
            {
                throw new ArgumentNullException(nameof(ent));
            }
            _context.Entrepots.Add(ent);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var ent = await _context.Entrepots.FindAsync(id);
            if (ent == null)
            {
                throw new KeyNotFoundException($"L'entrepôt avec id {id} introuvable.");
            }
            _context.Entrepots.Remove(ent);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Exists(int id)
        {
            return await _context.Entrepots.AnyAsync(c => c.IdEntrepot == id);
        }

        public async Task<IEnumerable<Models.Entrepot>> GetAll()
        {
            return await _context.Entrepots.ToListAsync();
        }

        public Task<Models.Entrepot?> GetById(int id)
        {
            var ent = _context.Entrepots.Find(id);
            if (ent == null)
            {
                return  Task.FromResult<Models.Entrepot?>(null);
            }
            return Task.FromResult<Models.Entrepot?>(ent);
        }

        public async Task Update(Models.Entrepot ent)
        {
            _context.Entry(ent).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<Models.Entrepot?> FirstorDefault(int id)
        {
            var ent = await _context.Entrepots.FirstOrDefaultAsync(m => m.IdEntrepot == id);
            if (ent == null)
            {
                return null;
            }

            return ent;
        }

    }
}
