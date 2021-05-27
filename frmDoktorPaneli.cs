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
    public partial class frmDoktorPaneli : Form
    {
        sqlbaglantisi s = new sqlbaglantisi();
        public string yedekTc, yedekAd;
        public frmDoktorPaneli()
        {
            InitializeComponent();
        }

        private void frmDoktorPaneli_Load(object sender, EventArgs e)
        {
            ListeGuncelle();
            SqlCommand komut = new SqlCommand("Select BransAd from tblBrans", s.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
                cbBrans.Items.Add(dr[0]);
            s.baglanti().Close();
        }

        private void ListeGuncelle()
        {
            txtAd.Text = "";
            txtSifre.Text = "";
            txtSoyad.Text = "";
            txtTc.Text = "";
            cbBrans.Text = "";
            SqlDataAdapter da = new SqlDataAdapter("Select DoktorAd as Ad,DoktorSoyad as Soyad,DoktorTc as Tc,DoktorBrans as Branş,DoktorSifre as Şifre,DoktorId as Id from tblDoktor", s.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            s.baglanti().Close();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtAd.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtSoyad.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtTc.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            cbBrans.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            txtSifre.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            label4.Text= dataGridView1.Rows[secilen].Cells[5].Value.ToString();

        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Insert into tblDoktor (DoktorAd,DoktorSoyad,DoktorTc,DoktorBrans,DoktorSifre) values (@d1,@d2,@d3,@d4,@d5)", s.baglanti());
            komut.Parameters.AddWithValue("@d1", txtAd.Text);
            komut.Parameters.AddWithValue("@d2", txtSoyad.Text);
            komut.Parameters.AddWithValue("@d3", txtTc.Text);
            komut.Parameters.AddWithValue("@d4", cbBrans.Text);
            komut.Parameters.AddWithValue("@d5", txtSifre.Text);
            komut.ExecuteNonQuery();
            MessageBox.Show("Doktor eklendi");
            s.baglanti().Close();
            ListeGuncelle();

        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Update tblDoktor set DoktorAd=@a1,DoktorSoyad=@a2,DoktorTc=@a3,DoktorBrans=@a4,DoktorSifre=@a5 where DoktorId=@a6", s.baglanti());
            komut.Parameters.AddWithValue("@a1", txtAd.Text);
            komut.Parameters.AddWithValue("@a2", txtSoyad.Text);
            komut.Parameters.AddWithValue("@a3", txtTc.Text);
            komut.Parameters.AddWithValue("@a4", cbBrans.Text);
            komut.Parameters.AddWithValue("@a5", txtSifre.Text);
            komut.Parameters.AddWithValue("@a6", label4.Text);

            komut.ExecuteNonQuery();
            MessageBox.Show("Doktor güncellendi");
            s.baglanti().Close();
            ListeGuncelle();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Delete from tblDoktor where DoktorId=@a3", s.baglanti());
            komut.Parameters.AddWithValue("@a3", label4.Text);
            komut.ExecuteNonQuery();
            MessageBox.Show("Doktor silindi");
            s.baglanti().Close();
            ListeGuncelle();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            frmSekreterDetay fr = new frmSekreterDetay();
            fr.ad = yedekAd;
            fr.TC = yedekTc;
            fr.Show();
            this.Hide();
        }
    }
}
