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
    public partial class FrmDoktorPanel : Form
    {
        public FrmDoktorPanel()
        {
            InitializeComponent();
        }

        private Veri_Tabani_Baglanti baglan = new Veri_Tabani_Baglanti();

        private void btnEkle_Click(object sender, EventArgs e)
        {
            SqlCommand doktorEkle = new SqlCommand("insert into Tbl_Doktor (DoktorTc,DoktorAd,DoktorSoyad,DoktorBrans,DoktorSifre) values (@p1,@p2,@p3,@p4,@p5)",baglan.baglanti());
            doktorEkle.Parameters.AddWithValue("@p1", mskTc.Text);
            doktorEkle.Parameters.AddWithValue("@p2", txtAd.Text);
            doktorEkle.Parameters.AddWithValue("@p3", txtSoyad.Text);
            doktorEkle.Parameters.AddWithValue("@p4", cmbBrans.SelectedItem);
            doktorEkle.Parameters.AddWithValue("@p5", txtSifre.Text);
            doktorEkle.ExecuteNonQuery();
            MessageBox.Show("Doktor Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void FrmDoktorPanel_Load(object sender, EventArgs e)
        {
            //Branşları cmb ye çekme
            SqlCommand bransCek = new SqlCommand("Select BransAd from Tbl_Brans",baglan.baglanti());
            SqlDataReader bransOku = bransCek.ExecuteReader();

            while (bransOku.Read())
            {
                cmbBrans.Items.Add(bransOku[0]);
            }

            baglan.baglanti().Close();

            //Doktorları DataGride Çekme
            DataTable doktorTablo = new DataTable();
            SqlDataAdapter doktorTabloBagla = new SqlDataAdapter("Select * From Tbl_Doktor", baglan.baglanti());
            doktorTabloBagla.Fill(doktorTablo);
            dataGridView1.DataSource = doktorTablo;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            mskTc.Text = "";
            txtAd.Text = "";
            txtSoyad.Text = "";
            txtSifre.Text = "";
            cmbBrans.SelectedItem = null;
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            DialogResult yesorno = new DialogResult();
            SqlCommand doktorSil = new SqlCommand("Delete From Tbl_Doktor where DoktorTC = @p1", baglan.baglanti());
            doktorSil.Parameters.AddWithValue("@p1", mskTc.Text);
            yesorno = MessageBox.Show("Doktor siliniyor emin misiniz ?", "Uyarı", MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);
            if (yesorno == DialogResult.Yes)
            {
                doktorSil.ExecuteNonQuery();
                MessageBox.Show("Doktor başarılı bir şekilde silindi", "Bilgi", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Doktor silme işlemi iptal edildi.","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }

            doktorSil.ExecuteNonQuery();
            baglan.baglanti().Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilenHucre = dataGridView1.SelectedCells[0].RowIndex;
            txtAd.Text = dataGridView1.Rows[secilenHucre].Cells[1].Value.ToString();
            txtSoyad.Text = dataGridView1.Rows[secilenHucre].Cells[2].Value.ToString();
            cmbBrans.Text = dataGridView1.Rows[secilenHucre].Cells[3].Value.ToString();
            mskTc.Text = dataGridView1.Rows[secilenHucre].Cells[4].Value.ToString();
            txtSifre.Text = dataGridView1.Rows[secilenHucre].Cells[5].Value.ToString();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand doktorGuncelle =
                new SqlCommand(
                    "Update Tbl_Doktor set DoktorAd = @p1, DoktorSoyAd = @p2, DoktorBrans = @p3, DoktorSifre = @p4 where DoktorTC = @p5",
                    baglan.baglanti());
            doktorGuncelle.Parameters.AddWithValue("@p1", txtAd.Text);
            doktorGuncelle.Parameters.AddWithValue("@p2", txtSoyad.Text);
            doktorGuncelle.Parameters.AddWithValue("@p3", cmbBrans.SelectedItem);
            doktorGuncelle.Parameters.AddWithValue("@p4", txtSifre.Text);
            doktorGuncelle.Parameters.AddWithValue("@p5", mskTc.Text);
            doktorGuncelle.ExecuteNonQuery();
            baglan.baglanti().Close();
            MessageBox.Show("Doktor güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
