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
    public partial class BiletSatış : Form
    {
        public BiletSatış()
        {
            InitializeComponent();
        }
        SqlConnection baglan = new SqlConnection(@"Data Source=DESKTOP-H8CVDQC\SQLEXPRESS;Initial Catalog=SinemaOtomasyonu1;Integrated Security=True");

        public void göster(string veri)
        {   
            SqlDataAdapter data = new SqlDataAdapter(veri, baglan);
            DataSet dataset = new DataSet();
            data.Fill(dataset);
           
                dataGridView1.DataSource = dataset.Tables[0];
        }

        public void göster1(string veri)
        {
            SqlDataAdapter data = new SqlDataAdapter(veri, baglan);
            DataSet dataset = new DataSet();
            data.Fill(dataset);

            dataGridView2.DataSource = dataset.Tables[0];
        }
        public void göster3(string veri)
        {
            SqlDataAdapter data = new SqlDataAdapter(veri, baglan);
            DataSet dataset = new DataSet();
            data.Fill(dataset);

            dataGridView3.DataSource = dataset.Tables[0];

        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void BiletSatış_Load(object sender, EventArgs e)
        {
            göster("Select *From MusteriBilet");
            göster1("Select *From Film");
            göster3("Select *From Seans");

            baglan.Open();
            SqlCommand getir = new SqlCommand("select FilmID from Seans", baglan);
            SqlDataReader dataread = getir.ExecuteReader();
            while (dataread.Read())
            {
                comboBox1.Items.Add(dataread[0]);
            }
            baglan.Close();
            baglan.Open();
            SqlCommand getir1 = new SqlCommand("select SalonID from Seans", baglan);
            SqlDataReader dataread1 = getir1.ExecuteReader();
            while (dataread1.Read())
            {
                comboBox3.Items.Add(dataread1[0]);
            }
            baglan.Close();
            baglan.Open();
            SqlCommand getir2 = new SqlCommand("select SeansID from Seans", baglan);
            SqlDataReader dataread2 = getir2.ExecuteReader();
            while (dataread2.Read())
            {
                comboBox2.Items.Add(dataread2[0]);
            }
            baglan.Close();
            baglan.Open();
            SqlCommand getir3 = new SqlCommand("select KoltukID from Koltuk", baglan);
            SqlDataReader dataread3 = getir3.ExecuteReader();
            while (dataread3.Read())
            {
                comboBox4.Items.Add(dataread3[0]);
            }
            baglan.Close();
            baglan.Open();
            SqlCommand getir4 = new SqlCommand("select BiletID from MusteriBilet", baglan);
            SqlDataReader dataread4 = getir4.ExecuteReader();
            while (dataread4.Read())
            {
                comboBox5.Items.Add(dataread4[0]);
            }
            baglan.Close();




        }

        private void button2_Click(object sender, EventArgs e)
        {
            göster("Select *From MusteriBilet");
            göster1("Select *From Film");
            göster3("Select *From Seans");
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && comboBox1.Text != "" && comboBox2.Text != "" && comboBox3.Text != "" && comboBox4.Text != "" )
            {
                baglan.Open();

                SqlCommand bilet = new SqlCommand("insert into MusteriBilet(TC,Adi,Soyadi,FilmID,SeansID,SalonID,KoltukID) values(@TC,@Adi,@Soyadi,@FilmID,@SeansID,@SalonID,@KoltukID)", baglan);
                bilet.Parameters.AddWithValue("@TC", textBox1.Text);
                bilet.Parameters.AddWithValue("@Adi", textBox2.Text);
                bilet.Parameters.AddWithValue("@Soyadi", textBox3.Text);
                bilet.Parameters.AddWithValue("@FilmID", comboBox1.Text);
                bilet.Parameters.AddWithValue("@SeansID", comboBox2.Text);
                bilet.Parameters.AddWithValue("@SalonID", comboBox3.Text);
                bilet.Parameters.AddWithValue("@KoltukID", comboBox4.Text);
                bilet.ExecuteNonQuery();
                göster("Select *From MusteriBilet");
                baglan.Close();
                MessageBox.Show("Kayıt Eklendi");
            }
            else MessageBox.Show("Eksik Alanları Doldurunuz ", "Hata!!");
            göster("Select *From MusteriBilet");
            göster1("Select *From Film");
            göster3("Select *From Seans");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {   if (comboBox5.Text != "")
            {
                baglan.Open();
                SqlCommand komut = new SqlCommand("delete from MusteriBilet where BiletID=@ID", baglan);
                komut.Parameters.AddWithValue("@ID", comboBox5.Text);
                komut.ExecuteNonQuery();

                baglan.Close();
                göster("Select *From MusteriBilet");
                MessageBox.Show("Bilet Silindi", "Bilgi");
            }
            else MessageBox.Show("BiletID yi Girmediniz !!", "Hata !!");
            }
    }
}
