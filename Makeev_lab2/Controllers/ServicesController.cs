﻿#nullable disable
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
    public class ServicesController : ControllerBase
    {
        private readonly Makeev_lab2Context _context;

        public ServicesController(Makeev_lab2Context context)
        {
            _context = context;
        }

        // GET: api/Services
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Service>>> GetServices()
        {
            return await _context.Services.ToListAsync();
        }

        // GET: api/Services/5
        [HttpGet("{id}")]
        public Task<ActionResult<Service>> GetService(long id)
        {
            return ServicesService.GetService(id, _context, NotFound());
        }

        [HttpGet("ServsWithSpecs")]
        public ActionResult<Service> GetServsWithSpecs()
        {
            var servsspecsQuery =
                from serv in _context.Services
                join doc in _context.Doctors on serv.Id equals doc.SpecialityId
                select new { doc.Name, serv.ServiceName, serv.Price };
            return Ok(servsspecsQuery);
        }

        // PUT: api/Services/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public Task<IActionResult> PutService(long id, Service service)
        {
            return ServicesService.PutService(id, service, _context, BadRequest(), NotFound(), NoContent());
        }

        // POST: api/Services
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        
        [Authorize(Roles = "admin")]
        [HttpPost]
        public Task<ActionResult<Service>> PostService(Service service)
        {
            return ServicesService.PostService(_context, service, CreatedAtAction("GetService", new { id = service.Id }, service));
        }

        [Authorize(Roles = "admin")]
        [HttpPost("{id}/{iddoc}")] //изменить специальност у доктора
        public Task<ActionResult<Service>> ChangeSpec(long id, long iddoc)
        {
            return ServicesService.ChangeSpec(id, iddoc, _context, NotFound());
        }

        // DELETE: api/Services/5

        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public Task<IActionResult> DeleteService(long id)
        {
            return ServicesService.DeleteService(id, _context, NotFound(), NoContent());
        }

        private bool ServiceExists(long id)
        {
            return _context.Services.Any(e => e.Id == id);
        }
    }
}
