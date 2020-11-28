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
    public partial class bebekbilgi : Form
    {
        public bebekbilgi()
        {
            InitializeComponent();
        }
        SqlConnection baglan = new SqlConnection("Data Source=DESKTOP-1U112KC;Initial Catalog=hastabilgi;Integrated Security=True");


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBox1.Text == "Sarılık")
            {
                Btnsarılık.BackColor = Color.Red;
                Btngöz.BackColor = Color.Gainsboro;
                Btnsolunum.BackColor = Color.Gainsboro;
                Btnishal.BackColor = Color.Gainsboro;
                Btnsaglıklı.BackColor = Color.Gainsboro;

            }
            if (comboBox1.Text == "Üst Solunum Yolu Enfeksiyonu")
            {
                Btnsolunum.BackColor = Color.Blue;
                Btngöz.BackColor = Color.Gainsboro;
                Btnsarılık.BackColor = Color.Gainsboro;
                Btnishal.BackColor = Color.Gainsboro;
                Btnsaglıklı.BackColor = Color.Gainsboro;
            }
            if (comboBox1.Text == "İshal Kabız")
            {
                Btnishal.BackColor = Color.Green;
                Btnsolunum.BackColor = Color.Gainsboro;
                Btnsarılık.BackColor = Color.Gainsboro;
                Btngöz.BackColor = Color.Gainsboro;
                Btnsaglıklı.BackColor = Color.Gainsboro;
            }
            if (comboBox1.Text == "Göz Enfeksiyonu")
            {
                Btngöz.BackColor = Color.Orange;
                Btnsolunum.BackColor = Color.Gainsboro;
                Btnsarılık.BackColor = Color.Gainsboro;
                Btnishal.BackColor = Color.Gainsboro;
                Btnsaglıklı.BackColor = Color.Gainsboro;
            }
            if (comboBox1.Text == "Ciddi Rahatsızlık Yok")
            {
                Btnsaglıklı.BackColor = Color.Yellow;
                Btnsolunum.BackColor = Color.Gainsboro;
                Btnsarılık.BackColor = Color.Gainsboro;
                Btnishal.BackColor = Color.Gainsboro;
                Btngöz.BackColor = Color.Gainsboro;
            }
        }



        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text.Length < 11)
                {
                    MessageBox.Show("Lütfen 11 haneli T.C. kimlik numaranızı kontrol ederek yeniden giriniz!\nEksik tuşlama yaptınız!", "UYARI!", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);

                }
                if(textBox16.Text.Length==0||textBox1.Text.Length==0 || textBox2.Text.Length==0||textBox6.Text.Length==0||textBox5.Text.Length==0||textBox3.Text.Length==0||comboBox1.Text.Length==0||comboBox2.Text.Length==0)
                {
                    MessageBox.Show("Lütfen bilgilerinizi eksiksiz giriniz!", "UYARI!", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                }



                else
                {

                    baglan.Open();
                    SqlCommand komut = new SqlCommand("insert into Bebek_Bilgi (Muayene_Tarihi,Bebek_Adı,Bebek_TC,Cinsiyet,Hastalık_Durumu,Kilo,Boy,Doğum_Tarihi,Yapılan_Aşı,ID)values('(" + dateTimePicker1.Text.ToString() + "','" + textBox2.Text.ToString() + "','" + textBox1.Text.ToString() + "','" + comboBox2.Text.ToString() + "','" + comboBox1.Text.ToString() + "','" + textBox6.Text.ToString() + "','" + textBox5.Text.ToString() + "','" + dateTimePicker2.Text.ToString() + "','" + textBox3.Text.ToString()+"','"+ textBox16.Text.ToString() + "')", baglan);
                    komut.ExecuteNonQuery();
                    baglan.Close();
                    MessageBox.Show("Bilgileriniz Kaydedildi!", "SONUÇ İLETİSİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox1.Clear();
                    textBox3.Clear();
                    textBox2.Clear();
                    textBox5.Clear();
                    textBox6.Clear();
                    textBox16.Clear();

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Sistemsel bir hata meydana geldi.\n\nLütfen ana menüye dönüp ID değerini kontrol edip unique değer girerek tekrar deneyiniz!", "SONUÇ İLETİSİ", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }


        private void button2_Click_1(object sender, EventArgs e)
        {
            kayıtsilme hastabilgi = new kayıtsilme();
            hastabilgi.Show();
            this.Hide();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 giriş = new Form1();
            giriş.Show();
        }

        private void bebekbilgi_Load(object sender, EventArgs e)
        {
            timer1.Interval = 1000;
            timer1.Enabled = true;
            this.AcceptButton = button1;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label10.Text = DateTime.Now.ToString();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
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
                }

            }
            catch (System.ArgumentOutOfRangeException)
            {
                MessageBox.Show("Fazla tuşlama yaptınız!\n\nLütfen T.C. değerini 11 haneli olarak yeniden yazın!", "UYARI!", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);

            }
        }

        private void bebekbilgi_FormClosed(object sender, FormClosedEventArgs e)
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

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string metin = textBox2.Text;
            string desen = @"\d+";
            Regex rgx = new Regex(desen);
            foreach (Match m in rgx.Matches(metin))
            {
                MessageBox.Show("Ad-Soyad kısmına sayı yazılamaz!\n\nLütfen tekrar deneyiniz!", "SONUÇ İLETİSİ", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                textBox2.Clear();
            }
            int sembol = -1;
            foreach (char chr in metin)
            {
                if (char.IsSymbol(chr) || char.IsPunctuation(chr))
                {
                    sembol++;
                }

            }
            if (sembol >= 0)
            {
                MessageBox.Show("Ad-Soyad kısmına sembol ve noktalama işareti yazılamaz!\n\nLütfen tekrar deneyiniz!", "SONUÇ İLETİSİ", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                textBox2.Clear();
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            string metin = textBox6.Text;
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
                textBox6.Clear();
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
                textBox6.Clear();

            }

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            string metin = textBox5.Text;
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
                textBox5.Clear();
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
                textBox5.Clear();

            }

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            string metin = textBox3.Text;
            string desen = @"\d+";
            Regex rgx = new Regex(desen);
            foreach (Match m in rgx.Matches(metin))
            {
                MessageBox.Show("Yapılan aşı kısmına sayı yazılamaz!\n\nLütfen tekrar deneyiniz!", "SONUÇ İLETİSİ", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                textBox3.Clear();
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
                MessageBox.Show("Yapılan aşı kısmına sembol yazılamaz!\n\nLütfen tekrar deneyiniz!", "SONUÇ İLETİSİ", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                textBox3.Clear();

            }
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            string metin = comboBox1.Text;
            string desen = @"\d+";
            Regex rgx = new Regex(desen);
            foreach (Match m in rgx.Matches(metin))
            {
                MessageBox.Show("Hastalık durumu kısmına sayı yazılamaz!\n\nLütfen tekrar deneyiniz!", "SONUÇ İLETİSİ", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                comboBox1.ResetText();
            }
            int sembol = -1;
            foreach (char chr in metin)
            {
                if (char.IsSymbol(chr) || char.IsPunctuation(chr))
                {
                    sembol++;
                }

            }
            if (sembol >= 0)
            {
                MessageBox.Show("Hastalık durumu kısmına sembol ve noktalama işareti yazılamaz!\n\nLütfen tekrar deneyiniz!", "SONUÇ İLETİSİ", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);

            }
        }

        private void comboBox2_TextChanged(object sender, EventArgs e)
        {
            string metin = comboBox2.Text;
            string desen = @"\d+";
            Regex rgx = new Regex(desen);
            foreach (Match m in rgx.Matches(metin))
            {
                MessageBox.Show("Cinsiyet kısmına sayı yazılamaz!\n\nLütfen tekrar deneyiniz!", "SONUÇ İLETİSİ", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
            }
            int sembol = -1;
            foreach (char chr in metin)
            {
                if (char.IsSymbol(chr) || char.IsPunctuation(chr))
                {
                    sembol++;
                }

            }
            if (sembol >= 0)
            {
                MessageBox.Show("Cinsiyet kısmına sembol ve noktalama işareti yazılamaz!\n\nLütfen tekrar deneyiniz!", "SONUÇ İLETİSİ", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);

            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox2.Text=="Kız")
            {
                comboBox2.BackColor = Color.DeepPink;

            }
            if(comboBox2.Text=="Erkek")
            {
                comboBox2.BackColor = Color.Blue;
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

