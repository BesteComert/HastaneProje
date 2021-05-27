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
    public partial class frmHastaBilgiDuzenle : Form
    {
        public string tc;
        sqlbaglantisi s = new sqlbaglantisi();
        public frmHastaBilgiDuzenle()
        {
            InitializeComponent();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            frmHastaDetay fr = new frmHastaDetay();
            fr.TC = tc;
            fr.Show();
            this.Hide();
        }

        private void frmHastaBilgiDuzenle_Load(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Select * from tblHasta where HastaTc=@a", s.baglanti());
            komut.Parameters.AddWithValue("@a", tc);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                txtAd.Text = dr[1].ToString();
                txtSoyad.Text = dr[2].ToString();
                txtTelefon.Text = dr[4].ToString();
                txtSifre.Text = dr[5].ToString();
                cbCinsiyet.Text = dr[6].ToString();
                txtTc.Text = dr[3].ToString();
            }
        }

        private void btnBilgiGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update tblHasta set HastaAd=@a1, HastaSoyad=@a2, HastaTelefon=@a3,HAstaSifre=@a4,HastaCinsiyet=@a6 where HastaTc=@a5", s.baglanti());
            komut.Parameters.AddWithValue("@a1",txtAd.Text);
            komut.Parameters.AddWithValue("@a2",txtSoyad.Text);
            komut.Parameters.AddWithValue("@a3",txtTelefon.Text);
            komut.Parameters.AddWithValue("@a4",txtSifre.Text);
            komut.Parameters.AddWithValue("@a5",tc);
            komut.Parameters.AddWithValue("@a6",cbCinsiyet.Text);

            komut.ExecuteNonQuery();
            s.baglanti().Close();
            MessageBox.Show("Bilgileriniz Güncellenmiştir!","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);

        }
    }
}
