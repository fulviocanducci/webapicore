using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApiAngularCore.Models;

namespace WebApiAngularCore.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    public class ReferencesController : Controller
    {
        private readonly IReferenceRepository database;        
        public ReferencesController(IReferenceRepository database)
        {
            this.database = database;                        
        }
        
        [HttpGet]
        public IEnumerable<Reference> Get()
        {
            return database.All();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Reference reference = database.Find(id);
            if (reference == null) return null;
            return new ObjectResult(reference);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Reference value)
        {
            database.Add(value);            
            return new ObjectResult(value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Reference value)
        {
            Reference update = database.Find(id);
            if (update != null && value.Id.Equals(update.Id))
            {
                update.Name = value.Name;
                database.Save();
                return new ObjectResult(value);
            }
            return null;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Reference delete = database.Find(id);
            if (delete != null && delete.Id.Equals(id))
            {
                database.Remove(delete);
                database.Save();
                return new ObjectResult(new { deleted = true, element = delete });                
            }
            return null;
        }
    }
}
