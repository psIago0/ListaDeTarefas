using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ListaDeTarefas.Model
{
    [Table("TB_TAREFAS")]
    public class TarefasModel
    {


        [Key]
        public int Id { get; set; }
        public string? Nome { get; set; }
        public bool Status { get; set; }
        public DateTime DtCriacao { get; set; }

        public TarefasModel(string? nome)
        {
            Nome = nome;
        }
        public TarefasModel(int id, bool status)
        {
            Id = id;
            Status = status;
        }

        public TarefasModel()
        {
        }

    }

}
