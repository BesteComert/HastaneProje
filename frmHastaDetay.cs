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
    public partial class frmHastaDetay : Form
    {
        public string TC;
        sqlbaglantisi s = new sqlbaglantisi();
        public frmHastaDetay()
        {
            InitializeComponent();
        }

        private void frmHastaDetay_Load(object sender, EventArgs e)
        {
            //Hasta Ad Soyad Çekme
            SqlCommand komut3 = new SqlCommand("Select HastaAd,HastaSoyad from tblHasta Where HastaTc=@tc", s.baglanti());
            komut3.Parameters.AddWithValue("@tc", TC);
            SqlDataReader dr2 = komut3.ExecuteReader();
            if (dr2.Read())
            {
                lblAd.Text = dr2[0]+" " + dr2[1];
                lblTc.Text = TC;
            }

            RandevuGecmisGuncelle();

            //Branşları Çekme
            SqlCommand komut = new SqlCommand("Select BransAd from tblBrans", s.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
                cbBrans.Items.Add(dr[0]);
            s.baglanti().Close();

        }

        private void cbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbDoktor.Text = "";
            cbDoktor.Items.Clear();
            SqlCommand komut2 = new SqlCommand("Select DoktorAd,DoktorSoyad from tblDoktor where DoktorBrans=@a", s.baglanti());
            komut2.Parameters.AddWithValue("@a", cbBrans.Text);
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                cbDoktor.Items.Add(dr2[0] + " " + dr2[1]);
            }
            s.baglanti().Close();

            SqlDataAdapter da = new SqlDataAdapter("Select RandevuId As Id,RandevuTarih as Tarih, RandevuSaat as Saat, RandevuDoktor as Doktor,RandevuBrans as Branş from tblRandevu where RandevuBrans='" + cbBrans.Text + "' and RandevuDurum= '"+false+"'", s.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridViewAktif.DataSource = dt;
            s.baglanti().Close();
        }

        private void cbDoktor_SelectedIndexChanged(object sender, EventArgs e)
        {
            RandevuAktifGuncelle();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmHastaBilgiDuzenle fr = new frmHastaBilgiDuzenle();
            fr.tc = TC;
            fr.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            frmHastaGiris fr = new frmHastaGiris();
            fr.Show();
            this.Hide();
        }

        private void btnRandevu_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Update tblRandevu Set HAstaTc=@a1,HastaSikayet=@a2,RandevuDurum=@a4 where RandevuID=@a3", s.baglanti());
            komut.Parameters.AddWithValue("@a1", lblTc.Text);
            komut.Parameters.AddWithValue("@a2", txtSikayet.Text);
            komut.Parameters.AddWithValue("@a3", txtRandevuId.Text);
            komut.Parameters.AddWithValue("@a4",true);

            komut.ExecuteNonQuery();
            s.baglanti().Close();
            MessageBox.Show("Randevunuz Kaydedilmiştir");
            RandevuGecmisGuncelle();
            RandevuAktifGuncelle();
        }

        private void dataGridViewAktif_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridViewAktif.SelectedCells[0].RowIndex;
            txtRandevuId.Text = dataGridViewAktif.Rows[secilen].Cells[0].Value.ToString();
            cbBrans.Text = dataGridViewAktif.Rows[secilen].Cells[4].Value.ToString();
            cbDoktor.Text = dataGridViewAktif.Rows[secilen].Cells[3].Value.ToString();
        }
        public void RandevuGecmisGuncelle()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select RandevuTarih as Tarih, RandevuSaat as Saat, RandevuDoktor as Doktor, HastaSikayet as Şikayet from tblRandevu where HastaTc='" + lblTc.Text + "' and RandevuDurum= '" + true + "'", s.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridViewGecmis.DataSource = dt;
            s.baglanti().Close();
        }
        public void RandevuAktifGuncelle()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select RandevuId As Id,RandevuTarih as Tarih, RandevuSaat as Saat, RandevuDoktor as Doktor,RandevuBrans as Branş from tblRandevu where RandevuDoktor='" + cbDoktor.Text + "' and RandevuDurum= '" + false + "'", s.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridViewAktif.DataSource = dt;
            s.baglanti().Close();
        }
    }
}
