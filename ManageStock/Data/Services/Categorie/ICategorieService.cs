namespace ManageStock.Data.Services.Categorie
{
    public interface ICategorieService
    {
        Task<IEnumerable<Models.Categorie>> GetAll();
        Task<Models.Categorie?> GetById(int id);
        Task Add(Models.Categorie categorie);
        Task Update(int id, Models.Categorie categorie);
        Task Delete(int id);
        Task<bool> Exists(int id);
    }
}
