using Microsoft.EntityFrameworkCore;
using Regular.Console.Domain;

namespace Regular.Console.Infrastructure.Repositories
{
    internal class ConfigurationRepository : IRepository<Configuration>
    {
        private ApplicationContext context;

        public ConfigurationRepository()
        {
            context = new ApplicationContext();
        }

        public IEnumerable<Configuration> GetAll()
        {
            return context.Configurations;
        }

        public Configuration Get(int id)
        {
            return context.Configurations.Find(id);
        }

        public void Create(Configuration item)
        {
            context.Configurations.Add(item);
        }

        public void Update(Configuration item)
        {
            context.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Configuration item = context.Configurations.Find(id);
            if (item != null)
                context.Configurations.Remove(item);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
