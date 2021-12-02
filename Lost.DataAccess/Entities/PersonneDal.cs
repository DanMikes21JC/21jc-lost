using Lost.DataAccess.Context;
using Lost.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lost.DataAccess.Entities
{
    public class PersonneDal : LostContextDal<Personne>
    {
        public static bool IsTelUsed(long id, string tel)
        {
            using (LostDbContext dbContext = CommonDal.CreateDbContext())
            {
                return (dbContext.Personne.AsNoTracking().FirstOrDefault(i => i.Id != id && i.Tel == tel)) != null;
            }
        }

        public new static async Task<Personne> GetAsync(long id)
        {
            using (LostDbContext dbContext = CommonDal.CreateDbContext())
            {
                return await dbContext.Personne.Include(p => p.Groupe).AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
            }
        }

        public static async Task<bool> CanBeDeleted(long idGroupe)
        {
            using (LostDbContext dbContext = CommonDal.CreateDbContext())
            {
                return true;
            }
        }

        public static async Task<List<Personne>> GetListWithGroupeAsync()
        {
            using (LostDbContext dbContext = CommonDal.CreateDbContext())
            {
                List<Personne> personnes = await dbContext.Personne.Include(p => p.Groupe).AsNoTracking().ToListAsync();
                return personnes;
            }
        }

        public static async Task<List<Personne>> GetListWithTauxAsync()
        {
            using (LostDbContext dbContext = CommonDal.CreateDbContext())
            {
                List<Personne> personnes = await dbContext.Personne.AsNoTracking().ToListAsync();
                foreach(Personne personne in personnes)
                {
                    personne.TauxBlanchiment = await TauxBlanchimentDal.GetLastTauxBlanchimentPersonneAsync(personne.Id);
                }
                return personnes;
            }
        }

        public static async Task<List<Personne>> GetListFromGroupeAsync(long idGroupe)
        {
            using (LostDbContext dbContext = CommonDal.CreateDbContext())
            {
                List<Personne> personnes = await dbContext.Personne.AsNoTracking().Where(p => p.GroupeId == idGroupe).ToListAsync();
                return personnes;
            }
        }
    }
}
