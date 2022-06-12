﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace WindowsFormsApp2
{
    public partial class UyeEklefrm : Form
    {
        bool durum;
        OleDbConnection con;
        OleDbDataAdapter da;
        OleDbCommand cmd;
        DataSet ds;
        DataTable tablo = new DataTable();
        public UyeEklefrm()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
        void VarMi()
        {
                con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + Application.StartupPath + "\\Kütüphane.accdb");
                con.Open();
                OleDbCommand komut = new OleDbCommand("Select * from Uye where OkulNumarasi=@p1", con);
                komut.Parameters.AddWithValue("@p1", textBox2.Text);
                OleDbDataReader dr = komut.ExecuteReader();
                if (dr.Read())
                {
                    durum = false;
                }
                else
                {
                    durum = true;
                }
                con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Üye Ad Soyad Bölgesini Boş Bırakmayın!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (string.IsNullOrEmpty(comboBox1.Text))
            {
                MessageBox.Show("Üye Cinsiyet Bölgesini Seçin!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Üye Okul Numarası Bölgesini Boş Bırakmayın!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (string.IsNullOrEmpty(comboBox3.Text))
            {
                MessageBox.Show("Üyenin Alanı Bölgesini Seçin!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (string.IsNullOrEmpty(comboBox2.Text))
            {
                MessageBox.Show("Üye Sınıf Bölgesini Seçin!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (string.IsNullOrEmpty(textBox4.Text))
            {
                MessageBox.Show("Üye Şube Bölgesini Boş Bırakmayın!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (string.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("Üye Okunan Kitap Sayısı Bölgesini Boş Bırakmayın!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                VarMi();

                if (durum == false)
                {
                    MessageBox.Show("Bu Öğrenci No İle Daha Önce Kayıt Yapılmış", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DialogResult cevap = new DialogResult();
                    cevap = MessageBox.Show("Üye Eklemek İstediğinize Eminmisiniz", "Üye Ekleme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (cevap == DialogResult.Yes)
                    {
                        con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + Application.StartupPath + "\\Kütüphane.accdb");
                        string sorgu = "Insert into Uye (AdSoyad,Cinsiyet,OkulNumarasi,Alani,Sinif,OkunanKitapSayisi) values (@AdSoyad,@Cinsiyet,@OkulNumarasi,@Alani,@Sinif,@OkunanKitapSayisi)";
                        cmd = new OleDbCommand(sorgu, con);
                        cmd.Parameters.AddWithValue("@AdSoyad", textBox1.Text);
                        cmd.Parameters.AddWithValue("@Cinsiyet", comboBox1.SelectedItem);
                        cmd.Parameters.AddWithValue("@OkulNumarasi", Convert.ToInt32(textBox2.Text));
                        cmd.Parameters.AddWithValue("@Alani", comboBox3.Text);
                        cmd.Parameters.AddWithValue("@Sinif", comboBox2.Text + "/" + textBox4.Text);
                        cmd.Parameters.AddWithValue("@OkunanKitapSayisi", Convert.ToInt32(textBox3.Text));
                        con.Open();
                        cmd.ExecuteNonQuery();
                        comboBox1.Text = "";
                        comboBox2.Text = "";
                        comboBox3.Text = "";
                        textBox1.Text = "";
                        textBox3.Text = "";
                        textBox2.Text = "";
                        textBox4.Text = "";
                        MessageBox.Show("Üye Başarıyla Kayıt Edildi.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        con.Close();
                    }
                    else
                    {
                        comboBox1.Text = "";
                        comboBox2.Text = "";
                        comboBox3.Text = "";
                        textBox1.Text = "";
                        textBox3.Text = "";
                        textBox2.Text = "";
                        textBox4.Text = "";
                        MessageBox.Show("İşlem İptal Edildi", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void UyeEklefrm_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult cevap = new DialogResult();
            cevap = MessageBox.Show("Çıkmak İstediğinize Eminmisiniz", "Çıkış", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (cevap == DialogResult.Yes)
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("İşlem İptal Edildi", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
