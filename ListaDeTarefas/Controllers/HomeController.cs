using ListaDeTarefas.Model;
using ListaDeTarefas.Models;
using ListaDeTarefas.Repositorios.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ListaDeTarefas.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITarefasRepositorio _tarefasRepositorio;

        public HomeController(ITarefasRepositorio tarefasRepositorio)
        {
            _tarefasRepositorio = tarefasRepositorio;
        }

        public async Task<ActionResult<List<TarefasModel>>> Index()
        {
            List<TarefasModel> tarefas = await _tarefasRepositorio.ListarTarefas();
            return View(tarefas);
        }

        [HttpGet("ListarTarefas")]
        public async Task<ActionResult<List<TarefasModel>>> ListarTarefas()
        {
            List<TarefasModel> tarefas = await _tarefasRepositorio.ListarTarefas();
            return Ok(tarefas);
        }

        [HttpGet("ListarPorId/{id}")]
        public async Task<ActionResult<TarefasModel>> ListarPorId(int id)
        {
            TarefasModel tarefa = await _tarefasRepositorio.ListarPorId(id);
            return Ok(tarefa);
        }

        [HttpPost("AdicionarTarefa")]
        public async Task<ActionResult<TarefasModel>> Adicionar([FromBody] TarefasModel tarefaModel)
        {
            TarefasModel tarefa = await _tarefasRepositorio.AddTarefa(tarefaModel);
            return Ok(tarefa);
        }

        [HttpPut("AlterarTarefa")]
        public async Task<ActionResult<TarefasModel>> Alterar([FromBody] TarefasModel tarefaModel)
        {
            TarefasModel tarefa = await _tarefasRepositorio.AtualizarTarefa(tarefaModel);
            return Ok(tarefa);
        }

        [HttpDelete("DeletarTarefa/{id}")]
        public async Task<ActionResult<TarefasModel>> Deletar(int id)
        {
            bool deletado = await _tarefasRepositorio.DeletarTarefas(id);
            return Ok(deletado);
        }

        [HttpGet("teste")]
        public ActionResult<Object> TESTAR()
        {
            return Ok(new { teste = "abc" });
        }

    }
}
