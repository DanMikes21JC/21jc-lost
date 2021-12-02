using Lost.DataAccess.Entities;
using Lost.Model;
using Lost.SharedLib.Utils.Assembly;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lost.SharedLib
{
    public class UtilisateurService : IUtilisateurService
    {
        public async Task<UtilisateurViewModel[]> GetAllAsync()
        {
            IList<Utilisateur> utilisateurList = await UtilisateurDal.GetListWithPersonneAndGroupeAsync();
            UtilisateurViewModel[] result = new UtilisateurViewModel[utilisateurList.Count];

            int i = 0;
            foreach (var utilisateur in utilisateurList.OrderBy(e => e.Id))
            {
                UtilisateurViewModel utilisateurViewModel = EntityToViewModel.FillViewModel<Utilisateur, UtilisateurViewModel>(utilisateur);
                utilisateurViewModel.PersonneViewModel = EntityToViewModel.FillViewModel<Personne, PersonneViewModel>(utilisateur.Personne);
                if(utilisateur.Personne.Groupe != null)
                {
                    utilisateurViewModel.PersonneViewModel.GroupeViewModel = EntityToViewModel.FillViewModel<Groupe, GroupeViewModel>(utilisateur.Personne.Groupe);
                }
                result[i] = utilisateurViewModel;
                i++;
            }
            return await Task.FromResult(result);
        }

        public async Task<UtilisateurViewModel> GetAsync(long id)
        {
            Utilisateur utilisateur = await UtilisateurDal.GetWithPersonneAsync(id);

            UtilisateurViewModel utilisateurViewModel = EntityToViewModel.FillViewModel<Utilisateur, UtilisateurViewModel>(utilisateur);
            utilisateurViewModel.PersonneViewModel = EntityToViewModel.FillViewModel<Personne, PersonneViewModel>(utilisateur.Personne);

            return utilisateurViewModel;
        }

        public async Task AddOrUpdateAsync(UtilisateurViewModel utilisateurViewModel)
        {
            Utilisateur utilisateur = ViewModelToEntity.FillEntity<UtilisateurViewModel, Utilisateur>(utilisateurViewModel);
            Personne personne = ViewModelToEntity.FillEntity<PersonneViewModel, Personne>(utilisateurViewModel.PersonneViewModel);
            utilisateur.PersonneId = personne.Id;

            if (utilisateurViewModel.Id == 0)
            {
                await UtilisateurDal.AddAsync(utilisateur);
            }
            else
            {
                await UtilisateurDal.UpdateAsync(utilisateur);
            }
        }

        public async Task DeleteAsync(long id)
        {
            await UtilisateurDal.DeleteAsync(id);
        }

    }
}
