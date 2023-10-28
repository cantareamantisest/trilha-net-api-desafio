using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly OrganizadorContext _context;

        public TarefaController(OrganizadorContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            // TODO: Buscar o Id no banco utilizando o EF
            // TODO: Validar o tipo de retorno. Se não encontrar a tarefa, retornar NotFound,
            // caso contrário retornar OK com a tarefa encontrada
            try
            {
                var tarefa =  await _context.Tarefas.FindAsync(id);
                if (tarefa == null)
                {
                    return NotFound();
                }
                return Ok(tarefa);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
           
        }

        [HttpGet("ObterTodos")]
        public async Task<IActionResult> ObterTodos()
        {
            // TODO: Buscar todas as tarefas no banco utilizando o EF
            try
            {
                var tarefas = await _context.Tarefas.ToListAsync();
                return Ok(tarefas);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("ObterPorTitulo")]
        public IActionResult ObterPorTitulo(string titulo)
        {
            // TODO: Buscar  as tarefas no banco utilizando o EF, que contenha o titulo recebido por parâmetro
            // Dica: Usar como exemplo o endpoint ObterPorData
            try
            {
                var tarefa = _context.Tarefas.Where(c => c.Titulo == titulo);
                return Ok(tarefa);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("ObterPorData")]
        public IActionResult ObterPorData(string data)
        {
            try 
            {
                if (DateTime.TryParse(data, out DateTime dataParse))
                {
                    var tarefa = _context.Tarefas.Where(x => x.Data.Date == dataParse.Date);
                    return Ok(tarefa);
                }
                return BadRequest("O formato da data está errado!");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }            
        }

        [HttpGet("ObterPorStatus")]
        public IActionResult ObterPorStatus(EnumStatusTarefa status)
        {
            // TODO: Buscar  as tarefas no banco utilizando o EF, que contenha o status recebido por parâmetro
            // Dica: Usar como exemplo o endpoint ObterPorData
            try 
            {
                var tarefa = _context.Tarefas.Where(x => x.Status == status);
                return Ok(tarefa);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }              
        }

        [HttpPost]
        public async Task<IActionResult> Criar(Tarefa tarefa)
        {
            try 
            {
                if (ModelState.IsValid)
                {
                    await _context.Tarefas.AddAsync(tarefa);
                    await _context.SaveChangesAsync();
                    return CreatedAtAction(nameof(ObterPorId), new { id = tarefa.Id }, tarefa);
                }
                return BadRequest();   
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, Tarefa tarefa)
        {
            try 
            {
                if (ModelState.IsValid)
                {
                    var tarefaBanco = await _context.Tarefas.FindAsync(id);

                    if (tarefaBanco == null)
                        return NotFound();

                    // TODO: Atualizar as informações da variável tarefaBanco com a tarefa recebida via parâmetro
                    // TODO: Atualizar a variável tarefaBanco no EF e salvar as mudanças (save changes)
                    tarefaBanco.Titulo = tarefa.Titulo;
                    tarefaBanco.Descricao = tarefa.Descricao;
                    tarefaBanco.Data = tarefa.Data;
                    tarefaBanco.Status = tarefa.Status;
                    _context.Tarefas.Update(tarefaBanco);
                    await _context.SaveChangesAsync();
                    return Ok("Tarefa atualizado com sucesso!");
                }   
                return BadRequest();             
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }            
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            try 
            {
                var tarefaBanco = await _context.Tarefas.FindAsync(id);

                if (tarefaBanco == null)
                    return NotFound();

                // TODO: Remover a tarefa encontrada através do EF e salvar as mudanças (save changes)
                _context.Tarefas.Remove(tarefaBanco);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }            
        }
    }
}