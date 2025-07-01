namespace ManageStock.Data.Services.Produit
{
    public interface IProduitService
    {
        Task<IEnumerable<Models.Produit>> GetAll();
        Task<Models.Produit?> GetById(int id);
        Task Add(Models.Produit produit);
        Task Update(Models.Produit produit);
        Task Delete(int id);
        Task<bool> Exists(int id);
        Task<Models.Produit?> FirstorDefault(int id);
        Task<Models.Produit?> DetailProduit(int id);

    }
}
