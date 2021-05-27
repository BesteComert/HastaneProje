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
    public partial class frmBransPaneli : Form
    {
        sqlbaglantisi s = new sqlbaglantisi();
        public frmBransPaneli()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            frmSekreterDetay fr = new frmSekreterDetay();
            fr.Show();
            this.Hide();
        }

        private void frmBransPaneli_Load(object sender, EventArgs e)
        {
            ListeGuncelle();
        }
        private void ListeGuncelle()
        {
            SqlDataAdapter komut = new SqlDataAdapter("Select * from tblBrans", s.baglanti());
            DataTable dt = new DataTable();
            komut.Fill(dt);
            dataGridView1.DataSource = dt;
            s.baglanti().Close();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Insert into tblBrans (BransAd) values (@b1)", s.baglanti());
            komut.Parameters.AddWithValue("@b1", txtAd.Text);
            komut.ExecuteNonQuery();
            MessageBox.Show("Branş eklendi");
            s.baglanti().Close();
            ListeGuncelle();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Delete from tblBrans where BransId=@a", s.baglanti());
            komut.Parameters.AddWithValue("@a", txtId.Text);
            komut.ExecuteNonQuery();
            MessageBox.Show("Branş silindi");
            s.baglanti().Close();
            ListeGuncelle();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Update tblBrans set BransAd=@a where BransId=@b", s.baglanti());
            komut.Parameters.AddWithValue("@a", txtAd.Text);
            komut.Parameters.AddWithValue("@b", txtId.Text);
            komut.ExecuteNonQuery();
            MessageBox.Show("Branş güncellendi");
            s.baglanti().Close();
            ListeGuncelle();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtId.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();

        }
    }
}
