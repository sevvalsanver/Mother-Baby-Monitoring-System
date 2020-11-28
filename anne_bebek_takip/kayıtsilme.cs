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
using System.Linq.Expressions;
using System.Data.Common;
using System.Text.RegularExpressions;

namespace anne_bebek_takip
{
    public partial class kayıtsilme : Form
    {
        public kayıtsilme()
        {
            InitializeComponent();
        }
        SqlConnection baglan = new SqlConnection("Data Source=DESKTOP-1U112KC;Initial Catalog=hastabilgi;Integrated Security=True");

        public void Anneveri(string veriler)//anne verileri tabloya yazma
        {
            SqlDataAdapter da = new SqlDataAdapter(veriler, baglan);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        public void Bebekveri(string veriler)//bebek verileri tabloya yazma
        {
            SqlDataAdapter da = new SqlDataAdapter(veriler, baglan);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView2.DataSource = ds.Tables[0];
        }


        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 giriş = new Form1();
            giriş.Show();
            

        }

        private void radioButton4_CheckedChanged_1(object sender, EventArgs e)
        {
            silmeonay onay = new silmeonay();
            onay.Show();
            this.Hide();

        }

        private void Çıkış_CheckedChanged(object sender, EventArgs e)
        {
            Form1 giriş = new Form1();
            giriş.Show();
            this.Hide();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Form1 giriş = new Form1();
            giriş.Show();
        }
        private void button2_Click(object sender, EventArgs e)// yazılan TC'ye göre bebek kaydını sil
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
                        dataGridView1.DataSource = bebekyenile();
                    }

                }
                catch (Exception)
                {
                    MessageBox.Show("Girilen bilgilere ait bir kayıt bulunmamaktadır.\nLütfen T.C. kısmına sayısal değerler giriniz ve bilgilerinizi tekrar kontrol ediniz!", "UYARI!", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);

                }
            }
            


        }
        
        private void button1_Click_1(object sender, EventArgs e)//yazılan TC'ye göre anne kaydını sil
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
                        dataGridView1.DataSource = anneyenile();
                    }

                }


                catch (Exception)
                {
                    MessageBox.Show("Girilen bilgilere ait bir kayıt bulunmamaktadır.\nLütfen T.C. kısmına sayısal değerler giriniz ve bilgilerinizi tekrar kontrol ediniz!", "UYARI!", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);

                }
            }
            
            


        }

        DataTable anneyenile()//anne yeni verileri yazdır
        {
        
                baglan.Open();
                SqlDataAdapter da = new SqlDataAdapter("Select *from Anne_Bilgi", baglan);
                DataTable tablo = new DataTable();
                da.Fill(tablo);
                baglan.Close();
                return tablo;

            
        }
        DataTable bebekyenile()//bebek yeni verileri yazdır
        {
            baglan.Open();
            SqlDataAdapter da = new SqlDataAdapter("Select *from Bebek_Bilgi", baglan);
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            baglan.Close();
            return tablo;
        }


        private void button4_Click(object sender, EventArgs e)//seçili anne kaydı sil
        {
            try
            {
                   DialogResult durum = MessageBox.Show("Kaydı silmek istediğinizden emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (DialogResult.Yes == durum)
                    {
                    
                        for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
                        {
                        try
                        {
                            baglan.Open();
                            SqlCommand komut = new SqlCommand("Delete from Anne_Bilgi where Anne_TC='" + dataGridView1.SelectedRows[i].Cells["Anne_TC"].Value.ToString() + "'", baglan);
                            komut.ExecuteNonQuery();
                            baglan.Close();

                        }

                        catch (Exception)
                        {
                            MessageBox.Show("Seçili kayıt bulunmamaktadır.", "UYARI!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            throw;
                        }
                    }

                    MessageBox.Show("Seçilen Kayıtlar Silinmiştir.", "SONUÇ İLETİSİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = anneyenile();
                    }
                        
            }
           
            catch(Exception)
            {
                MessageBox.Show("Seçili kayıt olmadığı için silme işlemi yapılamamaktadır.", "UYARI!", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }



        }

        private void button5_Click(object sender, EventArgs e)//seçili bebek kaydını sil
        {
            try
            {
                DialogResult durum = MessageBox.Show("Kaydı silmek istediğinizden emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (DialogResult.Yes == durum)
                {
                    for (int i = 0; i < dataGridView2.SelectedRows.Count; i++)
                    {
                        try
                        {
                            baglan.Open();
                            SqlCommand komut = new SqlCommand("Delete from Bebek_Bilgi where Bebek_TC='" + dataGridView2.SelectedRows[i].Cells["Bebek_TC"].Value.ToString() + "'", baglan);
                            komut.ExecuteNonQuery();
                            baglan.Close();
                        }
                        
                         catch (Exception)
                        {
                          MessageBox.Show("Seçili kayıt bulunmamaktadır.", "UYARI!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                          throw;
                        }

                 }
                    MessageBox.Show("Seçilen Kayıtlar Silinmiştir.","SONUÇ İLETİSİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView2.DataSource = bebekyenile();
                }
            }

            catch (Exception)
            {
                MessageBox.Show("Seçili kayıt olmadığı için silme işlemi yapılamamaktadır.", "UYARI!", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }

        private void kayıtsilme_Load(object sender, EventArgs e)
        {
            timer1.Interval = 1000;
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label3.Text = DateTime.Now.ToString();
        }


        private void textBox1_TextChanged(object sender, EventArgs e)//anne tc kontrol
        {
            try
            {
                progressBar2.Value = textBox1.Text.Length;
                string metin = textBox1.Text;
                string desen = @"\D+";
                Regex rgx = new Regex(desen);
                foreach (Match m in rgx.Matches(metin))
                {
                    MessageBox.Show("T.C. kısmına sadece sayı yazılabilir!\n\nLütfen tekrar deneyiniz!", "SONUÇ İLETİSİ", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                    textBox1.Clear();
                }
                this.AcceptButton = button1;
             
            }
            catch (System.ArgumentOutOfRangeException)
            {
                MessageBox.Show("Fazla tuşlama yaptınız!\n\nLütfen T.C. değerini 11 haneli olarak yeniden yazın!", "UYARI!", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);

            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)//bebek tc kontrol
        {
            try
            {
                progressBar1.Value = textBox2.Text.Length;
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

        private void kayıtsilme_FormClosed(object sender, FormClosedEventArgs e)
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

        private void button6_Click(object sender, EventArgs e)//anne veritabanı veri çekme
        {
            Anneveri("Select *from Anne_Bilgi");
        }

        private void button7_Click(object sender, EventArgs e)//bebek veritabanı veri çekme
        {
            Bebekveri("Select *from Bebek_Bilgi");
        }

        
    }
}

       
    
 

