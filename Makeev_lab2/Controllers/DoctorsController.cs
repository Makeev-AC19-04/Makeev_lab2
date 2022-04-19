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

        // GET: api/Doctors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetDoctors()
        {
            return await _context.Doctors.ToListAsync();
        }

        // GET: api/Doctors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Doctor>> GetDoctor(long id)
        {
            var doctor = await _context.Doctors.FindAsync(id);

            if (doctor == null)
            {
                return NotFound();
            }

            return doctor;
        }

        [HttpGet("{id}/GetSpeciality")]
        public async Task<ActionResult<Speciality>> GetSpeciality(long id)
        {
            var doctor = await _context.Doctors.FindAsync(id);

            if (doctor == null)
            {
                return NotFound();
            }

            long SpecId = doctor.SpecialityId;

            var speciality = await _context.Specialities.FindAsync(SpecId);

            if (speciality == null)
            {
                return NotFound();
            }

            return speciality;
        }

        [HttpGet("{id}/Name")]
        public async Task<string> GetName(long id)
        {
            var doctor = await _context.Doctors.FindAsync(id);

            if (doctor == null)
            {
                return "There is no such doctor";
            }
            return doctor.GetName();

        }

        [HttpGet("GetSpecialists/{SpecId}")]
        public IEnumerable<Doctor> GetSpecialists(long SpecId) //async Task<IEnumerable<Doctor>>
        {
            IEnumerable<Doctor> doctorQuery =
                            from doc in _context.Doctors
                            where doc.SpecialityId == SpecId
                            select doc;
            return doctorQuery;
        }

        // PUT: api/Doctors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDoctor(long id, Doctor doctor)
        {
            if (id != doctor.Id)
            {
                return BadRequest();
            }

            _context.Entry(doctor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoctorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok("Доктор изменен");
        }

        // POST: api/Doctors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult<Doctor>> PostDoctor(Doctor doctor)
        {
            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDoctor", new { id = doctor.Id }, doctor);
        }

        [Authorize(Roles = "admin")]
        [HttpPost("{id}/{idspec}")] //Изменить специальность доктора
        public async Task<ActionResult<Doctor>> ChangeSpec(long id, long idspec) //comment for git
        {
            var doctor = await _context.Doctors.FindAsync(id);

            if (doctor == null)
            {
                return NotFound();
            }
            doctor.ChangeSpeciality(idspec);
            await _context.SaveChangesAsync();
            return doctor;
        }

        // DELETE: api/Doctors/5
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")] //Удалить доктора
        public async Task<IActionResult> DeleteDoctor(long id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }

            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DoctorExists(long id)
        {
            return _context.Doctors.Any(e => e.Id == id);
        }
    }
}
