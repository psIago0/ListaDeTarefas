using ListaDeTarefas.Data;
using ListaDeTarefas.Model;
using ListaDeTarefas.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ListaDeTarefas.Repositorios
{
    public class TarefaRepositorio : ITarefasRepositorio
    {
        private readonly ToDoListDBContext _dbContext;
        public TarefaRepositorio(ToDoListDBContext toDoListDBContext)
        {
            _dbContext = toDoListDBContext;
        }

        public async Task<List<TarefasModel>> ListarTarefas()
        {
            return await _dbContext.Tarefas.ToListAsync();
        }
        public async Task<TarefasModel> ListarPorId(int id)
        {
            return await _dbContext.Tarefas.FirstOrDefaultAsync(x => x.Id == id); 
        }

        public async Task<TarefasModel> AddTarefa(TarefasModel tarefa)
        {
            await _dbContext.Tarefas.AddAsync(tarefa);
            await _dbContext.SaveChangesAsync();

            return tarefa;
        }

        public async Task<TarefasModel> AtualizarTarefa(TarefasModel tarefa)
        {
            TarefasModel tarefaPorId = await ListarPorId(tarefa.Id);

            if (tarefaPorId == null)
            {
                throw new Exception($"A tarefa número ID: {tarefa.Id} não foi encontrada.");
            }
            if (tarefa.Nome != null)
            {
                tarefaPorId.Nome = tarefa.Nome;

            }
            if (tarefa.Status != tarefaPorId.Status)
            {
                tarefaPorId.Status = tarefa.Status;

            }
            else
            {
                tarefaPorId.Status = false;

            }

            _dbContext.Tarefas.Update(tarefaPorId);
            await _dbContext.SaveChangesAsync();

            return tarefaPorId;
        }

        public async Task<bool> DeletarTarefas(int id)
        {
            TarefasModel tarefaPorId = await ListarPorId(id);

            if (tarefaPorId == null)
            {
                throw new Exception($"A tarefa número ID: {id} não foi encontrada.");
            }

            _dbContext.Tarefas.Remove(tarefaPorId);
            await _dbContext.SaveChangesAsync();

            return true;

        }

    }
}
