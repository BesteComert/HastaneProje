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
    public partial class frmDoktorDetay : Form
    {
        sqlbaglantisi s = new sqlbaglantisi();
        public string add, tcd;
        public frmDoktorDetay()
        {
            InitializeComponent();
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            frmDoktorGiris fr = new frmDoktorGiris();
            fr.Show();
            this.Hide();
        }

        private void btnBilgiDuzenle_Click(object sender, EventArgs e)
        {
            frmDoktorBilgiDuzenle fr = new frmDoktorBilgiDuzenle();
            fr.yedekAdd = add;
            fr.yedekTcd = tcd;
            fr.Show();
            this.Hide();
        }

        private void frmDoktorDetay_Load(object sender, EventArgs e)
        {
            lblAd.Text = add;
            lblTc.Text = tcd;

            SqlDataAdapter komut = new SqlDataAdapter("Select RandevuId as Id,RandevuTarih As Tarih, RandevuSaat as Saat, HastaSikayet as Şikayet from tblrandevu Where RandevuDoktor='"+add+ "' and RandevuDurum='" + true + "' ", s.baglanti());
            DataTable dt = new DataTable();
            komut.Fill(dt);
            dataGridView1.DataSource = dt;
            s.baglanti().Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtSikayet.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
        }

        private void btnDuyurular_Click(object sender, EventArgs e)
        {
            frmDuyurular fr = new frmDuyurular();
            fr.Show();
        }
    }
}
