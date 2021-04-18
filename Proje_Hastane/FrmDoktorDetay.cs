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
    public partial class FrmDoktorDetay : Form
    {
        private Veri_Tabani_Baglanti baglan = new Veri_Tabani_Baglanti();
        public FrmDoktorDetay()
        {
            InitializeComponent();
        }

        public string tc;

        private void FrmDoktorDetay_Load(object sender, EventArgs e)
        {
            //Doktor tc kimlik no çekme
            lblTc.Text = tc;

            //Doktor ad soyad çekme
            SqlCommand doktorIsimcek =
                new SqlCommand("Select DoktorAd,DoktorSoyad From Tbl_Doktor where DoktorTc = @p1", baglan.baglanti());
            doktorIsimcek.Parameters.AddWithValue("@p1", tc);
            SqlDataReader isimOku = doktorIsimcek.ExecuteReader();
            while (isimOku.Read())
            {
                lblAdSoyad.Text = isimOku[0] + " " + isimOku[1];
            }
            baglan.baglanti().Close();

            //Randevular
            DataTable randevuListe = new DataTable();
            SqlDataAdapter randevuListele = new SqlDataAdapter("Select * From Tbl_Randevu where RandevuDoktor ='" + lblAdSoyad.Text + "'", baglan.baglanti());
            randevuListele.Fill(randevuListe);
            dataGridView1.DataSource = randevuListe;
            baglan.baglanti().Close();
        }

        private void btnBilgiDuzenle_Click(object sender, EventArgs e)
        {
            FrmDoktorBilgiDuzenle doktorBilgi = new FrmDoktorBilgiDuzenle();
            doktorBilgi.tc = lblTc.Text;
            doktorBilgi.Show();
        }

        private void btnDuyurular_Click(object sender, EventArgs e)
        {
            FrmDuyurular frmDuyuru = new FrmDuyurular();
            frmDuyuru.Show();
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtSikayet.Text = dataGridView1.Rows[secilen].Cells[7].Value.ToString();
        }
    }
}
