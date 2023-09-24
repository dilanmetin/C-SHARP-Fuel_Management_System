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

namespace Akaryakıt_Takip_Sistemi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //

        SqlConnection baglanti = new SqlConnection(@"Data Source=DILAN\SQLEXPRESS;Initial Catalog=TestBenzin;Integrated Security=True");

        void listele()
        {
            // V / Max Kurşunsuz 95
            baglanti.Open();
            SqlCommand komut = new SqlCommand("SELECT * FROM TBLBENZIN where petroltur='V/Max Kurşunsuz 95' ", baglanti);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                lblKursunsuz95.Text = dr[3].ToString();
                progressBar1.Value = int.Parse(dr[4].ToString());
                lblKursunsuz95Lt.Text = dr[4].ToString();
            }
            baglanti.Close();

            // V / Max Diesel
            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("SELECT * FROM TBLBENZIN where petroltur='V/Max Diesel' ", baglanti);
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                lblMaxDiesel.Text = dr2[3].ToString();
                progressBar2.Value = int.Parse(dr2[4].ToString());
                lblMaxDieselLt.Text = dr2[4].ToString();


            }
            baglanti.Close();

            // V/Pro Diesel
            baglanti.Open();
            SqlCommand komut3 = new SqlCommand("SELECT * FROM TBLBENZIN where petroltur='V/Pro Diesel' ", baglanti);
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                lblProDiesel.Text = dr3[3].ToString();
                progressBar3.Value = int.Parse(dr3[4].ToString());
                lblProDieselLt.Text = dr3[4].ToString();


            }
            baglanti.Close();

            // PO/gaz Otogaz
            baglanti.Open();
            SqlCommand komut4 = new SqlCommand("SELECT * FROM TBLBENZIN where petroltur='PO/gaz Otogaz' ", baglanti);
            SqlDataReader dr4 = komut4.ExecuteReader();
            while (dr4.Read())
            {
                lblOtogaz.Text = dr4[3].ToString();
                progressBar4.Value = int.Parse(dr4[4].ToString());
                lblOtogazLt.Text = dr4[4].ToString();


            }
            baglanti.Close();
            baglanti.Open();
            SqlCommand komut5 = new SqlCommand("SELECT * FROM TBLKASA" , baglanti);
            SqlDataReader dr5 = komut5.ExecuteReader();
            while(dr5.Read())
            {
                lblKasa.Text = dr5[0].ToString();
            }
            baglanti.Close();

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
          
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            double kursunsuz95, litre, tutar;
            kursunsuz95= Convert.ToDouble(lblKursunsuz95.Text);
            litre = Convert.ToDouble(numericUpDown1.Value);
            tutar = kursunsuz95*litre;
            txtKursunsuzFyt.Text = tutar.ToString();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            double maxdiesel, litre, tutar;
            maxdiesel = Convert.ToDouble(lblMaxDiesel.Text);
            litre = Convert.ToDouble(numericUpDown2.Value);
            tutar = maxdiesel * litre;
            txtMaxDieselFyt.Text = tutar.ToString();

        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            double prodiesel, litre, tutar;
            prodiesel = Convert.ToDouble(lblProDiesel.Text);
            litre = Convert.ToDouble(numericUpDown3.Value);
            tutar = prodiesel * litre;
            txtProDieselFyt.Text = tutar.ToString();

        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            double otogaz, litre, tutar;
            otogaz = Convert.ToDouble(lblOtogaz.Text);
            litre = Convert.ToDouble(numericUpDown4.Value);
            tutar = otogaz * litre;
            txtOtogazFyt.Text = tutar.ToString();

        }

        private void btnDepoDoldur_Click(object sender, EventArgs e)
        {
            if (txtPlaka.Text == "")
            {
                MessageBox.Show("Plaka giriniz");


            }
            else
            {
                if (numericUpDown1.Value != 0)
                {
                    baglanti.Open();
                    SqlCommand komut = new SqlCommand("insert into TBLHAREKET (PLAKA, BENZINTURU, LITRE, FIYAT) " +
                        "VALUES (@P1, @P2, @P3, @P4)", baglanti);
                    komut.Parameters.AddWithValue("@P1", txtPlaka.Text);
                    komut.Parameters.AddWithValue("@P2", "V/Max Kurşunsuz 95");
                    komut.Parameters.AddWithValue("@P3", numericUpDown1.Value);
                    komut.Parameters.AddWithValue("@P4", decimal.Parse(txtKursunsuzFyt.Text));
                    komut.ExecuteNonQuery();
                    baglanti.Close();


                    baglanti.Open();
                    SqlCommand komut2 = new SqlCommand("UPDATE TBLKASA SET MIKTAR = MIKTAR + @P1", baglanti);
                    komut2.Parameters.AddWithValue("@P1", decimal.Parse(txtKursunsuzFyt.Text));
                    komut2.ExecuteNonQuery();
                    baglanti.Close();

                    baglanti.Open();
                    SqlCommand komut3 = new SqlCommand("UPDATE TBLBENZIN SET STOK = STOK - @P1 WHERE PETROLTUR= 'V/Max Kurşunsuz 95' ", baglanti);
                    komut3.Parameters.AddWithValue("@P1", numericUpDown1.Value);
                    komut3.ExecuteNonQuery();
                    baglanti.Close();
                    MessageBox.Show("Satış Yapıldı");
                    listele();
                }


                if (numericUpDown2.Value != 0)
                {
                    baglanti.Open();
                    SqlCommand komut = new SqlCommand("insert into TBLHAREKET (PLAKA, BENZINTURU, LITRE, FIYAT) " +
                        "VALUES (@P1, @P2, @P3, @P4)", baglanti);
                    komut.Parameters.AddWithValue("@P1", txtPlaka.Text);
                    komut.Parameters.AddWithValue("@P2", "V/Max Diesel");
                    komut.Parameters.AddWithValue("@P3", numericUpDown2.Value);
                    komut.Parameters.AddWithValue("@P4", decimal.Parse(txtMaxDieselFyt.Text));
                    komut.ExecuteNonQuery();
                    baglanti.Close();


                    baglanti.Open();
                    SqlCommand komut2 = new SqlCommand("UPDATE TBLKASA SET MIKTAR = MIKTAR + @P1", baglanti);
                    komut2.Parameters.AddWithValue("@P1", decimal.Parse(txtMaxDieselFyt.Text));
                    komut2.ExecuteNonQuery();
                    baglanti.Close();

                    baglanti.Open();
                    SqlCommand komut3 = new SqlCommand("UPDATE TBLBENZIN SET STOK = STOK - @P1 WHERE PETROLTUR= 'V/Max Diesel' ", baglanti);
                    komut3.Parameters.AddWithValue("@P1", numericUpDown2.Value);
                    komut3.ExecuteNonQuery();
                    baglanti.Close();
                    MessageBox.Show("Satış Yapıldı");
                    listele();
                }

                if (numericUpDown3.Value != 0)
                {
                    baglanti.Open();
                    SqlCommand komut = new SqlCommand("insert into TBLHAREKET (PLAKA, BENZINTURU, LITRE, FIYAT) " +
                        "VALUES (@P1, @P2, @P3, @P4)", baglanti);
                    komut.Parameters.AddWithValue("@P1", txtPlaka.Text);
                    komut.Parameters.AddWithValue("@P2", "V/Pro Diesel");
                    komut.Parameters.AddWithValue("@P3", numericUpDown3.Value);
                    komut.Parameters.AddWithValue("@P4", decimal.Parse(txtProDieselFyt.Text));
                    komut.ExecuteNonQuery();
                    baglanti.Close();


                    baglanti.Open();
                    SqlCommand komut2 = new SqlCommand("UPDATE TBLKASA SET MIKTAR = MIKTAR + @P1", baglanti);
                    komut2.Parameters.AddWithValue("@P1", decimal.Parse(txtProDieselFyt.Text));
                    komut2.ExecuteNonQuery();
                    baglanti.Close();

                    baglanti.Open();
                    SqlCommand komut3 = new SqlCommand("UPDATE TBLBENZIN SET STOK = STOK - @P1 WHERE PETROLTUR= 'V/Pro Diesel' ", baglanti);
                    komut3.Parameters.AddWithValue("@P1", numericUpDown3.Value);
                    komut3.ExecuteNonQuery();
                    baglanti.Close();
                    MessageBox.Show("Satış Yapıldı");
                    listele();
                }


                if (numericUpDown4.Value != 0)
                {
                    baglanti.Open();
                    SqlCommand komut = new SqlCommand("insert into TBLHAREKET (PLAKA, BENZINTURU, LITRE, FIYAT) " +
                        "VALUES (@P1, @P2, @P3, @P4)", baglanti);
                    komut.Parameters.AddWithValue("@P1", txtPlaka.Text);
                    komut.Parameters.AddWithValue("@P2", "PO/gaz Otogaz");
                    komut.Parameters.AddWithValue("@P3", numericUpDown4.Value);
                    komut.Parameters.AddWithValue("@P4", decimal.Parse(txtOtogazFyt.Text));
                    komut.ExecuteNonQuery();
                    baglanti.Close();


                    baglanti.Open();
                    SqlCommand komut2 = new SqlCommand("UPDATE TBLKASA SET MIKTAR = MIKTAR + @P1", baglanti);
                    komut2.Parameters.AddWithValue("@P1", decimal.Parse(txtOtogazFyt.Text));
                    komut2.ExecuteNonQuery();
                    baglanti.Close();

                    baglanti.Open();
                    SqlCommand komut3 = new SqlCommand("UPDATE TBLBENZIN SET STOK = STOK - @P1 WHERE PETROLTUR= 'PO/gaz Otogaz' ", baglanti);
                    komut3.Parameters.AddWithValue("@P1", numericUpDown4.Value);
                    komut3.ExecuteNonQuery();
                    baglanti.Close();
                    MessageBox.Show("Satış Yapıldı");
                    listele();
                }

            }

            
            
                

            





        }
    }
}
