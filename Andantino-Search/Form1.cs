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



        //static float size = 20f;
        

        
        //Brush color_player1 = Brushes.RoyalBlue;
        

        List<PointF> centers = new List<PointF>();
        List<Hexagon> hexes = new List<Hexagon>();
        List<Hexagon> hexes_board = new List<Hexagon>();
        List<Hexagon> hexes_outer_board = new List<Hexagon>();

        List<Hexagon> player1_hexes = new List<Hexagon>();


        List<Hexagon> player2_hexes = new List<Hexagon>();


        List<Hexagon> possible_hexes = new List<Hexagon>();

        List<Hexagon> all_players_hexes = new List<Hexagon>();

        //int row_init_coin = 9;
        //int col_init_coin = 9;
        //float radius_coins = 10f;

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
            float center_x = Option.first_center_x;
            float center_y = Option.first_center_y;

            ////Working part
            for (int i = 0; i < 19; i++)
            {
                for (int j = 0; j < 19; j++)
                {
                    centers.Add(new PointF(center_x, center_y));
                    hexes.Add(new Hexagon(j, i, new PointF(center_x, center_y)));
                    center_x += 2 * Option.size * (float)Math.Sqrt(0.75);
                }

                if (i % 2 == 1)
                {
                    center_x = Option.first_center_x;

                }
                else
                {
                    center_x = Option.first_center_x + Option.size * (float)Math.Sqrt(0.75);
                }
                center_y = center_y + 1.5f * Option.size;
            }

            hexes_board = set_hexes_board(hexes);


            //add player1 coin to his hexes
            for (int i = 0; i < hexes_board.Count; i++)
            {
                if (hexes_board[i].row == Option.row_init_coin && hexes_board[i].column == Option.col_init_coin)
                {
                    player1_hexes.Add(hexes_board[i]);

                }

            }
 
            possible_hexes = get_neighbors(player1_hexes[0]);

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


            draw_hexes_board(e.Graphics);

            //painting player1 coins
            draw_player1_hexes(e.Graphics);


            //painting player2 coins
            draw_player2_hexes(e.Graphics);
            

            draw_possible_hexes(e.Graphics);

            textBox4.Text = player1_hexes.Count.ToString();
            textBox5.Text = player2_hexes.Count.ToString();

        }

        public void draw_hexes_board(Graphics g)
        {
            for (int i = 0; i < hexes_board.Count; i++)
            {
                PointF[] hexes_points = new PointF[6];
                for (int j = 0; j < 6; j++)
                {
                    hexes_points[j] = pointy_hex_corner(hexes_board[i].center, Option.size, j);
                }
                g.DrawPolygon(Option.color_board, hexes_points);
                draw_coordinates_board(g, hexes_board[i]);

            }
        }

        public void draw_player1_hexes(Graphics g)
        {
            if (player1_hexes.Count > 0)
            {
                for (int i = 0; i < player1_hexes.Count; i++)
                {
                    PointF center_hex = player1_hexes[i].center;
                    g.FillEllipse(Option.color_player1, center_hex.X - Option.radius_coins, center_hex.Y - Option.radius_coins, 2 * Option.radius_coins, 2 * Option.radius_coins);

                }
            }
        }

        public void draw_player2_hexes(Graphics g)
        {
            if (player2_hexes.Count > 0)
            {
                for (int i = 0; i < player2_hexes.Count; i++)
                {
                    PointF center_hex = player2_hexes[i].center;
                    g.FillEllipse(Option.color_player2, center_hex.X - Option.radius_coins, center_hex.Y - Option.radius_coins, 2 * Option.radius_coins, 2 * Option.radius_coins);

                }
            }

        }

        public void draw_possible_hexes(Graphics g)
        {
            if (possible_hexes.Count > 0)
            {
                for (int i = 0; i < possible_hexes.Count; i++)
                {
                    PointF[] hex_points = new PointF[6];
                    for (int j = 0; j < 6; j++)
                    {
                        hex_points[j] = pointy_hex_corner(possible_hexes[i].center, Option.size, j);

                    }
                    if (isplayer1_turn)
                    {
                        g.DrawPolygon(Option.color_possible_hexes_p1, hex_points);
                    }
                    else
                    {
                        g.DrawPolygon(Option.color_possible_hexes_p2, hex_points);
                    }
                }
            }
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
                        hexes_points[j] = pointy_hex_corner(hexes_centers[i], Option.size, j);
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

        public void draw_hexes_outer_border(Graphics g)
        {
            for (int k = 0; k < hexes_outer_board.Count; k++)
            {
                PointF[] hexes_points = new PointF[6];
                for (int l = 0; l < 6; l++)
                {
                    hexes_points[l] = pointy_hex_corner(hexes_outer_board[k].center, Option.size, l);
                }
                g.DrawPolygon(Pens.Purple, hexes_points);
                g.FillPolygon(Brushes.LightBlue, hexes_points);
            }
        }

        public void draw_coordinates_board(Graphics g, Hexagon hex)
        {
            using (StringFormat sf = new StringFormat())
            {
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;
                float x = hex.center.X;
                float y = hex.center.Y;
                string label = "(" + hex.row.ToString() + ", " +
                    hex.column.ToString() + ")";
                g.DrawString(label, this.Font,
                    Brushes.Black, x, y, sf);
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

        public bool check_is_victory(Hexagon new_hex, int which_player_played)//P1 = 1, P2 = 2
        {
            bool is_winner = false;
            //keep going direction right 4 times, if hex is the opposite player or in outer hex board 
            //or not in all_players_hex, break
            //while going, keep incrementing a number that starts at 1
            //go opposite direction and do the same 5 - number times it went first, and keep incrementing same number
            //

            //part going horizontal
            int horizontal_sequential = 1;// -
            int right_diagonal_sequential = 1;// /
            int left_diagonal_sequential = 1;// \
            int number_coins_required = 5;
            int row = new_hex.row;
            int col = new_hex.column;

            #region horizontal
            for (int i = 1; i <= number_coins_required-1; i++)
            {
                if (which_player_played == 1)
                {
                    if (player2_hexes.Any(hex => hex.row == row && hex.column == col + i) || hexes_outer_board.Any(hex=>hex.row == row && hex.column == col + i) || !all_players_hexes.Any(hex=>hex.row == row && hex.column == col + i))
                    {
                        break;
                    }
                    else
                    {
                        horizontal_sequential += 1;
                    }
                        
                }
                else//player 2 just played
                {
                    if (player1_hexes.Any(hex => hex.row == row && hex.column == col + i) || hexes_outer_board.Any(hex => hex.row == row && hex.column == col + i) || !all_players_hexes.Any(hex => hex.row == row && hex.column == col + i))
                    {
                        break;
                    }
                    else
                    {
                        horizontal_sequential += 1;
                    }

                }
            }
            if(horizontal_sequential != 5)//if there are not already 5 in 1 direction
            {
                for (int i = 1; i <= number_coins_required - 1; i++)
                {
                    if (which_player_played == 1)
                    {
                        if (player2_hexes.Any(hex => hex.row == row && hex.column == col - i) || hexes_outer_board.Any(hex => hex.row == row && hex.column == col - i) || !all_players_hexes.Any(hex => hex.row == row && hex.column == col - i))
                        {
                            break;
                        }
                        else
                        {
                            horizontal_sequential += 1;
                        }

                    }
                    else//player 2 just played
                    {
                        if (player1_hexes.Any(hex => hex.row == row && hex.column == col - i) || hexes_outer_board.Any(hex => hex.row == row && hex.column == col - i) || !all_players_hexes.Any(hex => hex.row == row && hex.column == col - i))
                        {
                            break;
                        }
                        else
                        {
                            horizontal_sequential += 1;
                        }

                    }
                }
            }

            #endregion
            #region diagonal_/
            //right diagonal going up
            //row always - 1
            //if row even, next col is the same
            //if row is odd, next col + 1
            if (horizontal_sequential !=5)
            {
                int[] odd_start_addition_upright = new int[4] { 1, 0, 1, 0 };
                int[] even_start_addition_upright = new int[4] { 0, 1, 0, 1 };
                for (int i = 1; i <= number_coins_required - 1; i++)
                {
                    if (which_player_played == 1)
                    {
                        if (row % 2 == 0)
                        {
                            if (player2_hexes.Any(hex => hex.row == row - i && hex.column == col + even_start_addition_upright[i-1]) || hexes_outer_board.Any(hex => hex.row == row - i && hex.column == col + even_start_addition_upright[i - 1]) || !all_players_hexes.Any(hex => hex.row == row - i && hex.column == col + even_start_addition_upright[i - 1]))
                            {
                                break;
                            }
                            else
                            {
                                right_diagonal_sequential += 1;
                            }
                        }
                        else
                        {
                            if (player2_hexes.Any(hex => hex.row == row - i && hex.column == col + odd_start_addition_upright[i - 1]) || hexes_outer_board.Any(hex => hex.row == row - i && hex.column == col + odd_start_addition_upright[i - 1]) || !all_players_hexes.Any(hex => hex.row == row - i && hex.column == col + odd_start_addition_upright[i - 1]))
                            {
                                break;
                            }
                            else
                            {
                                right_diagonal_sequential += 1;
                            }

                        }


                    }
                    else//player 2 just played
                    {
                        if (row % 2 == 0)
                        {
                            if (player1_hexes.Any(hex => hex.row == row - i && hex.column == col + even_start_addition_upright[i-1]) || hexes_outer_board.Any(hex => hex.row == row - i && hex.column == col + even_start_addition_upright[i-1]) || !all_players_hexes.Any(hex => hex.row == row - i && hex.column == col + even_start_addition_upright[i-1]))
                            {
                                break;
                            }
                            else
                            {
                                right_diagonal_sequential += 1;
                            }
                        }
                        else
                        {
                            if (player1_hexes.Any(hex => hex.row == row - i && hex.column == col + odd_start_addition_upright[i-1]) || hexes_outer_board.Any(hex => hex.row == row - i && hex.column == col + odd_start_addition_upright[i-1]) || !all_players_hexes.Any(hex => hex.row == row - i && hex.column == col + odd_start_addition_upright[i-1]))
                            {
                                break;
                            }
                            else
                            {
                                right_diagonal_sequential += 1;
                            }

                        }

                    }
                }

                if (right_diagonal_sequential != 5)//diagonal right going down
                {
                    int[] odd_start_downleft = new int[4] { 0, 1, 0, 1 };
                    int[] even_start_downleft = new int[4] { 1, 0, 1, 0 };
                    for (int i = 1; i <= number_coins_required - 1; i++)
                    {
                        if (which_player_played == 1)
                        {
                            if (row % 2 == 0)
                            {
                                if (player2_hexes.Any(hex => hex.row == row + i && hex.column == col - even_start_downleft[i - 1]) || hexes_outer_board.Any(hex => hex.row == row + i && hex.column == col - even_start_downleft[i - 1]) || !all_players_hexes.Any(hex => hex.row == row + i && hex.column == col - even_start_downleft[i-1]))
                                {
                                    break;
                                }
                                else
                                {
                                    right_diagonal_sequential += 1;
                                }
                            }
                            else
                            {
                                if (player2_hexes.Any(hex => hex.row == row + i && hex.column == col + odd_start_downleft[i-1]) || hexes_outer_board.Any(hex => hex.row == row + i && hex.column == col + odd_start_downleft[i-1]) || !all_players_hexes.Any(hex => hex.row == row + i && hex.column == col + odd_start_downleft[i-1]))
                                {
                                    break;
                                }
                                else
                                {
                                    right_diagonal_sequential += 1;
                                }

                            }


                        }
                        else
                        {
                            if (row % 2 == 0)
                            {
                                if (player1_hexes.Any(hex => hex.row == row + i && hex.column == col - even_start_downleft[i - 1]) || hexes_outer_board.Any(hex => hex.row == row + i && hex.column == col - even_start_downleft[i - 1]) || !all_players_hexes.Any(hex => hex.row == row + i && hex.column == col - even_start_downleft[i - 1]))
                                {
                                    break;
                                }
                                else
                                {
                                    right_diagonal_sequential += 1;
                                }
                            }
                            else
                            {
                                if (player1_hexes.Any(hex => hex.row == row + i && hex.column == col + odd_start_downleft[i - 1]) || hexes_outer_board.Any(hex => hex.row == row + i && hex.column == col + odd_start_downleft[i - 1]) || !all_players_hexes.Any(hex => hex.row == row + i && hex.column == col + odd_start_downleft[i - 1]))
                                {
                                    break;
                                }
                                else
                                {
                                    right_diagonal_sequential += 1;
                                }

                            }
                        }
                    }

                }
                
            }
            #endregion

            #region diagonal_\
            if (horizontal_sequential != 5 || right_diagonal_sequential != 5)
            {
                int[] odd_start_upleft = new int[4] { 0, 1, 0, 1 };
                int[] even_start_upleft = new int[4] { 1, 0, 1, 0 };

                for (int i = 1; i <= number_coins_required - 1; i++)
                {
                    if (which_player_played == 1)
                    {
                        if (row % 2 == 0)
                        {
                            if (player2_hexes.Any(hex => hex.row == row - i && hex.column == col - even_start_upleft[i - 1]) || hexes_outer_board.Any(hex => hex.row == row - i && hex.column == col - even_start_upleft[i - 1]) || !all_players_hexes.Any(hex => hex.row == row - i && hex.column == col - even_start_upleft[i - 1]))
                            {
                                break;
                            }
                            else
                            {
                                left_diagonal_sequential += 1;
                            }
                        }
                        else
                        {
                            if (player2_hexes.Any(hex => hex.row == row - i && hex.column == col - odd_start_upleft[i - 1]) || hexes_outer_board.Any(hex => hex.row == row - i && hex.column == col - odd_start_upleft[i - 1]) || !all_players_hexes.Any(hex => hex.row == row - i && hex.column == col - odd_start_upleft[i - 1]))
                            {
                                break;
                            }
                            else
                            {
                                left_diagonal_sequential += 1;
                            }

                        }
                    }
                    else
                    {
                        if (row % 2 == 0)
                        {
                            if (player1_hexes.Any(hex => hex.row == row - i && hex.column == col - even_start_upleft[i - 1]) || hexes_outer_board.Any(hex => hex.row == row - i && hex.column == col - even_start_upleft[i - 1]) || !all_players_hexes.Any(hex => hex.row == row - i && hex.column == col - even_start_upleft[i - 1]))
                            {
                                break;
                            }
                            else
                            {
                                left_diagonal_sequential += 1;
                            }
                        }
                        else
                        {
                            if (player1_hexes.Any(hex => hex.row == row - i && hex.column == col - odd_start_upleft[i - 1]) || hexes_outer_board.Any(hex => hex.row == row - i && hex.column == col - odd_start_upleft[i - 1]) || !all_players_hexes.Any(hex => hex.row == row - i && hex.column == col - odd_start_upleft[i - 1]))
                            {
                                break;
                            }
                            else
                            {
                                left_diagonal_sequential += 1;
                            }

                        }
                    }
                }
                if (left_diagonal_sequential != 5)//diagonal left going down
                {
                    int[] odd_start_downright = new int[4] { 1, 0, 1, 0 };
                    int[] even_start_downright = new int[4] { 0, 1, 0, 1 };
                    for (int i = 1; i <= number_coins_required - 1; i++)
                    {
                        if (which_player_played == 1)
                        {
                            if (row % 2 == 0)
                            {
                                if (player1_hexes.Any(hex => hex.row == row + i && hex.column == col + even_start_downright[i - 1]) || hexes_outer_board.Any(hex => hex.row == row + i && hex.column == col + even_start_downright[i - 1]) || !all_players_hexes.Any(hex => hex.row == row + i && hex.column == col + even_start_downright[i - 1]))
                                {
                                    break;
                                }
                                else
                                {
                                    left_diagonal_sequential += 1;
                                }
                            }
                            else
                            {
                                if (player1_hexes.Any(hex => hex.row == row + i && hex.column == col + odd_start_downright[i - 1]) || hexes_outer_board.Any(hex => hex.row == row + i && hex.column == col + odd_start_downright[i - 1]) || !all_players_hexes.Any(hex => hex.row == row + i && hex.column == col + odd_start_downright[i - 1]))
                                {
                                    break;
                                }
                                else
                                {
                                    left_diagonal_sequential += 1;
                                }

                            }
                        }
                        else
                        {
                            if (row % 2 == 0)
                            {
                                if (player2_hexes.Any(hex => hex.row == row + i && hex.column == col + even_start_downright[i - 1]) || hexes_outer_board.Any(hex => hex.row == row + i && hex.column == col + even_start_downright[i - 1]) || !all_players_hexes.Any(hex => hex.row == row + i && hex.column == col + even_start_downright[i - 1]))
                                {
                                    break;
                                }
                                else
                                {
                                    left_diagonal_sequential += 1;
                                }
                            }
                            else
                            {
                                if (player2_hexes.Any(hex => hex.row == row + i && hex.column == col + odd_start_downright[i - 1]) || hexes_outer_board.Any(hex => hex.row == row + i && hex.column == col + odd_start_downright[i - 1]) || !all_players_hexes.Any(hex => hex.row == row + i && hex.column == col + odd_start_downright[i - 1]))
                                {
                                    break;
                                }
                                else
                                {
                                    left_diagonal_sequential += 1;
                                }

                            }

                        }

                    }
                }
                #endregion




            }



            if (horizontal_sequential == 5 || right_diagonal_sequential == 5 || left_diagonal_sequential == 5)
            {
                is_winner = true;
            }




            return is_winner;

        }
    }
}
