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
    public partial class kazanan : Form
    {
        public kazanan()
        {
            InitializeComponent();
        }
        OleDbConnection con;
        OleDbDataAdapter da;
        OleDbCommand cmd;
        DataSet ds;
        DataTable dt;
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void kazanan_Load(object sender, EventArgs e)
        {
      //*********************************************kırmızı tablo*******************************************
            con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=kazanan_kirmizi.accdb");
            da = new OleDbDataAdapter("SElect *from kazananalar", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "kazananalar");
            dataGridView2.DataSource = ds.Tables["kazananalar"];
            con.Close();
    //*******************************************************************************************************
            con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=kazanan_siyah.accdb");
            da = new OleDbDataAdapter("SElect *from kazananalar", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "kazananalar");
            dataGridView1.DataSource = ds.Tables["kazananalar"];
            con.Close();


        
        }
    }
}
