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
    public partial class frmSekreterDetay : Form
    {
        public string TC,ad,randevuid;
        sqlbaglantisi s = new sqlbaglantisi();
        public frmSekreterDetay()
        {
            InitializeComponent();
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            frmSekreterGiris fr = new frmSekreterGiris();
            fr.Show();
            this.Hide();
        }

        private void cbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbDoktor.Items.Clear();
            SqlCommand komut = new SqlCommand("Select DoktorAd,DoktorSoyad from tblDoktor where DoktorBrans=@a", s.baglanti());
            komut.Parameters.AddWithValue("@a", cbBrans.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while(dr.Read())
            {
                cbDoktor.Items.Add(dr[0]+" " + dr[1]);
            }
            s.baglanti().Close();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Insert into tblRandevu (RandevuTarih,RandevuSaat,RandevuDoktor,RandevuBrans) values (@r1,@r2,@r3,@r4)", s.baglanti());
            komut.Parameters.AddWithValue("@r1",txtTarih.Text);
            komut.Parameters.AddWithValue("@r2",txtSaat.Text);
            komut.Parameters.AddWithValue("@r3",cbDoktor.Text);
            komut.Parameters.AddWithValue("@r4",cbBrans.Text);

            komut.ExecuteNonQuery();
            MessageBox.Show("Randevu kaydedildi");
            s.baglanti().Close();
            //txtTarih.Text = "";
            //txtSaat.Text = "";
            //cbBrans.Text = "";
            //cbDoktor.Items.Clear();
            //cbDoktor.Text = "";
            //checkBox1.Checked = false;
        }

        private void btnOlustur_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Insert into tblduyuru (Duyuru) values (@d)", s.baglanti());
            komut.Parameters.AddWithValue("@d",txtDuyuru.Text);
            komut.ExecuteNonQuery();
            s.baglanti().Close();
            MessageBox.Show("Duyuru kaydedildi");

        }

        private void btnDoktorPaneli_Click(object sender, EventArgs e)
        {
            frmDoktorPaneli fr = new frmDoktorPaneli();
            fr.yedekAd = ad;
            fr.yedekTc = TC;
            fr.Show();
            this.Hide();
        }

        private void btnBransPaneli_Click(object sender, EventArgs e)
        {
            frmBransPaneli fr = new frmBransPaneli();
            fr.Show();
            this.Hide();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Update tblRandevu set RandevuTarih=@a1,RandevuSaat=@a2,RandevuBrans=@a3,RandevuDoktor=@a4,RandevuDurum=@a5,HastaTc=@a6 where RandevuId=@a7", s.baglanti());
            komut.Parameters.AddWithValue("@a1",txtTarih.Text);
            komut.Parameters.AddWithValue("@a2",txtSaat.Text);
            komut.Parameters.AddWithValue("@a3",cbBrans.Text);
            komut.Parameters.AddWithValue("@a4",cbDoktor.Text);
            komut.Parameters.AddWithValue("@a5",checkBox1.Checked);
            komut.Parameters.AddWithValue("@a6",txtTc.Text);
            komut.Parameters.AddWithValue("@a7",txtId.Text);
            komut.ExecuteNonQuery();
            MessageBox.Show("Randevu Güncellendi");
            s.baglanti().Close();
            //txtTarih.Text = "";
            //txtSaat.Text = "";
            //cbBrans.Text = "";
            //cbDoktor.Items.Clear();
            //cbDoktor.Text = "";
            //checkBox1.Checked = false;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmDuyurular fr = new frmDuyurular();
            fr.yedekAd = ad;
            fr.yedekTc = TC;
            fr.Show();
        }

        private void btnRandevuListele_Click(object sender, EventArgs e)
        {
            frmRandevuListesi fr = new frmRandevuListesi();
            fr.Show();
            this.Hide();
        }

        private void frmSekreterDetay_Load(object sender, EventArgs e)
        {
            lblAd.Text = ad;
            lblTc.Text = TC;

            //Branslari chkboxa cek

            SqlCommand komut = new SqlCommand("Select BransAd from tblBrans", s.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while(dr.Read())
            {
                cbBrans.Items.Add(dr[0]);
            }
            s.baglanti().Close();


            SqlDataAdapter komut2 = new SqlDataAdapter("Select * from tblBrans", s.baglanti());
            DataTable dt = new DataTable();
            komut2.Fill(dt);
            dataGridViewBrans.DataSource = dt;
            s.baglanti().Close();

            SqlDataAdapter komut3 = new SqlDataAdapter("Select (DoktorAd+' '+DoktorSoyad) as Doktorİsim,DoktorBrans from tblDoktor", s.baglanti());
            DataTable dt2 = new DataTable();
            komut3.Fill(dt2);
            dataGridViewDoktor.DataSource = dt2;
            s.baglanti().Close();

            if (randevuid != null)
            {
                txtId.Text = randevuid;
                SqlCommand komut4 = new SqlCommand("Select RandevuTarih,RandevuSaat,RandevuDoktor,RandevuBrans,HastaTc,RandevuDurum from tblRandevu where RandevuId=@r1", s.baglanti());
                komut4.Parameters.AddWithValue("@r1", randevuid);
                SqlDataReader dr3 = komut4.ExecuteReader();
                if (dr3.Read())
                {
                    txtSaat.Text = dr3[1].ToString();
                    txtTarih.Text = dr3[0].ToString();
                    cbDoktor.Text = dr3[2].ToString();
                    cbBrans.Text = dr3[3].ToString();
                    txtTc.Text = dr3[4].ToString();
                    checkBox1.Checked = (bool)dr3[5];
                }
                s.baglanti().Close();
            }
        }
    }
}
