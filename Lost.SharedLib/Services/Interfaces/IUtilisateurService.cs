using System.Threading.Tasks;

namespace Lost.SharedLib
{
    public interface IUtilisateurService
    {
        public Task<UtilisateurViewModel[]> GetAllAsync();

        public Task<UtilisateurViewModel> GetAsync(long id);

        public Task AddOrUpdateAsync(UtilisateurViewModel utilisateurViewModel);

        public Task DeleteAsync(long id);
    }
}
