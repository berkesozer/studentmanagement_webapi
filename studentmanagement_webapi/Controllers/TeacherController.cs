using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace studentmanagement_webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly DataContext _context;

        public TeacherController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<Teacher>>> GetTeachers()
        {
            return Ok(await _context.Teachers.ToListAsync());
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Teacher>> GetTeacherById(int id)
        {
            var dbTeacher = await _context.Teachers.FindAsync(id);
            if (dbTeacher == null)
                return BadRequest("Teacher Not Found.");
            return Ok(dbTeacher);
        }

        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult<List<Teacher>>> AddTeacher(Teacher teacher)
        {
            _context.Teachers.Add(teacher);
            await _context.SaveChangesAsync();

            return Ok(await _context.Teachers.ToListAsync());
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<Teacher>>> UpdateTeacher(Teacher request)
        {
            var dbTeacher = await _context.Teachers.FindAsync(request.Id);
            if (dbTeacher == null)
                return BadRequest("Teacher Not Found.");

            dbTeacher.FirstName = request.FirstName;
            dbTeacher.LastName = request.LastName;

            await _context.SaveChangesAsync();

            return Ok(await _context.Teachers.ToListAsync());
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Teacher>> DeleteTeacher(int id)
        {
            var dbTeacher = await _context.Teachers.FindAsync(id);
            if (dbTeacher == null)
                return BadRequest("Teacher Not Found.");

            _context.Teachers.Remove(dbTeacher);
            await _context.SaveChangesAsync();
            return Ok(await _context.Teachers.ToListAsync());
        }
    }
}


