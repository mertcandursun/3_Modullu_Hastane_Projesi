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
    public partial class FrmDoktorBilgiDuzenle : Form
    {
        private Veri_Tabani_Baglanti baglan = new Veri_Tabani_Baglanti();

        public string tc;
        public FrmDoktorBilgiDuzenle()
        {
            InitializeComponent();
        }

        private void FrmDoktorBilgiDuzenle_Load(object sender, EventArgs e)
        {
            //Doktor tc çekme
            mskTc.Text = tc;

            SqlCommand doktorBilgi = new SqlCommand("Select * From Tbl_Doktor where DoktorTc = @p1", baglan.baglanti());
            doktorBilgi.Parameters.AddWithValue("@p1", tc);
            SqlDataReader doktorBilgioku = doktorBilgi.ExecuteReader();
            while (doktorBilgioku.Read())
            {
                txtAd.Text = doktorBilgioku[1].ToString();
                txtSoyad.Text = doktorBilgioku[2].ToString();
                cmbBrans.Text = doktorBilgioku[3].ToString();
                txtSifre.Text = doktorBilgioku[5].ToString();
            }
            baglan.baglanti().Close();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand doktorGuncelle =
                new SqlCommand(
                    "Update Tbl_Doktor set DoktorAd = @p1, DoktorSoyad = @p2, DoktorSifre = @p3, DoktorBrans = @p4 where DoktorTc = @p5",
                    baglan.baglanti());
            doktorGuncelle.Parameters.AddWithValue("@p1", txtAd.Text);
            doktorGuncelle.Parameters.AddWithValue("@p2", txtSoyad.Text);
            doktorGuncelle.Parameters.AddWithValue("@p3", txtSifre.Text);
            doktorGuncelle.Parameters.AddWithValue("@p4", cmbBrans.Text);
            doktorGuncelle.Parameters.AddWithValue("@p5", mskTc.Text);
            doktorGuncelle.ExecuteNonQuery();
            baglan.baglanti().Close();
            MessageBox.Show("Doktor güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
