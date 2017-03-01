using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

/*
 ***********************************
 *        Color Storm              *
 *  è un tool per l'individuazione *
 *   del codice RGB di un colore,  *
 *    scelto tramite tavolozza     *
 *                                 *
 *  By Fharamir                    *
 ***********************************
*/

namespace ColorStorm
{
    public partial class Form1 : Form
    {

        Boolean clicked = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            pictureBox1.Image = ColorStorm.Properties.Resources.color;
            pictureBox2.Image = ColorStorm.Properties.Resources.tonalita;
            trackBar1.Value = 257;
            timer1.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            Bitmap Image = new Bitmap(pictureBox1.Image);

            int R, G, B;
            Color col = new Color();

            for (int y = 0; y<256; y++)
                for (int x = 0; x<256; x++)
                {
                    R = x;
                    G = y * x / 256;
                    B = G;

                    col = Color.FromArgb(R, G, B);
                    Image.SetPixel(x, y, col);

                }

            R = 255;
            G = 0;
            B = 0;

            pictureBox1.Image = Image;

            Image = new Bitmap(pictureBox2.Image);

            for (int k = 0; k<6; k++)
                for (int y = 0; y<43; y++)
                {
                    switch (k)
                    {
                        case 0: {
                            //Rosso e Verde fisso, Blu sale
                            B = (y * 255) / 42;
                            break; }
                        case 1: {
                            //Blu e Verde fisso, Rosso scende
                            R = 255 - (y * 255) / 42;
                            break; }
                        case 2: {
                            //Rosso e Blu fisso, Verde sale
                            G = (y * 255) / 42;
                            break; }
                        case 3: {
                            //Rosso e Verde fisso, Blu scende
                            B = 255 - (y * 255) / 42;
                            break; }
                        case 4: {
                            //Blu e Verde fisso, Rosso sale
                            R = (y * 255) / 42;
                            break; }
                        case 5: {
                            //Blu e Rosso fisso, Verde scende
                            G = 255 - (y * 255) / 42;
                            break; }
                    }

                    col = Color.FromArgb(R, G, B);

                    for (int x = 0; x<21; x++)
                        Image.SetPixel(x, y + (43 * k), col);

                }

            pictureBox2.Image = Image;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            pictureBox4.Location = new Point(292, 36 + (257 - trackBar1.Value));
            Bitmap Image = new Bitmap(pictureBox2.Image);
            
            int R, G, B;
            Color col = new Color();
            Color ton = new Color();
            ton = Image.GetPixel(0, 257 - trackBar1.Value);

           Image = new Bitmap(pictureBox1.Image);

           int max;
            
            for (int y = 0; y<256; y++)
                for (int x = 0; x<256; x++)
                {

                    R = (ton.R * x / 256);
                    G = (ton.G * x / 256);
                    B = (ton.B * x / 256);

                    if ((R > G) && (R > B))
                        max = R;
                    else if (G > B)
                        max = G;
                    else
                        max = B;

                    R += (max - R) * y / 256;
                    G += (max - G) * y / 256;
                    B += (max - B) * y / 256;

                    col = Color.FromArgb(R, G, B);

                    Image.SetPixel(x, y, col);
                }

            pictureBox1.Image = Image;

        }

        private void pictureBox1_MouseMove(object sender, EventArgs e)
        {
            try
            {
                if (!clicked)
                {
                    Color col;
                    Bitmap I = new Bitmap(pictureBox1.Image);
                    Point pt;
                    pt = pictureBox1.PointToClient(Cursor.Position);

                    int x = pt.X;
                    if (x < 0) x = 0;
                    if (x > 255) x = 255;
                    int y = pt.Y;
                    if (y < 0) y = 0;
                    if (y > 255) y = 255;

                    col = I.GetPixel(x, y);

                    pictureBox3.BackColor = col;
                    label7.Text = Convert.ToString(col.R);
                    label6.Text = Convert.ToString(col.G);
                    label5.Text = Convert.ToString(col.B);
                    hex.Text = ColorTranslator.ToHtml(col);
                    label10.Text = Convert.ToString(col.GetBrightness().ToString("0.0000"));
                    label9.Text = Convert.ToString(col.GetSaturation().ToString("0.0000"));
                    label8.Text = Convert.ToString(col.GetHue().ToString("000.0"));
                }
            }
            catch (InvalidCastException et) 
            {
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            clicked = true;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            clicked = false;
        }

    }
}
