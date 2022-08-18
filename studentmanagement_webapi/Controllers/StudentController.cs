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
    public class StudentController : ControllerBase
    {
        private readonly DataContext _context;

        public StudentController(DataContext context)
        {
            _context = context;
        }

        [HttpGet(Name = "GetStudents"),
            Authorize(Roles = "Admin, Teacher")]
        public async Task<ActionResult<List<Student>>> GetStudents()
        {
            return Ok(await _context.Students.ToListAsync());
        }

        [HttpGet("{GetStudentByid}"),
            Authorize(Roles = "Admin, Teacher")]
        public async Task<ActionResult<Student>> GetStudentById(int id)
        {
            var dbStudent = await _context.Students.FindAsync(id);
            if (dbStudent == null)
                return BadRequest("Student Not Found.");
            return Ok(dbStudent);
        }

        [HttpPost (Name = "CreateStudent"),
            Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<Student>>> AddStudent(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return Ok(await _context.Students.ToListAsync());
        }

        [HttpPut(Name = "UpdateStudent"),
            Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<Student>>> UpdateStudent (Student request)
        {
            var dbStudent = await _context.Students.FindAsync(request.Id);
            if (dbStudent == null)
                return BadRequest("Student Not Found.");

            dbStudent.FirstName = request.FirstName;
            dbStudent.LastName = request.LastName;
            dbStudent.grade = request.grade;

            await _context.SaveChangesAsync();

            return Ok(await _context.Students.ToListAsync());
        }

        [HttpDelete("{DeleteByid}"),
            Authorize(Roles = "Admin")]
        public async Task<ActionResult<Student>> DeleteStudent(int id)
        {
            var dbStudent = await _context.Students.FindAsync(id);
            if (dbStudent == null)
                return BadRequest("Student Not Found.");

            _context.Students.Remove(dbStudent);
            await _context.SaveChangesAsync();
            return Ok(await _context.Students.ToListAsync());
        }
    }
}
