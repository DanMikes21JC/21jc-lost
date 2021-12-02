using Lost.DataAccess.Context;
using Lost.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lost.DataAccess.Entities
{
    public class StatistiqueGroupeDal
    {
        public static IList<StatistiqueGroupe> GetList()
        {
            using (LostDbContext dbContext = CommonDal.CreateDbContext())
            {
                return dbContext.StatistiqueGroupe.AsNoTracking().ToList();
            }
        }

        public static async Task<IList<StatistiqueGroupe>> GetListAsync()
        {
            using (LostDbContext dbContext = CommonDal.CreateDbContext())
            {
                return await dbContext.StatistiqueGroupe.AsNoTracking().ToListAsync();
            }
        }
    }
}
