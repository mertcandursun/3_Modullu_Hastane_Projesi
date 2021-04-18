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
    public partial class FrmHastaDetay : Form
    {
        public FrmHastaDetay()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrmAnaGiris frmAna = new FrmAnaGiris();
            frmAna.Show();
            this.Hide();
        }
        public string tc;

        private Veri_Tabani_Baglanti baglan = new Veri_Tabani_Baglanti();

        private void FrmHastaDetay_Load(object sender, EventArgs e)
        {
            //Hasta Tc çekme

            lblTc.Text = tc;

            //Hasta Ad Soyad Çekme

            SqlCommand HastaisimCek = new SqlCommand("Select HastaAd,HastaSoyad From Tbl_Hasta where HastaTC = @p1",
                baglan.baglanti());
            HastaisimCek.Parameters.AddWithValue("@p1", lblTc.Text);
            SqlDataReader isimCek = HastaisimCek.ExecuteReader();

            while (isimCek.Read())
            {
                lblAdsoyad.Text = isimCek[0] + " " + isimCek[1];
            }
            baglan.baglanti().Close();

            //Randevu Geçmişi Çekme

            DataTable dt = new DataTable();
            SqlDataAdapter da =
                new SqlDataAdapter("Select * From Tbl_Randevu where HastaTC = " + tc, baglan.baglanti());
            da.Fill(dt);
            randevuGecmisiGrid.DataSource = dt;

            baglan.baglanti().Close();

            //Branşları çekme
            
            SqlCommand bransCek = new SqlCommand("Select BransAd From Tbl_Brans", baglan.baglanti());
            SqlDataReader bransOku = bransCek.ExecuteReader();

            while (bransOku.Read())
            {
                cmbBrans.Items.Add(bransOku[0]);
            }
            baglan.baglanti().Close();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void cmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbDoktor.Items.Clear();
            SqlCommand branstanDoktorCek =
                new SqlCommand("Select DoktorAd,DoktorSoyad From Tbl_Doktor where DoktorBrans = @p1",
                    baglan.baglanti());
            branstanDoktorCek.Parameters.AddWithValue("@p1", cmbBrans.SelectedItem);
            SqlDataReader doktorOku = branstanDoktorCek.ExecuteReader();

            while (doktorOku.Read())
            {
                cmbDoktor.Items.Add(doktorOku[0] + " " + doktorOku[1]);
            }

            baglan.baglanti().Close();
        }

        private void cmbDoktor_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable aktifRandevu = new DataTable();
            SqlDataAdapter aktifRandevular =
                new SqlDataAdapter("Select * From Tbl_Randevu where RandevuBrans='" + cmbBrans.SelectedItem + "'" + " and RandevuDoktor = '" + cmbDoktor.SelectedItem + "' and RandevuDurum=0",
                    baglan.baglanti());
            aktifRandevular.Fill(aktifRandevu);
            dataGridView2.DataSource = aktifRandevu;
        }

        private void lnkBilgiduzenle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmBilgiDuzenle frmBilgiDuzenle = new FrmBilgiDuzenle();
            frmBilgiDuzenle.TCno = lblTc.Text;
            frmBilgiDuzenle.Show();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView2.SelectedCells[0].RowIndex;
            txtId.Text = dataGridView2.Rows[secilen].Cells[0].Value.ToString();
        }

        private void btnRandevual_Click(object sender, EventArgs e)
        {
            SqlCommand randevuAl =
                new SqlCommand("Update Tbl_Randevu set RandevuDurum = 1, HastaTc = @p1, HastaSikayet = @p2 where RandevuId = @p3",
                    baglan.baglanti());
            randevuAl.Parameters.AddWithValue("@p1", lblTc.Text);
            randevuAl.Parameters.AddWithValue("@p2", txtSikayet.Text);
            randevuAl.Parameters.AddWithValue("@p3", txtId.Text);
            randevuAl.ExecuteNonQuery();
            baglan.baglanti().Close();
            MessageBox.Show("Randevu oluşturuldu.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
