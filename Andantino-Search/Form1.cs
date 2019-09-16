using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Andantino_Search
{
    public partial class Form1 : Form
    {

        static float size = 20f;
        static float first_center_x = 20f;
        float first_center_y = first_center_x;
        List<PointF> centers = new List<PointF>();
        List<Hexagon> hexes = new List<Hexagon>();
        List<Hexagon> hexes_board = new List<Hexagon>();
        EnumSymbols test = EnumSymbols.Empty;

        //List<PointF[]> hexes = new List<new PointF[6]>();


        public Form1()
        {
            InitializeComponent();
        }




        private void Form1_Load(object sender, EventArgs e)
        {
            float center_x = first_center_x;
            float center_y = first_center_y;

            ///TEST PART

            //int length = 5, i = 0, j = 0, k, l;
            //int row, col;
            //for (row = 0, k = length, l = 2 * length - 1; row < length; row++, k--, l++)
            //{
            //    for (col = 0; col < 3 * length; col++)
            //    {
            //        if (col >= k && col <= l)
            //        {
            //            centers.Add(new PointF(center_x, center_y));
            //            hexes.Add(new Hexagon())
            //        }
            //        else
            //        {
            //            centers.Add(new PointF(0f, 0f));
            //        }

            //        center_x += 2 * size * (float)Math.Sqrt(0.75);
            //    }

            //    if (row % 2 == 1)
            //    {
            //        center_x = first_center_x;
            //    }
            //    else
            //    {
            //        center_x = first_center_x + size * (float)Math.Sqrt(0.75);
            //    }
            //    center_y = center_y + 1.5f * size;
            //}
            /////END OF TEST PART






            ////Working part
            for (int i = 0; i < 19; i++)
            {
                for (int j = 0; j < 19; j++)
                {
                    centers.Add(new PointF(center_x, center_y));
                    hexes.Add(new Hexagon(j, i, new PointF(center_x, center_y)));
                    center_x += 2 * size * (float)Math.Sqrt(0.75);
                }

                if (i % 2 == 1)
                {
                    center_x = first_center_x;
                    center_y = center_y + 1.5f * size;
                }
                else
                {
                    center_x = first_center_x + size * (float)Math.Sqrt(0.75);
                    center_y = center_y + 1.5f * size;
                }
            }

            hexes_board =  set_hexes_board(hexes);
            //MessageBox.Show(hexes.Count.ToString());
            //MessageBox.Show(hexes_board.Count.ToString());


        }


        private void picGrid_Paint_1(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;


            ////WORKING PART

            //for (int i = 0; i < centers.Count; i++)
            //{
            //    if (centers[i].X != 0f && centers[i].Y != 0f)
            //    {
            //        PointF[] hexes_points = new PointF[6];
            //        for (int j = 0; j < 6; j++)
            //        {
            //            hexes_points[j] = pointy_hex_corner(centers[i], size, j);
            //        }
            //        e.Graphics.DrawPolygon(Pens.Black, hexes_points);
            //        using (StringFormat sf = new StringFormat())
            //        {
            //            sf.Alignment = StringAlignment.Center;
            //            sf.LineAlignment = StringAlignment.Center;
            //            float x = centers[i].X;
            //            float y = centers[i].Y;
            //            string label = "(" + hexes[i].row.ToString() + ", " +
            //                hexes[i].column.ToString() + ")";
            //            e.Graphics.DrawString(label, this.Font,
            //                Brushes.Black, x, y, sf);
            //        }
            //    }

            //}

            for (int i = 0; i < hexes_board.Count; i++)
            {
                PointF[] hexes_points = new PointF[6];
                for (int j = 0; j < 6; j++)
                {
                    hexes_points[j] = pointy_hex_corner(hexes_board[i].center, size, j);
                }
                e.Graphics.DrawPolygon(Pens.Black, hexes_points);

                //using (StringFormat sf = new StringFormat())
                //{
                //    sf.Alignment = StringAlignment.Center;
                //    sf.LineAlignment = StringAlignment.Center;
                //    float x = hexes_board[i].center.X;
                //    float y = hexes_board[i].center.Y;
                //    string label = "(" + hexes_board[i].row.ToString() + ", " +
                //        hexes_board[i].column.ToString() + ")";
                //    e.Graphics.DrawString(label, this.Font,
                //        Brushes.Black, x, y, sf);
                //}

            }

        }

        private List<Hexagon> set_hexes_board(List<Hexagon> hexes)
        {
            List<Hexagon> hexes_board = new List<Hexagon>();

            ///Working part

            //int row_start = 0;
            //int col_start = 5;
            //int number_hexes_row = 10;
            //for (int i = 0; i < 10; i++)
            //{
            //    for (int j = 0; j < number_hexes_row; j++)
            //    {
            //        for (int k = 0; k < hexes.Count; k++)
            //        {
            //            if (hexes[k].row == row_start && hexes[k].column == col_start)
            //            {
            //                hexes_board.Add(hexes[k]);
            //            }
            //        }
            //        col_start += 1;
            //    }
            //    col_start -= number_hexes_row;
            //    if (i % 2 == 0)
            //    {
            //        col_start -= 1;
            //    }
            //    row_start += 1;
            //    number_hexes_row += 1;
            //}


            //row_start = 10;
            //col_start = 1;
            //number_hexes_row = 18;

            //for (int i = 0; i < 9; i++)
            //{
            //    for (int j = 0; j < number_hexes_row; j++)
            //    {
            //        for (int k = 0; k < hexes.Count; k++)
            //        {
            //            if (hexes[k].row == row_start && hexes[k].column == col_start)
            //            {
            //                hexes_board.Add(hexes[k]);
            //            }
            //        }
            //        col_start += 1;
            //    }
            //    col_start -= number_hexes_row;
            //    if (i % 2 != 0)
            //    {
            //        col_start += 1;
            //    }
            //    row_start += 1;
            //    number_hexes_row -= 1;

            //}

            ///End Working part


            int row_start = 0;
            int col_start = 5;
            int number_hexes_row = 10;
            for (int i = 0; i < 19; i++)
            {
                for (int j = 0; j < number_hexes_row; j++)
                {
                    for (int k = 0; k < hexes.Count; k++)
                    {
                        if (hexes[k].row == row_start && hexes[k].column == col_start)
                        {
                            hexes_board.Add(hexes[k]);
                        }
                    }
                    col_start += 1;
                }
                col_start -= number_hexes_row;
                if(i >= 9)
                {
                    if (i % 2 != 0)
                    {
                        col_start += 1;
                    }
                    number_hexes_row -= 1;
                }
                else
                {
                    if (i % 2 == 0)
                    {
                        col_start -= 1;
                    }
                    number_hexes_row += 1;
                }
                row_start += 1;
            }
            return hexes_board;
        }

        private PointF pointy_hex_corner(PointF center, float size, int i)
        {
            var angle_deg = 60 * i - 30;
            var angle_rad = Math.PI / 180 * angle_deg;
            return new PointF(center.X + size * (float)Math.Cos(angle_rad), center.Y + size * (float)Math.Sin(angle_rad));
        }
    }
}
