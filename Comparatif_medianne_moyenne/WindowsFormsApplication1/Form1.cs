using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        const int N = 5, taille = 11;
        const double coef = 1.5;//Entre 0 et 100 pour un axe de 150pix
        int MaxVal = 100, MinVal = 0, Donnee,Mediane;
        double Moyenne;
        int[] TabDonnee, AxeX;
        List<double> Moyennes;
        List<int> Medianes;
        Point[] MoyTab, MedianeTab;
        Graphics graphics, graphicss;
        Rectangle rectangle;

        public Form1()
        {
            InitializeComponent();
            TabDonnee = new int[N];
            Moyennes = new List<double>();
            Medianes = new List<int>();
            MoyTab = new Point[taille];
            MedianeTab = new Point[taille];
            AxeX = new int[taille];
            _LblMax.Text = MaxVal.ToString();
            _LblMin.Text = MinVal.ToString();
            this.Paint += Form1_Paint;
            this.DoubleBuffered = true;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            int j = 0;
            graphics = this.CreateGraphics();
            graphicss = this.CreateGraphics();
            rectangle = new Rectangle(
                50, 100, 300, 150);
            graphics.DrawRectangle(Pens.Black, rectangle);
            for (int i = rectangle.X; i <= rectangle.X + rectangle.Width; i = i + 30)// lignes Vertical bleu
            {
                graphics.DrawLine(Pens.Blue, i, 100, i, 250);
                AxeX[j] = i;
                j++;
            }
        }

        private void _btn1_Click(object sender, EventArgs e)
        {
            while (true)
            {
                Donnee = DonneeRandom(MinVal, MaxVal);
                TabDonnee = StockageTableau(TabDonnee, Donnee);
                if (TabDonnee[N - 1] != 0)
                {
                    Moyenne = calculmoyenne(TabDonnee);
                    Mediane = calculmedianne(TabDonnee);
                    Medianes.Insert(0, Mediane);
                    Moyennes.Insert(0, Moyenne);
                    TracerPoint(MoyTab,MedianeTab, AxeX, Moyennes, Medianes);

                }
                System.Threading.Thread.Sleep(200);
            }
        }

        public void TracerPoint(Point[] MoyTab,Point[] MedianeTab, int[] Axe, List<double> moyennestab, List<int> mediannestab)
        {

           // this.CreateGraphics().Clear(Form1.ActiveForm.BackColor);//clean le background
            this.Refresh();
            Rectangle rectangle = new Rectangle(50, 100, 300, 150);
            graphics.DrawRectangle(Pens.Black, rectangle);
            for (int i = rectangle.X; i <= rectangle.X + rectangle.Width; i = i + 30)// lignes Vertical bleu
            {
                graphics.DrawLine(Pens.Blue, i, 100, i, 250);
            }

            for (int i = 0; i < moyennestab.Count; i++)
            {
                if (i < 11)
                {
                    MoyTab[i] = new Point(Axe[i], Convert.ToInt32(moyennestab[i]));
                    MedianeTab[i] = new Point(Axe[i], mediannestab[i]);
                }
                else break;
            }
            for (int j = 0; j < MoyTab.Length; j++)
            {
                if (MoyTab[j].Y != 0)
                {
                    //Points
                    //graphics.DrawLine(Pens.Black,
                    //    PointsTab[j].X - 5,
                    //    Convert.ToInt32(rectangle.Y + rectangle.Height - (PointsTab[j].Y) * coef),
                    //    PointsTab[j].X + 5,
                    //    Convert.ToInt32(rectangle.Y + rectangle.Height - (PointsTab[j].Y) * coef));
                    if (j >= 1)
                    {
                        graphicss.DrawLine(Pens.DarkBlue,MoyTab[j].X,Convert.ToInt32(rectangle.Y + rectangle.Height - (MoyTab[j].Y) * coef),MoyTab[j - 1].X,Convert.ToInt32(rectangle.Y + rectangle.Height - (MoyTab[j - 1].Y) * coef));
                        graphicss.DrawLine(Pens.Chocolate, MedianeTab[j].X, Convert.ToInt32(rectangle.Y + rectangle.Height - (MedianeTab[j].Y) * coef), MedianeTab[j - 1].X, Convert.ToInt32(rectangle.Y + rectangle.Height - (MedianeTab[j - 1].Y) * coef));
                    }
                }
            }
        }

        static int DonneeRandom(int min, int max)
        {
            int val;
            Random rdm = new Random();
            val = Convert.ToInt32(rdm.Next(min, max));
            Console.WriteLine("\nDonnee : " + val);
            return val;
        }

        static int[] StockageTableau(int[] tab, int val)
        {
            int i;
            for (i = N - 1; i > 0; i--)
            {
                tab[i] = tab[i - 1];
            }
            tab[0] = val;
            return tab;
        }

        static int calculmedianne(int[] tab)
        {
            int i = tab.Length / 2;
            int med = 0;
            int[] intertab = new int[tab.Length];
            var results = from n in tab orderby n ascending select n; //trie
            // var results = tab.OrderByDescending(n => n);
            intertab = results.ToArray();
            med = intertab[i];
            return med;
        }
        static double calculmoyenne(int[] tab)
        {
            int i;
            double moy = 0;
            for (i = 0; i < N; i++)
            {
                moy += tab[i];
            }
            moy = moy / N;
            return moy;
        }
    }
}
