using ListaDeTarefas.Model;

namespace ListaDeTarefas.Repositorios.Interfaces
{
    public interface ITarefasRepositorio
    {
        Task<List<TarefasModel>> ListarTarefas();
        Task<TarefasModel> ListarPorId(int id);
        Task<TarefasModel> AddTarefa(TarefasModel tarefa);
        Task<bool> DeletarTarefas(int id);
        Task<TarefasModel> AtualizarTarefa(TarefasModel tarefa);

    }
}
