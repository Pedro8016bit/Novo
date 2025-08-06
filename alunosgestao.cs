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
    public partial class Alunos_Gestao : UserControl
    {
        public Alunos_Gestao()
        {
            InitializeComponent();
        }

        private void btn_editar_Click(object sender, EventArgs e)
        {

        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            Tela_cad_alunos tela = new Tela_cad_alunos();
            tela.Show();

        }
    }
}
