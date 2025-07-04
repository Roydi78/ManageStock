namespace ManageStock.Data.Services.EntreeStock
{
    public interface IEntreeStockService
    {
        Task<IEnumerable<Models.Entreestock>> GetAll();
        Task<Models.Entreestock?> GetById(int id);
        Task Add(Models.Entreestock Entreestock);
        Task Update(Models.Entreestock Entreestock);
        Task Delete(int id);
        Task<bool> Exists(int id);
        Task<Models.Entreestock?> FirstorDefault(int id);
        Task<Models.Entreestock?> DetailEntreestock(int id);
    }
}
