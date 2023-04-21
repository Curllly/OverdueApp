using Microsoft.EntityFrameworkCore;
using OverdueAPI.Database;
using OverdueAPI.Models;

namespace OverdueAPI.Repositories
{
    public class BaseRepository<TDbModel> : IBaseRepository<TDbModel> where TDbModel : Product
    {
        private ApplicationContext Context { get; set; }
        public BaseRepository(ApplicationContext context)
        {
            Context = context;
        }

        public TDbModel Create(TDbModel model)
        {
            Context.Set<TDbModel>().Add(model);
            Context.SaveChanges();
            return model;
        }

        public List<TDbModel> GetAll()
        {
            return Context.Set<TDbModel>().ToList();
        }

        public TDbModel Update(TDbModel model)
        {
            var toUpdate = Context.Set<TDbModel>().Local.FirstOrDefault(m => m.Id.Equals(model.Id));
            if (toUpdate != null)
            {
                Context.Entry(toUpdate).State = EntityState.Detached;
                toUpdate = model;
            }
            Context.Entry(toUpdate).State = EntityState.Modified;
            Context.Update<TDbModel>(toUpdate);
            Context.SaveChanges();
            return toUpdate;
        }
        public void Delete(int id) 
        {
            var toDelete = Context.Set<TDbModel>().FirstOrDefault(m => m.Id == id);
            Context.Set<TDbModel>().Remove(toDelete);
            Context.SaveChanges();
        }

        public TDbModel Get(int id)
        {
            return Context.Set<TDbModel>().FirstOrDefault(m => m.Id == id);
        }
    }
}
