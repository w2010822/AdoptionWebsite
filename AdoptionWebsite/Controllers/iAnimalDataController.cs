using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AdoptionWebsite.Models;
using AdoptionWebsite.Models.Repo;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AdoptionWebsite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class iAnimalDataController : ControllerBase
    {
        public Animal_AdoptionContext _db = new Animal_AdoptionContext();
        private IAnimalRepository MyDB = new AnimalRepository();
        // GET: api/<iAnimalDataController>
        [HttpGet]
        public IActionResult Get()
        {
            IQueryable<Animal_FilesViewModel> item = from a in _db.Animal
                                                     join b in _db.AnimalCate on a.CateId equals b.Idno
                                                     select new Animal_FilesViewModel { Animals = a, _AnimalCate = b };

            List<Animal_FilesViewModel> tt = item.ToList();
            foreach (Animal_FilesViewModel i in tt)
            {
                i._Files = _db.Files.Where(w => w.TableId == i.Animals.Idno && w.XTable == "Animal").ToList();
            }

            return Ok(tt);
        }

        // GET api/<iAnimalDataController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Animal_FilesViewModel item = MyDB.GetAnimalByID(id);
            return Ok(item);
        }

        // POST api/<iAnimalDataController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<iAnimalDataController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<iAnimalDataController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
