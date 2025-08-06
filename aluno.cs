using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace ProGEs
{
    class Alunos : Usuarios
    {
        private int id_aluno;
        private string nome;
        private string matricula;
        private string cpf;
        private string rg;
        private DateTime data_nascimento;
        private string endereco;
        private string telefone;
        private string email;
        private int turma_id;
  


        public int Id_aluno
        {
            get { return id_aluno; }
            set { id_aluno = value; }
        }
        public int Turma_id
        {
            get { return turma_id; }
            set { turma_id = value; }
        }
        public string Nome_aluno
        {
            get { return nome; }
            set { nome = value; }
        }
        public string Matricula
        {
            get { return matricula; }
            set { matricula = value; }
        }
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        public string Cpf
        {
            get { return cpf; }
            set { cpf = value; }
        }
        public DateTime Data_nascimento
        {
            get { return data_nascimento; }
            set { data_nascimento = value; }
        } 
        public string Endereco
        {
            get { return endereco; }
            set { endereco = value; }
        }
        public string Rg
        {
            get { return rg; }
            set { rg = value; }
        }
        public string Telefone
        {
            get { return telefone; }
            set { telefone = value; }
        }
       



        public bool CriarAluno()
        {
            try
            {
                using (MySqlConnection conexaoBanco = new BancoDb().Conectar())
                {
                    string inserir = "INSERT INTO alunos (id_aluno, nome_completo, matricula, cpf, rg, data_nascimento, endereco, telefone, email_pessoal, turma_id) value (@id_aluno, @nome_completo, @matricula, @cpf, @rg, @data_nascimento, @endereco, @telefone, @email_pessoal, @turma_id)";

                    MySqlCommand comando = new MySqlCommand(inserir, conexaoBanco);
                    comando.Parameters.AddWithValue("@id_aluno", Id_aluno);
                    comando.Parameters.AddWithValue("@nome_completo", Nome_aluno);
                    comando.Parameters.AddWithValue("@matricula", Matricula);
                    comando.Parameters.AddWithValue("@cpf", Cpf);
                    comando.Parameters.AddWithValue("@rg", Rg);
                    comando.Parameters.AddWithValue("@data_nascimento", Data_nascimento);
                    comando.Parameters.AddWithValue("@endereco", Endereco);
                    comando.Parameters.AddWithValue("@telefone", Telefone);
                    comando.Parameters.AddWithValue("@email_pessoal", Email);
                    comando.Parameters.AddWithValue("@turma_id", Turma_id);
                    
                    

                    int resultado = comando.ExecuteNonQuery();

                    if (resultado > 0)
                    {
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Não foi possível cadastrar o usuário. Verifique os dados e tente novamente.", "Falha no Cadastro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao cadastrar usuário: " + ex.Message, "Erro - Cadastro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

        }
    }
}

