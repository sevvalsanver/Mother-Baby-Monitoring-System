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
using System.IO;

namespace anne_bebek_takip
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglan = new SqlConnection("Data Source=DESKTOP-1U112KC;Initial Catalog=hastabilgi;Integrated Security=True");

       

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                timer1.Interval = 1000;
                timer1.Enabled = true;
            }
            
            catch (Exception Ex)
            {
                MessageBox.Show("Sistemsel bir hata meydana geldi.\n\nLütfen tekrar deneyiniz! " + Ex, "SONUÇ İLETİSİ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

  
        private void radioButton2_CheckedChanged_1(object sender, EventArgs e)//annebilgi aç
        {
            if (radioButton2.Checked == true)
            {
                Form3 yenikayıt = new Form3();
                yenikayıt.Show();
                this.Hide();
            }
        }

        

        private void radioButton1_CheckedChanged_2(object sender, EventArgs e)//silmeonay aç
        {
            if(radioButton1.Checked==true)
            {
                silmeonay kayıtsil = new silmeonay();
                kayıtsil.Show();
                this.Hide();
            }
        }
       protected virtual void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            MessageBox.Show("Program Kapatılacaktır!", "Kapatma Uyarısı!", MessageBoxButtons.OK,MessageBoxIcon.Warning);
            Application.Exit();

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)//kayıt güncelle aç
        {
            if(radioButton3.Checked==true)
            {
            kayıtgüncelle bebek = new kayıtgüncelle();
            bebek.Show();
            this.Hide();

            }
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToString();
        }
        public void Kullanıcı(string veriler)//database veri çekme
        {
            SqlDataAdapter da = new SqlDataAdapter(veriler, baglan);
            DataSet ds = new DataSet();

        }

        private void button1_Click(object sender, EventArgs e)//şifre güncelleme
        {
            if(textBox2.Text.Length!=5)
            {
                MessageBox.Show("Lütfen 5 haneli bir şifre giriniz!\nEksik ya da fazla tuşlama yaptınız!", "UYARI!", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                textBox2.Clear();
            }
            else if(textBox2.Text.ToString()=="00000")
            {
                MessageBox.Show("Lütfen şifrenizi yeniden giriniz!\nŞifre '00000' olamaz!", "UYARI!", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                textBox2.Clear();
            }
            else
            {   try
                {
                    baglan.Open();
                    SqlCommand komut = new SqlCommand("Update Kullanıcı set Şifre='" + textBox2.Text + "'", baglan);
                    komut.ExecuteNonQuery();
                    Kullanıcı("Select *from Kullanıcı");
                    baglan.Close();

                    MessageBox.Show("Şifre değiştirildi!", "İleti", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox2.Clear();
                }
                catch (System.Data.SqlClient.SqlException)//format hatası
                {
                    MessageBox.Show("Lütfen şifre kısmına sayısal değerler giriniz!", "UYARI!", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                    textBox2.Clear();

                }
                catch(System.InvalidOperationException)//aşım hatası
                {
                    MessageBox.Show("Lütfen şifreyi yeniden giriniz!", "UYARI!", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                    textBox2.Clear();

                }

            }
            
        }

        private void button2_Click_1(object sender, EventArgs e)//kullanıcı adı güncelleme
        {  
            if(textBox1.Text.Length!=0)
            {
                try
                {   
                    baglan.Open();
                    SqlCommand komut = new SqlCommand("Update Kullanıcı set Kullanıcı_Adı ='" + textBox1.Text + "'", baglan);
                    komut.ExecuteNonQuery();
                    Kullanıcı("Select *from Kullanıcı");
                    baglan.Close();

                    MessageBox.Show("Kullanıcı Adı değiştirildi!", "İleti", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox1.Clear();
                }


                catch (System.InvalidOperationException)
                {
                    MessageBox.Show("Lütfen bilgilerinizi eksiksiz doldurunuz!", "UYARI!", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);

                }
            }
            else
            {

                MessageBox.Show("Lütfen bilgilerinizi eksiksiz doldurunuz!", "UYARI!", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
           
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)//şifre yazma
        {
            string metin = textBox2.Text;
            string desen = @"\D+"; //sayı dışında karakter girme
            Regex rgx = new Regex(desen);
            foreach (Match m in rgx.Matches(metin))
            {
                MessageBox.Show("Şifre kısmına sadece sayı yazılabilir!\n\nLütfen tekrar deneyiniz!", "SONUÇ İLETİSİ", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                textBox2.Clear();
            }
            this.AcceptButton = button1;
          
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            this.AcceptButton = button2;
        }
    }
           
    }

