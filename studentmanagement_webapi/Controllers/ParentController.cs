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
    public class ParentController : ControllerBase
    {
        private readonly DataContext _context;

        public ParentController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        //[Authorize(Roles = "Admin,Teacher")]
        public async Task<ActionResult<List<Parent>>> GetParents()
        {
            return Ok(await _context.Parents.ToListAsync());
        }

        [HttpGet("{id}")]
        //[Authorize(Roles = "Admin, Teacher")]
        public async Task<ActionResult<Parent>> GetParentById(int id)
        {
            var dbParent = await _context.Parents.FindAsync(id);
            if (dbParent == null)
                return BadRequest("Parent Not Found.");
            return Ok(dbParent);
        }

        [HttpPost]
        //[Authorize(Roles ="Admin")]
        public async Task<ActionResult<List<Parent>>> AddParent(Parent parent)
        {
            _context.Parents.Add(parent);
            await _context.SaveChangesAsync();

            return Ok(await _context.Parents.ToListAsync());
        }

        public async Task<ActionResult<List<Parent>>> UpdateParent(Parent request)
        {
            var dbParent = await _context.Parents.FindAsync(request.Id);
            if (dbParent == null)
                return BadRequest("Parent Not Found.");

            dbParent.FirstName = request.FirstName;
            dbParent.LastName = request.LastName;

            await _context.SaveChangesAsync();

            return Ok(await _context.Parents.ToListAsync());
        }

    }
}
 



