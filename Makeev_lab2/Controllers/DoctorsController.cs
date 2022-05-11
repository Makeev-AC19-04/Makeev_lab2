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
    public class DoctorsController : ControllerBase
    {
        private readonly Makeev_lab2Context _context;

        public DoctorsController(Makeev_lab2Context context)
        {
            _context = context;
        }

        // GET: api/Doctors - получить список всех докторов
        [HttpGet]
        public Task<ActionResult<IEnumerable<Doctor>>> GetDoctors()
        {
            return DoctorsService.GetDoctors(_context); //await _context.Doctors.ToListAsync();
        }

        // GET: api/Doctors/5
        [HttpGet("{id}")]
        public Task<ActionResult<Doctor>> GetDoctor(long id)
        { 
            return DoctorsService.GetDoctor(id, _context, NotFound()); //Костыль с NotFound
        }

        [HttpGet("{id}/GetSpeciality")]
        public Task<ActionResult<Speciality>> GetSpeciality(long id)
        {
            return DoctorsService.GetSpeciality(id, _context, NotFound());
        }

        [HttpGet("{id}/Name")]
        public Task<string> GetName(long id)
        {
            return DoctorsService.GetName(id, _context);

        }

        [HttpGet("GetSpecialists/{SpecId}")]
        public IEnumerable<Doctor> GetSpecialists(long SpecId) //async Task<IEnumerable<Doctor>>
        {
            return DoctorsService.GetSpecialists(SpecId, _context);
        }

        // PUT: api/Doctors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public Task<IActionResult> PutDoctor(long id, Doctor doctor)
        {
            return DoctorsService.PutDoctor(id, doctor, _context, BadRequest(), NotFound(), DoctorExists(id), Ok("Доктор изменен"));
        }

        // POST: api/Doctors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "admin")]
        [HttpPost]
        public Task<ActionResult<Doctor>> PostDoctor(Doctor doctor)
        {
            return DoctorsService.PostDoctor(doctor, _context, CreatedAtAction("GetDoctor", new { id = doctor.Id }, doctor));
        }

        [Authorize(Roles = "admin")]
        [HttpPost("{id}/{idspec}")] //Изменить специальность доктора
        public Task<ActionResult<Doctor>> ChangeSpec(long id, long idspec) //comment for git
        {
            return DoctorsService.ChangeSpec(id, idspec, _context, NotFound());
        }

        // DELETE: api/Doctors/5
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")] //Удалить доктора
        public Task<IActionResult> DeleteDoctor(long id)
        {
            return DoctorsService.DeleteDoctor(id, _context, NotFound(), NoContent())
        }

        private bool DoctorExists(long id)
        {
            return _context.Doctors.Any(e => e.Id == id);
        }
    }
}
