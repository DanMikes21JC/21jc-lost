using Lost.DataAccess.Entities;
using Lost.Model;
using Lost.SharedLib.Utils.Assembly;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lost.SharedLib
{
    public class VoiturePersonneService : IVoiturePersonneService
    {
        public async Task<VoiturePersonneViewModel[]> GetAllAsync()
        {
            IList<VoiturePersonne> voitureList = await VoiturePersonneDal.GetListWithPersonneAsync();
            VoiturePersonneViewModel[] result = new VoiturePersonneViewModel[voitureList.Count];

            int i = 0;
            foreach (var voiture in voitureList.OrderBy(e => e.Id))
            {
                VoiturePersonneViewModel voitureViewModel = EntityToViewModel.FillViewModel<VoiturePersonne, VoiturePersonneViewModel>(voiture);
                voitureViewModel.PersonneViewModel = EntityToViewModel.FillViewModel<Personne, PersonneViewModel>(voiture.Demandeur);

                result[i] = voitureViewModel;
                i++;
            }
            return await Task.FromResult(result);
        }

        public async Task AddOrUpdateAsync(VoiturePersonneViewModel voitureViewModel)
        {
            VoiturePersonne voiture = ViewModelToEntity.FillEntity<VoiturePersonneViewModel, VoiturePersonne>(voitureViewModel);
            voiture.DemandeurId = voitureViewModel.PersonneViewModel.Id;
            if (voitureViewModel.Id == 0)
            {
                Semaine semaine = await SemaineDal.GetLastAsync();
                //voiture.SemaineId = semaine.Id;
                await VoiturePersonneDal.AddAsync(voiture);
            }
            else
            {
                VoiturePersonne oldVoiture = await VoiturePersonneDal.GetAsync(voiture.Id);
                //voiture.SemaineId = oldVoiture.SemaineId;
                await VoiturePersonneDal.UpdateAsync(voiture);
            }
        }

        public async Task DeleteAsync(long id)
        {
            await VoiturePersonneDal.DeleteAsync(id);
        }

        public async Task<bool> CanBeDeleted(long id)
        {
            return true;//await VoitureDal.CanBeDeleted(id);
        }
    }
}
