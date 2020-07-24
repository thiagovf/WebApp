﻿using System;
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
        // GET: api/Aluno
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
        [Route("Recuperar/{id:int}/{nome}/{sobrenome=andrade}")]
        public Aluno Get(int id, string nome, string sobrenome)
        {
            Aluno aluno = new Aluno();
            return aluno.ListarAlunos().Where(x => x.Id == id).FirstOrDefault();
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

        // GET: api/Aluno/5
        [HttpGet]
        [Route("Recuperar/{id}")]
        public Aluno Get(int id)
        {
            Aluno aluno = new Aluno();
            return aluno.ListarAlunos().Where(x => x.Id == id).FirstOrDefault();
        }

        // POST: api/Aluno
        public List<Aluno> Post([FromBody]Aluno aluno)
        {
            aluno.Inserir(aluno);

            return aluno.ListarAlunos();
        }

        // PUT: api/Aluno/5
        public Aluno Put(int id, [FromBody]Aluno aluno)
        {
            aluno.Atualizar(id, aluno);

            return aluno.ListarAlunos().Where(a => a.Id == id).FirstOrDefault();
        }

        // DELETE: api/Aluno/5
        public void Delete(int id)
        {
            Aluno _aluno = new Aluno();
            _aluno.Deletar(id);
        }
    }
}
