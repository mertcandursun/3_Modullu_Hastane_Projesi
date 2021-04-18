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
    public partial class FrmBilgiDuzenle : Form
    {
        public FrmBilgiDuzenle()
        {
            InitializeComponent();
        }

        private Veri_Tabani_Baglanti baglan = new Veri_Tabani_Baglanti();

        public string TCno;

        private void FrmBilgiDuzenle_Load(object sender, EventArgs e)
        {
            mskTcno.Text = TCno;
            SqlCommand hastaVeriCek = new SqlCommand("Select * From Tbl_Hasta where HastaTc = @p1", baglan.baglanti());
            hastaVeriCek.Parameters.AddWithValue("@p1", mskTcno.Text);
            SqlDataReader veriOku = hastaVeriCek.ExecuteReader();

            while (veriOku.Read())
            {
                txtAd.Text = veriOku[1].ToString();
                txtSoyad.Text = veriOku[2].ToString();
                mskTelno.Text = veriOku[4].ToString();
                txtSifre.Text = veriOku[5].ToString();
                cmbCinsiyet.Text = veriOku[6].ToString();
            }

            baglan.baglanti().Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlCommand hastaVeriGuncelle =
                new SqlCommand(
                    "Update Tbl_Hasta set HastaAd = @p1, HastaSoyad = @p2, HastaTel = @p3, HastaSifre = @p4, HastaCinsiyet = @p5 where HastaTc = @p6",
                    baglan.baglanti());
            hastaVeriGuncelle.Parameters.AddWithValue("@p1", txtAd.Text);
            hastaVeriGuncelle.Parameters.AddWithValue("@p2", txtSoyad.Text);
            hastaVeriGuncelle.Parameters.AddWithValue("@p3", mskTelno.Text);
            hastaVeriGuncelle.Parameters.AddWithValue("@p4", txtSifre.Text);
            hastaVeriGuncelle.Parameters.AddWithValue("@p5", cmbCinsiyet.SelectedItem);
            hastaVeriGuncelle.Parameters.AddWithValue("@p6", mskTcno.Text);
            hastaVeriGuncelle.ExecuteNonQuery();
            baglan.baglanti().Close();
            MessageBox.Show("Bilgileriniz Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
