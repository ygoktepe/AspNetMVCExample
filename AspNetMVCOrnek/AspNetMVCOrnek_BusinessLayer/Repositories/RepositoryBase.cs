using AspNetMVCOrnek_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetMVCOrnek_BusinessLayer.Repositories
{
    public abstract class RepositoryBase<T,Id> where T: class,new()        //new yazdığımız için abstrack klasslar buradan kalıtım alamıyor

    {


        protected static MyContext dbContext;


        public List<T> GetAll()
        {
            try
            {
                dbContext = dbContext ?? new MyContext();
                return dbContext.Set<T>().ToList();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public T GetById(Id id)
        {
            try
            {
               // dbContext = dbContext == null ? new MyContext() : dbContext;
                dbContext = dbContext ?? new MyContext();
                return dbContext.Set<T>().Find(id);

            }
            catch (Exception)
            {
                throw;
            }
        }

        public int Insert(T entity)
        {
            try
            {
                dbContext = dbContext ?? new MyContext();
                dbContext.Set<T>().Add(entity);
                return dbContext.SaveChanges();
            }
            catch(Exception)
            {
                throw;
            }
        }

        public int Update()
        {
            try
            {
                dbContext = dbContext ?? new MyContext();
                return dbContext.SaveChanges();

                
            }
            catch (Exception)
            {

                throw;
            }
        }
        
        public int Delete(T entity)
        {
            try
            {
                dbContext = dbContext ?? new MyContext();
                dbContext.Set<T>().Remove(entity);
                return dbContext.SaveChanges();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public IQueryable<T> Queryable()
        {
            try
            {
                dbContext = dbContext ?? new MyContext();
                return dbContext.Set<T>().AsQueryable();


            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<int> InsertAsync(T entity)
        {
            try
            {
                dbContext = dbContext ?? new MyContext();
                dbContext.Set<T>().Add(entity);
                return await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<int> DeleteAsync(T entity)
        {
            try
            {
                dbContext = dbContext ?? new MyContext();
                dbContext.Set<T>().Remove(entity);
                return await dbContext.SaveChangesAsync();

            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
