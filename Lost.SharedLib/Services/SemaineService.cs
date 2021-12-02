using Lost.DataAccess.Entities;
using Lost.Model;
using Lost.SharedLib.Utils.Assembly;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lost.SharedLib
{
    public class SemaineService : ISemaineService
    {
        public async Task<SemaineViewModel[]> GetAllAsync()
        {
            IList<Semaine> semaineList = await SemaineDal.GetListAsync();
            SemaineViewModel[] result = new SemaineViewModel[semaineList.Count];

            int i = 0;
            foreach (var semaine in semaineList.OrderBy(e => e.Id))
            {
                SemaineViewModel semaineViewModel = EntityToViewModel.FillViewModel<Semaine, SemaineViewModel>(semaine);

                result[i] = semaineViewModel;
                i++;
            }
            return await Task.FromResult(result);
        }

        public async Task<SemaineViewModel> GetLastAsync()
        {
            Semaine semaine = await SemaineDal.GetLastAsync();
            SemaineViewModel semaineViewModel = EntityToViewModel.FillViewModel<Semaine, SemaineViewModel>(semaine);
            return semaineViewModel;
        }
    }
}
