using Microsoft.Ajax.Utilities;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http; 
using WEB_API.Data;
using WEB_API.Models;

namespace WEB_API.Controllers
{
    public class CarInventoryApiController : ApiController
    {
        CarInventoryDbContext db = new CarInventoryDbContext();

        //Get All Records   
        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            return Ok(db.cars.ToList()); 
        }
        [HttpGet]
        public Cars Get(int id)
        {
            return db.cars.FirstOrDefault(x=>x.Id ==id); 
        }
        [HttpPost]
        public string Post(Cars cars)
        {

            db.cars.Add(cars);
            db.SaveChanges();
            return "Car Added Successfully";
        }
        [HttpPut]
        public string Put(int id, Cars cars)
        {
            var car = db.cars.Find(id);
            car.Brand = cars.Brand;
            car.Model = cars.Model;
            car.Year = cars.Year;
            car.Price = cars.Price;
            car.New = cars.New;
            db.Entry(car).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return "Cars Details Updated Successfully";

        }
        [HttpDelete]

        public string Delete(int id)
        {
            Cars car = db.cars.Find(id);
            db.cars.Remove(car);
            db.SaveChanges();
            return "Delete Successfully";
        }

        //public  string  searchbymodelandbrand(string model, string brand)
        //{
        //    var results = db.cars.where(i => i.model == model && i.brand == brand).tolist();
        //    return ok(results);
        //}
        [Route("Signup")]
        [HttpPost]
        public  string Signup(Signup signup)
        {
          
                Signup signup1 = new Signup();
                if(signup1.Id == 0)
                {
                    signup1.FirstName = signup.FirstName;
                    signup1.LastName = signup.LastName;
                    signup1.Email = signup.Email;
                    signup1.Password = signup.Password;
                    db.signups.Add(signup1);
                    db.SaveChanges();
                    return "Record SuccessFully Saved";
                }
             return  "Record Saved";
        }
    }
}
