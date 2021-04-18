using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Proje_Hastane
{
    public partial class FrmDoktorGiris : Form
    {
        private Veri_Tabani_Baglanti baglan = new Veri_Tabani_Baglanti();
        public FrmDoktorGiris()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmAnaGiris frmAna = new FrmAnaGiris();
            frmAna.Show();
            this.Hide();
        }

        private void btnHastagiris_Click(object sender, EventArgs e)
        {
            SqlCommand doktorGiris = new SqlCommand("Select * From Tbl_Doktor where DoktorTc = @p1 and DoktorSifre = @p2",
                baglan.baglanti());
            doktorGiris.Parameters.AddWithValue("@p1", mskTcno.Text);
            doktorGiris.Parameters.AddWithValue("@p2", txtDoktorSifre.Text);
            SqlDataReader sifreOku = doktorGiris.ExecuteReader();
            if (sifreOku.Read())
            {
                FrmDoktorDetay doktorDetay = new FrmDoktorDetay();
                doktorDetay.tc = mskTcno.Text;
                doktorDetay.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("TC kimlik veya şifre yanlış","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            baglan.baglanti().Close();
        }
    }
}
