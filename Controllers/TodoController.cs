using Microsoft.AspNetCore.Mvc;
using NewTodo.Models;

namespace NewTodo.Controllers;

[ApiController]
[Route("v1")]
public class TodoController : ControllerBase
{
    public List<Todo> Get()
    {
        return new List<Todo>();
    }
}