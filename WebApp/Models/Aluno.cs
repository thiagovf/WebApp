using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Aluno
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Telefone { get; set; }
        public int RA { get; set; }
        public string Data { get; set; }

        public List<Aluno> ListarAlunos(int? id = null)
        {
            try
            {
                AlunoDAO alunoDAO = new AlunoDAO();
                return alunoDAO.ListarAlunosDB(id);
            }
            catch (Exception e)
            {

                throw new Exception($"Erro ao listar Alunos: Erro => {e.Message}");
            }
        }


        private bool ReescreverArquivo(List<Aluno> listaAlunos)
        {
            string caminhoArquivo = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data\Base.json");

            string json = JsonConvert.SerializeObject(listaAlunos, Formatting.Indented);

            File.WriteAllText(caminhoArquivo, json);

            return true;
        }

        public void Inserir(Aluno aluno)
        {
            try
            {
                AlunoDAO alunoDAO = new AlunoDAO();
                alunoDAO.InserirAluno(aluno);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao inserir Aluno: Erro => {ex.Message}");
            }

        }

        public void Atualizar(int id, Aluno aluno)
        {
            try
            {
                AlunoDAO alunoDAO = new AlunoDAO();
                alunoDAO.AtualizarAluno(id, aluno);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao atualizar Aluno: Erro => {ex.Message}");
            }

        }

        public void Deletar(int id)
        {
            try
            {
                AlunoDAO alunoDAO = new AlunoDAO();
                alunoDAO.DeletarAluno(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao deletar Auno: Erro => {ex.Message}");
            }
        }
    }
}