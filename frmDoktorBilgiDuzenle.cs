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
    public partial class frmDoktorBilgiDuzenle : Form
    {
        sqlbaglantisi s = new sqlbaglantisi();
        public string yedekAdd, yedekTcd;
        public frmDoktorBilgiDuzenle()
        {
            InitializeComponent();
        }

        private void frmDoktorBilgiDuzenle_Load(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Select BransAd from tblBrans", s.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
                cbBrans.Items.Add(dr[0]);
            s.baglanti().Close();
            SqlCommand komut2 = new SqlCommand("Select DoktorAd,DoktorSoyad,DoktorBrans,DoktorTc,DoktorSifre from tblDoktor where DoktorTc=@a1", s.baglanti());
            komut2.Parameters.AddWithValue("@a1", yedekTcd);
            SqlDataReader dr2 = komut2.ExecuteReader();
            if (dr2.Read())
            {
                txtAd.Text = dr2[0].ToString();
                txtSoyad.Text = dr2[1].ToString();
                cbBrans.Text = dr2[2].ToString();
                txtTc.Text = dr2[3].ToString();
                txtSifre.Text = dr2[4].ToString();
            }
            s.baglanti().Close();
        }

        private void btnBilgiGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Update tblDoktor set DoktorAd=@a1 , DoktorSoyad=@a2, DoktorBrans=@a3,DoktorSifre=@a4,DoktorTc=@a5 where DoktorTc=@a5 ", s.baglanti());
            komut.Parameters.AddWithValue("@a1",txtAd.Text);
            komut.Parameters.AddWithValue("@a2",txtSoyad.Text);
            komut.Parameters.AddWithValue("@a3",cbBrans.Text);
            komut.Parameters.AddWithValue("@a4",txtSifre.Text);
            komut.Parameters.AddWithValue("@a5",txtTc.Text);
            komut.ExecuteNonQuery();
            MessageBox.Show("Bilgileriniz Güncellenmiştir");
            s.baglanti().Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            frmDoktorDetay fr = new frmDoktorDetay();
            fr.add = yedekAdd;
            fr.tcd = yedekTcd;
            fr.Show();
            this.Hide();
        }
    }
}
