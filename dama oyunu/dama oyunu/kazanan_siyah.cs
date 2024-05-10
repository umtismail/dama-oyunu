using System;
using System.Data;
using System.Windows.Forms;
using System.Data.OleDb;


namespace dama_oyunu
{
    public partial class kazanan_siyah : Form
    {
        public kazanan_siyah()
        {
            InitializeComponent();
        }

        OleDbConnection con;
        OleDbDataAdapter da;
        OleDbCommand cmd;
        DataSet ds;
        DataTable dt;
        Form1 frm = new Form1();
        void gri()
        {
            con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=kazanan_siyah.accdb");
            da = new OleDbDataAdapter("SElect *from kazananalar", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "kazananalar");
            dataGridView1.DataSource = ds.Tables["kazananalar"];
            con.Close();
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

        private void kazanan_siyah_Load(object sender, EventArgs e)
        {
            gri();
            textBox1.Text = Form1.player2;
        }
      }        
    }

