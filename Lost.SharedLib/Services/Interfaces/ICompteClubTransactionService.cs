using System.Threading.Tasks;

namespace Lost.SharedLib
{
    public interface ICompteClubTransactionService
    {
        public Task<CompteClubFormulaireViewModel[]> GetAllAsync();

        public Task<CompteClubFormulaireViewModel> GetAsync(long id);

        public Task AddOrUpdateAsync(CompteClubFormulaireViewModel compteClubFormulaireViewModel);

        public Task DeleteAsync(long id);

        public Task<bool> CanBeDeleted(long id);
    }
}
