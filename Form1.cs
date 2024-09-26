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

namespace KitapProjesi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

       //----------------------------------------------------

        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-6LPFTLN\\SQLEXPRESS;Initial Catalog=KitapProjesi;Integrated Security=True;Encrypt=False");


        void Listele()
        {
            baglanti.Open();

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from Kitaplar",baglanti);
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            baglanti.Close();
        }
        
        // textBox lari temizle
        void Temizle()
        {
            txtID.Text = "";
            txtAd.Text = "";
            txtYazar.Text = "";
            cmbTur.Text = "";
            txtSayfaSayisi.Text = "";
        }

        string durum = "";
        //----------------------------------------------------


        private void Form1_Load(object sender, EventArgs e)
        {
            txtID.Enabled = false;
            txtAd.Focus();

            Listele();

        }

        private void btnListele_Click(object sender, EventArgs e)
        {
            Listele();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open();    

            SqlCommand kaydet = new SqlCommand("insert into Kitaplar (Ad,Yazar,Tur,Sayfa,Durum) values (@p1,@p2,@p3,@p4,@p5)",baglanti);
            kaydet.Parameters.AddWithValue("@p1",txtAd.Text);
            kaydet.Parameters.AddWithValue("@p2",txtYazar.Text);
            kaydet.Parameters.AddWithValue("@p3",cmbTur.Text);
            kaydet.Parameters.AddWithValue("@p4",txtSayfaSayisi.Text);
            kaydet.Parameters.AddWithValue("@p5",durum);
            kaydet.ExecuteNonQuery();

            baglanti.Close() ;
            Temizle();
            MessageBox.Show("Kitap Eklendi...","Bilgi",MessageBoxButtons.OK);

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            durum = "0";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            durum = "1";
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();

            SqlCommand sil = new SqlCommand("delete from Kitaplar where Id=@p1",baglanti);
            sil.Parameters.AddWithValue("@p1", txtID.Text);
            sil.ExecuteNonQuery();

            baglanti.Close();

            Temizle();

            MessageBox.Show("Kitap Silindi...", "Bilgi", MessageBoxButtons.OK);

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;

            txtID.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtYazar.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            txtSayfaSayisi.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            cmbTur.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();

            if (dataGridView1.Rows[secilen].Cells[5].Value.ToString() == "True")
            {
                radioButton2.Checked = true;
            }
            else
            {
                radioButton1.Checked = true;
            }



        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();

            SqlCommand guncelle = new SqlCommand("update Kitaplar set Ad=@p1,Yazar=@p2,Tur=@p3,Sayfa=@p4,Durum=@p5 where Id=@p6",baglanti);
            
            guncelle.Parameters.AddWithValue("@p1",txtAd.Text);
            guncelle.Parameters.AddWithValue("@p2",txtYazar.Text);
            guncelle.Parameters.AddWithValue("@p3",cmbTur.Text);
            guncelle.Parameters.AddWithValue("@p4",txtSayfaSayisi.Text);
            if(radioButton1.Checked == true)
            {
                guncelle.Parameters.AddWithValue("@p5", "False");
            }
            if(radioButton2.Checked == true)
            {
                guncelle.Parameters.AddWithValue("@p5", "True");
            }
            guncelle.Parameters.AddWithValue("@p6",txtID.Text);

            guncelle.ExecuteNonQuery();



            baglanti.Close();

            Temizle();

            MessageBox.Show("Kitap güncellendi...", "Bilgi", MessageBoxButtons.OK);
        }
    }
}
