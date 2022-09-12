using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewTodo.Data;
using NewTodo.Models;
using NewTodo.ViewModels;

namespace NewTodo.Controllers;

[ApiController]
[Route("v1")]
public class TodoController : ControllerBase
{
    //READ
    [HttpGet]
    [Route("todos")]
    public async Task<IActionResult> GetAsync(
        [FromServices] AppDbContext context)
    {
        var todos = await context
            .Todos
            .AsNoTracking()
            .ToListAsync();
        return Ok(todos);
    }
    //READ
    [HttpGet]
    [Route("todos/{id}")]
    public async Task<IActionResult> GetByIdAsync(
        [FromServices] AppDbContext context,
        [FromRoute] int id)
    {
        var todo = await context
            .Todos
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
        return todo == null 
            ? NotFound() 
            : Ok(todo);
    }

    //CREATE
    [HttpPost("todos")]
    public async Task<IActionResult> PostAsync(
        [FromServices] AppDbContext context,
        [FromBody] CreateTodoViewModel model)
    {
        //ModelState faz a validação do objeto da classe CreateTodoViewModel
        if (!ModelState.IsValid)
            return BadRequest();
        
        //Criação do ToDo
        var todo = new Todo
        {
            Date = DateTime.Now, 
            Done = false, 
            Title = model.Title
        };

        try
        {
            //Salvamento do ToDo no banco de dados
            //AddAsync >> Salva somente na memória
            await context.Todos.AddAsync(todo);
        
            //SaveChangesAsync >> Para comitar as ações no banco
            await context.SaveChangesAsync();

            return Created($"v1/todos/{todo.Id}", todo);
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }
    
    //UPDATE
    [HttpPut("todos/{id}")]
    //[FromRoute] >> O parâmetro está informado na rota
    public async Task<IActionResult> PutAsync(
        [FromServices] AppDbContext context,
        [FromBody] CreateTodoViewModel model,
        [FromRoute] int id)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        
        var todo = await context
            .Todos
            .FirstOrDefaultAsync(x => x.Id == id);

        if (todo == null)
            return NotFound();
        try
        {
            //Update
            todo.Title = model.Title;
            context.Todos.Update(todo);
            await context.SaveChangesAsync();

            return Ok(todo);
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }
    
    //DELETE
    [HttpDelete("todos/{id}")]
    //Aqui o model não será preciso
    public async Task<IActionResult> DeleteAsync(
        [FromServices] AppDbContext context,
        [FromRoute] int id)
    {
        var todo = await context
            .Todos
            .FirstOrDefaultAsync(x => x.Id == id);

        try
        {
            context.Todos.Remove(todo);
            await context.SaveChangesAsync();
            return Ok("Seu item foi removido com sucesso.");
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }
}