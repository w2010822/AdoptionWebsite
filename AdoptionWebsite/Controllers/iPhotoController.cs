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
    public class iPhotoController : ControllerBase
    {
        public Animal_AdoptionContext _db = new Animal_AdoptionContext();

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Files item = _db.Files.Find(id);
            item.isDel = 1;
            int c = _db.SaveChanges();
            return Ok(c);
        }
    }
}
