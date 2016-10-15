using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace labirint_drum_minim
{
    public partial class Form1 : Form
    {//declarare variabile//
        struct Pozitie { public  int lin, col;};
        Pozitie ps, pc, v, p; int nr = 0;
        Pozitie[] C=new Pozitie[10000];
        int[,] L = new int[100, 100];
        int  n, m, prim, ultim,i,j;
        int[] dl=new int[4]{-1,0,1,0};
        int[] dc=new int[4]{0,1,0,-1};
        int[] tata = new int[10000];
        Button[,] b = new Button[50, 50];

        void citeste_labirint()
        {
            using (StreamReader f = new StreamReader("labirint.txt"))
            {
                string s = f.ReadLine();
                string[] sir = s.Split(' ');
                m = Int32.Parse(sir[0]);
                n = Int32.Parse(sir[1]);
                pc.lin = Int32.Parse(sir[2]);
                pc.col = Int32.Parse(sir[3]);
                for (int i = 1; i <= m; i++)
                {//citim cate o linie si formam cu ea un vector de stringuri, separate prin spatiu cu metoda split
                    sir = f.ReadLine().Split(' ');
                    for (int j = 0; j < n; j++)
                    {
                        L[i, j + 1] = Int32.Parse(sir[j]);
                    }
                }
            }
        }

        public void but_Click(object sender, EventArgs e)
        {

            if (nr == 0)
            {
                string s = ((Button)sender).Text;
                string[] sir = s.Split(' ');
                //((Button)sender).BackColor = Color.Orange;
               // ((Button)sender).ForeColor = Color.White;
                ((Button)sender).BackgroundImage = Image.FromFile("images.jpg");
                ((Button)sender).BackgroundImageLayout =ImageLayout.Stretch;
                ((Button)sender).Text = "1";
                ps.lin = Int32.Parse(sir[0]);
                ps.col = Int32.Parse(sir[1]);
                nr = 1;
            }
            else
               if(nr<=1) 
            {
                string c = ((Button)sender).Text;
                string[] sir = c.Split(' ');
                //((Button)sender).BackColor = Color.Black;
                //((Button)sender).ForeColor = Color.White;
                ((Button)sender).BackgroundImage = Image.FromFile("descărcare.jpg");
               ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
                ((Button)sender).Text = "1";
                pc.lin = Int32.Parse(sir[0]);
                pc.col = Int32.Parse(sir[1]);
                nr = 2;
            }
        }
        void lee()
        {
            int k;
            for (i = 0; i <= m + 1; i++)
                L[0,i] = L[n + 1,i] = -1;
            for (i = 0; i <= n + 1; i++)
                L[i,0] = L[i,m + 1] = -1;
            C[0] = ps;
            L[ps.lin,ps.col] = 1;
            prim = ultim = 0;
            tata[prim] = 0;
            while (prim <= ultim && L[pc.lin,pc.col] == 0)
            {
                p = C[prim];
                for (k = 0; k <= 3; k++)
                {
                    v.lin = p.lin + dl[k];
                    v.col = p.col + dc[k];
                    if (L[v.lin,v.col] == 0)
                    {
                        L[v.lin,v.col] = L[p.lin,p.col] + 1;
                        C[++ultim] = v;
                        tata[ultim] = prim;
                    }
                }
                prim++;
            }
         }
     
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            citeste_labirint();
            //deseneaza labirint
            for (i = 1; i <= m; i++)
                for (j = 1; j <= n; j++)
                {
                    b[i, j] = new Button();
                    b[i, j].Width = 60;
                    b[i, j].Height = 60;
                    b[i, j].Left = j * 60;
                    b[i, j].Top = 50 + 60 * i;
                    if (L[i, j] == 0)
                    {
                        b[i, j].BackColor = Color.White;
                        b[i, j].ForeColor = Color.White;
                        b[i, j].Text = i + " " + j;
                        //click pw punctul de plecare
                        b[i, j].Click += new EventHandler(but_Click);
                    }
                    else
                        b[i, j].Image = Image.FromFile("brick.jpg");
                    b[i, j].FlatStyle = FlatStyle.Flat;
                    b[i, j].FlatAppearance.BorderSize = 0;
                    this.Controls.Add(b[i, j]);
                }


        }
        void drum(int i)
        {
            if (tata[i]!=0)
             {
                drum(tata[i]);
             }
            b[C[i].lin, C[i].col].BackColor = Color.Orange;
            b[C[i].lin, C[i].col].Text = Convert.ToString(L[C[i].lin, C[i].col]);


        }
        private void button1_Click(object sender, EventArgs e)
        {
            lee(); 
            drum(ultim);
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            b[C[i].lin, C[i].col].BackColor = Color.Orange; 
        }
    }
}
