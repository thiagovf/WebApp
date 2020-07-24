using App.Domain;
using App.Repository;
using System;
using System.Collections.Generic;

namespace WebApp.Models
{
    public class AlunoModel
    {
        public List<AlunoDTO> ListarAlunos(int? id = null)
        {
            try
            {
                AlunoDAO alunoDAO = new AlunoDAO();
                return alunoDAO.ListarAlunos(id);
            }
            catch (Exception e)
            {

                throw new Exception($"Erro ao listar Alunos: Erro => {e.Message}");
            }
        }

        public void Inserir(AlunoDTO aluno)
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

        public void Atualizar(int id, AlunoDTO aluno)
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