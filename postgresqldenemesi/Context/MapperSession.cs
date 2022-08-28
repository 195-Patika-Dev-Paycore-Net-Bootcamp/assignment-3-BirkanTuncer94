using HW3_BirkanTuncer.Data;
using NHibernate;
using System.Linq;
using System.Threading.Tasks;

namespace HW3_BirkanTuncer.Context
{
    public class MapperSession : IMapperSession
    {
        public readonly ISession session;

        private ITransaction transaction;

        public MapperSession(ISession session)
        {
            this.session = session;
        }

        public IQueryable<Vehicle> Vehicles => session.Query<Vehicle>();

        public void BeginTransaction()
        {
            transaction = session.BeginTransaction();
        }

        public void CloseTransaction()
        {
            if(transaction != null)
            {
                transaction.Dispose();
                transaction = null;
            }
        }

        public void Commit()
        {
            transaction.Commit();
        }

        public void Rollback()
        {
            transaction.Rollback();
        }

        public void Save(Vehicle entity)
        {
            session.Save(entity);
        }

        public void Update(Vehicle entity)
        {
            session.Update(entity);
        }

        public void Delete(Vehicle entity)
        {
            session.Delete(entity);
        }
    }
}
