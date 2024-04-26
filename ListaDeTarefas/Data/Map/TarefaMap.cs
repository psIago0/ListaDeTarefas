using ListaDeTarefas.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ListaDeTarefas.Data.Map
{
    public class TarefaMap : IEntityTypeConfiguration<TarefasModel>
    {
        public void Configure(EntityTypeBuilder<TarefasModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Status).IsRequired();
        }
    }
}
