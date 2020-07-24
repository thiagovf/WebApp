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

        public List<Aluno> ListarAlunos(int? id)
        {
            List<Aluno> alunos = new List<Aluno>();
            try
            {

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
                    Aluno alu = new Aluno()
                    {
                        Id = Convert.ToInt32(resultado["Id"]),
                        Nome = Convert.ToString(resultado["nome"]),
                        Sobrenome = Convert.ToString(resultado["sobrenome"]),
                        Telefone = Convert.ToString(resultado["telefone"]),
                        RA = Convert.ToInt32(resultado["ra"])
                    };

                    alunos.Add(alu);
                }
                return alunos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conexao.Close();
            }
        }

        public void InserirAluno(Aluno aluno)
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conexao.Close();
            }
  
        }

        public void AtualizarAluno(int id, Aluno aluno)
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conexao.Close();
            }

        }

        public void DeletarAluno(int id)
        {
            try
            {
                IDbCommand deleteCmd = conexao.CreateCommand();
                deleteCmd.CommandText = "delete from Alunos where id = @id";

                IDbDataParameter paramId = new SqlParameter("id", id);
                deleteCmd.Parameters.Add(paramId);

                deleteCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conexao.Close();
            }

        }
    }
}