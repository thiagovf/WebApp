using App.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApp.Models;

namespace WebApp.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/Aluno")]
    public class AlunoController : ApiController
    {

        [HttpGet]
        [Route("Recuperar")]
        [Authorize (Roles = Papel.Professor)]
        public IHttpActionResult Recuperar()
        {
            try
            {
                AlunoModel aluno = new AlunoModel();
                return Ok(aluno.ListarAlunos());
            } catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        [HttpGet]
        [Route("Recuperar/{id:int}/{nome?}/{sobrenome?}")]
        public IHttpActionResult Get(int id, string nome = null, string sobrenome = null)
        {
            try
            {
                AlunoModel aluno = new AlunoModel();
                return Ok(aluno.ListarAlunos(id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        [HttpGet]
        [Route(@"RecuperarPorData/{data:regex([0-9]{4}\-[0-9]{2})}/{nome:minlength(5)}")]
        public IHttpActionResult Recuperar(string data, string nome)
        {
            try
            {
                AlunoModel aluno = new AlunoModel();
                IEnumerable<AlunoDTO> alunos = aluno.ListarAlunos().Where(x => x.Data == data || x.Nome == nome);

                if (!alunos.Any())
                    return NotFound();

                return Ok(alunos);
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            } 
        }

        [HttpGet]
        [Route("Recuperar/{id}")]
        public IHttpActionResult RecuperarPorId(int id)
        {
            try
            {
                AlunoModel aluno = new AlunoModel();
                return Ok(aluno.ListarAlunos(id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        [HttpPost]
        public IHttpActionResult Post([FromBody]AlunoDTO alunoDTO)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AlunoModel aluno = new AlunoModel();
                aluno.Inserir(alunoDTO);
                return Ok(aluno.ListarAlunos());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody]AlunoDTO alunoDTO)
        {
            try
            {
                AlunoModel aluno = new AlunoModel();
                aluno.Atualizar(id, alunoDTO);
                return Ok(aluno.ListarAlunos(id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                AlunoModel _aluno = new AlunoModel();
                _aluno.Deletar(id);
                return Ok("Deletado com sucesso");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }
    }
}
