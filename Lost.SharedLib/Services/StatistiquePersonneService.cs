using Lost.DataAccess.Entities;
using Lost.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lost.SharedLib
{
    public class StatistiquePersonneService : IStatistiquePersonneService
    {
        public async Task<StatistiquePersonneViewModel[]> GetAllAsync()
        {
            IList<StatistiquePersonne> statistiqueList = await StatistiquePersonneDal.GetListAsync();
            StatistiquePersonneViewModel[] result = new StatistiquePersonneViewModel[statistiqueList.Count];

            int i = 0;
            foreach (var statistique in statistiqueList)
            {
                StatistiquePersonneViewModel statistiqueViewModel = new StatistiquePersonneViewModel();
                statistiqueViewModel.Benefice = statistique.Benefice;
                statistiqueViewModel.Billet = statistique.Billet;
                statistiqueViewModel.Nom = statistique.Nom;
                statistiqueViewModel.Numero = statistique.Numero;
                statistiqueViewModel.Sac = statistique.Sac;
                statistiqueViewModel.Voiture = statistique.Voiture;
                result[i] = statistiqueViewModel;
                i++;
            }
            return await Task.FromResult(result);
        }
    }
}
