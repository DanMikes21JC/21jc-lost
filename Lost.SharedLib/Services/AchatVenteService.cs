using Lost.DataAccess.Entities;
using Lost.Model;
using Lost.SharedLib.Utils.Assembly;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lost.SharedLib
{
    public class AchatVenteService : IAchatVenteService
    {
        public async Task<AchatVenteViewModel[]> GetAllAsync()
        {
            IList<AchatVente> achatVenteList = await TransactionDal<AchatVente>.GetListWithDetailsAsync();
            AchatVenteViewModel[] result = new AchatVenteViewModel[achatVenteList.Count];

            int i = 0;
            foreach (var achatVente in achatVenteList.OrderBy(e => e.Id))
            {
                AchatVenteViewModel transactionViewModel = EntityToViewModel.FillViewModel<AchatVente, AchatVenteViewModel>(achatVente);
                if (achatVente.TauxBlanchiment.Personne != null)
                {
                    transactionViewModel.PersonneViewModel = EntityToViewModel.FillViewModel<Personne, PersonneViewModel>(achatVente.TauxBlanchiment.Personne);
                    transactionViewModel.PersonneViewModel.TauxBlanchimentViewModel = EntityToViewModel.FillViewModel<TauxBlanchiment, TauxBlanchimentViewModel>(achatVente.TauxBlanchiment);
                }
                if (achatVente.TauxBlanchiment.Groupe != null)
                {
                    transactionViewModel.GroupeViewModel = EntityToViewModel.FillViewModel<Groupe, GroupeViewModel>(achatVente.TauxBlanchiment.Groupe);
                    transactionViewModel.GroupeViewModel.TauxBlanchimentViewModel = EntityToViewModel.FillViewModel<TauxBlanchiment, TauxBlanchimentViewModel>(achatVente.TauxBlanchiment);
                }
                if (achatVente.Semaine != null)
                {
                    transactionViewModel.SemaineViewModel = EntityToViewModel.FillViewModel<Semaine, SemaineViewModel>(achatVente.Semaine);
                }

                result[i] = transactionViewModel;
                i++;
            }
            return await Task.FromResult(result);
        }

        public async Task<AchatVenteViewModel> GetAsync(long id)
        {
            AchatVente achatVente = await TransactionDal<AchatVente>.GetWithDetailsAsync(id);

            AchatVenteViewModel achatVenteViewModel = EntityToViewModel.FillViewModel<AchatVente, AchatVenteViewModel>(achatVente);
            TauxBlanchimentViewModel tauxBlanchimentViewModel = EntityToViewModel.FillViewModel<TauxBlanchiment, TauxBlanchimentViewModel>(achatVente.TauxBlanchiment);
            if (achatVente.TauxBlanchiment.Groupe != null)
            {
                achatVenteViewModel.GroupeViewModel = EntityToViewModel.FillViewModel<Groupe, GroupeViewModel>(achatVente.TauxBlanchiment.Groupe);
                achatVenteViewModel.GroupeViewModel.TauxBlanchimentViewModel = tauxBlanchimentViewModel;
            }
            if (achatVente.TauxBlanchiment.Personne != null)
            {
                achatVenteViewModel.PersonneViewModel = EntityToViewModel.FillViewModel<Personne, PersonneViewModel>(achatVente.TauxBlanchiment.Personne);
                achatVenteViewModel.PersonneViewModel.TauxBlanchimentViewModel = tauxBlanchimentViewModel;
            }

            return achatVenteViewModel;
        }

        public async Task AddOrUpdateAsync(AchatVenteViewModel achatVenteViewModel)
        {
            AchatVente achatVente = ViewModelToEntity.FillEntity<AchatVenteViewModel, AchatVente>(achatVenteViewModel);

            if (!achatVenteViewModel.IsPetiteMain)
            {
                GroupeViewModel groupeViewModel = achatVenteViewModel.GroupeViewModel;
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
                achatVente.TauxBlanchimentId = lastTauxBlanchiment.Id;
            }
            else
            {
                PersonneViewModel personneViewModel = achatVenteViewModel.PersonneViewModel;
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
                achatVente.TauxBlanchimentId = lastTauxBlanchiment.Id;
            }

            if (achatVenteViewModel.Id == 0)
            {
                Semaine semaine = await SemaineDal.GetLastAsync();
                achatVente.SemaineId = semaine.Id;
                await TransactionDal<AchatVente>.AddAsync(achatVente);
            }
            else
            {
                AchatVente oldAchatVente = await TransactionDal<AchatVente>.GetAsync(achatVente.Id);
                achatVente.SemaineId = oldAchatVente.SemaineId;
                await TransactionDal<AchatVente>.UpdateAsync(achatVente);
            }
        }

        public async Task DeleteAsync(long id)
        {
            await TransactionDal<AchatVente>.DeleteAsync(id);
        }

        public async Task<bool> CanBeDeleted(long id)
        {
            return await TransactionDal<AchatVente>.CanBeDeleted(id);
        }
    }
}
