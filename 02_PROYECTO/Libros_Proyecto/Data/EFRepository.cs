using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    //PATRÓN REPOSITORY
    public class EFRepository : IRepository
    {
        DbContext dbContext;
        public EFRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public TEntity Create<TEntity>(TEntity toCreate) where TEntity : class
        {

            TEntity Result = default(TEntity);
            try
            {
                dbContext.Set<TEntity>().Add(toCreate);
                dbContext.SaveChanges();
                Result = toCreate;

            }
            catch (Exception)
            {
                throw;
            }
            return Result;

        }

        public bool Delete<TEntity>(TEntity toDelete) where TEntity : class
        {

            bool Result = false;
            try
            {
                dbContext.Entry<TEntity>(toDelete).State = EntityState.Deleted;
                Result = dbContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            return Result;
        }

        public void Dispose()
        {
            if (dbContext != null)
            {
                dbContext.Dispose();
            }
        }

        public List<TEntity> Filter<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class
        {
            List<TEntity> Result = null;

            try
            {
                Result = dbContext.Set<TEntity>().Where(criteria).ToList();
            }
            catch (Exception) { throw; }

            return Result;
        }

        public TEntity Retrieve<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class
        {
            TEntity result = null;

            try
            {
                result = dbContext.Set<TEntity>().FirstOrDefault(criteria);

            }
            catch (Exception) { throw; }


            return result;
        }

        public bool Update<TEntity>(TEntity toUpdate) where TEntity : class
        {
            bool Result = false;
            try
            {
                dbContext.Entry<TEntity>(toUpdate).State = EntityState.Modified;
                Result = dbContext.SaveChanges() > 0;

            }

            catch (Exception) { throw; }

            return Result;
        }
    }
}
