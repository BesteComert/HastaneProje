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
    public partial class frmDoktorGiris : Form
    {
        sqlbaglantisi s = new sqlbaglantisi();
        public frmDoktorGiris()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            frmAnaGiris fr = new frmAnaGiris();
            fr.Show();
            this.Hide();
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Select DoktorAd,DoktorSoyad from tblDoktor where DoktorTc=@a1 and DoktorSifre=@a2", s.baglanti());
            komut.Parameters.AddWithValue("@a1", txtTc.Text);
            komut.Parameters.AddWithValue("@a2", txtSifre.Text);
            SqlDataReader dr = komut.ExecuteReader();
            if(dr.Read())
            {
                frmDoktorDetay fr = new frmDoktorDetay();
                fr.tcd = txtTc.Text;
                fr.add = dr[0] + " " + dr[1];
                fr.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı Tc veya şifre girdiniz. Lütfen Tekrar deneyiniz.");
                txtSifre.Clear();
                txtTc.Clear();
            }
        }
    }
}
