using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Professor
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Telefone { get; set; }
        public DateTime DataContratacao { get; set; }

        public List<Professor> ListarProfessores()
        {
            string caminhoArquivo = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data\BaseProf.json");

            string json = System.IO.File.ReadAllText(caminhoArquivo);

            List<Professor> listaProfessores = JsonConvert.DeserializeObject<List<Professor>>(json);

            return listaProfessores;
        }

        private bool ReescreverArquivo(List<Professor> professores)
        {
            string caminhoArquivo = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data\BaseProf.json");

            string json = JsonConvert.SerializeObject(professores, Formatting.Indented);

            File.WriteAllText(caminhoArquivo, json);

            return true;
        }

        public Professor Inserir(Professor professor)
        {
            List<Professor> listaProfessores = ListarProfessores();
            int maxId = listaProfessores.Max(p => p.Id);
            professor.Id = maxId + 1;

            listaProfessores.Add(professor);

            ReescreverArquivo(listaProfessores);

            return professor;
        }

        public Professor Atualizar(int id, Professor professor)
        {
            List<Professor> listaProfessores = ListarProfessores();
            int profIndex = listaProfessores.FindIndex(p => p.Id == id);
            if (profIndex >= 0)
            {
                professor.Id = id;
                listaProfessores[profIndex] = professor;
                ReescreverArquivo(listaProfessores);
            } else
            {
                professor = null;
            }

            return professor;
        }

        public bool Deletar(int id)
        {
            bool deletou = false;

            List<Professor> listaProfessores = ListarProfessores();
            Professor professor = listaProfessores.Where(p => p.Id == id).FirstOrDefault();
            if (professor != null)
            {
                listaProfessores.Remove(professor);
                ReescreverArquivo(listaProfessores);
                deletou = true;
            }

            return deletou;
        }
    }
}