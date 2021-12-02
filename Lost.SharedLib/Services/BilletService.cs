using Lost.DataAccess.Entities;
using Lost.Model;
using Lost.SharedLib.Utils.Assembly;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lost.SharedLib
{
    public class BilletService : IBilletService
    {
        public async Task<BilletViewModel[]> GetAllAsync()
        {
            IList<Billet> billetList = await TransactionDal<Billet>.GetListWithDetailsAsync();
            BilletViewModel[] result = new BilletViewModel[billetList.Count];

            int i = 0;
            foreach (var billet in billetList.OrderBy(e => e.Id))
            {
                BilletViewModel transactionViewModel = EntityToViewModel.FillViewModel<Billet, BilletViewModel>(billet);
                if (billet.TauxBlanchiment.Personne != null)
                {
                    transactionViewModel.PersonneViewModel = EntityToViewModel.FillViewModel<Personne, PersonneViewModel>(billet.TauxBlanchiment.Personne);
                    transactionViewModel.PersonneViewModel.TauxBlanchimentViewModel = EntityToViewModel.FillViewModel<TauxBlanchiment, TauxBlanchimentViewModel>(billet.TauxBlanchiment);
                }
                if (billet.TauxBlanchiment.Groupe != null)
                {
                    transactionViewModel.GroupeViewModel = EntityToViewModel.FillViewModel<Groupe, GroupeViewModel>(billet.TauxBlanchiment.Groupe);
                    transactionViewModel.GroupeViewModel.TauxBlanchimentViewModel = EntityToViewModel.FillViewModel<TauxBlanchiment, TauxBlanchimentViewModel>(billet.TauxBlanchiment);
                }
                if (billet.Semaine != null)
                {
                    transactionViewModel.SemaineViewModel = EntityToViewModel.FillViewModel<Semaine, SemaineViewModel>(billet.Semaine);
                }

                result[i] = transactionViewModel;
                i++;
            }
            return await Task.FromResult(result);
        }

        public async Task<BilletViewModel> GetAsync(long id)
        {
            Billet billet = await TransactionDal<Billet>.GetWithDetailsAsync(id);

            BilletViewModel billetViewModel = EntityToViewModel.FillViewModel<Billet, BilletViewModel>(billet);
            TauxBlanchimentViewModel tauxBlanchimentViewModel = EntityToViewModel.FillViewModel<TauxBlanchiment, TauxBlanchimentViewModel>(billet.TauxBlanchiment);
            if (billet.TauxBlanchiment.Groupe != null)
            {
                billetViewModel.GroupeViewModel = EntityToViewModel.FillViewModel<Groupe, GroupeViewModel>(billet.TauxBlanchiment.Groupe);
                billetViewModel.GroupeViewModel.TauxBlanchimentViewModel = tauxBlanchimentViewModel;
            }
            if (billet.TauxBlanchiment.Personne != null)
            {
                billetViewModel.PersonneViewModel = EntityToViewModel.FillViewModel<Personne, PersonneViewModel>(billet.TauxBlanchiment.Personne);
                billetViewModel.PersonneViewModel.TauxBlanchimentViewModel = tauxBlanchimentViewModel;
            }

            return billetViewModel;
        }

        public async Task AddOrUpdateAsync(BilletViewModel billetViewModel)
        {
            Billet billet = ViewModelToEntity.FillEntity<BilletViewModel, Billet>(billetViewModel);

            if (!billetViewModel.IsPetiteMain)
            {
                GroupeViewModel groupeViewModel = billetViewModel.GroupeViewModel;
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
                billet.TauxBlanchimentId = lastTauxBlanchiment.Id;
            }
            else
            {
                PersonneViewModel personneViewModel = billetViewModel.PersonneViewModel;
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
                billet.TauxBlanchimentId = lastTauxBlanchiment.Id;
            }

            if (billetViewModel.Id == 0)
            {
                Semaine semaine = await SemaineDal.GetLastAsync();
                billet.SemaineId = semaine.Id;
                await TransactionDal<Billet>.AddAsync(billet);
            }
            else
            {
                Billet oldBillet = await TransactionDal<Billet>.GetAsync(billet.Id);
                billet.SemaineId = oldBillet.SemaineId;
                await TransactionDal<Billet>.UpdateAsync(billet);
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
