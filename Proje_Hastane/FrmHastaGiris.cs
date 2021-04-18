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
    public partial class FrmHastaGiris : Form
    {
        public FrmHastaGiris()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmAnaGiris frmAna = new FrmAnaGiris();
            frmAna.Show();
            this.Hide();
        }

        private Veri_Tabani_Baglanti baglan = new Veri_Tabani_Baglanti();

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmHastaKayit frmHastakayit = new FrmHastaKayit();
            frmHastakayit.Show();
            this.Hide();
        }


        private void FrmHastaGiris_Load(object sender, EventArgs e)
        {
            
        }

        private void btnHastagiris_Click(object sender, EventArgs e)
        {
            SqlCommand komutGiris = new SqlCommand("Select * From Tbl_Hasta where HastaTc = @p1 and HastaSifre = @p2",baglan.baglanti());
            komutGiris.Parameters.AddWithValue("@p1", mskTcno.Text);
            komutGiris.Parameters.AddWithValue("@p2", txtHastasifre.Text);
            SqlDataReader sifreOku = komutGiris.ExecuteReader();

            if (sifreOku.Read())
            {
                FrmHastaDetay frmHastadetay = new FrmHastaDetay();
                frmHastadetay.tc = mskTcno.Text;
                frmHastadetay.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı TC Kimlik veya Şifre !", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            baglan.baglanti().Close();
        }
    }
}
