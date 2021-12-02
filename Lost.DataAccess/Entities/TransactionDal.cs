using Lost.DataAccess.Context;
using Lost.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lost.DataAccess.Entities
{
    public class TransactionDal<T> : LostContextDal<T> where T : Transaction
    {
        public static async Task<List<T>> GetListWithDetailsAsync()
        {
            using (LostDbContext dbContext = CommonDal.CreateDbContext())
            {
                DbSet<T> dbSet = dbContext.Set<T>();

                return await dbSet
                    .Include(t => t.Semaine)
                    .Include(t => t.TauxBlanchiment.Groupe)
                    .Include(t => t.TauxBlanchiment.Personne)
                    .AsNoTracking().ToListAsync();
            }
        }

        public static async Task<T> GetWithDetailsAsync(long id)
        {
            using (LostDbContext dbContext = CommonDal.CreateDbContext())
            {
                DbSet<T> dbSet = dbContext.Set<T>();

                return await dbSet
                    .Include(t => t.TauxBlanchiment.Groupe)
                    .Include(t => t.TauxBlanchiment.Personne)
                    .AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
            }
        }

        public static async Task<bool> CanBeDeleted(long idTransaction)
        {
            using (LostDbContext dbContext = CommonDal.CreateDbContext())
            {
                return true;
            }
        }
    }
}
