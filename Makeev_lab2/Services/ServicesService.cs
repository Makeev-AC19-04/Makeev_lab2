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
    public interface ServicesService
    {
        public static async Task<ActionResult<Service>> GetService(long id, Makeev_lab2Context context, NotFoundResult notfound)
        {
            var service = await context.Services.FindAsync(id);

            if (service == null)
            {
                return notfound;
            }

            return service;
        }

        public static async Task<IActionResult> PutService(long id, Service service, Makeev_lab2Context context, BadRequestResult badRequestResult, NotFoundResult notFound, NoContentResult noContentResult)
        {
            if (id != service.Id)
            {
                return badRequestResult;
            }

            context.Entry(service).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (service == null)
                {
                    return notFound;
                }
                else
                {
                    throw;
                }
            }

            return noContentResult;
        }

    }
}
