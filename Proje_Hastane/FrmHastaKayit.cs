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
using System.Security.Cryptography;

namespace Proje_Hastane
{
    public partial class FrmHastaKayit : Form
    {
        private void KontrolEt()
        {
            if (mskTcno.Text == "" || txtAd.Text == "" || txtSoyad.Text == "" ||
                txtSifre.Text == "" || mskTelno.Text == "" || cmbCinsiyet.Text == ""
                || mskTcno.Text == String.Empty || txtAd.Text == String.Empty || txtSoyad.Text == String.Empty
                || txtSifre.Text == String.Empty || mskTelno.Text == String.Empty || cmbCinsiyet.Text == String.Empty)
            {
                btnKayitol.Enabled = false;
            }
            else
            {
                btnKayitol.Enabled = true;
            }
        }


        public FrmHastaKayit()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmHastaGiris frmHasta = new FrmHastaGiris();
            frmHasta.Show();
            this.Hide();
        }

        private Veri_Tabani_Baglanti baglan = new Veri_Tabani_Baglanti();
        
        private void FrmHastaKayit_Load(object sender, EventArgs e)
        {
            KontrolEt();
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            KontrolEt();
        }

        private void btnKayitol_Click(object sender, EventArgs e)
        {
            SqlCommand komutKayit =
                    new SqlCommand(
                        "insert into Tbl_Hasta (HastaTC,HastaAd,HastaSoyad,HastaSifre,HastaTel,HastaCinsiyet) values (@p1,@p2,@p3,@p4,@p5,@p6)",
                        baglan.baglanti());

                komutKayit.Parameters.AddWithValue("@p1", mskTcno.Text);
                komutKayit.Parameters.AddWithValue("@p2", txtAd.Text);
                komutKayit.Parameters.AddWithValue("@p3", txtSoyad.Text);
                komutKayit.Parameters.AddWithValue("@p4", txtSifre.Text);
                komutKayit.Parameters.AddWithValue("@p5", mskTelno.Text);
                komutKayit.Parameters.AddWithValue("@p6", cmbCinsiyet.Text);
                komutKayit.ExecuteNonQuery();
                baglan.baglanti().Close();
                MessageBox.Show($"Kaydınız Gerçekleşti // Şifreniz : {txtSifre.Text}", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        
    }
}
