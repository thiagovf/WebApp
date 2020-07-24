using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        public IHttpActionResult Recuperar()
        {
            try
            {
                Aluno aluno = new Aluno();
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
                Aluno aluno = new Aluno();
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
                Aluno aluno = new Aluno();
                IEnumerable<Aluno> alunos = aluno.ListarAlunos().Where(x => x.Data == data || x.Nome == nome);

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
        public IHttpActionResult Get(int id)
        {
            try
            {
                Aluno aluno = new Aluno();
                return Ok(aluno.ListarAlunos(id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        [HttpPost]
        public IHttpActionResult Post([FromBody]Aluno aluno)
        {
            try
            {
                aluno.Inserir(aluno);
                return Ok(aluno.ListarAlunos());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody]Aluno aluno)
        {
            try
            {
                aluno.Atualizar(id, aluno);
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
                Aluno _aluno = new Aluno();
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
