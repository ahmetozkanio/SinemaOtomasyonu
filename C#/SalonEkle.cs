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
    public partial class SalonEkle : Form
    {
        public SalonEkle()
        {
            InitializeComponent();
        }
        SqlConnection baglan = new SqlConnection(@"Data Source=DESKTOP-H8CVDQC\SQLEXPRESS;Initial Catalog=SinemaOtomasyonu1;Integrated Security=True");

        public void verigörüntüle(string veri)
        {
            SqlDataAdapter data = new SqlDataAdapter(veri,baglan);
            DataSet dataset = new DataSet();
            data.Fill(dataset);
            dataGridView1.DataSource = dataset.Tables[0];
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {   verigörüntüle("Select *from SalonID");
            if (comboBox2.Text != "")
            {

                baglan.Open();
                SqlCommand salon = new SqlCommand("insert into SalonID(Kapasite) values(@Kapasite)", baglan);

                salon.Parameters.AddWithValue("@kapasite", comboBox2.Text);
                salon.ExecuteNonQuery();
                baglan.Close();
                verigörüntüle("Select *from SalonID");
                MessageBox.Show("Salon Eklendi", "BİLGİ");
            }
            else MessageBox.Show("Kapasiteyi Girmediniz", "Hata !!");
            }

        private void SalonEkle_Load(object sender, EventArgs e)
        {
            verigörüntüle("Select *from SalonID");

        }

        private void button3_Click(object sender, EventArgs e)
        {
            verigörüntüle("Select *from SalonID");
            if (textBox1.Text != "")
            {

                baglan.Open();
                SqlCommand komut = new SqlCommand("delete from SalonID where SalonID=@ID", baglan);
                komut.Parameters.AddWithValue("@ID", textBox1.Text);
                komut.ExecuteNonQuery();

                baglan.Close();
                textBox1.Clear();
                verigörüntüle("Select *from SalonID");
                MessageBox.Show("Salon Çıkarıldı", "BİLGİ");
            }
            else MessageBox.Show("SalonID yi Girmediniz", "Hata!!");
            }

    }
}
