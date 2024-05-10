using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dama_oyunu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        public static string player1, player2;
        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            tahta b = new tahta(player1, player2);
            b.Show();
            player1 = textBox1.Text;
            player2 = textBox2.Text;
        }

        private void oyundanÇıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void kazananKıtmızıToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void kırmızıKazananToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void kazananlarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            kazanan kz = new kazanan();
            kz.Show();
        }
    }
}
