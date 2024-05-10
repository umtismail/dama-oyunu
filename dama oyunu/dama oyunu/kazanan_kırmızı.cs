using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
namespace dama_oyunu
{
    public partial class kazanan_kırmızı : Form
    {
        public kazanan_kırmızı()
        {
            InitializeComponent();
        }
        OleDbConnection con;
        OleDbDataAdapter da;
        OleDbCommand cmd;
        DataSet ds;
        DataTable dt;
        void gri()
        {
            con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=kazanan_kirmizi.accdb");
            da = new OleDbDataAdapter("SElect *from kazananalar", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "kazananalar");
            dataGridView1.DataSource = ds.Tables["kazananalar"];
            con.Close();
        }

        private void kazanan_kırmızı_Load(object sender, EventArgs e)
        {
            gri();
            textBox1.Text = Form1.player1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                cmd = new OleDbCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "insert into kazananalar (kazananID) values (@kazananID)";
                cmd.Parameters.AddWithValue("@kazananID", textBox1.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                gri();
            }
            catch (Exception h)
            {
                MessageBox.Show(h.Message);
            }
            Form1 x = new Form1();
            x.Show();
            this.Close();
      
        }
    }
}
