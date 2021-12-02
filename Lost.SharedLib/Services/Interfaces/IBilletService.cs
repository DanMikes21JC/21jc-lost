using System.Threading.Tasks;

namespace Lost.SharedLib
{
    public interface IBilletService
    {
        public Task<BilletViewModel[]> GetAllAsync();

        public Task<BilletViewModel> GetAsync(long id);

        public Task AddOrUpdateAsync(BilletViewModel billetViewModel);

        public Task DeleteAsync(long id);

        public Task<bool> CanBeDeleted(long id);
    }
}
