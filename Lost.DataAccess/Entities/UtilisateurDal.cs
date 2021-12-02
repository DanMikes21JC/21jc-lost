using Lost.DataAccess.Context;
using Lost.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lost.DataAccess.Entities
{
    public class UtilisateurDal : LostContextDal<Utilisateur>
    {
        public static async Task<List<Utilisateur>> GetListWithPersonneAndGroupeAsync()
        {
            using (LostDbContext dbContext = CommonDal.CreateDbContext())
            {
                return await dbContext.Utilisateur.AsNoTracking().Include(u => u.Personne.Groupe).ToListAsync();
            }
        }

        public static async Task<Utilisateur> GetWithPersonneAsync(long id)
        {
            using (LostDbContext dbContext = CommonDal.CreateDbContext())
            {
                return await dbContext.Utilisateur.AsNoTracking().Include(u => u.Personne).FirstOrDefaultAsync(i => i.Id == id);
            }
        }

        public static async Task<Utilisateur> GetUtilisateurByDiscordId(string discordId)
        {
            using (LostDbContext dbContext = CommonDal.CreateDbContext())
            {
                return await dbContext.Utilisateur.AsNoTracking().Include(u => u.Personne).FirstOrDefaultAsync(i => i.DiscordAuth.ToLower() == discordId.ToLower());
            }
        }
    }
}
