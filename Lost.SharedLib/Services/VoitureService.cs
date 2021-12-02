using Lost.DataAccess.Entities;
using Lost.Model;
using Lost.SharedLib.Utils.Assembly;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lost.SharedLib
{
    public class VoitureService : IVoitureService
    {
        public async Task<VoitureViewModel[]> GetAllAsync()
        {
            IList<Voiture> voitureList = await TransactionDal<Voiture>.GetListWithDetailsAsync();
            VoitureViewModel[] result = new VoitureViewModel[voitureList.Count];

            int i = 0;
            foreach (var voiture in voitureList.OrderBy(e => e.Id))
            {
                VoitureViewModel transactionViewModel = EntityToViewModel.FillViewModel<Voiture, VoitureViewModel>(voiture);
                if (voiture.TauxBlanchiment.Personne != null)
                {
                    transactionViewModel.PersonneViewModel = EntityToViewModel.FillViewModel<Personne, PersonneViewModel>(voiture.TauxBlanchiment.Personne);
                    transactionViewModel.PersonneViewModel.TauxBlanchimentViewModel = EntityToViewModel.FillViewModel<TauxBlanchiment, TauxBlanchimentViewModel>(voiture.TauxBlanchiment);
                }
                if (voiture.TauxBlanchiment.Groupe != null)
                {
                    transactionViewModel.GroupeViewModel = EntityToViewModel.FillViewModel<Groupe, GroupeViewModel>(voiture.TauxBlanchiment.Groupe);
                    transactionViewModel.GroupeViewModel.TauxBlanchimentViewModel = EntityToViewModel.FillViewModel<TauxBlanchiment, TauxBlanchimentViewModel>(voiture.TauxBlanchiment);
                }
                if (voiture.Semaine != null)
                {
                    transactionViewModel.SemaineViewModel = EntityToViewModel.FillViewModel<Semaine, SemaineViewModel>(voiture.Semaine);
                }

                result[i] = transactionViewModel;
                i++;
            }
            return await Task.FromResult(result);
        }

        public async Task<VoitureViewModel> GetAsync(long id)
        {
            Voiture voiture = await TransactionDal<Voiture>.GetWithDetailsAsync(id);

            VoitureViewModel voitureViewModel = EntityToViewModel.FillViewModel<Voiture, VoitureViewModel>(voiture);
            TauxBlanchimentViewModel tauxBlanchimentViewModel = EntityToViewModel.FillViewModel<TauxBlanchiment, TauxBlanchimentViewModel>(voiture.TauxBlanchiment);
            if (voiture.TauxBlanchiment.Groupe != null)
            {
                voitureViewModel.GroupeViewModel = EntityToViewModel.FillViewModel<Groupe, GroupeViewModel>(voiture.TauxBlanchiment.Groupe);
                voitureViewModel.GroupeViewModel.TauxBlanchimentViewModel = tauxBlanchimentViewModel;
            }
            if (voiture.TauxBlanchiment.Personne != null)
            {
                voitureViewModel.PersonneViewModel = EntityToViewModel.FillViewModel<Personne, PersonneViewModel>(voiture.TauxBlanchiment.Personne);
                voitureViewModel.PersonneViewModel.TauxBlanchimentViewModel = tauxBlanchimentViewModel;
            }

            return voitureViewModel;
        }

        public async Task AddOrUpdateAsync(VoitureViewModel voitureViewModel)
        {
            Voiture voiture = ViewModelToEntity.FillEntity<VoitureViewModel, Voiture>(voitureViewModel);

            if (!voitureViewModel.IsPetiteMain)
            {
                GroupeViewModel groupeViewModel = voitureViewModel.GroupeViewModel;
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
                voiture.TauxBlanchimentId = lastTauxBlanchiment.Id;
            }
            else
            {
                PersonneViewModel personneViewModel = voitureViewModel.PersonneViewModel;
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
                voiture.TauxBlanchimentId = lastTauxBlanchiment.Id;
            }

            if (voitureViewModel.Id == 0)
            {
                Semaine semaine = await SemaineDal.GetLastAsync();
                voiture.SemaineId = semaine.Id;
                await TransactionDal<Voiture>.AddAsync(voiture);
            }
            else
            {
                Voiture oldVoiture = await TransactionDal<Voiture>.GetAsync(voiture.Id);
                voiture.SemaineId = oldVoiture.SemaineId;
                await TransactionDal<Voiture>.UpdateAsync(voiture);
            }
        }

        public async Task DeleteAsync(long id)
        {
            await TransactionDal<Voiture>.DeleteAsync(id);
        }

        public async Task<bool> CanBeDeleted(long id)
        {
            return await TransactionDal<Voiture>.CanBeDeleted(id);
        }
    }
}
