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
    public interface SpecialitiesService
    {
        public static async Task<ActionResult<Speciality>> GetSpeciality(long id, Makeev_lab2Context _context, NotFoundResult notFoundResult)
        {
            var speciality = await _context.Specialities.FindAsync(id);

            if (speciality == null)
            {
                return notFoundResult;
            }

            return speciality;
        }
        public static async Task<IActionResult> PutSpeciality(long id, Speciality speciality, Makeev_lab2Context _context, NotFoundResult notFoundResult, BadRequestResult badRequest, NoContentResult noContent)
        {
            if (id != speciality.Id)
            {
                return badRequest;
            }

            _context.Entry(speciality).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (speciality == null)
                {
                    return notFoundResult;
                }
                else
                {
                    throw;
                }
            }

            return noContent;
        }
        public static async Task<IActionResult> DeleteSpeciality(long id, Makeev_lab2Context _context, NotFoundResult notFound, NoContentResult noContent)
        {
            var speciality = await _context.Specialities.FindAsync(id);
            if (speciality == null)
            {
                return notFound;
            }

            _context.Specialities.Remove(speciality);
            await _context.SaveChangesAsync();

            return noContent;
        }
    }
}
