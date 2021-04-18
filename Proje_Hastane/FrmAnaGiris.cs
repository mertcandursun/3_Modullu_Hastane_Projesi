using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proje_Hastane
{
    public partial class FrmAnaGiris : Form
    {
        public FrmAnaGiris()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnHasta_Click(object sender, EventArgs e)
        {
            FrmHastaGiris frmHasta = new FrmHastaGiris();
            frmHasta.Show();
            this.Hide();
        }

        private void btnDoktor_Click(object sender, EventArgs e)
        {
            FrmDoktorGiris frmDoktor = new FrmDoktorGiris();
            frmDoktor.Show();
            this.Hide();
        }

        private void btnSekreter_Click(object sender, EventArgs e)
        {
            FrmSekreterGiris frmSekreter = new FrmSekreterGiris();
            frmSekreter.Show();
            this.Hide();
        }
    }
}
