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

namespace DepoProje2
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        string markaid;
        string renkid;
        string marka;
        string renk;

        SqlConnection baglanti = new SqlConnection(@"Data Source=localhost;Initial Catalog=depo;Integrated Security=True");

        private void Form2_Load(object sender, EventArgs e)
        {
            listele();
            listelekisi();
            markalistele();
            renklistele();
            adet();
            label10.Text = DateTime.Now.ToShortDateString();
        }

        void adet()
        {   
            baglanti.Open();
            SqlCommand cmdadet = new SqlCommand("select count(model) from Tbl_aracmain", baglanti);
            object sayi = cmdadet.ExecuteScalar();
            label9.Text = sayi.ToString();
            baglanti.Close();
        }/*sistemdeki arac sayisini labela yazar*/

        void listele()
        {
            SqlCommand cmd = new SqlCommand("select * from Tbl_aracmain", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            adet();
        } /*datayi gridde gosterir*/

        void listelekisi()
        {
            SqlCommand cmdalici = new SqlCommand("select * from Tbl_alici", baglanti);
            SqlDataAdapter da2 = new SqlDataAdapter(cmdalici);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            dataGridView2.DataSource = dt2;
        } /*alici datasini gridde gosterir*/

        void temizle()
        {
            comboBox1.Text = null;
            textBox1.Text = null;
            textBox2.Text = null;
            textBox3.Text = null;
            comboBox2.Text = null;
            textBox4.Text = null;
            textBox5.Text = null;
            textBox6.Text = null;
            textBox7.Text = null;
            textBox8.Text = null;
        }
        void temizle2()
        {
            textBox6.Text = null;
            textBox7.Text = null;
            textBox11.Text = null;
        }

        void markalistele()
        {
            SqlCommand cmd = new SqlCommand("select * from Tbl_aracmarka", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBox1.ValueMember = "markaid";
            comboBox1.DisplayMember = "marka";
            comboBox1.DataSource = dt;
        }/*markayi comboboxta gosterir*/

        void markalsitele2()
        {
            baglanti.Open();
            SqlCommand cmd2 = new SqlCommand("select * from Tbl_aracmarka where markaid=@p1", baglanti);
            cmd2.Parameters.AddWithValue("@p1", marka);
            SqlDataReader dr;
            dr = cmd2.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Text = dr["marka"].ToString();
            }
            baglanti.Close();
        }

        void markaidoku()
        {
            baglanti.Open();
            SqlCommand cmd2 = new SqlCommand("select * from Tbl_aracmarka where marka=@p1", baglanti);
            cmd2.Parameters.AddWithValue("@p1", comboBox1.Text);
            SqlDataReader dr;
            dr = cmd2.ExecuteReader();
            while (dr.Read())
            {
                markaid = dr["markaid"].ToString();
            }
            baglanti.Close();
        }

        void renklistele()
        {
            SqlCommand cmd = new SqlCommand("select * from Tbl_aracrenk", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBox2.ValueMember = "renkid";
            comboBox2.DisplayMember = "renk";
            comboBox2.DataSource = dt;
        }/*rengi comboboxta gosterir*/

        void renklistele2()
        {
            baglanti.Open();
            SqlCommand cmd2 = new SqlCommand("select * from Tbl_aracrenk where renkid=@p1", baglanti);
            cmd2.Parameters.AddWithValue("@p1", renk);
            SqlDataReader dr;
            dr = cmd2.ExecuteReader();
            while (dr.Read())
            {
                comboBox2.Text = dr["renk"].ToString();
            }
            baglanti.Close();
        }/*comboboxtaki secilmis veriyi stringe cevirir*/

        void renkidoku()
        {
            baglanti.Open();
            SqlCommand cmd2 = new SqlCommand("select * from Tbl_aracrenk where renk=@p1", baglanti);
            cmd2.Parameters.AddWithValue("@p1", comboBox2.Text);
            SqlDataReader dr;
            dr = cmd2.ExecuteReader();
            while (dr.Read())
            {
                renkid = dr["renkid"].ToString();
            }
            baglanti.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            markaidoku();
            renkidoku();
            baglanti.Open();
            SqlCommand cmdekle = new SqlCommand("insert into Tbl_aracmain(marka,model,km,yil,renk,fiyat) values (@p1,@p2,@p3,@p4,@p5,@p6)", baglanti);
            cmdekle.Parameters.AddWithValue("@p1", Convert.ToInt32(markaid));
            cmdekle.Parameters.AddWithValue("@p2", textBox1.Text);
            cmdekle.Parameters.AddWithValue("@p3", textBox2.Text);
            cmdekle.Parameters.AddWithValue("@p4", textBox3.Text);
            cmdekle.Parameters.AddWithValue("@p5", Convert.ToInt32(renkid));
            cmdekle.Parameters.AddWithValue("@p6", textBox4.Text);
            cmdekle.ExecuteNonQuery();
            baglanti.Close();
            listele();
            temizle();
            adet();
        }/*secilen ve yazilan veriyi gride ekler*/

        private void button2_Click(object sender, EventArgs e)
        {
            markaidoku();
            renkidoku();
            baglanti.Open();
            SqlCommand cmdgun = new SqlCommand("update Tbl_aracmain set marka=@p1, model=@p2, km=@p3, yil=@p4, renk=@p5, fiyat=@p6 where aracid=@p7", baglanti);
            cmdgun.Parameters.AddWithValue("@p1", Convert.ToInt32(markaid));
            cmdgun.Parameters.AddWithValue("@p2", textBox1.Text);
            cmdgun.Parameters.AddWithValue("@p3", textBox2.Text);
            cmdgun.Parameters.AddWithValue("@p4", textBox3.Text);
            cmdgun.Parameters.AddWithValue("@p5", Convert.ToInt32(renkid));
            cmdgun.Parameters.AddWithValue("@p6", textBox4.Text);
            cmdgun.Parameters.AddWithValue("@p7", Convert.ToInt32(textBox5.Text));
            cmdgun.ExecuteNonQuery();
            baglanti.Close();
            listele();
            temizle();
            adet();
        }/*secilmis veriyi gunceller ve gride gonderir*/

        private void button3_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmdsil = new SqlCommand("delete from Tbl_aracmain where aracid=@p1", baglanti);
            cmdsil.Parameters.AddWithValue("@p1", textBox5.Text);
            cmdsil.ExecuteNonQuery();
            baglanti.Close();
            listele();
            temizle();
            adet();
        }/*Gridde secili olan datayi tamamen siler*/

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            marka = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            renk = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            textBox8.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            markalsitele2();
            renklistele2();
        }/*gridde ustune tiklanan satiri textboxlara ekler*/

        private void button4_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmdsil = new SqlCommand("delete from Tbl_aracmain where aracid=@p1", baglanti);
            cmdsil.Parameters.AddWithValue("@p1", textBox5.Text);
            cmdsil.ExecuteNonQuery();
            baglanti.Close();
            if (textBox9.Text == null)
            {
                textBox9.Text = "0";
                double tutar, bakiye, toplam;
                tutar = Convert.ToDouble(textBox8.Text);
                bakiye = Convert.ToDouble(textBox9.Text);
                toplam = tutar + bakiye;
                textBox9.Text = toplam.ToString();
            }
            else if (textBox9.Text == "")
            {
                textBox9.Text = "0";
                double tutar, bakiye, toplam;
                tutar = Convert.ToDouble(textBox8.Text);
                bakiye = Convert.ToDouble(textBox9.Text);
                toplam = tutar + bakiye;
                textBox9.Text = toplam.ToString();
            }
            else
            {
                double tutar, bakiye, toplam;
                tutar = Convert.ToDouble(textBox8.Text);
                bakiye = Convert.ToDouble(textBox9.Text);
                toplam = tutar + bakiye;
                textBox9.Text = toplam.ToString();
            }
            listele();
            temizle();
            adet();
        }

        private void button5_Click(object sender, EventArgs e)
        { 
            temizle();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmdekle = new SqlCommand("insert into Tbl_alici(aliciadi,aliciadres,alicitc) values (@p1,@p2,@p3)", baglanti);
            cmdekle.Parameters.AddWithValue("@p1", textBox10.Text);
            cmdekle.Parameters.AddWithValue("@p2", textBox12.Text);
            cmdekle.Parameters.AddWithValue("@p3", textBox13.Text.ToString());
            cmdekle.ExecuteNonQuery();
            baglanti.Close();
            listelekisi();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmdekle2 = new SqlCommand("insert into Tbl_alici(aliciadi,aliciadres,alicitc) values (@p1,@p2,@p3)", baglanti);
            cmdekle2.Parameters.AddWithValue("@p1", textBox6.Text);
            cmdekle2.Parameters.AddWithValue("@p2", textBox7.Text);
            cmdekle2.Parameters.AddWithValue("@p3", textBox11.Text.ToString());
            cmdekle2.ExecuteNonQuery();
            baglanti.Close();
            listelekisi();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmdsil2 = new SqlCommand("delete from Tbl_alici where aliciid=@p1", baglanti);
            cmdsil2.Parameters.AddWithValue("@p1", textBox14.Text);
            cmdsil2.ExecuteNonQuery();
            baglanti.Close();
            listelekisi();
            temizle2();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox14.Text = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
            textBox10.Text = dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox12.Text = dataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString();
            textBox13.Text = dataGridView2.Rows[e.RowIndex].Cells[3].Value.ToString();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmdgun2 = new SqlCommand("update Tbl_alici set aliciadi=@p1, aliciadres=@p2, alicitc=@p3 where aliciid=@p4", baglanti);
            cmdgun2.Parameters.AddWithValue("@p1", textBox10.Text);
            cmdgun2.Parameters.AddWithValue("@p2", textBox12.Text);
            cmdgun2.Parameters.AddWithValue("@p3", textBox13.Text);
            cmdgun2.Parameters.AddWithValue("@p4", Convert.ToInt32(textBox14.Text));
            cmdgun2.ExecuteNonQuery();
            baglanti.Close();
            listelekisi();
            temizle2();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
