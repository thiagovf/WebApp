﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

        public List<Aluno> ListarAlunos()
        {
            string caminhoArquivo = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data\Base.json");

            string json = System.IO.File.ReadAllText(caminhoArquivo);

            List<Aluno> listaAlunos = JsonConvert.DeserializeObject<List<Aluno>>(json);

            return listaAlunos;
        }

        

        public List<Aluno> ListarAlunosDB()
        {
            string stringConexao = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\dev\source\repos\WebApp\WebApp\App_Data\Database.mdf;Integrated Security=True";
            IDbConnection conexao;
            conexao = new SqlConnection(stringConexao);
            conexao.Open();

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

        private bool ReescreverArquivo(List<Aluno> listaAlunos)
        {
            string caminhoArquivo = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data\Base.json");

            string json = JsonConvert.SerializeObject(listaAlunos, Formatting.Indented);

            File.WriteAllText(caminhoArquivo, json);

            return true;
        }

        public Aluno Inserir(Aluno Aluno)
        {
            List<Aluno> listaAlunos = this.ListarAlunos();
            int maxId = listaAlunos.Max(aluno => aluno.Id);
            Aluno.Id = maxId + 1;
            listaAlunos.Add(Aluno);

            ReescreverArquivo(listaAlunos);
            return Aluno;
        }

        public Aluno Atualizar(int id, Aluno Aluno)
        {
            List<Aluno> listaAlunos = ListarAlunos();

            int itemIndice = listaAlunos.FindIndex(aluno => aluno.Id == id);
            if(itemIndice >= 0)
            {
                Aluno.Id = id;
                listaAlunos[itemIndice] = Aluno;

                ReescreverArquivo(listaAlunos);
            } else
            {
                Aluno = null;
            }

            return Aluno;
        }

        public bool Deletar(int id)
        {
            bool deletou = false;

            List<Aluno> listaAlunos = ListarAlunos();
            int itemIndex = listaAlunos.FindIndex(aluno => aluno.Id == id);
            if (itemIndex > 0)
            {
                listaAlunos.RemoveAt(itemIndex);
                ReescreverArquivo(listaAlunos);
                deletou = true;
            }

            return deletou;
        }
    }
}