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
        float first_center_y = first_center_x + 30f;

        Pen color_board = Pens.LightGray;
        Brush color_player1 = Brushes.RoyalBlue;
        Brush color_player2 = Brushes.Crimson;

        Pen color_possible_hexes_p1 = Pens.DarkBlue;
        Pen color_possible_hexes_p2 = Pens.Crimson;

        List<PointF> centers = new List<PointF>();
        List<Hexagon> hexes = new List<Hexagon>();
        List<Hexagon> hexes_board = new List<Hexagon>();
        List<Hexagon> hexes_outer_board = new List<Hexagon>();

        List<Hexagon> player1_hexes = new List<Hexagon>();
        //List<Hexagon> player1_possible_hexes = new List<Hexagon>();

        List<Hexagon> player2_hexes = new List<Hexagon>();
        //List<Hexagon> player2_possible_hexes = new List<Hexagon>();

        List<Hexagon> possible_hexes = new List<Hexagon>();

        int row_init_coin = 9;
        int col_init_coin = 9;
        float radius_coins = 10f;

        bool isplayer1_turn = false;
        bool isplayer2_turn = true;

        //int turn_number = 1;
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

            hexes_board = set_hexes_board(hexes);


            //add player1 coin to his hexes
            for (int i = 0; i < hexes_board.Count; i++)
            {
                if (hexes_board[i].row == row_init_coin && hexes_board[i].column == col_init_coin)
                {
                    player1_hexes.Add(hexes_board[i]);

                }

            }
            //player2_possible_hexes = get_neighbors(player1_hexes[0]);
            possible_hexes = get_neighbors(player1_hexes[0]);
            //for (int i = 0; i < possible_hexes.Count; i++)
            //{
            //    dataGridView1.Rows.Add("(" + possible_hexes[i].row.ToString() + "," + possible_hexes[i].column.ToString() + ")");
            //}
            textBox1.Text = possible_hexes.Count.ToString();
            
            label2.ForeColor = Color.Crimson;


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
                    //if(!hexes_board.Contains(hexes_board[i]) && !hexes_board.Contains(hexes_board[i]))
                    //{
                        neighbors.Add(hexes_board[i]);
                    //}
                    
                }
            }

            int right_neighbor_row = row_hex;
            int right_neighbor_col = column_hex + 1;
            for (int i = 0; i < hexes_board.Count; i++)
            {
                if (hexes_board[i].row == right_neighbor_row && hexes_board[i].column == right_neighbor_col)
                {
                    //if (!hexes_board.Contains(hexes_board[i]) && !hexes_board.Contains(hexes_board[i]))
                    //{
                        neighbors.Add(hexes_board[i]);
                    //}
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
            else
            {
                int upper_left_neighbor_col = column_hex-1;
                for (int i = 0; i < hexes_board.Count; i++)
                {
                    if (hexes_board[i].row == upper_left_neighbor_row && hexes_board[i].column == upper_left_neighbor_col)
                    {
                        neighbors.Add(hexes_board[i]);
                    }

                }


                int upper_right_neighbor_col = column_hex ;
                for (int i = 0; i < hexes_board.Count; i++)
                {
                    if (hexes_board[i].row == upper_right_neighbor_row && hexes_board[i].column == upper_right_neighbor_col)
                    {
                        neighbors.Add(hexes_board[i]);
                    }

                }

                int bottom_left_neighbor_col = column_hex - 1;
                for (int i = 0; i < hexes_board.Count; i++)
                {
                    if (hexes_board[i].row == bottom_left_neighbor_row && hexes_board[i].column == bottom_left_neighbor_col)
                    {
                        neighbors.Add(hexes_board[i]);
                    }

                }

                int bottom_right_neighbor_col = column_hex;
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


            PointF center_coin_init = new PointF(10f, 10f); ;
            for (int i = 0; i < hexes_board.Count; i++)
            {
                if (hexes_board[i].row == row_init_coin && hexes_board[i].column == col_init_coin)
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

            //if (player1_possible_hexes.Count > 0)
            //{
            //    for (int i = 0; i < player1_possible_hexes.Count; i++)
            //    {
            //        PointF[] hexes_points = new PointF[6];
            //        for (int j = 0; j < 6; j++)
            //        {
            //            hexes_points[j] = pointy_hex_corner(player1_possible_hexes[i].center, size, j);
            //        }
            //        e.Graphics.DrawPolygon(color_possible_hexes_p1, hexes_points);

            //    }
            //}



            //if (player2_possible_hexes.Count > 0)
            //{
            //    for (int i = 0; i < player2_possible_hexes.Count; i++)
            //    {
            //        PointF[] hexes_points = new PointF[6];
            //        for (int j = 0; j < 6; j++)
            //        {
            //            hexes_points[j] = pointy_hex_corner(player2_possible_hexes[i].center, size, j);
            //        }
            //        e.Graphics.DrawPolygon(color_possible_hexes_p2, hexes_points);

            //    }
            //}


            //painting player1 coins
            if (player1_hexes.Count > 0)
            {
                for (int i = 0; i < player1_hexes.Count; i++)
                {
                    PointF center_hex = player1_hexes[i].center;
                    e.Graphics.FillEllipse(color_player1, center_hex.X - radius_coins, center_hex.Y - radius_coins, radius_coins + radius_coins, radius_coins + radius_coins);

                }
            }


            //painting player2 coins
            if (player2_hexes.Count > 0)
            {
                for (int i = 0; i < player2_hexes.Count; i++)
                {
                    PointF center_hex = player2_hexes[i].center;
                    e.Graphics.FillEllipse(color_player2, center_hex.X - radius_coins, center_hex.Y - radius_coins, radius_coins + radius_coins, radius_coins + radius_coins);

                }
            }

            if (possible_hexes.Count>0)
            {
                for (int i = 0; i < possible_hexes.Count; i++)
                {
                    PointF[] hex_points = new PointF[6];
                    for (int j = 0; j < 6; j++)
                    {
                        hex_points[j] = pointy_hex_corner(possible_hexes[i].center, size, j);

                    }
                    if(isplayer1_turn)
                    {
                        e.Graphics.DrawPolygon(color_possible_hexes_p1, hex_points);
                    }
                    else
                    {
                        e.Graphics.DrawPolygon(color_possible_hexes_p2, hex_points);
                    }
                }
            }

            textBox4.Text = player1_hexes.Count.ToString();
            textBox5.Text = player2_hexes.Count.ToString();

        }

        public void show_all_hexes(List<PointF> hexes_centers, Graphics g)
        {
            for (int i = 0; i < hexes_centers.Count; i++)
            {
                if (hexes_centers[i].X != 0f && hexes_centers[i].Y != 0f)
                {
                    PointF[] hexes_points = new PointF[6];
                    for (int j = 0; j < 6; j++)
                    {
                        hexes_points[j] = pointy_hex_corner(hexes_centers[i], size, j);
                    }
                    g.DrawPolygon(Pens.Black, hexes_points);
                    using (StringFormat sf = new StringFormat())
                    {
                        sf.Alignment = StringAlignment.Center;
                        sf.LineAlignment = StringAlignment.Center;
                        float x = centers[i].X;
                        float y = centers[i].Y;
                        string label = "(" + hexes[i].row.ToString() + ", " +
                            hexes[i].column.ToString() + ")";
                        g.DrawString(label, this.Font,
                            Brushes.Black, x, y, sf);
                    }
                }
            }

        }

        public List<Hexagon> set_hexes_board(List<Hexagon> hexes)
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
                if (i >= 9)
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

        public void picGrid_MouseClick(object sender, MouseEventArgs e)
        {
            
            List<float> distances = new List<float>();
            for (int i = 0; i < hexes_board.Count; i++)
            {
                distances.Add((float)Math.Sqrt(Math.Pow(hexes_board[i].center.Y - e.Y, 2) + Math.Pow(hexes_board[i].center.X - e.X, 2)));

            }
            float min_distance = distances.Min();
            int index_min_distance = distances.IndexOf(min_distance);
            int row_clicked = hexes_board[index_min_distance].row;
            int col_clicked = hexes_board[index_min_distance].column;
            textBox2.Text = "row = " + row_clicked.ToString() + ", col = " + col_clicked.ToString();

            List<Hexagon> neighbors2 = new List<Hexagon>();

            if (!isplayer1_turn && isplayer2_turn)//PLAYER 2 TURN
            {
                int number_hexes_player2_before = player2_hexes.Count;
                if(possible_hexes.Any(hex => hex.row == row_clicked && hex.column == col_clicked))
                {
                    player2_hexes.Add(possible_hexes.Find(hex => hex.row == row_clicked && hex.column == col_clicked));
                }
                
                if (player2_hexes.Count>number_hexes_player2_before)//if new hex in player2
                {
                    //Hexagon last_p2_hex = player2_hexes[player2_hexes.Count - 1];
                    isplayer1_turn = true;
                    label2.Text = "Player 1";
                    label2.ForeColor = Color.RoyalBlue;
                    isplayer2_turn = false;
                    for (int i = 0; i < player1_hexes.Count; i++)//choosing possible hexes
                    {
                        neighbors2.AddRange(get_neighbors(player1_hexes[i]));

                    }
                    for (int i = 0; i < player2_hexes.Count; i++)
                    {
                        neighbors2.AddRange(get_neighbors(player2_hexes[i]));
                    }

                    for (int i = 0; i < player1_hexes.Count; i++)
                    {
                        int index = neighbors2.FindIndex(hex => hex.Equals(player1_hexes[i]));
                        neighbors2.RemoveAt(index);
                    }
                    for (int i = 0; i < player2_hexes.Count; i++)
                    {
                        int index = neighbors2.FindIndex(hex => hex.Equals(player2_hexes[i]));
                        neighbors2.RemoveAt(index);
                    }

                    possible_hexes.Clear();
                    for (int i = 0; i < neighbors2.Count; i++)
                    {
                        var temp_list = new List<Hexagon>(neighbors2);
                        temp_list.RemoveAt(i);
                        if (temp_list.Contains(neighbors2[i]) && !player1_hexes.Contains(neighbors2[i]) && !player2_hexes.Contains(neighbors2[i]) && !possible_hexes.Contains(neighbors2[i]))
                        {
                            possible_hexes.Add(neighbors2[i]);
                        }
                    }

                    for (int i = 0; i < possible_hexes.Count; i++)
                    {
                        dataGridView1.Rows.Add("(" + possible_hexes[i].row.ToString() + "," + possible_hexes[i].column.ToString() + ")");
                    }
                    textBox1.Text = possible_hexes.Count.ToString();
                    picGrid.Refresh();
                }

                else
                {
                    MessageBox.Show("Impossible move ! Please choose another place");
                    return;
                }

            }

            else//PLAYER 1 TURN
            {

                int number_hexes_player1_before = player1_hexes.Count;
                if (possible_hexes.Any(hex => hex.row == row_clicked && hex.column == col_clicked))
                {
                    player1_hexes.Add(possible_hexes.Find(hex => hex.row == row_clicked && hex.column == col_clicked));
                }

                if (player1_hexes.Count>number_hexes_player1_before)
                {
                    isplayer1_turn = false;
                    isplayer2_turn = true;
                    label2.Text = "Player 2";
                    label2.ForeColor = Color.Crimson;
                    for (int i = 0; i < player1_hexes.Count; i++)//choosing possible hexes
                    {
                        neighbors2.AddRange(get_neighbors(player1_hexes[i]));

                    }
                    for (int i = 0; i < player2_hexes.Count; i++)
                    {
                        neighbors2.AddRange(get_neighbors(player2_hexes[i]));
                    }

                    for (int i = 0; i < player1_hexes.Count; i++)
                    {
                        int index = neighbors2.FindIndex(hex => hex.Equals(player1_hexes[i]));
                        neighbors2.RemoveAt(index);
                    }
                    for (int i = 0; i < player2_hexes.Count; i++)
                    {
                        int index = neighbors2.FindIndex(hex => hex.Equals(player2_hexes[i]));
                        neighbors2.RemoveAt(index);
                    }

                    possible_hexes.Clear();
                    for (int i = 0; i < neighbors2.Count; i++)
                    {
                        var temp_list = new List<Hexagon>(neighbors2);
                        temp_list.RemoveAt(i);
                        if (temp_list.Contains(neighbors2[i]) && !player1_hexes.Contains(neighbors2[i]) && !player2_hexes.Contains(neighbors2[i]) && !possible_hexes.Contains(neighbors2[i]))
                        {
                            possible_hexes.Add(neighbors2[i]);
                        }
                    }

                    for (int i = 0; i < possible_hexes.Count; i++)
                    {
                        dataGridView1.Rows.Add("(" + possible_hexes[i].row.ToString() + "," + possible_hexes[i].column.ToString() + ")");
                    }
                    textBox1.Text = possible_hexes.Count.ToString();
                    picGrid.Refresh();
                }
                else
                {
                    MessageBox.Show("Impossible move ! Please choose another place");
                    return;

                }

            }

        }
    }
}
