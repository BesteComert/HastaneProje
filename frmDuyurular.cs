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
    public partial class frmDuyurular : Form
    {
        public string yedekTc, yedekAd;
        sqlbaglantisi s = new sqlbaglantisi();
        public frmDuyurular()
        {
            InitializeComponent();
        }
        private void frmDuyurular_Load(object sender, EventArgs e)
        {
            SqlDataAdapter komut = new SqlDataAdapter("Select Duyuru from tblDuyuru", s.baglanti());
            DataTable dt = new DataTable();
            komut.Fill(dt);
            dataGridView1.DataSource = dt;
            s.baglanti().Close();
        }
    }
}
