using Lost.DataAccess.Entities;
using Lost.Model;
using Lost.SharedLib.Utils.Assembly;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lost.SharedLib
{
    public class SacService : ISacService
    {
        public async Task<SacViewModel[]> GetAllAsync()
        {
            IList<Sac> sacList = await TransactionDal<Sac>.GetListWithDetailsAsync();
            SacViewModel[] result = new SacViewModel[sacList.Count];

            int i = 0;
            foreach (var sac in sacList.OrderBy(e => e.Id))
            {
                SacViewModel transactionViewModel = EntityToViewModel.FillViewModel<Sac, SacViewModel>(sac);
                if (sac.TauxBlanchiment.Personne != null)
                {
                    transactionViewModel.PersonneViewModel = EntityToViewModel.FillViewModel<Personne, PersonneViewModel>(sac.TauxBlanchiment.Personne);
                    transactionViewModel.PersonneViewModel.TauxBlanchimentViewModel = EntityToViewModel.FillViewModel<TauxBlanchiment, TauxBlanchimentViewModel>(sac.TauxBlanchiment);
                }
                if (sac.TauxBlanchiment.Groupe != null)
                {
                    transactionViewModel.GroupeViewModel = EntityToViewModel.FillViewModel<Groupe, GroupeViewModel>(sac.TauxBlanchiment.Groupe);
                    transactionViewModel.GroupeViewModel.TauxBlanchimentViewModel = EntityToViewModel.FillViewModel<TauxBlanchiment, TauxBlanchimentViewModel>(sac.TauxBlanchiment);
                }
                if (sac.Semaine != null)
                {
                    transactionViewModel.SemaineViewModel = EntityToViewModel.FillViewModel<Semaine, SemaineViewModel>(sac.Semaine);
                }

                result[i] = transactionViewModel;
                i++;
            }
            return await Task.FromResult(result);
        }

        public async Task<SacViewModel> GetAsync(long id)
        {
            Sac sac = await TransactionDal<Sac>.GetWithDetailsAsync(id);

            SacViewModel sacViewModel = EntityToViewModel.FillViewModel<Sac, SacViewModel>(sac);
            TauxBlanchimentViewModel tauxBlanchimentViewModel = EntityToViewModel.FillViewModel<TauxBlanchiment, TauxBlanchimentViewModel>(sac.TauxBlanchiment);
            if (sac.TauxBlanchiment.Groupe != null)
            {
                sacViewModel.GroupeViewModel = EntityToViewModel.FillViewModel<Groupe, GroupeViewModel>(sac.TauxBlanchiment.Groupe);
                sacViewModel.GroupeViewModel.TauxBlanchimentViewModel = tauxBlanchimentViewModel;
            }
            if (sac.TauxBlanchiment.Personne != null)
            {
                sacViewModel.PersonneViewModel = EntityToViewModel.FillViewModel<Personne, PersonneViewModel>(sac.TauxBlanchiment.Personne);
                sacViewModel.PersonneViewModel.TauxBlanchimentViewModel = tauxBlanchimentViewModel;
            }

            return sacViewModel;
        }

        public async Task AddOrUpdateAsync(SacViewModel sacViewModel)
        {
            Sac sac = ViewModelToEntity.FillEntity<SacViewModel, Sac>(sacViewModel);

            if (!sacViewModel.IsPetiteMain)
            {
                GroupeViewModel groupeViewModel = sacViewModel.GroupeViewModel;
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
                sac.TauxBlanchimentId = lastTauxBlanchiment.Id;
            }
            else
            {
                PersonneViewModel personneViewModel = sacViewModel.PersonneViewModel;
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
                sac.TauxBlanchimentId = lastTauxBlanchiment.Id;
            }

            if (sacViewModel.Id == 0)
            {
                Semaine semaine = await SemaineDal.GetLastAsync();
                sac.SemaineId = semaine.Id;
                await TransactionDal<Sac>.AddAsync(sac);
            }
            else
            {
                Sac oldSac = await TransactionDal<Sac>.GetAsync(sac.Id);
                sac.SemaineId = oldSac.SemaineId;
                await TransactionDal<Sac>.UpdateAsync(sac);
            }
        }

        public async Task DeleteAsync(long id)
        {
            await TransactionDal<Sac>.DeleteAsync(id);
        }

        public async Task<bool> CanBeDeleted(long id)
        {
            return await TransactionDal<Sac>.CanBeDeleted(id);
        }
    }
}
