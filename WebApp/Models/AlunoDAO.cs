using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class AlunoDAO
    {
        //private string stringConexao = ConfigurationManager.AppSettings["ConnectionString"];
        private string stringConexao = ConfigurationManager.ConnectionStrings["ConexaoDev"].ConnectionString;
        private IDbConnection conexao;

        public AlunoDAO()
        {
            conexao = new SqlConnection(stringConexao);
            conexao.Open();
        }

        public List<Aluno> ListarAlunosDB(int? id)
        {
            List<Aluno> alunos = new List<Aluno>();

            IDbCommand selectCmd = conexao.CreateCommand();
            if (id == null)
            {
                selectCmd.CommandText = "select * from Alunos";
            }
            else
            {
                selectCmd.CommandText = $"select * from Alunos where id = {id}";
            }

            IDataReader resultado = selectCmd.ExecuteReader();
            while (resultado.Read())
            {
                Aluno alu = new Aluno();
                alu.Id = Convert.ToInt32(resultado["Id"]);
                alu.Nome = Convert.ToString(resultado["nome"]);
                alu.Sobrenome = Convert.ToString(resultado["sobrenome"]);
                alu.Telefone = Convert.ToString(resultado["telefone"]);
                alu.RA = Convert.ToInt32(resultado["ra"]);

                alunos.Add(alu);
            }

            conexao.Close();

            return alunos;
        }

        public void InserirAluno(Aluno aluno)
        {
            IDbCommand insertCmd = conexao.CreateCommand();
            insertCmd.CommandText = "insert into Alunos (nome, sobrenome, telefone, ra) values (@nome, @sobrenome, @telefone, @ra)";

            IDbDataParameter paramNome = new SqlParameter("nome", aluno.Nome);
            insertCmd.Parameters.Add(paramNome);

            IDbDataParameter paramSobrenome = new SqlParameter("sobrenome", aluno.Sobrenome);
            insertCmd.Parameters.Add(paramSobrenome);

            IDbDataParameter paramTelefone = new SqlParameter("telefone", aluno.Telefone);
            insertCmd.Parameters.Add(paramTelefone);

            IDbDataParameter paramRA = new SqlParameter("ra", aluno.RA);
            insertCmd.Parameters.Add(paramRA);

            insertCmd.ExecuteNonQuery();
        }

        public void AtualizarAluno(int id, Aluno aluno)
        {
            IDbCommand updateCmd = conexao.CreateCommand();
            updateCmd.CommandText = "update Alunos set nome = @nome, sobrenome = @sobrenome, telefone = @telefone, ra = @ra where id = @id";

            IDbDataParameter paramId = new SqlParameter("id", id);
            updateCmd.Parameters.Add(paramId);

            IDbDataParameter paramNome = new SqlParameter("nome", aluno.Nome);
            updateCmd.Parameters.Add(paramNome);

            IDbDataParameter paramSobrenome = new SqlParameter("sobrenome", aluno.Sobrenome);
            updateCmd.Parameters.Add(paramSobrenome);

            IDbDataParameter paramTelefone = new SqlParameter("telefone", aluno.Telefone);
            updateCmd.Parameters.Add(paramTelefone);

            IDbDataParameter paramRA = new SqlParameter("ra", aluno.RA);
            updateCmd.Parameters.Add(paramRA);

            updateCmd.ExecuteNonQuery();

        }

        public void DeletarAluno(int id)
        {
            IDbCommand deleteCmd = conexao.CreateCommand();
            deleteCmd.CommandText = "delete from Alunos where id = @id";

            IDbDataParameter paramId = new SqlParameter("id", id);
            deleteCmd.Parameters.Add(paramId);

            deleteCmd.ExecuteNonQuery();
        }
    }
}