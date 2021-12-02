using System.Threading.Tasks;

namespace Lost.SharedLib
{
    public interface IVoitureService
    {
        public Task<VoitureViewModel[]> GetAllAsync();

        public Task<VoitureViewModel> GetAsync(long id);

        public Task AddOrUpdateAsync(VoitureViewModel voitureViewModel);

        public Task DeleteAsync(long id);

        public Task<bool> CanBeDeleted(long id);
    }
}
