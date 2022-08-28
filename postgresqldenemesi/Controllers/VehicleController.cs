using HW3_BirkanTuncer.Data;
using HW3_BirkanTuncer.Controllers;
using Microsoft.AspNetCore.Mvc;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;



namespace HW3_BirkanTuncer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly ISession session;
        public VehicleController(ISession session)
        {
            this.session = session;
        }
        
        [HttpGet("GetAll")]
        public List<Vehicle> Get() // Tüm listeyi gösteriyorum.
        {
            var response = session.Query<Vehicle>().ToList(); 
            return response;
        } // Madde 3
        [HttpPost]
        public Vehicle Post([FromBody] Vehicle request) // Standart bir post işlemi.
        {
            using (var transaction = session.BeginTransaction())
            {
                var vehicles = new Vehicle();
                vehicles.Id = request.Id;
                vehicles.VehicleName = request.VehicleName;
                vehicles.VehiclePlate = request.VehiclePlate;

                session.Save(vehicles);
                transaction.Commit();
                return vehicles;
            }
        } // Madde 3
        [HttpGet("FindContainersByVehicleId")]
        public List<Container> GetById(int id) //Vehicle ID ye göre Containerları listeliyorum. Bu yüzden dönüş tipi Container listesi.
        {
            var response = session.Query<Container>().Where(x => x.VehicleId == id).ToList(); 
            return response;
        } // Madde 10
        [HttpPut]
        public ActionResult<Vehicle> Put([FromBody] Vehicle request) // Standart bir update işlemi. Verilen bilgiye göre Araç adı ve plakası güncelleniyor.
        {
            using ( var transaction = session.BeginTransaction())
            {
                Vehicle vehicle = session.Query<Vehicle>().Where(x => x.Id == request.Id).FirstOrDefault();
                if (vehicle == null)
                {
                    return NotFound();
                }
                vehicle.VehicleName = request.VehicleName;
                vehicle.VehiclePlate = request.VehiclePlate;               
                session.Update(vehicle);
                transaction.Commit();
                return Ok();
            }

            

        } // Madde 4
        [HttpDelete]
        public ActionResult<Vehicle> Delete(int id) // Aracın, kendisine bağlı olan containerlar ile beraber silinmesi
        {
            using ( var transaction = session.BeginTransaction())
            {
                Vehicle vehicle = session.Query<Vehicle>().Where(x => x.Id == id).FirstOrDefault();
                var containersGonnaDelete = session.Query<Container>().Where(x => x.VehicleId == id).ToList();
                if (vehicle == null)
                {
                    return NotFound();
                }
                try
                {
                    foreach (var item in containersGonnaDelete) // Araç ile beraber bu araca bağlı tüm containerları foreach yardımı ile siliyorum.
                    {
                        session.Delete(item);
                    }


                    session.Delete(vehicle); // Kendisine bağlı containerlar silindikten sonra aracı da siliyorum.
                    transaction.Commit();
                }
                catch (Exception ex)
                {

                    transaction.Rollback();          // Silme işlemi sırasında bir problem çıkarsa Rollback yapıyorum.          
                }
                finally 
                {
                    transaction.Dispose();                // Problem çıkması durumunda commit olmayacağı için dispose işlemi yapıyorum.    
                }               
                return Ok();
            }
        } // Madde 5

    }
}
