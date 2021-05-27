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
    public partial class frmSekreterGiris : Form
    {
        sqlbaglantisi s = new sqlbaglantisi();
        public frmSekreterGiris()
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
            SqlCommand komut = new SqlCommand("Select SekreterAdSoyad from tblSekreter where SekreterTc=@a1 and SekreterSifre=@a2", s.baglanti());
            komut.Parameters.AddWithValue("@a1",txtTc.Text);
            komut.Parameters.AddWithValue("@a2", txtSifre.Text);

            SqlDataReader dr = komut.ExecuteReader();
            if(dr.Read())
            {
                frmSekreterDetay fr = new frmSekreterDetay();
                fr.TC = txtTc.Text;
                fr.ad = dr[0].ToString();
                fr.Show();
                this.Close();
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
