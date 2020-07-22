using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class ProfessorController : ApiController
    {

        // GET: api/Professor
        public IEnumerable<Professor> Get()
        {
            Professor professor = new Professor();
            return professor.ListarProfessores();
        }

        // GET: api/Professor/5
        public Professor Get(int id)
        {
            Professor professor = new Professor();
            return professor.ListarProfessores().Where(p => p.Id == id).FirstOrDefault();
        }

        // POST: api/Professor
        public List<Professor> Post([FromBody]Professor professor)
        {
            professor.Inserir(professor);
            return professor.ListarProfessores();
        }

        // PUT: api/Professor/5
        public Professor Put(int id, [FromBody]Professor professor)
        {
            return professor.Atualizar(id, professor);
        }

        // DELETE: api/Professor/5
        public void Delete(int id)
        {
            Professor professor = new Professor();
            professor.Deletar(id);
        }
    }
}
