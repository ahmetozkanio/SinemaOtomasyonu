using Microsoft.SqlServer.Server;
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

namespace SinemaOtomasyonu1
{
    public partial class SeansEkle : Form
    {
        public SeansEkle()
        {
            InitializeComponent();
        }
        SqlConnection baglan = new SqlConnection(@"Data Source=DESKTOP-H8CVDQC\SQLEXPRESS;Initial Catalog=SinemaOtomasyonu1;Integrated Security=True");

        public void göster(string veri)
        {
            SqlDataAdapter data = new SqlDataAdapter(veri, baglan);
            DataSet dataset = new DataSet();
            data.Fill(dataset);

            dataGridView2.DataSource = dataset.Tables[0];
        }


            public void gösterFilm(string veri)
        {
            SqlDataAdapter data = new SqlDataAdapter(veri, baglan);
            DataSet dataset = new DataSet();
            data.Fill(dataset);

            dataGridView1.DataSource = dataset.Tables[0];


        }
    

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void SeansEkle_Load(object sender, EventArgs e)
        {
            göster("Select *from Seans");
            gösterFilm("Select *from Film");

            baglan.Open();
            SqlCommand komut = new SqlCommand("select FilmID from Film", baglan);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr[0]);
            }
            baglan.Close();
            baglan.Open();
            SqlCommand komut2 = new SqlCommand("select SalonID from SalonID", baglan);
            SqlDataReader dread = komut2.ExecuteReader();
            while (dread.Read())
            {
                comboBox2.Items.Add(dread[0]);
            }
            baglan.Close();
            baglan.Open();
            SqlCommand komut1 = new SqlCommand("select SeansID from Seans", baglan);
            SqlDataReader dr1 = komut1.ExecuteReader();
            while (dr1.Read())
            {
                comboBox4.Items.Add(dr1[0]);
            }
            baglan.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            göster("Select *from Seans");
            gösterFilm("Select *from Film");
            if (comboBox1.Text != "" && comboBox2.Text != "" && comboBox3.Text != "")
            {
                baglan.Open();
                SqlCommand seans = new SqlCommand("insert into Seans(SeansSaat,FilmID,Tarih,SalonID) values(@SeansSaat,@FilmID,@Tarih,@SalonID)", baglan);

                seans.Parameters.AddWithValue("@SeansSaat", comboBox3.Text);
                seans.Parameters.AddWithValue("@FilmID", comboBox1.Text);
                seans.Parameters.AddWithValue("@Tarih", dateTimePicker1.Text);
                seans.Parameters.AddWithValue("@SalonID", comboBox2.Text);
                seans.ExecuteNonQuery();
                baglan.Close();
                göster("Select *from Seans");
                gösterFilm("Select *from Film");
                MessageBox.Show("Seans Eklendi", "Bilgi");
            }
            else MessageBox.Show("Eksik Alanları Lütfen Doldurunuz.", "Hata !!");
            }

        private void button3_Click(object sender, EventArgs e)
        {
            göster("Select *from Seans");
            if (comboBox4.Text != "")
            {
                baglan.Open();
                SqlCommand komut = new SqlCommand("delete from Seans where SeansID=@ID", baglan);
                komut.Parameters.AddWithValue("@ID", comboBox4.Text);
                komut.ExecuteNonQuery();
                baglan.Close();
                göster("Select *from Seans");
                MessageBox.Show("Seans Silindi", "Bilgi");
            }
            else MessageBox.Show("SeansID yi Girmediniz .", "Hata !!");


            }
    }
}
