using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EggIncubator
{
    public partial class Form1 : Form
    {
        Image Egg;
        Graphics graphics;

        Brush solid;

        LinearGradientBrush linearBrush;

        Image background;

        Image buffer;
        Graphics canvasGraphics;
        public Form1()
        {
            InitializeComponent();
            Egg = Resource1.알;
            buffer = new Bitmap(canvas.Width, canvas.Height);
            background = new Bitmap(canvas.Width, canvas.Height);
            graphics = Graphics.FromImage(buffer);
            canvasGraphics = canvas.CreateGraphics();
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.InterpolationMode = InterpolationMode.High;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;

            solid = new SolidBrush(Color.Transparent);
            SetSolidColor(Color.Transparent);
        }

        private void DrawEgg()
        {
            graphics.DrawImage(Egg, 0, 0, canvas.Width, canvas.Height);
        }

        private void DrawBg(Brush b)
        {
            graphics.FillRectangle(b, 0, 0, canvas.Width, canvas.Height);
        }
        
        private void DrawSolidBg()
        {
            graphics.FillRectangle(solid, 0, 0, canvas.Width, canvas.Height);
        }

        private void DrawLinearGradientBg()
        {
            DrawBg(linearBrush);
        }
        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            DrawSolidBg();
            DrawEgg();

            canvasGraphics.DrawImage(buffer, 0, 0, canvas.Width, canvas.Height);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ColorDialog c = new ColorDialog();
            if (c.ShowDialog() == DialogResult.OK)
            {
                solid = new SolidBrush(c.Color);
                SetSolidColor(c.Color);
            }
        }

        private void SetSolidColor(Color c)
        {
            this.button1.BackColor= Color.FromArgb(255, c.R, c.G, c.B);
            
            //this.textBox1.BackColor = Color.FromArgb(255 - c.R, 255 - c.G, 255 - c.B);
            this.textBox1.Text = "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
            canvas.Invalidate();
        }

        private void pNG로저장ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new SaveFileDialog();
            dialog.Filter = "PNG|*.png|모든 파일|*.*";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.buffer.Save(dialog.FileName, System.Drawing.Imaging.ImageFormat.Png);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "이미지|*.bmp;*.jpg;*.jpeg;*.png;*.tif;*.tiff|모든 파일|*.*";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                ImageEditor editor = new ImageEditor(Image.FromFile(dialog.FileName));
                editor.Show();
            }
        }
    }
}
