using System.Threading.Tasks;

namespace Lost.SharedLib
{
    public interface ITransactionService
    {
        public Task<TransactionViewModel[]> GetAllAsync();

        public Task<TransactionViewModel> GetAsync(long id);

        public Task AddOrUpdateAsync(TransactionViewModel transactionViewModel);

        public Task DeleteAsync(long id);

        public Task<bool> CanBeDeleted(long id);
    }
}
