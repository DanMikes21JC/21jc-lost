using Lost.DataAccess.Context;
using Lost.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lost.DataAccess.Entities
{
    public class VoiturePersonneDal : LostContextDal<VoiturePersonne>
    {
        public static async Task<List<VoiturePersonne>> GetListWithPersonneAsync()
        {
            using (LostDbContext dbContext = CommonDal.CreateDbContext())
            {
                return await dbContext.VoiturePersonne.AsNoTracking().Include(u => u.Demandeur).ToListAsync();
            }
        }
    }
}
