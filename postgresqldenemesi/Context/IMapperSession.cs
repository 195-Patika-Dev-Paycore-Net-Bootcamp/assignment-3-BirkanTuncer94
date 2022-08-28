using HW3_BirkanTuncer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace HW3_BirkanTuncer.Context
{
    public interface IMapperSession
    {
        void BeginTransaction();
        void Commit();
        void Rollback();
        void CloseTransaction();
        void Save(Vehicle entity);
        void Update(Vehicle entity);
        void Delete(Vehicle entity);

        IQueryable<Vehicle> Vehicles { get; }

    }
}
