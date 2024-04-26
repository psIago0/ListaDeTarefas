using ListaDeTarefas.Data.Map;
using ListaDeTarefas.Model;
using Microsoft.EntityFrameworkCore;

namespace ListaDeTarefas.Data
{
    public class ToDoListDBContext : DbContext
    {
        public ToDoListDBContext(DbContextOptions<ToDoListDBContext> options) : base(options)
        {
        
        }

        public DbSet<TarefasModel> Tarefas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TarefaMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
