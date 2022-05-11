#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Makeev_lab2.Data;
using Makeev_lab2.Models;
using Makeev_lab2.Services;
using Microsoft.AspNetCore.Authorization;

namespace Makeev_lab2.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SpecialitiesController : ControllerBase
    {
        private readonly Makeev_lab2Context _context;

        public SpecialitiesController(Makeev_lab2Context context)
        {
            _context = context;
        }

        // GET: api/Specialities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Speciality>>> GetSpecialities()
        {
            return await _context.Specialities.ToListAsync();
        }

        // GET: api/Specialities/5
        [HttpGet("{id}")]
        public Task<ActionResult<Speciality>> GetSpeciality(long id)
        {
            return SpecialitiesService.GetSpeciality(id, _context, NotFound());
        }

        // PUT: api/Specialities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public Task<IActionResult> PutSpeciality(long id, Speciality speciality)
        {
            return SpecialitiesService.PutSpeciality(id, speciality, _context, NotFound(), BadRequest(), NoContent());
        }

        [HttpGet("SpecsWithDoctors")]
        public ActionResult<Service> GetSpecsWithDoctors()
        {
            //Вывести список докторов со специальностями
            var servsspecsQuery =
                from spec in _context.Specialities
                join doc in _context.Doctors on spec.Id equals doc.SpecialityId
                select new { doc.Name, spec.SpecialityName };
            return Ok(servsspecsQuery);
        }

        // POST: api/Specialities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "admin")]
        [HttpPost]
        public Task<ActionResult<Speciality>> PostSpeciality(Speciality speciality)
        {
            return SpecialitiesService.PostSpeciality(speciality, _context, CreatedAtAction("GetSpeciality", new { id = speciality.Id }, speciality));
        }

        // DELETE: api/Specialities/5
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public Task<IActionResult> DeleteSpeciality(long id)
        {
            return SpecialitiesService.DeleteSpeciality(id, _context, NotFound(), NoContent());
        }

        private bool SpecialityExists(long id)
        {
            return _context.Specialities.Any(e => e.Id == id);
        }
    }
}
