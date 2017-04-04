using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EggIncubator
{
    public partial class ImageEditor : Form
    {
        public event Action<Image> Done;

        Image original;
        Rectangle rect;
        Graphics g;
        public ImageEditor(Image original)
        {
            InitializeComponent();
            this.original = original;
            panel1.MouseWheel += Panel1_MouseWheel;
            this.DoubleBuffered = true;
            g = panel1.CreateGraphics();
            rect = new Rectangle(0, 0, original.Width, original.Height);
        }

        private void Panel1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta < 0)
                rect = new Rectangle(rect.X+5, rect.Y+5, rect.Width - 10, rect.Height - 10);
            else
                rect = new Rectangle(rect.X-5, rect.Y-5, rect.Width + 10, rect.Height + 10);
            panel1.Invalidate();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            g.DrawImage(original, rect.X, rect.Y, rect.Width, rect.Height);
        }
        
        Point last = new Point();
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            last = Cursor.Position;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if(!last.IsEmpty && e.Button == MouseButtons.Left)
            {
                Point cur = Cursor.Position;
                rect = new Rectangle(rect.X + (cur.X - last.X), rect.Y + (cur.Y - last.Y), rect.Width, rect.Height);
                panel1.Invalidate();
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            last = Point.Empty;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Image res = new Bitmap(panel1.Width, panel1.Height);
            using (Graphics g = Graphics.FromImage(res))
                g.DrawImage(original, rect, 0, 0, panel1.Width, panel1.Height, GraphicsUnit.Pixel);
            Done?.Invoke(res);
        }
    }
}
