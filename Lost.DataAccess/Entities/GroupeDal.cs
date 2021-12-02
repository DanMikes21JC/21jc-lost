using Lost.DataAccess.Context;
using Lost.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lost.DataAccess.Entities
{
    public class GroupeDal : LostContextDal<Groupe>
    {
        public new static async Task<Groupe> GetAsync(long id)
        {
            using (LostDbContext dbContext = CommonDal.CreateDbContext())
            {
                return await dbContext.Groupe.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
            }
        }

        public static async Task<List<Groupe>> GetListWithTauxAsync()
        {
            using (LostDbContext dbContext = CommonDal.CreateDbContext())
            {
                List<Groupe> groupes = await dbContext.Groupe.AsNoTracking().ToListAsync();
                foreach (Groupe groupe in groupes)
                {
                    groupe.TauxBlanchiment = await TauxBlanchimentDal.GetLastTauxBlanchimentGroupeAsync(groupe.Id);
                }
                return groupes;
            }
        }

        public static async Task<bool> CanBeDeleted(long idGroupe)
        {
            using (LostDbContext dbContext = CommonDal.CreateDbContext())
            {
                return await dbContext.Personne.CountAsync(p => p.GroupeId == idGroupe) == 0;
            }
        }

        public new static async Task DeleteAsync(long id)
        {
            using (LostDbContext dbContext = CommonDal.CreateDbContext())
            {
                List<TauxBlanchiment> tauxBlanchimentList = dbContext.TauxBlanchiment.Where(i => i.GroupeId == id).ToList();
                foreach(TauxBlanchiment tb in tauxBlanchimentList)
                {
                    dbContext.TauxBlanchiment.Remove(tb);
                }

                Groupe item = dbContext.Groupe.SingleOrDefault(i => i.Id == id);

                dbContext.Groupe.Remove(item);

                await dbContext.SaveChangesAsync();
            }
        }
    }
}
