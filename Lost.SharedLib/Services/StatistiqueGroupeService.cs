using Lost.DataAccess.Entities;
using Lost.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lost.SharedLib
{
    public class StatistiqueGroupeService : IStatistiqueGroupeService
    {
        public async Task<StatistiqueGroupeViewModel[]> GetAllAsync()
        {
            IList<StatistiqueGroupe> statistiqueList = await StatistiqueGroupeDal.GetListAsync();
            StatistiqueGroupeViewModel[] result = new StatistiqueGroupeViewModel[statistiqueList.Count];

            int i = 0;
            foreach (var statistique in statistiqueList)
            {
                StatistiqueGroupeViewModel statistiqueViewModel = new StatistiqueGroupeViewModel();
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
