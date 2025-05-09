using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todo.Models;

namespace Todo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly TodosContext _context;
        public TodoController(TodosContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        [HttpGet]
        public async Task<ActionResult> GetAllTodos()
        {
            return Ok(await _context.Todos.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetTodo(int id)
        {
            var todo = await _context.Todos.FindAsync(id);
            return Ok(todo);
        }

        [HttpPost]
        public async Task<ActionResult> PostTodo(Todos todo)
        {
            if (todo == null) return BadRequest();
            var result = await _context.Todos.AddAsync(todo);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTodo), new { id = todo.Id }, todo);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutTodo(int id, [FromBody] Todos todo)
        {
            if (id != todo.Id) return BadRequest();
            _context.Entry(todo).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTodo), new { id = todo.Id }, todo);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo == null) return BadRequest();

            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();

            return Ok(todo);
        }
    }
}