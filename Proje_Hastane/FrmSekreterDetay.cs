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
    public partial class FrmSekreterDetay : Form
    {
        public FrmSekreterDetay()
        {
            InitializeComponent();
        }

        private void lblTc_Click(object sender, EventArgs e)
        {
            
        }

        public string tc;

        private Veri_Tabani_Baglanti baglan = new Veri_Tabani_Baglanti();

        private void FrmSekreterDetay_Load(object sender, EventArgs e)
        {

            //Sekreter Tc çekme
            lblTc.Text = tc;

            //Sekreter AdSoyad Çekme
            SqlCommand SekreterisimCek = new SqlCommand("Select SekreterAdSoyad From Tbl_Sekreter where SekreterTc = @p1",baglan.baglanti());
            SekreterisimCek.Parameters.AddWithValue("@p1", lblTc.Text);
            SqlDataReader oku = SekreterisimCek.ExecuteReader();

            while (oku.Read())
            {
                lblAdSoyad.Text = oku[0].ToString();
            }
            baglan.baglanti().Close();

            //Branşları Datagride aktarma
            DataTable bransTablo = new DataTable();
            SqlDataAdapter bransCek = new SqlDataAdapter("Select * From Tbl_Brans", baglan.baglanti());
            bransCek.Fill(bransTablo);
            dataGridView2.DataSource = bransTablo;

            //Doktor bilgi çekme
            DataTable doktorBilgi = new DataTable();
            SqlDataAdapter doktorBilgiCek =
                new SqlDataAdapter("Select (DoktorAd + ' ' + DoktorSoyad) as 'Doktor İsim' ,DoktorBrans From Tbl_Doktor",
                    baglan.baglanti());
            doktorBilgiCek.Fill(doktorBilgi);
            dataGridView1.DataSource = doktorBilgi;

            //Branşı comboBox'a aktarma 

            SqlCommand bransListeCek = new SqlCommand("Select BransAd From Tbl_Brans", baglan.baglanti());
            SqlDataReader bransOku = bransListeCek.ExecuteReader();
            while (bransOku.Read())
            {
                cmbBrans.Items.Add(bransOku[0]);
            }
            baglan.baglanti().Close();

        }

        private void btnDoktor_Click(object sender, EventArgs e)
        {
            FrmDoktorPanel doktorPanelAc = new FrmDoktorPanel();
            doktorPanelAc.Show();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand randevuKaydet =
                new SqlCommand(
                    "insert into Tbl_Randevu (RandevuTarih, RandevuSaat, RandevuBrans, RandevuDoktor) values (@p1,@p2,@p3,@p4)",
                    baglan.baglanti());
            randevuKaydet.Parameters.AddWithValue("@p1", mskTarih.Text);
            randevuKaydet.Parameters.AddWithValue("@p2", mskSaat.Text);
            randevuKaydet.Parameters.AddWithValue("@p3", cmbBrans.SelectedItem);
            randevuKaydet.Parameters.AddWithValue("@p4", cmbDoktor.SelectedItem);
            randevuKaydet.ExecuteNonQuery();
            baglan.baglanti().Close();
            MessageBox.Show("Randevu Oluşturuldu","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void cmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbDoktor.Items.Clear();
            SqlCommand bransDoktorCek =
                new SqlCommand("Select DoktorAd,DoktorSoyad From Tbl_Doktor where DoktorBrans = @p1",
                    baglan.baglanti());
            bransDoktorCek.Parameters.AddWithValue("@p1", cmbBrans.SelectedItem);
            SqlDataReader bransDoktorOku = bransDoktorCek.ExecuteReader();
            while (bransDoktorOku.Read())
            {
                cmbDoktor.Items.Add(bransDoktorOku[0] + " " + bransDoktorOku[1]);
            }
            baglan.baglanti().Close();
        }

        private void btnDuyuruOlustur_Click(object sender, EventArgs e)
        {
            SqlCommand duyuruOlustur =
                new SqlCommand("Insert into Tbl_Duyuru (Duyuru) values (@p1)", baglan.baglanti());
            duyuruOlustur.Parameters.AddWithValue("@p1", rchDuyuru.Text);
            duyuruOlustur.ExecuteNonQuery();
            baglan.baglanti().Close();
            MessageBox.Show("Duyuru Oluşturuldu", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnRandevu_Click(object sender, EventArgs e)
        {
            FrmRandevuListesi randevuListe = new FrmRandevuListesi();
            randevuListe.Show();
        }

        private void btnBrans_Click(object sender, EventArgs e)
        {
            FrmBransPaneli frmBransPanel = new FrmBransPaneli();
            frmBransPanel.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmDuyurular duyuruTablosu = new FrmDuyurular();
            duyuruTablosu.Show();
        }
    }
}
