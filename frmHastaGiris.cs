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
    public partial class frmHastaGiris : Form
    {
        sqlbaglantisi s = new sqlbaglantisi();
        public frmHastaGiris()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmHastaKayit fr = new frmHastaKayit();
            fr.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            frmAnaGiris fr = new frmAnaGiris();
            fr.Show();
            this.Hide();
        }

        private void frmHastaGiris_Load(object sender, EventArgs e)
        {

        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("select HastaId,HastaTc,HastaAd,HastaSoyad from tblHasta where HastaTc=@a1 and HastaSifre=@a2", s.baglanti());
            komut.Parameters.AddWithValue("@a1", txtTc.Text);
            komut.Parameters.AddWithValue("@a2",txtSifre.Text);

            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                frmHastaDetay fr = new frmHastaDetay();
                fr.TC = txtTc.Text;
                fr.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı Tc veya şifre girdiniz. Lütfen Tekrar deneyiniz.");
                txtSifre.Clear();
                txtTc.Clear();
            }
            s.baglanti().Close();
        }
    }
}
