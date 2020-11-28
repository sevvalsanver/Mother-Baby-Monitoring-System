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
using System.Globalization;
using System.Text.RegularExpressions;


namespace anne_bebek_takip
{
    public partial class kayıtgüncelle : Form
    {
        public kayıtgüncelle()
        {
            InitializeComponent();
        }
        SqlConnection baglan = new SqlConnection("Data Source=DESKTOP-1U112KC;Initial Catalog=hastabilgi;Integrated Security=True");
       
       
        public void anneveri(string veriler)//anne veri çekme
        {
            SqlDataAdapter da = new SqlDataAdapter(veriler,baglan);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        public void bebekveri(string veriler)//bebek veri çekme
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

        private void button1_Click(object sender, EventArgs e)//anne silme
        {
            if (textBox1.Text.Length != 11)
            {
                MessageBox.Show("Lütfen 11 haneli T.C. kimlik numaranızı kontrol ederek yeniden giriniz!\nEksik tuşlama yaptınız!", "UYARI!", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);

            }
            else
            {
                try
                {
                        baglan.Open();
                        SqlCommand komut = new SqlCommand("Select *from Anne_Bilgi where Anne_TC like '%" + textBox1.Text + "%'", baglan);
                        SqlDataAdapter da = new SqlDataAdapter(komut);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        dataGridView1.DataSource = ds.Tables[0];
                        baglan.Close();
                        textBox1.Clear();
                   
                }
                catch (Exception)
                {
                    MessageBox.Show("Böyle bir kayıt bulunmamaktadır.\n\nLütfen tekrar deneyiniz! ", "SONUÇ İLETİSİ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox1.Clear();

                }
            }

        }

        private void button2_Click(object sender, EventArgs e)//bebek silme
        {
            if (textBox2.Text.Length != 11)
            {
                MessageBox.Show("Lütfen 11 haneli T.C. kimlik numaranızı kontrol ederek yeniden giriniz!\nEksik tuşlama yaptınız!", "UYARI!", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                textBox2.Clear();
            }
            else
            {
                try
                {
                        baglan.Open();
                        SqlCommand komut = new SqlCommand("Select *from Bebek_Bilgi where Bebek_TC like '%" + textBox2.Text + "%'", baglan);
                        SqlDataAdapter da = new SqlDataAdapter(komut);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        dataGridView2.DataSource = ds.Tables[0];
                        baglan.Close();
                        textBox2.Clear();
                }
                catch (Exception)
                {
                    MessageBox.Show("Böyle bir kayıt bulunmamaktadır.\n\nLütfen tekrar deneyiniz! ", "SONUÇ İLETİSİ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox2.Clear();

                }
            }
           
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)//anne verileri tabloya yazma
        {
            try
            {
                int seçilialan = dataGridView1.SelectedCells[0].RowIndex;
                string Anne_Adı = dataGridView1.Rows[seçilialan].Cells[0].Value.ToString();
                string Anne_TC = dataGridView1.Rows[seçilialan].Cells[1].Value.ToString();
                string Doğum_Tarihi = dataGridView1.Rows[seçilialan].Cells[2].Value.ToString();
                string Kilo = dataGridView1.Rows[seçilialan].Cells[3].Value.ToString();
                string Boy = dataGridView1.Rows[seçilialan].Cells[4].Value.ToString();
                string Muayene_Tarihi = dataGridView1.Rows[seçilialan].Cells[5].Value.ToString();
                string Kan_Uyuşmazlığı = dataGridView1.Rows[seçilialan].Cells[6].Value.ToString();
                string ID = dataGridView1.Rows[seçilialan].Cells[7].Value.ToString();

                textBox6.Text = Anne_Adı;
                textBox7.Text = Anne_TC;
                textBox5.Text = Doğum_Tarihi;
                textBox3.Text = Kilo;
                textBox4.Text = Boy;
                textBox11.Text= Muayene_Tarihi;
                kan.Text = Kan_Uyuşmazlığı;
                textBox17.Text = ID;
            }
            catch(Exception Ex)
            {
                MessageBox.Show("Sistemsel bir hata meydana geldi.\n\nLütfen tekrar deneyiniz! "+Ex, "SONUÇ İLETİSİ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)//bebek verileri tabloya yazma
        {
            try
            {
                int seçilialan = dataGridView2.SelectedCells[0].RowIndex;
                string Bebek_Adı = dataGridView2.Rows[seçilialan].Cells[0].Value.ToString();
                string Bebek_TC = dataGridView2.Rows[seçilialan].Cells[1].Value.ToString();
                string Doğum_Tarihi = dataGridView2.Rows[seçilialan].Cells[2].Value.ToString();
                string Kilo = dataGridView2.Rows[seçilialan].Cells[3].Value.ToString();
                string Boy = dataGridView2.Rows[seçilialan].Cells[4].Value.ToString();
                string Cinsiyet = dataGridView2.Rows[seçilialan].Cells[5].Value.ToString();
                string Hastalık_Durumu = dataGridView2.Rows[seçilialan].Cells[6].Value.ToString();
                string Muayene_Tarihi = dataGridView2.Rows[seçilialan].Cells[7].Value.ToString() ;
                string Yapılan_Aşı = dataGridView2.Rows[seçilialan].Cells[8].Value.ToString();
                string ID = dataGridView2.Rows[seçilialan].Cells[9].Value.ToString();

                textBox13.Text = Bebek_Adı;
                textBox12.Text = Bebek_TC;
                textBox9.Text = Kilo;
                textBox10.Text = Boy;
                comboBox1.Text = Cinsiyet;
                comboBox2.Text = Hastalık_Durumu;
                textBox8.Text = Yapılan_Aşı;
                textBox14.Text = Doğum_Tarihi;
                textBox15.Text = Muayene_Tarihi;
                textBox16.Text = ID;


            }
            
            catch(Exception Ex)
            {
                MessageBox.Show("Sistemsel bir hata meydana geldi.\n\nLütfen tekrar deneyiniz!"+Ex, "SONUÇ İLETİSİ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void kayıtgüncelle_Load(object sender, EventArgs e)
        {
            timer1.Interval = 1000;
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label20.Text = DateTime.Now.ToString();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)//anne tc arama 
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

        private void textBox2_TextChanged(object sender, EventArgs e)//bebek tc arama
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

        private void kayıtgüncelle_FormClosed(object sender, FormClosedEventArgs e)
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

        private void button5_Click(object sender, EventArgs e)//anne veritabanı güncelleme
        {
            try
            {
                    baglan.Open();
                    SqlCommand komut = new SqlCommand("Update Anne_Bilgi set Anne_Adı='" + textBox6.Text + "',Doğum_Tarihi='" + textBox5.Text + "',Kilo='" + textBox3.Text + "',Boy='" + textBox4.Text +
                   "',Muayene_Tarihi='" + textBox11.Text + "',Anne_TC='" + textBox7.Text + "',Kan_Uyuşmazlığı='" + kan.Text+"',ID='"+textBox17.Text + "'where ID='" + textBox17.Text + "'", baglan);
                    komut.ExecuteNonQuery();
                    anneveri("Select *from Anne_Bilgi");
                    baglan.Close();
                
                textBox6.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox7.Clear();
                textBox5.Clear();
                textBox11.Clear();
                textBox17.Clear();
                kan.ResetText();

            }
            catch (Exception Ex)
            {
                MessageBox.Show("Sistemsel bir hata meydana geldi\n\nLütfen tekrar deneyiniz!" + Ex, "UYARI!", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
             
            }
        }

        private void button4_Click(object sender, EventArgs e)// bebek veritabanı güncelleme
        {

            try
            {
                    baglan.Open();

                    SqlCommand komut = new SqlCommand("Update Bebek_Bilgi set Bebek_Adı='" + textBox13.Text + "',Doğum_Tarihi='" + textBox14.Text + "',Kilo='" + textBox9.Text + "',Boy='" + textBox10.Text +
                    "',Cinsiyet='" + comboBox1.Text + "',Hastalık_Durumu='" + comboBox2.Text + "',Muayene_Tarihi='" + textBox15.Text + "',ID='" + textBox16.Text +"',Bebek_TC='" + textBox12.Text + "',Yapılan_Aşı='" + textBox8.Text + "'where ID='" + textBox16.Text + "'", baglan);
                    komut.ExecuteNonQuery();
                    bebekveri("Select *from Bebek_Bilgi");
                    baglan.Close();

                textBox16.Clear();
                textBox13.Clear();
                textBox9.Clear();
                textBox10.Clear();
                comboBox1.ResetText();
                comboBox2.ResetText();
                textBox12.Clear();
                textBox8.Clear();
                textBox14.Clear();
                textBox15.Clear();
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Sistemsel bir hata meydana geldi\n\nLütfen tekrar deneyiniz!"+Ex, "UYARI!", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
              
            }
        }

        private void button6_Click(object sender, EventArgs e)//database anne veri seçme
        {
            anneveri("Select *from Anne_Bilgi");
        }

        private void button7_Click(object sender, EventArgs e)//database bebek veri seçme
        {
            bebekveri("Select *from Bebek_Bilgi");
        }

        private void textBox6_TextChanged(object sender, EventArgs e)//anne ad soyad
        {
            string metin = textBox6.Text;
            string desen = @"\d+";

            Regex rgx = new Regex(desen);
            foreach (Match m in rgx.Matches(metin))
            {
                MessageBox.Show("Ad-Soyad kısmına sayı yazılamaz!\n\nLütfen tekrar deneyiniz!", "SONUÇ İLETİSİ", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                textBox6.Clear();
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
                textBox6.Clear();
            }
        }

        private void textBox7_TextChanged(object sender, EventArgs e)//anne tc güncelleme
        {
            string metin = textBox7.Text;
            string desen = @"\D+";

            Regex rgx = new Regex(desen);
            foreach (Match m in rgx.Matches(metin))
            {
                MessageBox.Show("T.C. kısmına sadece sayı yazılabilir!\n\nLütfen tekrar deneyiniz!", "SONUÇ İLETİSİ", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                textBox7.Clear();
            }
          
        }

        private void textBox5_TextChanged(object sender, EventArgs e)//anne doğum t. güncelleme
        {
            string metin = textBox5.Text;
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
                MessageBox.Show("Doğum tarihi kısmına sembol yazılamaz!\n\nLütfen tekrar deneyiniz!", "SONUÇ İLETİSİ", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                textBox5.Clear();
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)//anne kilo güncelleme
        {
            string metin = textBox3.Text;
           
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
                MessageBox.Show("Ağırlık kısmına sembol yazılamaz!\n\nLütfen tekrar deneyiniz!", "SONUÇ İLETİSİ", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                textBox3.Clear();
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)//anne boy güncelleme
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

        private void textBox11_TextChanged(object sender, EventArgs e)//anne muayene t. güncelleme
        {
            string metin = textBox5.Text;
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
                MessageBox.Show("Muayene tarihi kısmına sembol yazılamaz!\n\nLütfen tekrar deneyiniz!", "SONUÇ İLETİSİ", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                textBox11.Clear();
            }
        }

      

        private void kan_TextChanged(object sender, EventArgs e)//kan güncelleme
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

        private void textBox13_TextChanged(object sender, EventArgs e)//bebek ad soyad güncelleme
        {
            string metin = textBox13.Text;
            string desen = @"\d+";
            Regex rgx = new Regex(desen);
            foreach (Match m in rgx.Matches(metin))
            {
                MessageBox.Show("Ad-Soyad kısmına sayı yazılamaz!\n\nLütfen tekrar deneyiniz!", "SONUÇ İLETİSİ", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                textBox13.Clear();
            }
            int sembol = -1;
            foreach (char chr in metin)
            {
                if (char.IsSymbol(chr)||char.IsPunctuation(chr))
                {
                    sembol++;
                }

            }
            if (sembol >= 0)
            {
                MessageBox.Show("Ad-Soyad kısmına sembol ve noktalama işareti yazılamaz!\n\nLütfen tekrar deneyiniz!", "SONUÇ İLETİSİ", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                textBox13.Clear();
            }
        }

        private void textBox12_TextChanged(object sender, EventArgs e)//bebek tc güncelleme
        {
            string metin = textBox1.Text;
            string desen = @"\D+";
            Regex rgx = new Regex(desen);
            foreach (Match m in rgx.Matches(metin))
            {
                MessageBox.Show("T.C. kısmına sadece sayı yazılabilir!\n\nLütfen tekrar deneyiniz!", "SONUÇ İLETİSİ", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                textBox12.Clear();
            }
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)//bebek cinsiyet güncelleme
        {
            string metin = comboBox1.Text;
            string desen = @"\d+";
            Regex rgx = new Regex(desen);
            foreach (Match m in rgx.Matches(metin))
            {
                MessageBox.Show("Cinsiyet kısmına sayı yazılamaz!\n\nLütfen tekrar deneyiniz!", "SONUÇ İLETİSİ", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                
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
                MessageBox.Show("Cinsiyet kısmına sembol ve noktalama işareti yazılamaz!\n\nLütfen tekrar deneyiniz!", "SONUÇ İLETİSİ", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);

            }
        }

        private void textBox14_TextChanged(object sender, EventArgs e)//bebek doğum t. güncelleme
        {
            string metin = textBox14.Text;
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
                MessageBox.Show("Doğum tarihi kısmına sembol yazılamaz!\n\nLütfen tekrar deneyiniz!", "SONUÇ İLETİSİ", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                textBox14.Clear();
            }
        }

        private void textBox8_TextChanged(object sender, EventArgs e)//bebek aşı güncelleme
        {

            string metin = textBox8.Text;
            string desen = @"\d+";
            Regex rgx = new Regex(desen);
            foreach (Match m in rgx.Matches(metin))
            {
                MessageBox.Show("Yapılan aşı kısmına sayı yazılamaz!\n\nLütfen tekrar deneyiniz!", "SONUÇ İLETİSİ", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                textBox8.Clear();
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
                MessageBox.Show("Yapılan aşı kısmına sembol ve noktalama işareti yazılamaz!\n\nLütfen tekrar deneyiniz!", "SONUÇ İLETİSİ", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                textBox8.Clear();
            }
        }

        private void textBox9_TextChanged(object sender, EventArgs e)//bebek kilo güncelleme
        {
            string metin = textBox9.Text;
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
                textBox9.Clear();

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
                textBox9.Clear();
            }
        }

        private void textBox10_TextChanged(object sender, EventArgs e)//bebek boy güncelleme
        {
            string metin = textBox10.Text;
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
                textBox10.Clear();
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
                textBox10.Clear();
            }
        }

        private void comboBox2_TextChanged(object sender, EventArgs e)//bebek hastalık güncelleme
        {
            string metin = comboBox1.Text;
            string desen = @"\d+";
            Regex rgx = new Regex(desen);
            foreach (Match m in rgx.Matches(metin))
            {
                MessageBox.Show("Hastalık durumu kısmına sayı yazılamaz!\n\nLütfen tekrar deneyiniz!", "SONUÇ İLETİSİ", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
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

        private void textBox15_TextChanged(object sender, EventArgs e)//bebek muayene t. güncelleme
        {
            string metin = textBox15.Text;
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
                MessageBox.Show("Muayene tarihi kısmına sembol yazılamaz!\n\nLütfen tekrar deneyiniz!", "SONUÇ İLETİSİ", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                textBox11.Clear();
            }
        }

        private void textBox17_TextChanged(object sender, EventArgs e)
        {
            string metin = textBox17.Text;
            string desen = @"\D+";
            Regex rgx = new Regex(desen);
            foreach (Match m in rgx.Matches(metin))
            {
                MessageBox.Show("ID kısmına sadece sayı yazılabilir!\n\nLütfen tekrar deneyiniz!", "SONUÇ İLETİSİ", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                textBox17.Clear();
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


