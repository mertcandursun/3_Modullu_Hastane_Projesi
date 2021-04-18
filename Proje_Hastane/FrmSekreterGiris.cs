using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proje_Hastane
{
    public partial class FrmSekreterGiris : Form
    {
        public FrmSekreterGiris()
        {
            InitializeComponent();
        }

        private Veri_Tabani_Baglanti baglan = new Veri_Tabani_Baglanti();

        private void btnHastagiris_Click(object sender, EventArgs e)
        {
            SqlCommand komutSekreterGiris =
                new SqlCommand("Select * From Tbl_Sekreter where SekreterTc = @p1 and SekreterSifre = @p2",
                    baglan.baglanti());
            komutSekreterGiris.Parameters.AddWithValue("@p1", mskTcno.Text);
            komutSekreterGiris.Parameters.AddWithValue("@p2", txtSekreterSifre.Text);

            SqlDataReader sekreterSifreOku = komutSekreterGiris.ExecuteReader();

            if (sekreterSifreOku.Read())
            {
                FrmSekreterDetay frmSekDetay = new FrmSekreterDetay();
                frmSekDetay.tc = mskTcno.Text;
                frmSekDetay.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı TC Kimlik veya Şifre !", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            baglan.baglanti().Close();
        }

        private void FrmSekreterGiris_Load(object sender, EventArgs e)
        {

        }
    }
}
