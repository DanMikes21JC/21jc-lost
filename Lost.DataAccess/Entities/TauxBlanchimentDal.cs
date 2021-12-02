using Lost.DataAccess.Context;
using Lost.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Lost.DataAccess.Entities
{
    public class TauxBlanchimentDal : LostContextDal<TauxBlanchiment>
    {
        public static async Task<bool> CanBeDeleted(long idGroupe)
        {
            using (LostDbContext dbContext = CommonDal.CreateDbContext())
            {
                return true;
            }
        }

        public static async Task<TauxBlanchiment> GetLastTauxBlanchimentGroupeAsync(long idGroupe)
        {
            using (LostDbContext dbContext = CommonDal.CreateDbContext())
            {
                return await dbContext.TauxBlanchiment.Where(tb => tb.GroupeId == idGroupe).OrderByDescending(tb => tb.DateDebut).FirstOrDefaultAsync();
            }
        }

        public static async Task<TauxBlanchiment> GetLastTauxBlanchimentPersonneAsync(long idPersonne)
        {
            using (LostDbContext dbContext = CommonDal.CreateDbContext())
            {
                return await dbContext.TauxBlanchiment.Where(tb => tb.PersonneId == idPersonne).OrderByDescending(tb => tb.DateDebut).FirstOrDefaultAsync();
            }
        }
    }
}
