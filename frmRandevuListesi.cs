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

namespace WFAProjeHasta
{
    public partial class frmRandevuListesi : Form
    {
        sqlbaglantisi s = new sqlbaglantisi();
        public frmRandevuListesi()
        {
            InitializeComponent();
        }

        private void frmRandevuListesi_Load(object sender, EventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * from tblRandevu", s.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            s.baglanti().Close();
        }
        public string id;
     

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            frmSekreterDetay fr = new frmSekreterDetay();
            fr.randevuid = id;
            fr.Show();
            this.Hide();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            id = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
        }
    }
}
