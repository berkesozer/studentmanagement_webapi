using System;
using System.Collections.Generic;
using System.Data;
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
    public class LessonController : ControllerBase
    {
        private readonly DataContext _context;

        public LessonController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        //[Authorize(Roles = "Admin,Teacher")]
        public async Task<ActionResult<List<Lesson>>> GetLessons()
        {
            return Ok(await _context.Lessons.ToListAsync());
        }

        [HttpGet("{id}")]
        //[Authorize(Roles = "Admin, Teacher")]
        public async Task<ActionResult<Lesson>> GetLessonById(int id)
        {
            var dbLesson = await _context.Lessons.FindAsync(id);
            if (dbLesson == null)
                return BadRequest("Lesson Not Found.");
            return Ok(dbLesson);
        }

        [HttpPost]
        //[Authorize(Roles ="Admin")]
        public async Task<ActionResult<List<Lesson>>> AddLesson(Lesson lesson)
        {
            _context.Lessons.Add(lesson);
            await _context.SaveChangesAsync();

            return Ok(await _context.Lessons.ToListAsync());
        }

    }
}

