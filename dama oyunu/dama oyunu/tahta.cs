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
    public partial class tahta : Form
    {
        Panel pa = new Panel();
        konum[,] bu;
        bool tur = true;
        bool ilktıklama = true;
        taslar pictures;
        string player1 = "kırmızı", player2 = "siyah";
            
        
        class konum : Button
        {
            private int x, y;
            taslar pictures;
            public konum(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
            public int GetX()
            {
                return x;
            }

            public int GetY()
            {
                return y;
            }

            public void AddIms()
            {
                pictures = new taslar(this.Size);
                this.Image = pictures.Getsiyah();
            }

            public void AddImks()
            {
                pictures = new taslar(this.Size);
                this.Image = pictures.Getskralice();
            }

            public void AddImkk()
            {
                pictures = new taslar(this.Size);
                this.Image = pictures.Getkkralice();
            }

            public void AddImk()
            {
                pictures = new taslar(this.Size);
                this.Image = pictures.Getkırımızı();
            }

            public void RemIm()
            {
                this.Image = null;
            }
        }
        public tahta(string pl1, string pl2)
        {
            
            if (pl1 != "")
            {
                player1 = pl1;
            }
            if (pl2 != "")
            {
                player2 = pl2;
            }
            this.Text = player1 + " vs. " + player2;
            this.Size = new Size(1000, 1000);
            pa.Size = new Size(this.Width, this.Height);
            pa.Location = new Point(0, 0);
            this.Size = new Size(800, 800);
            pa.Size = new Size(this.Width, this.Height);
            this.Controls.Add(pa);
            bu = new konum[8, 8];
            for (int i = 0; i < bu.GetLength(0); i++)
            {
                for (int j = 0; j < bu.GetLength(1); j++)
                {
                    bu[i, j] = new konum(i, j);
                    bu[i, j].Size = new Size((pa.Width - 15) / 8, (pa.Height - 36) / 8); //tablodaki karelerin boyutlarını ayarlıyor
                    bu[i, j].Location = new Point(pa.Location.X + (j * ((pa.Width - 15) / 8)), pa.Location.Y + (i * ((pa.Height - 36) / 8))); // kareleri yerleştiriyor
                    bu[i, j].Click += new EventHandler(bu_Click);
                    bu[i, j].Tag = new int[3] { i, j,0 };
                    if ((i + j) % 2 == 1) // kırmızı karelerin renkleri
                    {
                        bu[i, j].BackColor = Color.FromArgb(250, 0, 0);
                        bu[i, j].Enabled = false;
                    }
                    else
                    {
                        bu[i, j].BackColor = Color.FromArgb(250, 250, 250); // beyaz karenin renkleri
                        bu[i, j].FlatAppearance.MouseOverBackColor = Color.FromArgb(170, 170, 170);
                        if (i > 4)
                        {
                            pictures = new taslar(bu[i, j].Size);
                            bu[i, j].Image = pictures.Getkırımızı(); // kırmızı taşları yerleştime
                            bu[i, j].Tag = new int[3] { i, j, 1 };
                        }
                        if (i < 3)
                        {
                            pictures = new taslar(bu[i, j].Size);
                            bu[i, j].Image = pictures.Getsiyah();       // siyah taşları yerleştime
                            bu[i, j].Tag = new int[3] { i, j, 2 };
                        }
                    }
                    pa.Controls.Add(bu[i, j]);
                }
            }
        }
        int[,] tahtaa = new int[,]
        {
            {2,-3,2,-3,2,-3,2,-3},   //2 olanlar siyash taşların yerleştirildiği yer
            {-3,2,-3,2,-3,2,-3,2},   // 1 olanlar kırmızıların terleştiği yer
            {2,-3,2,-3,2,-3,2,-3},   //0 olanlar hareket edebilir boş yerler
            {-3,0,-3,0,-3,0,-3,0},   // -3 taşların hareket edemediği yerler
            {0,-3,0,-3,0,-3,0,-3},
            {-3,1,-3,1,-3,1,-3,1},
            {1,-3,1,-3,1,-3,1,-3},
            {-3,1,-3,1,-3,1,-3,1}
        };
        int oldx = 0, oldy = 0, tag = 0;
       public void bu_Click(object sender, EventArgs e)
        {
           int[] but = (int[])(((Button)(sender)).Tag);
            int x = but[0], y = but[1];
            if (tur)
            {
                if (ilktıklama)
                {
                    if (tahtaa[x, y] == 1) //piyon hareket
                    {
                        if (x > 0 && y < tahtaa.GetLength(1) - 1 && tahtaa[x - 1, y + 1] == 0) // piyon hareket
                        {
                            bu[x - 1, y + 1].BackColor = Color.Green;
                            bu[x - 1, y + 1].FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 250, 0);
                        }
                        if (x > 0 && y > 0 && tahtaa[x - 1, y - 1] == 0)
                        {
                            bu[x - 1, y - 1].BackColor = Color.Green;
                            bu[x - 1, y - 1].FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 250, 0);
                        }
                        if (x > 1 && y > 1 && (tahtaa[x - 1, y - 1] == 2) && tahtaa[x - 2, y - 2] == 0)// sol atlama
                        {
                            bu[x - 2, y - 2].BackColor = Color.Green;
                            bu[x - 2, y - 2].FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 250, 0);
                        }
                        if (x > 1 && y < tahtaa.GetLength(1) - 2 && (tahtaa[x - 1, y + 1] == 2 || tahtaa[x - 1, y + 1] == 22) && tahtaa[x - 2, y + 2] == 0)// sağ atlama
                        {
                            bu[x - 2, y + 2].BackColor = Color.Green;
                            bu[x - 2, y + 2].FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 250, 0);
                        }
                        oldx = x;
                        oldy = y;
                        ilktıklama = !ilktıklama;
                    }
                    if (tahtaa[x, y] == 11) //kraliçe hareket
                    {
                        for (int i = x + 1, j = y + 1; i < tahtaa.GetLength(0) && j < tahtaa.GetLength(1); i++, j++) // sağ alt
                        {
                            if (tahtaa[i, j] == 1 || tahtaa[i, j] == 11)
                            {
                                break;
                            }
                            if (tahtaa[i, j] == 2 || tahtaa[i, j] == 22) // atlama
                            {
                                if (j < tahtaa.GetLength(1) - 1 && i < tahtaa.GetLength(0) - 1 && tahtaa[i + 1, j + 1] == 0)
                                {
                                    bu[i + 1, j + 1].BackColor = Color.Green;
                                    bu[i + 1, j + 1].FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 250, 0);
                                }
                                break;
                            }
                            bu[i, j].BackColor = Color.Green;
                            bu[i, j].FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 250, 0);
                        }
                        for (int i = x + 1, j = y - 1; i < tahtaa.GetLength(0) && j > -1; i++, j--) // sağ alt
                        {
                            if (tahtaa[i, j] == 1 || tahtaa[i, j] == 11)
                            {
                                break;
                            }
                            if ((tahtaa[i, j] == 2 || tahtaa[i, j] == 22))
                            {
                                if (j > 0 && i < tahtaa.GetLength(0) - 1 && tahtaa[i + 1, j - 1] == 0)
                                {
                                    bu[i + 1, j - 1].BackColor = Color.Green;
                                    bu[i + 1, j - 1].FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 250, 0);
                                }
                                break;
                            }
                            bu[i, j].BackColor = Color.Green;
                            bu[i, j].FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 250, 0);
                        }
                        for (int i = x - 1, j = y + 1; i > -1 && j < tahtaa.GetLength(1); i--, j++) // sol ust hareket
                        {
                            if (tahtaa[i, j] == 1 || tahtaa[i, j] == 11)
                            {
                                break;
                            }
                            if (tahtaa[i, j] == 2 || tahtaa[i, j] == 22)
                            {
                                if (j < tahtaa.GetLength(1) - 1 && i > 0 && tahtaa[i - 1, j + 1] == 0)
                                {
                                    bu[i - 1, j + 1].BackColor = Color.Green;
                                    bu[i - 1, j + 1].FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 250, 0);
                                }
                                break;
                            }
                            bu[i, j].BackColor = Color.Green;
                            bu[i, j].FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 250, 0);
                        }
                        for (int i = x - 1, j = y - 1; i > -1 && j > -1; i--, j--) // sol alt
                        {
                            if (tahtaa[i, j] == 1 || tahtaa[i, j] == 11)
                            {
                                break;
                            }
                            if (tahtaa[i, j] == 2 || tahtaa[i, j] == 22)
                            {
                                if (j > 0 && i > 0 && tahtaa[i - 1, j - 1] == 0)
                                {
                                    bu[i - 1, j - 1].BackColor = Color.Green;
                                    bu[i - 1, j - 1].FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 250, 0);
                                }
                                break;
                            }
                            bu[i, j].BackColor = Color.Green;
                            bu[i, j].FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 250, 0);
                        }
                        oldx = x;
                        oldy = y;
                        ilktıklama = !ilktıklama;
                    }
                }
                else
                {
                   if ((tahtaa[x, y] == 1 || tahtaa[x, y] == 11))
                    {
                        for (int i = 0; i < tahtaa.GetLength(0); i++)
                        {
                            for (int j = 0; j < tahtaa.GetLength(1); j++)
                            {
                                if (bu[i, j].BackColor == Color.Green)
                                {
                                    bu[i, j].BackColor = Color.White;
                                }
                            }
                        }
                        ilktıklama = !ilktıklama;
                    }
                    if (bu[x, y].BackColor == Color.Green)
                    {
                        for (int i = 0; i < tahtaa.GetLength(0); i++)
                        {
                            for (int j = 0; j < tahtaa.GetLength(1); j++)
                            {
                                if (bu[i, j].BackColor == Color.Green)
                                {
                                    bu[i, j].BackColor = Color.White;   // yeşil yolu beyaz yapar
                                    bu[i, j].FlatAppearance.MouseOverBackColor = Color.FromArgb(170, 170, 170);
                                }
                            }
                        }
                        tag = tahtaa[oldx, oldy];
                        if (tag == 1 && Math.Abs(oldx - x) == 2) // piyon yeme
                        {
                            tahtaa[(oldx + x) / 2, (oldy + y) / 2] = 0;         //siyah taşların kaybolma kodu
                            bu[(oldx + x) / 2, (oldy + y) / 2].RemIm();
                            bu[(oldx + x) / 2, (oldy + y) / 2].BackColor = Color.White;

                        }
                        if (tag == 11) //kraliçe yeme
                        {
                            int k = 1;
                            int i = oldx, j = oldy;
                            if (x < i && y < j)
                            {
                                if (tahtaa[x + k, y + k] == 2 || tahtaa[x + k, y + k] == 22)
                                {
                                    tahtaa[x + k, y + k] = 0;
                                    bu[x + k, y + k].BackColor = Color.White;
                                    bu[x + k, y + k].RemIm();

                                }
                            }
                            if (x < i && y > j)
                                if (tahtaa[x + k, y - k] == 2 || tahtaa[x + k, y - k] == 22)
                                {
                                    tahtaa[x + k, y - k] = 0;
                                    bu[x + k, y - k].BackColor = Color.White;
                                    bu[x + k, y - k].RemIm();

                                }
                            if (x > i && y < j)
                                if (tahtaa[x - k, y + k] == 2 || tahtaa[x - k, y + k] == 22)
                                {
                                    tahtaa[x - k, y + k] = 0;
                                    bu[x - k, y + k].BackColor = Color.White;
                                    bu[x - k, y + k].RemIm();

                                }
                            if (x > i && y > j)
                                if (tahtaa[x - k, y - k] == 2 || tahtaa[x - k, y - k] == 22)
                                {
                                    tahtaa[x - k, y - k] = 0;
                                    bu[x - k, y - k].BackColor = Color.White;
                                    bu[x - k, y - k].RemIm();

                                }
                        }
                           if (x == 0) //dönüşüm
                        {
                            tahtaa[x, y] = 11;      // kırmızı kraliçe dönüşüm kodu
                            bu[x, y].AddImkk();
                        }
                        else
                        {
                            if (tag == 1)
                            {
                                bu[x, y].AddImk();
                                tahtaa[x, y] = 1;
                            }
                             if (tag == 11)
                            {
                                bu[x, y].AddImkk();
                                tahtaa[x, y] = 11;
                            }
                        }
                        bu[oldx, oldy].RemIm();
                        tahtaa[oldx, oldy] = 0;
                        tur = !tur;
                        ilktıklama = !ilktıklama;
                        kazanan(2);
                    }
                }
            }
            else //siyah taş
            {
                if (ilktıklama)
                {
                    if (tahtaa[x, y] == 2)
                    {
                        if (x < tahtaa.GetLength(0) - 1 && y < tahtaa.GetLength(1) - 1 && tahtaa[x + 1, y + 1] == 0)
                        {
                            bu[x + 1, y + 1].BackColor = Color.Green;
                            bu[x + 1, y + 1].FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 250, 0);
                        }
                        if (x < tahtaa.GetLength(0) - 1 && y > 0 && tahtaa[x + 1, y - 1] == 0)
                        {
                            bu[x + 1, y - 1].BackColor = Color.Green;
                            bu[x + 1, y - 1].FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 250, 0);
                        }
                        if (x < tahtaa.GetLength(0) - 2 && y > 1 && (tahtaa[x + 1, y - 1] == 1 || tahtaa[x + 1, y - 1] == 11) && tahtaa[x + 2, y - 2] == 0)
                        {
                            bu[x + 2, y - 2].BackColor = Color.Green;
                            bu[x + 2, y - 2].FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 250, 0);
                        }
                        if (x < tahtaa.GetLength(0) - 2 && y < tahtaa.GetLength(1) - 2 && (tahtaa[x + 1, y + 1] == 1 || tahtaa[x + 1, y + 1] == 11) && tahtaa[x + 2, y + 2] == 0)
                        {
                            bu[x + 2, y + 2].BackColor = Color.Green;
                            bu[x + 2, y + 2].FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 250, 0);
                        }
                        oldx = x;
                        oldy = y;
                        ilktıklama = !ilktıklama;
                    }
                    if (tahtaa[x, y] == 22) //hareket kraliçe
                    {
                        for (int i = x + 1, j = y + 1; i < tahtaa.GetLength(0) && j < tahtaa.GetLength(1); i++, j++) // sol aşaı
                        {
                            if (tahtaa[i, j] == 2 || tahtaa[i, j] == 22)
                            {
                                break;
                            }
                            if (tahtaa[i, j] == 1 || tahtaa[i, j] == 11)
                            {
                                if (j < tahtaa.GetLength(1) - 1 && i < tahtaa.GetLength(0) - 1 && tahtaa[i + 1, j + 1] == 0)
                                {
                                    bu[i + 1, j + 1].BackColor = Color.Green;
                                    bu[i + 1, j + 1].FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 250, 0);
                                }
                                break;
                            }
                            bu[i, j].BackColor = Color.Green;
                            bu[i, j].FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 250, 0);
                        }
                        for (int i = x + 1, j = y - 1; i < tahtaa.GetLength(0) && j > -1; i++, j--) // sağ aşaı
                        {
                            if (tahtaa[i, j] == 2 || tahtaa[i, j] == 22)
                            {
                                break;
                            }
                            if ((tahtaa[i, j] == 1 || tahtaa[i, j] == 11))
                            {
                                if (j > 0 && i < tahtaa.GetLength(0) - 1 && tahtaa[i + 1, j - 1] == 0)
                                {
                                    bu[i + 1, j - 1].BackColor = Color.Green;
                                    bu[i + 1, j - 1].FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 250, 0);
                                }
                                break;
                            }
                            bu[i, j].BackColor = Color.Green;
                            bu[i, j].FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 250, 0);
                        }
                        for (int i = x - 1, j = y + 1; i > -1 && j < tahtaa.GetLength(1); i--, j++) // sol ust
                        {
                            if (tahtaa[i, j] == 2 || tahtaa[i, j] == 22)
                            {
                                break;
                            }
                            if (tahtaa[i, j] == 1 || tahtaa[i, j] == 11)
                            {
                                if (j < tahtaa.GetLength(1) - 1 && i > 0 && tahtaa[i - 1, j + 1] == 0)
                                {
                                    bu[i - 1, j + 1].BackColor = Color.Green;
                                    bu[i - 1, j + 1].FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 250, 0);
                                }
                                break;
                            }
                            bu[i, j].BackColor = Color.Green;
                            bu[i, j].FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 250, 0);
                        }
                        for (int i = x - 1, j = y - 1; i > -1 && j > -1; i--, j--) // sağ yukarı
                        {
                            if (tahtaa[i, j] == 2 || tahtaa[i, j] == 22)
                            {
                                break;
                            }
                            if (tahtaa[i, j] == 1 || tahtaa[i, j] == 11)
                            {
                                if (j > 0 && i > 0 && tahtaa[i - 1, j - 1] == 0)
                                {
                                    bu[i - 1, j - 1].BackColor = Color.Green;
                                    bu[i - 1, j - 1].FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 250, 0);
                                }
                                break;
                            }
                            bu[i, j].BackColor = Color.Green;
                            bu[i, j].FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 250, 0);
                        }
                        oldx = x;
                        oldy = y;
                        ilktıklama = !ilktıklama;
                    }
                }
                else
                {
                    if ((tahtaa[x, y] == 2 || tahtaa[x, y] == 22))
                    {
                        for (int i = 0; i < tahtaa.GetLength(0); i++)
                        {
                            for (int j = 0; j < tahtaa.GetLength(1); j++)
                            {
                                if (bu[i, j].BackColor == Color.Green)
                                {
                                    bu[i, j].BackColor = Color.White;
                                }
                            }
                        }
                        ilktıklama = !ilktıklama;
                    }
                    if (bu[x, y].BackColor == Color.Green)
                    {
                        for (int i = 0; i < tahtaa.GetLength(0); i++)
                        {
                            for (int j = 0; j < tahtaa.GetLength(1); j++)
                            {
                                if (bu[i, j].BackColor == Color.Green)
                                {
                                    bu[i, j].BackColor = Color.White;
                                    bu[i, j].FlatAppearance.MouseOverBackColor = Color.FromArgb(170, 170, 170);
                                }
                            }
                        }
                        tag = tahtaa[oldx, oldy];
                        if (tag == 2 && Math.Abs(oldx - x) == 2) // kırmızı yeme
                        {
                            tahtaa[(oldx + x) / 2, (oldy + y) / 2] = 0;
                            bu[(oldx + x) / 2, (oldy + y) / 2].RemIm();
                            bu[(oldx + x) / 2, (oldy + y) / 2].BackColor = Color.White;

                        }
                        if (tag == 22) //kraliçenin yeme
                        {
                            int k = 1;
                            int i = oldx, j = oldy;
                            if (x < i && y < j)
                                if (tahtaa[x + k, y + k] == 1 || tahtaa[x + k, y + k] == 11)
                                {
                                    tahtaa[x + k, y + k] = 0;
                                    bu[x + k, y + k].BackColor = Color.White;
                                    bu[x + k, y + k].RemIm();

                                }
                            if (x < i && y > j)
                                if (tahtaa[x + k, y - k] == 1 || tahtaa[x + k, y - k] == 11)
                                {
                                    tahtaa[x + k, y - k] = 0;
                                    bu[x + k, y - k].BackColor = Color.White;
                                    bu[x + k, y - k].RemIm();

                                }
                            if (x > i && y < j)
                                if (tahtaa[x - k, y + k] == 1 || tahtaa[x - k, y + k] == 11)
                                {
                                    tahtaa[x - k, y + k] = 0;
                                    bu[x - k, y + k].BackColor = Color.White;
                                    bu[x - k, y + k].RemIm();

                                }
                            if (x > i && y > j)
                                if (tahtaa[x - k, y - k] == 1 || tahtaa[x - k, y - k] == 11)
                                {
                                    tahtaa[x - k, y - k] = 0;
                                    bu[x - k, y - k].BackColor = Color.White;
                                    bu[x - k, y - k].RemIm();

                                }
                        }
                        if (x == 7) //haraket ve yeme
                        {
                            tahtaa[x, y] = 22;
                            bu[x, y].AddImks();
                        }
                        else
                        {
                            if (tag == 2)
                            {
                                bu[x, y].AddIms();
                                tahtaa[x, y] = 2;
                            }
                            if (tag == 22)
                            {
                                bu[x, y].AddImks();
                                tahtaa[x, y] = 22;
                            }
                        }
                        bu[oldx, oldy].RemIm();
                        tahtaa[oldx, oldy] = 0;
                        tur = !tur;
                        ilktıklama = !ilktıklama;
                        kazanan(1);
                    }
                }
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // tahta
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "tahta";
            this.Load += new System.EventHandler(this.tahta_Load_3);
            this.ResumeLayout(false);

        }

        private void tahta_Load_2(object sender, EventArgs e)
        {

        }

        public void kazanan(int other)
        {
            int i = 1, j = 1;
            bool t = false;
            for (i = 0; i < tahtaa.GetLength(0); i++)
            {
                for(j=  0; j < tahtaa.GetLength(0); j++)
                {
                    if (tahtaa[i, j] == other || tahtaa[i, j] == other * 11)
                    {
                        if (other == 1)
                        {
                            if (i > 0 && j < tahtaa.GetLength(1) - 1 && tahtaa[i - 1, j + 1] == 0)
                            {
                                t = true;
                                break;
                            }
                            if (i > 0 && j > 0 && tahtaa[i - 1, j - 1] == 0)
                            {
                                t = true;
                                break;
                            }
                            if (i > 1 && j > 1 && (tahtaa[i - 1, j - 1] == other || tahtaa[i - 1, j - 1] == other * 11) && tahtaa[i - 2, j - 2] == 0)
                            {
                                t = true;
                                break;
                            }
                            if (i > 1 && j < tahtaa.GetLength(1) - 2 && (tahtaa[i - 1, j + 1] == other || tahtaa[i - 1, j + 1] == other * 11) && tahtaa[i - 2, j + 2] == 0)
                            {
                                t = true;
                                break;
                            }
                        }
                        if (other == 2)
                        {
                            if (i < tahtaa.GetLength(0) - 1 && j < tahtaa.GetLength(1) - 1 && tahtaa[i + 1, j + 1] == 0)
                            {
                                t = true;
                                break;
                            }
                            if (i < tahtaa.GetLength(0) - 1 && j > 0 && tahtaa[i + 1, j - 1] == 0)
                            {
                                t = true;
                                break;
                            }
                            if (i < tahtaa.GetLength(0) - 2 && j > 1 && (tahtaa[i + 1, j - 1] == 1 || tahtaa[i + 1, j - 1] == 11) && tahtaa[i + 2, j - 2] == 0)
                            {
                                t = true;
                                break;
                            }
                            if (i < tahtaa.GetLength(0) - 2 && j < tahtaa.GetLength(1) - 2 && (tahtaa[i + 1, j + 1] == 1 || tahtaa[i + 1, j + 1] == 11) && tahtaa[i + 2, j + 2] == 0)
                            {
                                t = true;
                                break;
                            }
                        }
                    }
                }
                if (t)
                {
                    break;
                }
            }
            if (other == 1 && i == 8 && j == 8)
            {
               
               
                MessageBox.Show(player2   +  "   oyunu kazanan");

                kazanan_siyah ks = new kazanan_siyah();
                ks.Show();
                this.Close();
            }
            if (other == 2 && i == 8 && j == 8)
            {
                MessageBox.Show(player1   +   "  oyunu kazanan");
               kazanan_kırmızı k = new kazanan_kırmızı();  
                k.Show();
                this.Close();
            }
        }
        private void tahta_Load(object sender, EventArgs e)
        {

        }
        private void tahta_Load_1(object sender, EventArgs e)
        {

        }

        private void tahta_Load_3(object sender, EventArgs e)
        {

        }
    }
}
