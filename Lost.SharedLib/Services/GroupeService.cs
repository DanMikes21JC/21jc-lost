using Lost.DataAccess.Entities;
using Lost.Model;
using Lost.SharedLib.Utils.Assembly;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lost.SharedLib
{
    public class GroupeService : IGroupeService
    {
        public async Task<GroupeViewModel[]> GetAllAsync()
        {
            IList<Groupe> groupeList = await GroupeDal.GetListAsync();
            GroupeViewModel[] result = new GroupeViewModel[groupeList.Count];

            int i = 0;
            foreach (var societe in groupeList.OrderBy(e => e.Nom))
            {
                GroupeViewModel groupeViewModel = EntityToViewModel.FillViewModel<Groupe, GroupeViewModel>(societe);

                result[i] = groupeViewModel;
                i++;
            }
            return await Task.FromResult(result);
        }

        public async Task<GroupeViewModel[]> GetAllWithTauxAsync()
        {
            IList<Groupe> groupeList = await GroupeDal.GetListWithTauxAsync();
            GroupeViewModel[] result = new GroupeViewModel[groupeList.Count];

            int i = 0;
            foreach (var groupe in groupeList.OrderBy(e => e.Id))
            {
                GroupeViewModel groupeViewModel = EntityToViewModel.FillViewModel<Groupe, GroupeViewModel>(groupe);

                if (groupe.TauxBlanchiment != null)
                {
                    groupeViewModel.TauxBlanchimentViewModel = EntityToViewModel.FillViewModel<TauxBlanchiment, TauxBlanchimentViewModel>(groupe.TauxBlanchiment);
                }

                result[i] = groupeViewModel;
                i++;
            }
            return await Task.FromResult(result);
        }

        public async Task<GroupeViewModel> GetAsync(long id)
        {
            Groupe groupe = await GroupeDal.GetAsync(id);

            GroupeViewModel groupeViewModel = EntityToViewModel.FillViewModel<Groupe, GroupeViewModel>(groupe);
            TauxBlanchiment tauxBlanchiment = await TauxBlanchimentDal.GetLastTauxBlanchimentGroupeAsync(id);

            if (tauxBlanchiment != null)
            {
                groupeViewModel.TauxBlanchimentViewModel = EntityToViewModel.FillViewModel<TauxBlanchiment, TauxBlanchimentViewModel>(tauxBlanchiment);
            }

            return groupeViewModel;
        }

        public async Task AddOrUpdateAsync(GroupeViewModel groupeViewModel)
        {
            Groupe groupe = ViewModelToEntity.FillEntity<GroupeViewModel, Groupe>(groupeViewModel);

            if (groupeViewModel.Id == 0)
            {
                await GroupeDal.AddAsync(groupe);
            }
            else
            {
                await GroupeDal.UpdateAsync(groupe);
            }

            TauxBlanchiment tauxBlanchiment = ViewModelToEntity.FillEntity<TauxBlanchimentViewModel, TauxBlanchiment>(groupeViewModel.TauxBlanchimentViewModel);
            tauxBlanchiment.DateDebut = System.DateTime.Now;
            tauxBlanchiment.GroupeId = groupe.Id;
            tauxBlanchiment.Id = 0;
            await TauxBlanchimentDal.AddAsync(tauxBlanchiment);
        }

        public async Task DeleteAsync(long id)
        {
            await GroupeDal.DeleteAsync(id);
        }

        public async Task<bool> CanBeDeleted(long id)
        {
            return await GroupeDal.CanBeDeleted(id);
        }
    }
}
