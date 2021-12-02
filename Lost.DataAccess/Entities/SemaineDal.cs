using Lost.DataAccess.Context;
using Lost.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lost.DataAccess.Entities
{
    public class SemaineDal : LostContextDal<Semaine>
    {
        public static async Task<Semaine> GetLastAsync()
        {
            using (LostDbContext dbContext = CommonDal.CreateDbContext())
            {
                IList<Semaine> semaineList = await GetListAsync();

                int actualWeek = 0;
                Semaine semaineActuelle = semaineList.ToList().OrderByDescending(s => s.Num).FirstOrDefault();
                if (semaineActuelle != null)
                {
                    actualWeek = semaineActuelle.Num;
                }

                if (semaineActuelle == null || semaineActuelle.DateDebut.AddDays(7) < System.DateTime.Now)
                {
                    actualWeek++;
                    Semaine newSemaine = new Semaine();
                    newSemaine.DateDebut = new DateTime(System.DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                    newSemaine.Num = actualWeek;
                    newSemaine.Libelle = "Semaine " + newSemaine.Num;
                    SemaineDal.Add(newSemaine);
                    semaineActuelle = newSemaine;
                }

                return await Task.FromResult(semaineActuelle);
            }
        }
    }
}
