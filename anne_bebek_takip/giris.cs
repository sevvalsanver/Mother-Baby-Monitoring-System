using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Drawing;

namespace anne_bebek_takip
{
    public partial class giris : Form
    {
        public giris()
        {
            InitializeComponent();
        }
        SqlConnection baglan = new SqlConnection("Data Source=DESKTOP-1U112KC;Initial Catalog=hastabilgi;Integrated Security=True");
      
        private void timer1_Tick(object sender, EventArgs e)
        {
            label5.Text = DateTime.Now.ToString();
        }
        int hak = 3;
        bool durum = false;


        private void giris_Load(object sender, EventArgs e)//form açılınca
        {   
            timer1.Interval = 1000;
            timer1.Enabled = true;
            this.AcceptButton = button1;
            button3.Enabled = false;
            label7.Text = Convert.ToString(hak);

        }
        private void button1_Click(object sender, EventArgs e)//kullanıcı doğrulama
        {
            try
            {
                if (hak != 0)
                {
                    baglan.Open();
                    SqlCommand komut = new SqlCommand("Select *from Kullanıcı", baglan);
                    SqlDataReader okuma = komut.ExecuteReader();
                    while (okuma.Read()) //tabloda bilgi varsa true döner
                    {
                        if (okuma["Kullanıcı_Adı"].ToString().Trim() == textBox2.Text && okuma["Şifre"].ToString().Trim() == textBox1.Text)
                        {
                            durum = true;
                            MessageBox.Show("Giriş Başarılı!\n\nHoşgeldiniz " + textBox2.Text.ToString(), "Sonuç İletisi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            button3.Enabled = true;
                            this.AcceptButton = button3;
                        }

                    }
                    if (textBox1.Text.Length != 5)
                    {
                        MessageBox.Show("Lütfen kullanıcı adını kontrol ederek 5 haneli bir şifre giriniz!\nEksik tuşlama yaptınız!\nKalan Giriş Hakkı: " + hak.ToString(), "UYARI!", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                        hak--;
                        textBox1.Clear();
                    }
                    else if (durum == false)//eşleşme yoksa
                    {
                        MessageBox.Show("Yanlış Kullanıcı Adı veya Şifre\nKalan Giriş Hakkı: " + hak.ToString(), "UYARI!", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                        hak--;
                        textBox1.Clear();
                        textBox2.Clear();
                        textBox2.Focus();
                    }


                    baglan.Close();

                }
                if (hak == 0)
                {
                    button1.Enabled = false;
                    MessageBox.Show("Giriş Hakkı Kalmadı!", "Anne Bebek Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }

            }
            catch (Exception Ex)
            {
                MessageBox.Show("Sistemsel bir hata meydana geldi.\n\nLütfen tekrar deneyiniz!" + Ex, "SONUÇ İLETİSİ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
               
            }


        }

        
        private void giris_FormClosed(object sender, FormClosedEventArgs e)//kapatma tuşu
        {
            MessageBox.Show("Program Kapatılacaktır!", "Kapatma Uyarısı!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            Application.Exit();
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            label5.Text = DateTime.Now.ToString();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)//şifre kutucuğu
        {

            try
            {
                progressBar1.Value = textBox1.Text.Length;
                string metin = textBox1.Text;
                string desen = @"\D+"; //sayı dışında veri girildiyse
                Regex rgx = new Regex(desen);
                foreach (Match m in rgx.Matches(metin))
                {
                    hak--;
                    MessageBox.Show("Şifre kısmına sadece sayı yazılabilir!\n\nLütfen tekrar deneyiniz!\nKalan Giriş Hakkı: " + hak.ToString(), "SONUÇ İLETİSİ", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                    textBox1.Clear();
                   
                }
                if (hak == 0)
                {
                    button1.Enabled = false;
                    MessageBox.Show("Giriş Hakkı Kalmadı!", "Anne Bebek Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
            catch (System.ArgumentOutOfRangeException)//fazla karakter
            {
                hak--;
                MessageBox.Show("Fazla tuşlama yaptınız!\n\nLütfen şifre değerini 5 haneli olarak yeniden yazın!\nKalan Giriş Hakkı: " + hak.ToString(), "UYARI!", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                if (hak == 0)
                {
                    button1.Enabled = false;
                    MessageBox.Show("Giriş Hakkı Kalmadı!", "Anne Bebek Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
        }

        
        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 giris = new Form1();
            giris.Show();
        }

        
    }
}
