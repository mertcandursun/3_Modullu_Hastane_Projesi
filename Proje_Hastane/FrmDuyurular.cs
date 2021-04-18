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
    public partial class FrmDuyurular : Form
    {
        public FrmDuyurular()
        {
            InitializeComponent();
        }

        private Veri_Tabani_Baglanti baglan = new Veri_Tabani_Baglanti();

        private void FrmDuyurular_Load(object sender, EventArgs e)
        {
            DataTable duyuruTablo = new DataTable();
            SqlDataAdapter duyuruTablobagla = new SqlDataAdapter("Select * From Tbl_Duyuru", baglan.baglanti());
            duyuruTablobagla.Fill(duyuruTablo);
            dataGridView1.DataSource = duyuruTablo;
        }
    }
}
