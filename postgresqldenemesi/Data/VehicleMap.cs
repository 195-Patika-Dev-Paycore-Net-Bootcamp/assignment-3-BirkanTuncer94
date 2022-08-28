using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace HW3_BirkanTuncer.Data
{
    public class VehicleMap : ClassMapping<Vehicle>
    {
        public VehicleMap()
        {
            Id(x => x.Id, x =>
            {
                x.Type(NHibernateUtil.Int32);
                x.Column("Id");
                x.UnsavedValue(0);
                x.Generator(Generators.Increment);
            });
            Property(b => b.VehicleName, x =>
            {
                x.Length(50);
                x.Column("vehiclename");
                x.NotNullable(false);
            });
            Property(x => x.VehiclePlate, x =>
            {
                x.Length(14);
                x.Column("vehicleplate");
                x.NotNullable(false);
            });

            Table("vehicle");
        }
    }
}
