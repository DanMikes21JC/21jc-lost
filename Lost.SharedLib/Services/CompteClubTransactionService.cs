using Lost.DataAccess.Entities;
using Lost.Model;
using Lost.SharedLib.Utils.Assembly;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lost.SharedLib
{
    public class CompteClubTransactionService : ICompteClubTransactionService
    {
        public async Task<CompteClubFormulaireViewModel[]> GetAllAsync()
        {
            IList<CompteClubTransaction> transactionList = await CompteClubTransactionDal.GetListWithPersonneAsync();
            CompteClubFormulaireViewModel[] result = new CompteClubFormulaireViewModel[transactionList.Count];

            int i = 0;
            foreach (var transaction in transactionList.OrderBy(e => e.Id))
            {
                CompteClubFormulaireViewModel transactionViewModel = EntityToViewModel.FillViewModel<CompteClubTransaction, CompteClubFormulaireViewModel>(transaction);
                if (transaction.Personne != null)
                {
                    transactionViewModel.PersonneViewModel = EntityToViewModel.FillViewModel<Personne, PersonneViewModel>(transaction.Personne);
                }

                result[i] = transactionViewModel;
                i++;
            }
            return await Task.FromResult(result);
        }

        public async Task<CompteClubFormulaireViewModel> GetAsync(long id)
        {
            CompteClubTransaction compteClubTransaction = await CompteClubTransactionDal.GetWithDetailsAsync(id);

            CompteClubFormulaireViewModel compteClubFormulaireViewModel = EntityToViewModel.FillViewModel<CompteClubTransaction, CompteClubFormulaireViewModel>(compteClubTransaction);
            if (compteClubTransaction.Personne != null)
            {
                compteClubFormulaireViewModel.PersonneViewModel = EntityToViewModel.FillViewModel<Personne, PersonneViewModel>(compteClubTransaction.Personne);
            }

            return compteClubFormulaireViewModel;
        }

        public async Task AddOrUpdateAsync(CompteClubFormulaireViewModel compteClubFormulaireViewModel)
        {
            CompteClubTransaction compteClubTransaction = ViewModelToEntity.FillEntity<CompteClubFormulaireViewModel, CompteClubTransaction>(compteClubFormulaireViewModel);
            compteClubTransaction.Date = System.DateTime.Now;
            compteClubTransaction.PersonneId = compteClubFormulaireViewModel.PersonneViewModel.Id;

            if (compteClubFormulaireViewModel.Id == 0)
            {
                await CompteClubTransactionDal.AddAsync(compteClubTransaction);
            }
            else
            {
                await CompteClubTransactionDal.UpdateAsync(compteClubTransaction);
            }
        }

        public async Task DeleteAsync(long id)
        {
            await TransactionDal<Billet>.DeleteAsync(id);
        }

        public async Task<bool> CanBeDeleted(long id)
        {
            return await TransactionDal<Billet>.CanBeDeleted(id);
        }
    }
}
