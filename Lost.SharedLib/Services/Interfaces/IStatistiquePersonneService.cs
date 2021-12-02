using System.Threading.Tasks;

namespace Lost.SharedLib
{
    public interface IStatistiquePersonneService
    {
        public Task<StatistiquePersonneViewModel[]> GetAllAsync();
    }
}
