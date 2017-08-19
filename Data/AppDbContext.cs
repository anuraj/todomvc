using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace TodoMvc.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<Todo> Todos { get; set; }
    }

    public class Todo
    {
        public int Id { get; set; }
        [Required, StringLength(100)]
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
    }
}