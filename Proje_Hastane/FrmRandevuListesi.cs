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
    public partial class FrmRandevuListesi : Form
    {
        public FrmRandevuListesi()
        {
            InitializeComponent();
        }

        private Veri_Tabani_Baglanti baglan = new Veri_Tabani_Baglanti();

        private void FrmRandevuListesi_Load(object sender, EventArgs e)
        {
            DataTable randevuTablo = new DataTable();
            SqlDataAdapter randevuListele = new SqlDataAdapter("Select * From Tbl_Randevu", baglan.baglanti());
            randevuListele.Fill(randevuTablo);
            dataGridView1.DataSource = randevuTablo;
        }

        public int secilen;

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            secilen = dataGridView1.SelectedCells[0].RowIndex;
        }
    }
}
