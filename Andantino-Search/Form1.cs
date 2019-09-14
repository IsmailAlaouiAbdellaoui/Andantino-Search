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

namespace Andantino_Search
{
    public partial class Form1 : Form
    {

        static float size = 20f;
        static float first_center_x = 20f;
        float first_center_y = first_center_x;
        List<PointF> centers = new List<PointF>();
        //List<PointF[]> hexes = new List<new PointF[6]>();


        public Form1()
        {
            InitializeComponent();
        }

        


        private void Form1_Load(object sender, EventArgs e)
        {
            float center_x = first_center_x;
            float center_y = first_center_y;
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 19; j++)
                {
                    
                    centers.Add(new PointF(center_x, center_y));
                    center_x +=  2 *size * (float)Math.Sqrt(0.75);
                }
                center_x = first_center_x + size * (float)Math.Sqrt(0.75);
                center_y = center_y + 1.5f * size;

            }


        }


        private void picGrid_Paint_1(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            //float size = 20f;
            for (int i = 0; i < centers.Count; i++)
            {
                PointF[] hexes_points = new PointF[6];
                for (int j = 0; j < 6; j++)
                {
                    hexes_points[j] = pointy_hex_corner(centers[i], size, j);
                }
                e.Graphics.DrawPolygon(Pens.Black, hexes_points);
            }



            PointF center = new PointF(20, 20);
            
            float horizontal_spacing = size * (float)Math.Sqrt(0.75);
            float vertical_spacing = size;
            

            PointF[] points = new PointF[6];

            //for (int i = 0; i < hexes.Count; i++)
            //{
            //    e.Graphics.DrawPolygon(Pens.Black, hexes[i]);
            //}

            //for (int i = 0; i < length; i++)
            //{

            //}
            //for (int i = 0; i < 6; i++)
            //{
            //    points[i] = pointy_hex_corner(center, size, i);
            //}
            //e.Graphics.DrawPolygon(Pens.Black, points);

            //PointF[] points2 = new PointF[6];
            //PointF new_center = new PointF(center.X + 2 * horizontal_spacing, center.Y);
            //for (int i = 0; i < 6; i++)
            //{
            //    points2[i] = pointy_hex_corner(new_center, size, i);

            //}
            //e.Graphics.DrawPolygon(Pens.Black, points2);


            //PointF[] points3 = new PointF[6];
            //PointF center3 = new PointF(center.X + horizontal_spacing, center.Y +  1.5f* vertical_spacing);
            //for (int i = 0; i < 6; i++)
            //{
            //    points3[i] = pointy_hex_corner(center3, size, i);

            //}
            //e.Graphics.DrawPolygon(Pens.Black, points3);


        }

        private PointF pointy_hex_corner(PointF center, float size, int i)
        {
            var angle_deg = 60 * i - 30;
            var angle_rad = Math.PI / 180 * angle_deg;
            return new PointF(center.X + size * (float)Math.Cos(angle_rad), center.Y + size * (float)Math.Sin(angle_rad));
        }
    }
}
