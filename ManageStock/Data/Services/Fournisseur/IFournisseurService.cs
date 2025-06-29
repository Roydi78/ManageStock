namespace ManageStock.Data.Services.Fournisseur
{
    public interface IFournisseurService
    {
        Task<IEnumerable<Models.Fournisseur>> GetAll();
        Task<Models.Fournisseur?> GetById(int id);
        Task Add(Models.Fournisseur fournisseur);
        Task Update(int id, Models.Fournisseur fournisseur);
        Task Delete(int id);
        Task<bool> Exists(int id);
    }
}
