using System.Threading.Tasks;

namespace Lost.SharedLib
{
    public interface IStatistiqueGroupeService
    {
        public Task<StatistiqueGroupeViewModel[]> GetAllAsync();
    }
}
