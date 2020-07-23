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

        public List<Aluno> ListarAlunosDB()
        {
            List<Aluno> alunos = new List<Aluno>();

            IDbCommand selectCmd = conexao.CreateCommand();
            selectCmd.CommandText = "select * from Alunos";
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
    }
}