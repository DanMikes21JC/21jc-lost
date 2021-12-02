using Lost.DataAccess.Context;
using Lost.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lost.DataAccess.Entities
{
    public class LostContextDal<T> where T : IdEntity
    {

        public static IList<T> GetList()
        {
            using (LostDbContext dbContext = CommonDal.CreateDbContext())
            {
                DbSet<T> dbSet = dbContext.Set<T>();

                return dbSet.AsNoTracking().ToList();
            }
        }

        public static async Task<IList<T>> GetListAsync()
        {
            using (LostDbContext dbContext = CommonDal.CreateDbContext())
            {
                DbSet<T> dbSet = dbContext.Set<T>();
                return await dbSet.AsNoTracking().ToListAsync();
            }
        }

        public static async Task<T> GetAsync(long id)
        {
            using (LostDbContext dbContext = CommonDal.CreateDbContext())
            {
                DbSet<T> dbSet = dbContext.Set<T>();

                return await dbSet.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
            }
        }

        public static T Get(long? id)
        {
            using (LostDbContext dbContext = CommonDal.CreateDbContext())
            {
                DbSet<T> dbSet = dbContext.Set<T>();

                return dbSet.AsNoTracking().FirstOrDefault(i => i.Id == id);
            }
        }

        public static void Add(T item)
        {
            using (LostDbContext dbContext = CommonDal.CreateDbContext())
            {
                DbSet<T> dbSet = dbContext.Set<T>();
                dbSet.Add(item);

                dbContext.SaveChanges();
            }
        }

        public static void AddRange(List<T> itemList)
        {
            using (LostDbContext dbContext = CommonDal.CreateDbContext())
            {

                DbSet<T> dbSet = dbContext.Set<T>();
                dbSet.AddRange(itemList);

                dbContext.SaveChanges();
            }
        }

        public static async Task AddAsync(T item)
        {
            using (LostDbContext dbContext = CommonDal.CreateDbContext())
            {
                DbSet<T> dbSet = dbContext.Set<T>();
                dbSet.Add(item);

                await dbContext.SaveChangesAsync();
            }
        }

        public static async Task UpdateAsync(T item)
        {
            using (LostDbContext dbContext = CommonDal.CreateDbContext())
            {
                DbSet<T> dbSet = dbContext.Set<T>();
                dbSet.Attach(item);

                dbContext.Entry(item).State = EntityState.Modified;
                await dbContext.SaveChangesAsync();
            }
        }

        public static async Task DeleteAsync(long id)
        {
            using (LostDbContext dbContext = CommonDal.CreateDbContext())
            {
                DbSet<T> dbSet = dbContext.Set<T>();

                T item = dbSet.SingleOrDefault(i => i.Id == id);

                if (item != null)
                {
                    dbSet.Remove(item);

                    await dbContext.SaveChangesAsync();
                }

            }
        }
    }
}
