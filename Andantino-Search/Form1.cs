using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Linq;

namespace Andantino_Search
{
    public partial class Form1 : Form
    {



        static float size = 20f;
        static float first_center_x = 20f;
        float first_center_y = first_center_x;

        Pen color_board = Pens.DarkSlateGray;
        Brush color_player1 = Brushes.RoyalBlue;
        Pen color_possible_hexes_p2 = Pens.Crimson;

        List<PointF> centers = new List<PointF>();
        List<Hexagon> hexes = new List<Hexagon>();
        List<Hexagon> hexes_board = new List<Hexagon>();
        List<Hexagon> hexes_outer_board = new List<Hexagon>();

        List<Hexagon> player1_hexes = new List<Hexagon>();
        List<Hexagon> player1_possible_hexes = new List<Hexagon>();

        List<Hexagon> player2_hexes = new List<Hexagon>();
        List<Hexagon> player2_possible_hexes = new List<Hexagon>();

        int row_init_coin = 9;
        int col_init_coin = 9;
        float radius_coins = 10f;

        bool isplayer1_turn = false;
        bool isplayer2_turn = true;

        //EnumSymbols test = EnumSymbols.Empty;

        //List<PointF[]> hexes = new List<new PointF[6]>();


        public Form1()
        {
            InitializeComponent();
        }




        private void Form1_Load(object sender, EventArgs e)
        {
            //List<Hexagon> p1_neighbors = new List<Hexagon>();
            label2.Text = "Player 2";
            float center_x = first_center_x;
            float center_y = first_center_y;

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
                    
                }
                else
                {
                    center_x = first_center_x + size * (float)Math.Sqrt(0.75);
                }
                center_y = center_y + 1.5f * size;
            }

            hexes_board =  set_hexes_board(hexes);


            //add player1 coin to his hexes
            for (int i = 0; i < hexes_board.Count; i++)
            {
                if (hexes_board[i].row == row_init_coin && hexes_board[i].column == col_init_coin)
                {
                    player1_hexes.Add(hexes_board[i]);

                }

            }
            player2_possible_hexes = get_neighbors(player1_hexes[0]);
            




        }

        private List<Hexagon> get_neighbors(Hexagon middle_hex)
        {   
            List<Hexagon> neighbors = new List<Hexagon>();
            int column_hex = middle_hex.column;
            int row_hex = middle_hex.row;

            int left_neighbor_row = row_hex;
            int left_neighbor_col = column_hex - 1;
            for (int i = 0; i < hexes_board.Count; i++)
            {
                if (hexes_board[i].row == left_neighbor_row && hexes_board[i].column == left_neighbor_col)
                {
                    neighbors.Add(hexes_board[i]);
                }
            }

            int right_neighbor_row = row_hex;
            int right_neighbor_col = column_hex + 1;
            for (int i = 0; i < hexes_board.Count; i++)
            {
                if (hexes_board[i].row == right_neighbor_row && hexes_board[i].column == right_neighbor_col)
                {
                    neighbors.Add(hexes_board[i]);
                }

            }


            int upper_left_neighbor_row = row_hex - 1;

            int upper_right_neighbor_row = upper_left_neighbor_row;

            int bottom_left_neighbor_row = row_hex + 1;

            int bottom_right_neighbor_row = bottom_left_neighbor_row;

            if (row_hex % 2 != 0)
            {
                int upper_left_neighbor_col = column_hex;
                for (int i = 0; i < hexes_board.Count; i++)
                {
                    if (hexes_board[i].row == upper_left_neighbor_row && hexes_board[i].column == upper_left_neighbor_col)
                    {
                        neighbors.Add(hexes_board[i]);
                    }

                }


                int upper_right_neighbor_col = column_hex + 1;
                for (int i = 0; i < hexes_board.Count; i++)
                {
                    if (hexes_board[i].row == upper_right_neighbor_row && hexes_board[i].column == upper_right_neighbor_col)
                    {
                        neighbors.Add(hexes_board[i]);
                    }

                }

                int bottom_left_neighbor_col = column_hex;
                for (int i = 0; i < hexes_board.Count; i++)
                {
                    if (hexes_board[i].row == bottom_left_neighbor_row && hexes_board[i].column == bottom_left_neighbor_col)
                    {
                        neighbors.Add(hexes_board[i]);
                    }

                }

                int bottom_right_neighbor_col = column_hex + 1;
                for (int i = 0; i < hexes_board.Count; i++)
                {
                    if (hexes_board[i].row == bottom_right_neighbor_row && hexes_board[i].column == bottom_right_neighbor_col)
                    {
                        neighbors.Add(hexes_board[i]);
                    }
                }
            }

            return neighbors;
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
            PointF center_coin_init = new PointF(10f, 10f); ;
            for (int i = 0; i < hexes_board.Count; i++)
            {
                if(hexes_board[i].row == row_init_coin && hexes_board[i].column == col_init_coin)
                {
                    center_coin_init = hexes_board[i].center;

                }

            }
            //e.Graphics.DrawEllipse(Pens.Red,center_coin_init.X - radius_coins, center_coin_init.Y - radius_coins, 2 * radius_coins, 2 * radius_coins);
            e.Graphics.FillEllipse(color_player1, center_coin_init.X - radius_coins, center_coin_init.Y - radius_coins, radius_coins + radius_coins, radius_coins + radius_coins);

            for (int i = 0; i < hexes_board.Count; i++)
            {
                PointF[] hexes_points = new PointF[6];
                for (int j = 0; j < 6; j++)
                {
                    hexes_points[j] = pointy_hex_corner(hexes_board[i].center, size, j);
                }
                e.Graphics.DrawPolygon(color_board, hexes_points);
                //if (checkBox1.Checked)
                //{
                using (StringFormat sf = new StringFormat())
                {
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;
                    float x = hexes_board[i].center.X;
                    float y = hexes_board[i].center.Y;
                    string label = "(" + hexes_board[i].row.ToString() + ", " +
                        hexes_board[i].column.ToString() + ")";
                    e.Graphics.DrawString(label, this.Font,
                        Brushes.Black, x, y, sf);
                }

                //}
                //else
                //{
                //    picGrid.Refresh();
                //}


            }

            //for (int k = 0; k < hexes_outer_board.Count; k++)
            //{
            //    PointF[] hexes_points = new PointF[6];
            //    for (int l = 0; l < 6; l++)
            //    {
            //        hexes_points[l] = pointy_hex_corner(hexes_outer_board[k].center, size, l);
            //    }
            //    e.Graphics.DrawPolygon(Pens.Purple, hexes_points);
            //    e.Graphics.FillPolygon(Brushes.LightBlue, hexes_points);
            //}

            for (int i = 0; i < player2_possible_hexes.Count; i++)
            {
                PointF[] hexes_points = new PointF[6];
                for (int j = 0; j < 6; j++)
                {
                    hexes_points[j] = pointy_hex_corner(player2_possible_hexes[i].center, size, j);
                }
                e.Graphics.DrawPolygon(color_possible_hexes_p2, hexes_points);

            }


        }

        public  List<Hexagon> set_hexes_board(List<Hexagon> hexes)
        {
            List<Hexagon> hexes_board = new List<Hexagon>();
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
                    //part outer hexes
                    if (j == 0)
                    {
                        for (int l = 0; l < hexes.Count; l++)
                        {
                            if (hexes[l].row == row_start && (hexes[l].column == (col_start - 1)))
                            {
                                hexes_outer_board.Add(hexes[l]);
                            }
                        }
                    }

                    if (j == number_hexes_row - 1)
                    {
                        for (int l = 0; l < hexes.Count; l++)
                        {
                            if (hexes[l].row == row_start && (hexes[l].column == (col_start + 1)))
                            {
                                hexes_outer_board.Add(hexes[l]);
                            }
                        }
                    }
                    //end part outer hexes


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

        private void picGrid_MouseClick(object sender, MouseEventArgs e)
        {
            List<float> distances = new List<float>();
            for (int i = 0; i < hexes_board.Count; i++)
            {
                distances.Add((float)Math.Sqrt(Math.Pow(hexes_board[i].center.Y - e.Y, 2) + Math.Pow(hexes_board[i].center.X - e.X, 2)));

            }
            //textBox1.Text = "(" + e.X.ToString() + "," + e.Y.ToString() + ")";
            float min_distance = distances.Min();
            int index_min_distance = distances.IndexOf(min_distance);
            textBox2.Text = "row = " + hexes_board[index_min_distance].row.ToString() + ", col = " + hexes_board[index_min_distance].column.ToString();
            //MessageBox.Show(hexes_outer_board.Count.ToString());
            //MessageBox.Show(hexes_outer_board[0].center.ToString());
            //if (isplayer1_turn)
            //{
            //    label2.Text = "Player 1";
            //}
            //else
            //{
            //    label2.Text = "Player 2";
            //}

            //if(isplayer2_turn)
            //{
            //    label2.Text = "Player 2";
            //}
            //else
            //{
            //    label2.Text = "Player 1";
            //}

        }
    }
}
