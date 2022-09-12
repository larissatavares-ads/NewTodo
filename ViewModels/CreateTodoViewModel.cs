using System.ComponentModel.DataAnnotations;

namespace NewTodo.ViewModels;

public class CreateTodoViewModel
{
    //É uma validação, o objeto abaixo é requerido.
    //Se a validação falhar, uma mensagem de erro será exibida.
    [Required]  
    public string Title { get; set; }
}