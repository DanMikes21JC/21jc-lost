using System.Threading.Tasks;

namespace Lost.SharedLib
{
    public interface IAchatVenteService
    {
        public Task<AchatVenteViewModel[]> GetAllAsync();

        public Task<AchatVenteViewModel> GetAsync(long id);

        public Task AddOrUpdateAsync(AchatVenteViewModel achatVenteViewModel);

        public Task DeleteAsync(long id);

        public Task<bool> CanBeDeleted(long id);
    }
}
