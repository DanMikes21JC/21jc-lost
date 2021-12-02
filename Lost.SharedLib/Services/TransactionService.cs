using Lost.DataAccess.Entities;
using Lost.Model;
using Lost.SharedLib.Utils.Assembly;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lost.SharedLib
{
    /*public class TransactionService : ITransactionService
    {
        public async Task<TransactionViewModel[]> GetAllAsync()
        {
            IList<Transaction> transactionList = await TransactionDal.GetListWithDetailsAsync();
            TransactionViewModel[] result = new TransactionViewModel[transactionList.Count];

            int i = 0;
            foreach (var transaction in transactionList.OrderBy(e => e.Id))
            {
                TransactionViewModel transactionViewModel = EntityToViewModel.FillViewModel<Transaction, TransactionViewModel>(transaction);
                if (transaction.TauxBlanchiment.Personne != null)
                {
                    transactionViewModel.PersonneViewModel = EntityToViewModel.FillViewModel<Personne, PersonneViewModel>(transaction.TauxBlanchiment.Personne);
                    transactionViewModel.PersonneViewModel.TauxBlanchimentViewModel = EntityToViewModel.FillViewModel<TauxBlanchiment, TauxBlanchimentViewModel>(transaction.TauxBlanchiment);
                }
                if (transaction.TauxBlanchiment.Groupe != null)
                {
                    transactionViewModel.GroupeViewModel = EntityToViewModel.FillViewModel<Groupe, GroupeViewModel>(transaction.TauxBlanchiment.Groupe);
                    transactionViewModel.GroupeViewModel.TauxBlanchimentViewModel = EntityToViewModel.FillViewModel<TauxBlanchiment, TauxBlanchimentViewModel>(transaction.TauxBlanchiment);
                }
                if (transaction.Semaine != null)
                {
                    transactionViewModel.SemaineViewModel = EntityToViewModel.FillViewModel<Semaine, SemaineViewModel>(transaction.Semaine);
                }

                result[i] = transactionViewModel;
                i++;
            }
            return await Task.FromResult(result);
        }

        public async Task<TransactionViewModel> GetAsync(long id)
        {
            Transaction transaction = await TransactionDal.GetWithDetailsAsync(id);

            TransactionViewModel transactionViewModel = EntityToViewModel.FillViewModel<Transaction, TransactionViewModel>(transaction);
            TauxBlanchimentViewModel tauxBlanchimentViewModel = EntityToViewModel.FillViewModel<TauxBlanchiment, TauxBlanchimentViewModel>(transaction.TauxBlanchiment);
            if (transaction.TauxBlanchiment.Groupe != null)
            {
                transactionViewModel.GroupeViewModel = EntityToViewModel.FillViewModel<Groupe, GroupeViewModel>(transaction.TauxBlanchiment.Groupe);
                transactionViewModel.GroupeViewModel.TauxBlanchimentViewModel = tauxBlanchimentViewModel;
            }
            if (transaction.TauxBlanchiment.Personne != null)
            {
                transactionViewModel.PersonneViewModel = EntityToViewModel.FillViewModel<Personne, PersonneViewModel>(transaction.TauxBlanchiment.Personne);
                transactionViewModel.PersonneViewModel.TauxBlanchimentViewModel = tauxBlanchimentViewModel;
            }

            return transactionViewModel;
        }

        public async Task AddOrUpdateAsync(TransactionViewModel transactionViewModel)
        {
            Transaction transaction = ViewModelToEntity.FillEntity<TransactionViewModel, Transaction>(transactionViewModel);

            if (!transactionViewModel.IsPetiteMain)
            {
                GroupeViewModel groupeViewModel = transactionViewModel.GroupeViewModel;
                TauxBlanchiment lastTauxBlanchiment = await TauxBlanchimentDal.GetLastTauxBlanchimentGroupeAsync(groupeViewModel.Id);
                if (lastTauxBlanchiment.ValeurBillet != groupeViewModel.TauxBlanchimentViewModel.ValeurBillet ||
                   lastTauxBlanchiment.ValeurSac != groupeViewModel.TauxBlanchimentViewModel.ValeurSac ||
                   lastTauxBlanchiment.ValeurVoiture != groupeViewModel.TauxBlanchimentViewModel.ValeurVoiture)
                {
                    lastTauxBlanchiment = new TauxBlanchiment();
                    lastTauxBlanchiment.ValeurBillet = groupeViewModel.TauxBlanchimentViewModel.ValeurBillet;
                    lastTauxBlanchiment.ValeurSac = groupeViewModel.TauxBlanchimentViewModel.ValeurSac;
                    lastTauxBlanchiment.ValeurVoiture = groupeViewModel.TauxBlanchimentViewModel.ValeurVoiture;
                    lastTauxBlanchiment.GroupeId = groupeViewModel.Id;
                    lastTauxBlanchiment.DateDebut = System.DateTime.Now;
                    await TauxBlanchimentDal.AddAsync(lastTauxBlanchiment);
                }
                transaction.TauxBlanchimentId = lastTauxBlanchiment.Id;
            }
            else
            {
                PersonneViewModel personneViewModel = transactionViewModel.PersonneViewModel;
                TauxBlanchiment lastTauxBlanchiment = await TauxBlanchimentDal.GetLastTauxBlanchimentPersonneAsync(personneViewModel.Id);
                if (lastTauxBlanchiment == null ||
                    (lastTauxBlanchiment.ValeurBillet != personneViewModel.TauxBlanchimentViewModel.ValeurBillet ||
                   lastTauxBlanchiment.ValeurSac != personneViewModel.TauxBlanchimentViewModel.ValeurSac ||
                   lastTauxBlanchiment.ValeurVoiture != personneViewModel.TauxBlanchimentViewModel.ValeurVoiture))
                {
                    lastTauxBlanchiment = new TauxBlanchiment();
                    lastTauxBlanchiment.ValeurBillet = personneViewModel.TauxBlanchimentViewModel.ValeurBillet;
                    lastTauxBlanchiment.ValeurSac = personneViewModel.TauxBlanchimentViewModel.ValeurSac;
                    lastTauxBlanchiment.ValeurVoiture = personneViewModel.TauxBlanchimentViewModel.ValeurVoiture;
                    lastTauxBlanchiment.PersonneId = personneViewModel.Id;
                    lastTauxBlanchiment.DateDebut = System.DateTime.Now;
                    await TauxBlanchimentDal.AddAsync(lastTauxBlanchiment);
                }
                transaction.TauxBlanchimentId = lastTauxBlanchiment.Id;
            }

            if (transactionViewModel.Id == 0)
            {
                Semaine semaine = await SemaineDal.GetLastAsync();
                transaction.SemaineId = semaine.Id;
                await TransactionDal.AddAsync(transaction);
            }
            else
            {
                await TransactionDal.UpdateAsync(transaction);
            }
        }

        public async Task DeleteAsync(long id)
        {
            await TransactionDal.DeleteAsync(id);
        }

        public async Task<bool> CanBeDeleted(long id)
        {
            return await TransactionDal.CanBeDeleted(id);
        }
    }*/
}
