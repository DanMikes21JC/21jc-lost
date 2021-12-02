using System.Threading.Tasks;

namespace Lost.SharedLib
{
    public interface IVoiturePersonneService
    {
        public Task<VoiturePersonneViewModel[]> GetAllAsync();

        //public Task<VoitureViewModel> GetAsync(long id);

        public Task AddOrUpdateAsync(VoiturePersonneViewModel voitureViewModel);

        public Task DeleteAsync(long id);

        public Task<bool> CanBeDeleted(long id);
    }
}
