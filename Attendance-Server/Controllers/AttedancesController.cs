﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AttendanceServer.Entities;
using AttendanceServer.Models;

namespace AttendanceServer.Controllers
{
    [Route("attendance/api/attendances")]
    [ApiController]
    public class AttedancesController : ControllerBase
    {
        private readonly AttendanceContext _context;

        public AttedancesController(AttendanceContext context)
        {
            _context = context;
        }

        // GET: api/Attedances
        [HttpGet]
        public IEnumerable<Attedance> GetAttedances()
        {
            return _context.Attedances;
        }

        // GET: api/Attedances/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAttedance([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var attedance = await _context.Attedances.FindAsync(id);

            if (attedance == null)
            {
                return NotFound();
            }

            return Ok(attedance);
        }

        // PUT: api/Attedances/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAttedance([FromRoute] int id, [FromBody] Attedance attedance)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != attedance.AttedanceId)
            {
                return BadRequest();
            }

            _context.Entry(attedance).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttedanceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Attedances
        [HttpPost]
        public async Task<IActionResult> PostAttedance([FromBody] Attedance attedance)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Attedances.Add(attedance);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAttedance", new { id = attedance.AttedanceId }, attedance);
        }

        // DELETE: api/Attedances/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttedance([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var attedance = await _context.Attedances.FindAsync(id);
            if (attedance == null)
            {
                return NotFound();
            }

            _context.Attedances.Remove(attedance);
            await _context.SaveChangesAsync();

            return Ok(attedance);
        }

        private bool AttedanceExists(int id)
        {
            return _context.Attedances.Any(e => e.AttedanceId == id);
        }
    }
}