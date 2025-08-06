using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProGEs
{
    public partial class Tela_cad_alunos : Form
    {
        public Tela_cad_alunos()
        {
            InitializeComponent();
        }

        private void Tela_cad_alunos_Load(object sender, EventArgs e)
        {

        }
        public bool Tudo_em_ordem()
        {
            if (!txt_email.Equals(null) && !txt_nome.Equals(null) && !txt_senha.Equals(null) && !txt_endereco.Equals(null) && !txt_telefone.Equals(null) && !dtp_dataN.Equals(null) && !txt_matricula.Equals(null) && !txt_cpf.Equals(null) && !txt_rg.Equals(null) && !txt_turma.Equals(null))
            {
                return true;
            }
            else
            {
                return false;
            }

        } 

        public void Limpa_tudo()
        {
            txt_email.Clear();
            txt_nome.Clear();
            txt_senha.Clear();
            txt_endereco.Clear();
            txt_telefone.Clear();
            txt_matricula.Clear();
            txt_cpf.Clear();
            txt_rg.Clear();
            txt_matricula.Clear();
               
        }

        private void label11_Click(object sender, EventArgs e)
        {
            try
            {
                Usuarios user = new Usuarios();
                Alunos aluno = new Alunos();
                user.Nome = txt_nome.Text;
                user.Senha = txt_senha.Text;
                user.Email = txt_email.Text;
                user.Tipo = "aluno";
                bool usuarioCriado = user.CriarUser();

                aluno.Nome_aluno = txt_nome.Text;
                aluno.Cpf = txt_cpf.Text;
                aluno.Email = txt_email.Text;
                aluno.Endereco = txt_endereco.Text;
                aluno.Telefone = txt_telefone.Text;
                aluno.Rg = txt_rg.Text;
                aluno.Matricula = txt_matricula.Text;
                aluno.Data_nascimento = dtp_dataN.Value;

                aluno.Turma_id = 1;

                if (Tudo_em_ordem())
                {
                    if (usuarioCriado)
                    { 
                        aluno.CriarAluno();
                        MessageBox.Show("Aluno cadastrado com sucesso!!");
                        Limpa_tudo();
                    }
                }
                else
                {
                    MessageBox.Show("Erro!! /n Preencha os campos corretamente");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Erro ao cadastrar");
            }
        }
    }
}
