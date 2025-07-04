namespace ManageStock.Data.Services.Stock
{
    public interface IStockService
    {
        Task<IEnumerable<Models.Stock>> GetAll();
        Task<IEnumerable<Models.Stock?>> Detail(int id);

        Task<IEnumerable<Models.Stock?>> GetAllNoGrp();

    }
}
