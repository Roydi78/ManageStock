namespace ManageStock.Data.Services.Entrepot
{
    public interface IEntrepotService
    {
        Task<IEnumerable<Models.Entrepot>> GetAll();
        Task<Models.Entrepot?> GetById(int id);
        Task Add(Models.Entrepot ent);
        Task Update(Models.Entrepot ent);
        Task Delete(int id);
        Task<bool> Exists(int id);
        Task<Models.Entrepot?> FirstorDefault(int id);
    }
}
