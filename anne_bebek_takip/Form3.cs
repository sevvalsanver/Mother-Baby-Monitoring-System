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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        SqlConnection baglan = new SqlConnection("Data Source=DESKTOP-1U112KC;Initial Catalog=hastabilgi;Integrated Security=True");
       
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 giris = new Form1();
            giris.Show();
            
        }


        private void button3_Click(object sender, EventArgs e)//giriş sayfası
        {
            this.Hide();
            Form1 giriş = new Form1();
            giriş.Show();
            
        }

       

        private void Form3_Load(object sender, EventArgs e)
        {
            timer1.Interval = 1000;
            timer1.Enabled = true;
            this.AcceptButton = button1;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label7.Text = DateTime.Now.ToString();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)//tc
        {
            try
            {
                progressBar1.Value = textBox3.Text.Length;
                string metin = textBox3.Text;
                string desen = @"\D+";
                Regex rgx = new Regex(desen);
                foreach (Match m in rgx.Matches(metin))
                {
                    MessageBox.Show("T.C. kısmına sadece sayı yazılabilir!\n\nLütfen tekrar deneyiniz!", "SONUÇ İLETİSİ", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                }
            }
            catch (System.ArgumentOutOfRangeException)
            {
                MessageBox.Show("Fazla tuşlama yaptınız!\n\nLütfen T.C. değerini 11 haneli olarak yeniden yazın!", "UYARI!", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);

            }

        }

        private void kan_SelectedIndexChanged(object sender, EventArgs e)//kan
        {
            try
            {
               
                if (kan.Text == "Var")
                {
                    DialogResult durum = MessageBox.Show("Kan Uyuşmazlığı İğnesi Yaptırdınız Mı?", "Kan Uyuşmazlığı İğne Uyarısı", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (DialogResult.Yes == durum)
                    {
                        MessageBox.Show("Dikkatiniz İçin Teşekkür Ederiz :)", "Sonuç İletisi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (DialogResult.No == durum)
                    {
                        MessageBox.Show("Acilen Kan Uyuşmazlığı İğnesi Yaptırmalısınız!", "Acil Durum Uyarısı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }


                }
                else if (kan.Text == "Yok")
                {
                    MessageBox.Show("Kan Uyuşmazlığı İğnesi Yaptırmanıza Gerek Yoktur!", "Sonuç İletisi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                }
                else
                {
                    MessageBox.Show("Yanlış seçim yaptınız,lütfen iki seçenekten birisini seçiniz!", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                  
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Sistemsel bir hata meydana geldi.\n\nLütfen tekrar deneyiniz!" + Ex, "SONUÇ İLETİSİ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            
            MessageBox.Show("Program Kapatılacaktır!", "Kapatma Uyarısı!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            Application.Exit();
        }

        private void button1_Click_1(object sender, EventArgs e)//tc
        {
            try
            {
                if (textBox3.Text.Length < 11)
                {
                    MessageBox.Show("Lütfen 11 haneli T.C. kimlik numaranızı kontrol ederek yeniden giriniz!\nEksik tuşlama yaptınız!", "UYARI!", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);

                }
                if(textBox3.Text.Length==0|| textBox2.Text.Length==0||textBox1.Text.Length==0||textBox4.Text.Length==0||kan.Text.Length==0||textBox16.Text.Length==0)
                {
                        MessageBox.Show("Lütfen bilgilerinizi eksiksiz giriniz!", "UYARI!", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                       
                }
                else//anneye veri ekleme
                {
                    baglan.Open();
                    SqlCommand komut = new SqlCommand("insert into Anne_Bilgi (Muayene_Tarihi,Anne_Adı,Doğum_Tarihi,Anne_TC,Kilo,Boy,Kan_Uyuşmazlığı,ID)values('(" + dateTimePicker1.Text.ToString() + "','" + textBox1.Text.ToString() + "','" + dateTimePicker2.Text.ToString() + "','" + textBox3.Text.ToString() + "','" + textBox2.Text.ToString() + "','" + textBox4.Text.ToString() + "','" + kan.Text.ToString() +"','"+ textBox16.Text.ToString()+ "')", baglan);
                    komut.ExecuteNonQuery();
                    baglan.Close();

                    
                    textBox1.Clear();
                    textBox3.Clear();
                    textBox2.Clear();
                    textBox4.Clear();
                    textBox16.Clear();
                    textBox1.Focus();

                    this.Hide();
                    bebekbilgi bebek = new bebekbilgi();
                    bebek.Show();

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Sistemsel bir hata meydana geldi.\n\nLütfen ana menüye dönüp ID değerini kontrol edip unique değer girerek tekrar deneyiniz!", "SONUÇ İLETİSİ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)//ad soyad
        {
            string metin = textBox1.Text;
            string desen = @"\d+";
            
            Regex rgx = new Regex(desen);
            foreach (Match m in rgx.Matches(metin))
            {
                MessageBox.Show("Ad-Soyad kısmına sayı yazılamaz!\n\nLütfen tekrar deneyiniz!", "SONUÇ İLETİSİ", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                textBox1.Clear();
            }
            int sembol = -1;
            foreach (char chr in metin)
            {
                if (char.IsSymbol(chr)|| char.IsPunctuation(chr))
                {
                    sembol++;
                }

            }
            if(sembol>=0)
            {
                MessageBox.Show("Ad-Soyad kısmına sembol ve noktalama işareti yazılamaz!\n\nLütfen tekrar deneyiniz!", "SONUÇ İLETİSİ", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                textBox1.Clear();
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)//kilo
        {
            string metin = textBox2.Text;
            int harf = -1;
            foreach (char chr in metin)
            {
                if (char.IsLetter(chr))
                {
                    harf++;
                }

            }
            if (harf >= 0)
            {
                MessageBox.Show("Ağırlık kısmına harf yazılamaz!\n\nLütfen tekrar deneyiniz!", "SONUÇ İLETİSİ", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                textBox2.Clear();
            }
            int sembol = -1;
            foreach (char chr in metin)
            {
                if (char.IsSymbol(chr))
                {
                    sembol++;
                }

            }
            if (sembol >= 0)
            {
                MessageBox.Show("Ağırlık kısmına sembol yazılamaz!\n\nLütfen tekrar deneyiniz!", "SONUÇ İLETİSİ", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                textBox2.Clear();
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)//boy
        {
            string metin = textBox4.Text;
            int harf = -1;
            foreach (char chr in metin)
            {
                if (char.IsLetter(chr))
                {
                    harf++;
                }

            }
            if (harf >= 0)
            {
                MessageBox.Show("Boy kısmına harf yazılamaz!\n\nLütfen tekrar deneyiniz!", "SONUÇ İLETİSİ", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                textBox4.Clear();
            }
            int sembol = -1;
            foreach (char chr in metin)
            {
                if (char.IsSymbol(chr))
                {
                    sembol++;
                }

            }
            if (sembol >= 0)
            {
                MessageBox.Show("Boy kısmına sembol yazılamaz!\n\nLütfen tekrar deneyiniz!", "SONUÇ İLETİSİ", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                textBox4.Clear();
            }
        }

        private void kan_TextChanged(object sender, EventArgs e)//kan
        {
            string metin = kan.Text;
            string desen = @"\d+";
            Regex rgx = new Regex(desen);
            foreach (Match m in rgx.Matches(metin))
            {
                MessageBox.Show("Kan uyuşmazlığı kısmına sayı yazılamaz!\n\nLütfen tekrar deneyiniz!", "SONUÇ İLETİSİ", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
            }
            int sembol = -1;
            foreach (char chr in metin)
            {
                if (char.IsSymbol(chr)|| char.IsPunctuation(chr))
                {
                    sembol++;
                }

            }
            if (sembol >= 0)
            {
                MessageBox.Show("Kan uyuşmazlığı kısmına sembol ve noktalama işareti yazılamaz!\n\nLütfen tekrar deneyiniz!", "SONUÇ İLETİSİ", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);

            }
        }

        private void textBox16_TextChanged(object sender, EventArgs e)
        {
            string metin = textBox16.Text;
            string desen = @"\D+";
            Regex rgx = new Regex(desen);
            foreach (Match m in rgx.Matches(metin))
            {
                MessageBox.Show("ID kısmına sadece sayı yazılabilir!\n\nLütfen tekrar deneyiniz!", "SONUÇ İLETİSİ", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                textBox16.Clear();
            }
            
        }
    }
}

