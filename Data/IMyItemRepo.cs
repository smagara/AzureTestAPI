using azure_test_api.Models;

namespace azure_test_api.Data
{
    public interface IMyItemRepo
    {
        Task<IEnumerable<MyItem>> GetItems();
    }
}