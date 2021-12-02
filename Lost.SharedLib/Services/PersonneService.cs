using Lost.DataAccess.Entities;
using Lost.Model;
using Lost.SharedLib.Utils.Assembly;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lost.SharedLib
{
    public class PersonneService : IPersonneService
    {
        public async Task<PersonneViewModel[]> GetAllAsync()
        {
            IList<Personne> personneList = await PersonneDal.GetListWithGroupeAsync();
            PersonneViewModel[] result = new PersonneViewModel[personneList.Count];

            int i = 0;
            foreach (var personne in personneList.OrderBy(e => e.Id))
            {
                PersonneViewModel personneViewModel = EntityToViewModel.FillViewModel<Personne, PersonneViewModel>(personne);

                if (personne.Groupe != null)
                {
                    personneViewModel.GroupeViewModel = EntityToViewModel.FillViewModel<Groupe, GroupeViewModel>(personne.Groupe);
                }

                result[i] = personneViewModel;
                i++;
            }
            return await Task.FromResult(result);
        }

        public async Task<PersonneViewModel[]> GetAllWithTauxAsync()
        {
            IList<Personne> personneList = await PersonneDal.GetListWithTauxAsync();
            PersonneViewModel[] result = new PersonneViewModel[personneList.Count];

            int i = 0;
            foreach (var personne in personneList.OrderBy(e => e.Id))
            {
                PersonneViewModel personneViewModel = EntityToViewModel.FillViewModel<Personne, PersonneViewModel>(personne);

                if(personne.TauxBlanchiment != null)
                {
                    personneViewModel.TauxBlanchimentViewModel = EntityToViewModel.FillViewModel<TauxBlanchiment, TauxBlanchimentViewModel>(personne.TauxBlanchiment);
                }

                result[i] = personneViewModel;
                i++;
            }
            return await Task.FromResult(result);
        }

        public async Task<PersonneViewModel[]> GetPersonneFromGroupe(long idGroupe)
        {
            IList<Personne> personneList = await PersonneDal.GetListFromGroupeAsync(idGroupe);
            PersonneViewModel[] result = new PersonneViewModel[personneList.Count];

            int i = 0;
            foreach (var personne in personneList.OrderBy(e => e.Id))
            {
                PersonneViewModel personneViewModel = EntityToViewModel.FillViewModel<Personne, PersonneViewModel>(personne);

                result[i] = personneViewModel;
                i++;
            }
            return await Task.FromResult(result);
        }

        public async Task<PersonneViewModel> GetAsync(long id)
        {
            Personne personne = await PersonneDal.GetAsync(id);
            TauxBlanchiment tauxBlanchiment = await TauxBlanchimentDal.GetLastTauxBlanchimentPersonneAsync(id);

            PersonneViewModel personneViewModel = EntityToViewModel.FillViewModel<Personne, PersonneViewModel>(personne);

            if (tauxBlanchiment != null)
            {
                personneViewModel.TauxBlanchimentViewModel = EntityToViewModel.FillViewModel<TauxBlanchiment, TauxBlanchimentViewModel>(tauxBlanchiment);
            }

            if (personne.Groupe != null)
            {
                personneViewModel.GroupeViewModel = EntityToViewModel.FillViewModel<Groupe, GroupeViewModel>(personne.Groupe);
            }

            return personneViewModel;
        }

        public async Task AddOrUpdateAsync(PersonneViewModel personneViewModel)
        {
            Personne personne = ViewModelToEntity.FillEntity<PersonneViewModel, Personne>(personneViewModel);

            if (!personne.IsPetiteMain && personneViewModel.GroupeViewModel?.Id != 0)
            {
                personne.GroupeId = personneViewModel.GroupeViewModel?.Id;
            }

            personne.Groupe = null;
            personne.TauxBlanchiment = null;

            if (personneViewModel.Id == 0)
            {
                await PersonneDal.AddAsync(personne);
            }
            else
            {
                await PersonneDal.UpdateAsync(personne);
            }

            if (personne.IsPetiteMain)
            {
                TauxBlanchiment tauxBlanchiment = ViewModelToEntity.FillEntity<TauxBlanchimentViewModel, TauxBlanchiment>(personneViewModel.TauxBlanchimentViewModel);
                tauxBlanchiment.DateDebut = System.DateTime.Now;
                tauxBlanchiment.PersonneId = personne.Id;
                tauxBlanchiment.Id = 0;
                await TauxBlanchimentDal.AddAsync(tauxBlanchiment);
            }
        }

        public async Task DeleteAsync(long id)
        {
            await PersonneDal.DeleteAsync(id);
        }

        public async Task<bool> CanBeDeleted(long id)
        {
            return await PersonneDal.CanBeDeleted(id);
        }
    }
}
