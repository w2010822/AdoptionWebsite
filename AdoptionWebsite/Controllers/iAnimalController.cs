using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdoptionWebsite.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdoptionWebsite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class iAnimalController : ControllerBase
    {
        public Animal_AdoptionContext _db = new Animal_AdoptionContext();

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Animal item = _db.Animal.Find(id);
            _db.Animal.Remove(item);

            foreach (var itemfile in _db.Files.Where(w => w.TableId == id && w.XTable == "Animal"))
            {
                _db.Files.Remove(itemfile);
            }

            int c = _db.SaveChanges();

            return Ok(c);
        }
    }
}
