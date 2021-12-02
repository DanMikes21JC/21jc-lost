using Lost.DataAccess.Context;
using Lost.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lost.DataAccess.Entities
{
    public class StatistiquePersonneDal
    {
        public static IList<StatistiquePersonne> GetList()
        {
            using (LostDbContext dbContext = CommonDal.CreateDbContext())
            {
                return dbContext.StatistiquePersonne.AsNoTracking().ToList();
            }
        }

        public static async Task<IList<StatistiquePersonne>> GetListAsync()
        {
            using (LostDbContext dbContext = CommonDal.CreateDbContext())
            {
                return await dbContext.StatistiquePersonne.AsNoTracking().ToListAsync();
            }
        }
    }
}
