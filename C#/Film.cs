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
    public partial class Film : Form
    {
        public Film()
        {
            InitializeComponent();
        }
        SqlConnection baglan = new SqlConnection(@"Data Source=DESKTOP-H8CVDQC\SQLEXPRESS;Initial Catalog=SinemaOtomasyonu1;Integrated Security=True");
        
        public void vericekme(string veri)
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

        private void Film_Load(object sender, EventArgs e)
        {
            vericekme("Select *from Film");







        }

        private void button2_Click(object sender, EventArgs e)
        {
            vericekme("Select *From Film");
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox5.Text != "")
            {

                baglan.Open();
                SqlCommand film = new SqlCommand("insert into Film(FilmAdi,FilmTürü,Yönetmen,Imdb) values(@FilmAdi,@FilmTürü,@Yönetmen,@Imdb)", baglan);

                film.Parameters.AddWithValue("@FilmAdi", textBox1.Text);
                film.Parameters.AddWithValue("@FilmTürü", textBox2.Text);
                film.Parameters.AddWithValue("@Yönetmen", textBox3.Text);
                film.Parameters.AddWithValue("@Imdb", float.Parse(textBox5.Text));

                film.ExecuteNonQuery();
                vericekme("Select *From Film");
                baglan.Close();
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox5.Clear();
                MessageBox.Show("Film Eklendi", "Bilgi");
            }
            else MessageBox.Show("Eksik Alanları Giriniz ", "Hata !!");
            
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
      
        }

        private void button4_Click(object sender, EventArgs e)
        {
            vericekme("Select *From Film");
            if (textBox6.Text != "")
            {

                baglan.Open();
                SqlCommand komut = new SqlCommand("delete from Film where FilmID=@ID", baglan);
                komut.Parameters.AddWithValue("@ID", textBox6.Text);
                komut.ExecuteNonQuery();

                baglan.Close();
                textBox6.Clear();
                vericekme("Select *From Film");
                MessageBox.Show("Film Silindi");
            }
            else MessageBox.Show("FilmID yi girmediniz !!", "Hata!!");
        }
    }
}
