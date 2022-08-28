using HW3_BirkanTuncer.Data;
using Microsoft.AspNetCore.Mvc;
using NHibernate;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;



namespace HW3_BirkanTuncer.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class ContainerController : ControllerBase
    {
       

        private double NumberOfElementsInCluster(double numberOfElements, double numberOfClusters) // Cluster = Küme
        {
            double result = numberOfElements / numberOfClusters;
            double numberAndHalf = 0.5;
            for (int i = 1; i < numberOfElements; i++) // Kümeye gelecek olan container sayısı buçuklu bir sayı gelecek olursa. ( Örneğin 1.5 , 2.5 , 3.5 vs ) Sistem otomatik olarak ,sayıyı çift sayı olan tarafa doğru yuvarlıyor. ( örneğin 0.5 i 0 a, 1.5 i 2 ye ) Bu da istemediğim bir durum. Çünkü bu sayı 0.5 yukarıya doğru yuvarlandığı durumlarda kümse sayısı fazla geliyor. bazı kümelere eleman kalmıyor. Bu yüzden 0.5 i manuel bir şekilde aşağıya çekiyorum. // for döngüsünü numberofelement kadar döndürme sebebim ise kümelere yerleştirilecek sayının hiçbir zaman tüm container sayısından fazla olamayacağı).
            {
                
                if(result == numberAndHalf) // Bulduğumuz sonuc sonu 0.5 olan sayılardan birisi ise 0.5 eksiltiyoruz.
                {
                    result = result - 0.5;
                }
                numberAndHalf = numberAndHalf + i;
            }

            double numberOfElementsInCluster = Math.Round(result);
            


            return numberOfElementsInCluster;
        } /// <summary>
        /// Verilen ID ye göre işleme tabi tutulacak tüm containerların sayısını ve oluşturulacak olan küme sayısını parametre olarak alır, eleman sayısını küme sayısına böler ve ondalık bir sayı elde eder. elde edilen bu ondalık sayıyı yuvarlar ve tam sayı haline getirir. (2.6 yı 3 e, 2.2 yi 2 ye )Daha sonra elde edilen bu sayı, kümelerin içerisindeki eleman sayısı olarak işlem görür.
        /// </summary>       
        
        private readonly ISession session;
        public List<Container> containerList;
        public List<List<Container>> clusterList;
        public ContainerController(ISession session)
        {
            this.session = session;
            this.containerList = new List<Container>();
            this.clusterList = new List<List<Container>>();

        }

        [HttpGet("GetAll")]
        public List<Container> Get()
        {
            var response = session.Query<Container>().ToList(); // Liste olarak tüm containerları çekiyorum.
            return response;
        } // Madde 7

        [HttpGet("GetByIdForCluster")]
        public List<List<Container>> Get(int id ,int numberOfClusters)
        {
            
            var containersWillDistribute = session.Query<Container>().Where(x => x.VehicleId == id).ToList(); // İşleme tabi tutulacak containerların listesi.
            int count = 0;
            int element = 0; // İşleme tabi TUTULMUŞ containerların sayısı
            int clusterRemain = numberOfClusters; // İşleme tabi TUTULACAK küme sayısı



            double numberOfContainers = NumberOfElementsInCluster(containersWillDistribute.Count, numberOfClusters); // Küme içerisinde kaç adet container olacağı belirleniyor
            

            foreach (var container in containersWillDistribute) // İşleme tabi tutulacak containerların hepsini foreach yapısı ile döndürüyorum.
            {
                containerList.Add(container); // Containerları sırası ile yeni bir listeye atıyorum
                element++; // Listeye eklenen her container için işleme tabi tutulmuş containerların sayısını artırıyorum.
                count++; // Elimizde bulunan ama kümelere aktarılmamış container sayısı // ( Aşağıdaki if else bloklarında daha net anlaşılıyor.)

                if (count == numberOfContainers && clusterRemain > 1) // Kümelere yerleştirilmemiş containerların sayısı ile kümelere yerleştirilmesine karar verdiğimiz container sayısı tutarlı hale geldiğinde kümelerden oluşan farklı bir listenin elemanı olarak bu containerları toplu bir şekilde atıyorum.
                {
                    clusterList.Add(containerList.GetRange(element - count, count)); // Oluşturduğum container listesindeki INDEXE göre atamayı gerçekleştiriyorum. element-count = listenin başlangıç noktası, count = başlangıç noktasından sonra kaç adet eleman ilerleyeceği.
                    clusterRemain--; // Oluşturmam gereken küme sayısını azaltıyorum
                    count = 0; // kümeye atılmamış containerların sayısını 0 yapıyorum.
                }else if(clusterRemain == 1 && element == containersWillDistribute.Count) // oluşturmam gereken son kümede geri kalan tüm elemanları bu kümeye atıyorum. mesela 2 2 2 3 deki 3 eleman burada belirleniyor. ( Başka küme oluşturma şansım olmadığı için hepsini atıyorum )
                {
                    clusterList.Add(containerList.GetRange(element - count, count));
                    clusterRemain--;
                    count--;
                }               
            }
            
            return clusterList;
        } // Madde 11

        [HttpPost]
        public Container Post([FromBody] Container request)
        {
            using (var transaction = session.BeginTransaction()) // Transaction başlatıp eklemeleri gerçekleştiriyorum
            {
                var containers = new Container();

                containers.Id = request.Id;
                containers.ContainerName = request.ContainerName;
                containers.Latitude = request.Latitude;
                containers.Longitude = request.Longitude;
                containers.VehicleId = request.VehicleId;

                session.Save(containers);
                transaction.Commit(); // Transaction işlemini kapatıp kalıcı hale getiriyorum.
                return containers; // Eklenen container gösteriliyor.
            }
        } // Madde 7

        [HttpPut]
        public ActionResult<Container> Put([FromBody] Container request) ///// Vehicle ID Güncellenmeyecek şekilde yazıldı.
        {
            using (var transaction = session.BeginTransaction())
            {
                Container container = session.Query<Container>().Where(x => x.Id == request.Id).FirstOrDefault();
                if (container == null)
                {
                    return NotFound();
                }
                container.ContainerName = request.ContainerName;
                container.Latitude = request.Latitude;
                container.Longitude = request.Longitude;

                session.Update(container);
                transaction.Commit();
                return Ok();
            }
        } // Madde 8

        [HttpDelete]
        public ActionResult<Container> Delete(int id)
        {
            using (var transaction = session.BeginTransaction())
            {
                Container container = session.Query<Container>().Where(x => x.Id == id).FirstOrDefault();
                if (container == null)
                {
                    return NotFound();
                }
                try
                {

                    session.Delete(container);  // Başarılı bir şekilde silerse commitle
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    
                    transaction.Rollback(); // Hata olursa Rollback atıyorum
                }
                finally
                {
                    transaction.Dispose(); // Silme işleminde problem çıkarsa begintransaction çalıştırıldığı için dispose yapmak gerekiyor.
                }
                return Ok();
            }
        } // Madde 9






    }
}
