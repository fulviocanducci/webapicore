using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApiAngularCore.Models;
using System.Net.Http;
using System.Web.Http;
using System.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;
using System.Net.Http.Formatting;
using Microsoft.AspNetCore.Mvc.WebApiCompatShim;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;

namespace WebApiAngularCore.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    public class ReferencesController : Controller
    {
        private readonly Database database;        
        public ReferencesController(Database database)
        {
            this.database = database;                        
        }
        
        [HttpGet]
        public IEnumerable<Reference> Get()
        {
            return database.References.AsEnumerable();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Reference reference = await database.References.FindAsync(id);
            if (reference == null) return null;
            return new ObjectResult(reference);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Reference value)
        {
            database.References.Add(value);
            await database.SaveChangesAsync();
            return new ObjectResult(value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Reference value)
        {
            Reference update = database.References.FirstOrDefault(x => x.Id == id);
            if (update != null && value.Id.Equals(update.Id))
            {
                update.Name = value.Name;
                await database.SaveChangesAsync();
                return new ObjectResult(value);
            }
            return null;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Reference delete = database.References.FirstOrDefault(x => x.Id == id);
            if (delete != null && delete.Id.Equals(id))
            {
                database.References.Remove(delete);
                await database.SaveChangesAsync();
                return new ObjectResult(new { deleted = true, element = delete });
            }
            return null;
        }
    }
}
