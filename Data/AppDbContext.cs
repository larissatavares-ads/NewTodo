using Microsoft.EntityFrameworkCore;
using NewTodo.Models;

namespace NewTodo.Data;

//DbContext >> faz consulta no banco de dados e agrupa as alterações que serão gravadas no repositório.
public class AppDbContext : DbContext
{
    //DbSet >> É a representação da tabela.
    //Representa a coleção de todas as entidades no contexto ou que podem ser consultadas do DB
    public DbSet<Todo> Todos { get; set; }

    protected override void OnConfiguring(
        DbContextOptionsBuilder optionsBuilder) 
        => optionsBuilder.UseSqlite("DataSource=app.db;Cache=Shared");
}