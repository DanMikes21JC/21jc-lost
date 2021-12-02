using System.Threading.Tasks;

namespace Lost.SharedLib
{
    public interface IPersonneService
    {
        public Task<PersonneViewModel[]> GetAllAsync();

        public Task<PersonneViewModel[]> GetAllWithTauxAsync();

        public Task<PersonneViewModel[]> GetPersonneFromGroupe(long idGroupe);

        public Task<PersonneViewModel> GetAsync(long id);

        public Task AddOrUpdateAsync(PersonneViewModel personneViewModel);

        public Task DeleteAsync(long id);

        public Task<bool> CanBeDeleted(long id);
    }
}
