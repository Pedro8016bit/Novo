using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProGEs
{
    public partial class TelaEntrar : UserControl
    {
        public TelaEntrar()
        {
            InitializeComponent();
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            Usuarios usuarioLogado = Usuarios.FazerLogin(txtEmail.Text, txtSenha.Text);

            if (usuarioLogado != null)
            {
                MessageBox.Show("Login bem-sucedido!\nBem-vindo, " + usuarioLogado.Nome);

                this.Hide(); // Esconde a tela de login

                if (usuarioLogado.Tipo == "gestor")
                    new Dashboard_Gestor(usuarioLogado).Show();
                else if (usuarioLogado.Tipo == "professor")
                    new Dashboard_Professor(usuarioLogado).Show();
                else if (usuarioLogado.Tipo == "aluno")
                    new Dashboard_Aluno(usuarioLogado).Show();
            }
            else
            {
                MessageBox.Show("Usuário ou senha incorretos.");
                txtEmail.Clear();
                txtSenha.Clear();
            }

            Form tela = this.FindForm();
            tela.Hide();
        }

        private void lblEsqueciSenha_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
    }
}
