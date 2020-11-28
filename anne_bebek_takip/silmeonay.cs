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
using System.Text.RegularExpressions;
namespace anne_bebek_takip
{
    public partial class silmeonay : Form
    {
        public silmeonay()
        {
            InitializeComponent();
        }
        SqlConnection baglan = new SqlConnection("Data Source=DESKTOP-1U112KC;Initial Catalog=hastabilgi;Integrated Security=True");

        public void Anneveri(string veriler)//anne veri çekme
        {
            SqlDataAdapter da = new SqlDataAdapter(veriler, baglan);
            DataSet ds = new DataSet();

        }

        public void Bebekveri(string veriler)//bebek veri çekme
        {
            SqlDataAdapter da = new SqlDataAdapter(veriler, baglan);
            DataSet ds = new DataSet();

        }

        private void button1_Click(object sender, EventArgs e)//anne TC ye göre silme
        {
            if (textBox1.Text.Length < 11)
            {
                MessageBox.Show("Lütfen 11 haneli T.C. kimlik numaranızı kontrol ederek yeniden giriniz!\nEksik tuşlama yaptınız!", "UYARI!", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);

            }
            else
            {
                try
                {
                    DialogResult durum = MessageBox.Show("Kaydı silmek istediğinizden emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (DialogResult.Yes == durum)
                    {
                        baglan.Open();
                        SqlCommand komut = new SqlCommand("Delete from Anne_Bilgi where Anne_TC=@Anne_TC", baglan);
                        komut.Parameters.AddWithValue("@Anne_TC", textBox1.Text);
                        komut.ExecuteNonQuery();
                        Anneveri("Select *from Anne_Bilgi");
                        baglan.Close();
                        textBox1.Clear();
                        MessageBox.Show("Seçilen Kayıtlar Silinmiştir.", "SONUÇ İLETİSİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Girilen bilgilere ait bir kayıt bulunmamaktadır.\nLütfen bilgilerinizi tekrar kontrol ediniz!!", "SONUÇ İLETİSİ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            
        }


        private void button2_Click(object sender, EventArgs e)//bebek TC ye göre silme
        {
            if (textBox2.Text.Length < 11)
            {
                MessageBox.Show("Lütfen 11 haneli T.C. kimlik numaranızı kontrol ederek yeniden giriniz!\nEksik tuşlama yaptınız!", "UYARI!", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);

            }
            else
            {
                try
                {
                    DialogResult durum = MessageBox.Show("Kaydı silmek istediğinizden emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (DialogResult.Yes == durum)
                    {
                        baglan.Open();
                        SqlCommand komut = new SqlCommand("Delete from Bebek_Bilgi where Bebek_TC=@Bebek_TC", baglan);
                        komut.Parameters.AddWithValue("@Bebek_TC", textBox2.Text);
                        komut.ExecuteNonQuery();
                        Bebekveri("Select *from Bebek_Bilgi");
                        baglan.Close();
                        textBox2.Clear();
                        textBox2.Focus();
                        MessageBox.Show("Seçilen Kayıtlar Silinmiştir.", "SONUÇ İLETİSİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception)//veri tipi farkı veya eksik bilgi girme 
                {
                    MessageBox.Show("Girilen bilgilere ait bir kayıt bulunmamaktadır.\nLütfen bilgilerinizi tekrar kontrol ediniz!", "UYARI!", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);

                }
            }
           

            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 giriş = new Form1();
            giriş.Show();
        }




        private void button5_Click(object sender, EventArgs e)
        {
            kayıtsilme sil = new kayıtsilme();
            sil.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            kayıtsilme sil = new kayıtsilme();
            sil.Show();
            this.Hide();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label3.Text = DateTime.Now.ToString();
        }

        private void silmeonay_Load(object sender, EventArgs e)
        {
            timer1.Interval = 1000;
            timer1.Enabled = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)//anne tc
        { 
            try
            {
                progressBar1.Value = textBox1.Text.Length;
                string metin = textBox1.Text;
                string desen = @"\D+";
                Regex rgx = new Regex(desen);
                foreach (Match m in rgx.Matches(metin))
                {
                    MessageBox.Show("T.C. kısmına sadece sayı yazılabilir!\n\nLütfen tekrar deneyiniz!", "SONUÇ İLETİSİ", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                    textBox1.Clear();
                }
                foreach (char chr in metin)
                {
                    if (char.IsSymbol(chr))
                    {
                        MessageBox.Show("T.C. kısmına sembol yazılamaz!\n\nLütfen tekrar deneyiniz!", "SONUÇ İLETİSİ", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                    }

                }
                this.AcceptButton = button1;
            }
            catch (System.ArgumentOutOfRangeException)
            {
                MessageBox.Show("Fazla tuşlama yaptınız!\n\nLütfen T.C. değerini 11 haneli olarak yeniden yazın!", "UYARI!", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);

            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)//bebek tc
        {
            try
            {
                progressBar2.Value = textBox2.Text.Length;
                string metin = textBox2.Text;
                string desen = @"\D+";
                Regex rgx = new Regex(desen);
                foreach (Match m in rgx.Matches(metin))
                {
                    MessageBox.Show("T.C. kısmına sadece sayı yazılabilir!\n\nLütfen tekrar deneyiniz!", "SONUÇ İLETİSİ", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                    textBox2.Clear();
                }
                this.AcceptButton = button2;
                
            }
            catch (System.ArgumentOutOfRangeException)
            {
                MessageBox.Show("Fazla tuşlama yaptınız!\n\nLütfen T.C. değerini 11 haneli olarak yeniden yazın!", "UYARI!", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);

            }
        }

        private void silmeonay_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                MessageBox.Show("Program Kapatılacaktır!", "Kapatma Uyarısı!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Application.Exit();

            }
            catch (Exception Ex)
            {
                MessageBox.Show("Sistemsel bir hata meydana geldi.\n\nLütfen tekrar deneyiniz!" + Ex, "SONUÇ İLETİSİ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


    }
}
