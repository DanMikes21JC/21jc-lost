using System.Threading.Tasks;

namespace Lost.SharedLib
{
    public interface ISacService
    {
        public Task<SacViewModel[]> GetAllAsync();

        public Task<SacViewModel> GetAsync(long id);

        public Task AddOrUpdateAsync(SacViewModel sacViewModel);

        public Task DeleteAsync(long id);

        public Task<bool> CanBeDeleted(long id);
    }
}
