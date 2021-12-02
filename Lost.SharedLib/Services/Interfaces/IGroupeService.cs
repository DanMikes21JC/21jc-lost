using System.Threading.Tasks;

namespace Lost.SharedLib
{
    public interface IGroupeService
    {
        public Task<GroupeViewModel[]> GetAllAsync();

        public Task<GroupeViewModel[]> GetAllWithTauxAsync();

        public Task<GroupeViewModel> GetAsync(long id);

        public Task AddOrUpdateAsync(GroupeViewModel groupeViewModel);

        public Task DeleteAsync(long id);

        public Task<bool> CanBeDeleted(long id);
    }
}
