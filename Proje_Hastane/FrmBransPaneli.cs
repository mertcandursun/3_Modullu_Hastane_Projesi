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
    public partial class FrmBransPaneli : Form
    {
        public FrmBransPaneli()
        {
            InitializeComponent();
        }

        private Veri_Tabani_Baglanti baglan = new Veri_Tabani_Baglanti();
        private void FrmBransPaneli_Load(object sender, EventArgs e)
        {
            DataTable bransTablo = new DataTable();
            SqlDataAdapter bransTabloolustur = new SqlDataAdapter("Select * From Tbl_Brans", baglan.baglanti());
            bransTabloolustur.Fill(bransTablo);
            dataGridView1.DataSource = bransTablo;
            baglan.baglanti().Close();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            SqlCommand bransEkle = new SqlCommand("Insert into Tbl_Brans (BransAd) values (@p1)",baglan.baglanti());
            bransEkle.Parameters.AddWithValue("@p1", txtBrans.Text);
            bransEkle.ExecuteNonQuery();
            baglan.baglanti().Close();
            MessageBox.Show("Branş eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtID.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtBrans.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            DialogResult yesorno = new DialogResult();
            SqlCommand bransSil = new SqlCommand("Delete From Tbl_Brans where BransId = @p1", baglan.baglanti());
            bransSil.Parameters.AddWithValue("@p1", txtID.Text);
            yesorno = MessageBox.Show("Branş siliniyor emin misiniz ? ", "Uyarı", MessageBoxButtons.YesNo,
                MessageBoxIcon.Information);
            if (yesorno == DialogResult.Yes)
            {
                bransSil.ExecuteNonQuery();
                MessageBox.Show("Branş silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Branş silme iptal edildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
            
            
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand bransGuncelle = new SqlCommand("Update Tbl_Brans set BransAd = @p1 where BransId = @p2", baglan.baglanti());
            bransGuncelle.Parameters.AddWithValue("@p1", txtBrans.Text);
            bransGuncelle.Parameters.AddWithValue("@p2", txtID.Text);
            bransGuncelle.ExecuteNonQuery();
            baglan.baglanti().Close();
            MessageBox.Show("Branş güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
