using Lost.DataAccess.Context;
using Lost.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lost.DataAccess.Entities
{
    public class CompteClubTransactionDal : LostContextDal<CompteClubTransaction>
    {
        public static async Task<List<CompteClubTransaction>> GetListWithPersonneAsync()
        {
            using (LostDbContext dbContext = CommonDal.CreateDbContext())
            {
                return await dbContext.CompteClubTransaction.AsNoTracking().Include(u => u.Personne).ToListAsync();
            }
        }

        public static async Task<CompteClubTransaction> GetWithDetailsAsync(long id)
        {
            using (LostDbContext dbContext = CommonDal.CreateDbContext())
            {
                return await dbContext.CompteClubTransaction.AsNoTracking().Include(u => u.Personne).FirstOrDefaultAsync(t => t.PersonneId == id);
            }
        }
    }
}
