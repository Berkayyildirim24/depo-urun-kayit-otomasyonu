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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=localhost;Initial Catalog=depo;Integrated Security=True");

        private void Form1_Load(object sender, EventArgs e)
        {
            label3.Text = DateTime.Now.ToShortDateString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                string kul = "select * from Tbl_kullanici where kullaniciadi=@p1 and kullanicisifre=@p2";
                SqlParameter prm1 = new SqlParameter("p1", textBox1.Text.Trim());
                SqlParameter prm2 = new SqlParameter("p2", textBox2.Text.Trim());
                SqlCommand cmd = new SqlCommand(kul, baglanti);
                cmd.Parameters.Add(prm1);
                cmd.Parameters.Add(prm2);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    Form2 frm = new Form2();
                    frm.Show();
                    this.Hide();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("hatali giris");
            }
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
