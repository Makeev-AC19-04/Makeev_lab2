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
using Makeev_lab2.Controllers;
using Microsoft.AspNetCore.Authorization;

namespace Makeev_lab2.Services
{

    public interface DoctorsService
    {
        public static async Task<ActionResult<IEnumerable<Doctor>>> GetDoctors(Makeev_lab2Context context)
        {
            return await context.Doctors.ToListAsync();
        }
        public static async Task<ActionResult<Doctor>> GetDoctor(long id, Makeev_lab2Context context, NotFoundResult notfound)
        {
            var doctor = await context.Doctors.FindAsync(id);
            if (doctor == null)
            {
                return notfound;
            }

            return doctor;
        }
        public static async Task<ActionResult<Speciality>> GetSpeciality(long id, Makeev_lab2Context context, NotFoundResult notfound)
        {
            var doctor = await context.Doctors.FindAsync(id);

            if (doctor == null)
            {
                return notfound;
            }

            long SpecId = doctor.SpecialityId;

            var speciality = await context.Specialities.FindAsync(SpecId);

            if (speciality == null)
            {
                return notfound;
            }

            return speciality;
        }
        public static async Task<string> GetName(long id, Makeev_lab2Context context)
        {
            var doctor = await context.Doctors.FindAsync(id);

            if (doctor == null)
            {
                return "There is no such doctor";
            }
            return doctor.GetName();

        }
        public static IEnumerable<Doctor> GetSpecialists(long SpecId, Makeev_lab2Context context) //async Task<IEnumerable<Doctor>>
        {
            IEnumerable<Doctor> doctorQuery =
                            from doc in context.Doctors
                            where doc.SpecialityId == SpecId
                            select doc;
            return doctorQuery;
        }
        public static async Task<IActionResult> PutDoctor(long id, Doctor doctor, Makeev_lab2Context context, BadRequestResult badRequestResult, NotFoundResult notfound, bool doctorExists, OkObjectResult ok)
        {
            if (id != doctor.Id)
            {
                return badRequestResult;
            }

            context.Entry(doctor).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!doctorExists)
                {
                    return notfound;
                }
                else
                {
                    throw;
                }
            }
            return ok;
        }
        public static async Task<ActionResult<Doctor>> PostDoctor(Doctor doctor, Makeev_lab2Context context, CreatedAtActionResult caaresult)
        {
            context.Doctors.Add(doctor);
            await context.SaveChangesAsync();

            return caaresult;
        }
    }
}
