namespace ManageStock.Data.Services.SortieStock
{
    public interface ISortieStockService
    {
        Task<IEnumerable<Models.Sortiestock>> GetAll();
        Task Add(Models.Sortiestock Sortiestock);
        Task Update(Models.Sortiestock Sortiestock);
        Task Delete(int id);
        Task<bool> Exists(int id);
        Task<Models.Sortiestock?> FirstorDefault(int id);
        Task<Models.Sortiestock?> Detail(int id);
    }
}
