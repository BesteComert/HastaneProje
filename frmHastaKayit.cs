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
    public partial class frmHastaKayit : Form
    {
        sqlbaglantisi s = new sqlbaglantisi();
        public frmHastaKayit()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            frmHastaGiris fr = new frmHastaGiris();
            fr.Show();
            this.Hide();
        }

        private void btnKayit_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Insert into tblHasta (hastaad,hastasoyad,hastatc,hastatelefon,hastacinsiyet,hastasifre) values (@a1,@a2,@a3,@a4,@a5,@a6)", s.baglanti());
            komut.Parameters.AddWithValue("@a1", txtAd.Text);
            komut.Parameters.AddWithValue("@a2", txtSoyad.Text);
            komut.Parameters.AddWithValue("@a3", txtTc.Text);
            komut.Parameters.AddWithValue("@a4", txtTelefon.Text);
            komut.Parameters.AddWithValue("@a5", cbCinsiyet.Text);
            komut.Parameters.AddWithValue("@a6", txtSifre.Text);
            komut.ExecuteNonQuery();
            s.baglanti().Close();
            MessageBox.Show("Kayıt işlemi başarıyla tamamlanmıştır. Şifreniz :" + txtSifre.Text,"BİLGİ",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }
    }
}
