using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Linq;
using System.Diagnostics;

namespace Andantino_Search
{
    public partial class Form1 : Form
    {
        //P1 AI
        //P2 Human

        List<PointF> centers = new List<PointF>();
        List<Hexagon> all_hexes = new List<Hexagon>();
        static List<Hexagon> hexes_board = new List<Hexagon>();
        //List<Hexagon> hexes_outer_board = new List<Hexagon>();

        List<Hexagon> player1_hexes = new List<Hexagon>();


        List<Hexagon> player2_hexes = new List<Hexagon>();


        List<Hexagon> possible_hexes = new List<Hexagon>();


        List<Hexagon> empty_hexes;

        Hexagon ai_move = new Hexagon();

        State game_state = new State();


        bool isplayer1_turn = false;
        bool isplayer2_turn = true;

        int depth_game = 1;

        static bool global_is_game_over = false;
        //private static Hexagon ai_move;

        public Form1()
        {
            InitializeComponent();
        }




        private void Form1_Load(object sender, EventArgs e)
        {
            label2.Text = "Player 2";

            set_all_hexes(Option.first_center_x, Option.first_center_y);
            GameStatic.all_hexes = new List<Hexagon>(all_hexes);
            //GameStatic.set_all_hexes(Option.first_center_x, Option.first_center_y);

            //hexes_board = GameStatic.set_hexes_board(all_hexes);
            hexes_board = set_hexes_board(all_hexes);
            GameStatic.hexes_in_board = new List<Hexagon>(hexes_board);
            //all_hexes /*=*/ set_

            empty_hexes = new List<Hexagon>(hexes_board);

            Hexagon player1_hex = hexes_board.Find(hex => hex.row == Option.row_init_coin && hex.column == Option.col_init_coin);
            player1_hexes.Add(player1_hex);
            empty_hexes.Remove(player1_hex);
 
            possible_hexes = get_neighbors(player1_hexes[0]);

            textBox1.Text = possible_hexes.Count.ToString();
            
            label2.ForeColor = Color.Crimson;
            game_state.is_game_over = false;
            game_state.move = player1_hex;
            game_state.player = 1;
            game_state.player1_hexes = player1_hexes;
            game_state.player2_hexes = player2_hexes;
            game_state.possible_hexes = possible_hexes;
            game_state.value = 1;
            game_state.depth = depth_game;
            game_state.empty_hexes = empty_hexes;


        }

        public void set_all_hexes(float center_x, float center_y)
        {
            for (int i = 0; i < 19; i++)
            {
                for (int j = 0; j < 19; j++)
                {
                    //centers.Add(new PointF(center_x, center_y));
                    all_hexes.Add(new Hexagon(j, i, new PointF(center_x, center_y)));
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


            Drawing.draw_hexes_board(e.Graphics, hexes_board);

            //painting player1 coins
            Drawing.draw_player1_hexes(e.Graphics, player1_hexes);


            //painting player2 coins
            Drawing.draw_player2_hexes(e.Graphics, player2_hexes);


            Drawing.draw_possible_hexes(e.Graphics, possible_hexes,isplayer1_turn);

            textBox4.Text = player1_hexes.Count.ToString();
            textBox5.Text = player2_hexes.Count.ToString();

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
                    for (int k = 0; k < all_hexes.Count; k++)
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
                                Option.hexes_outer_board.Add(hexes[l]);
                            }
                        }
                    }

                    if (j == number_hexes_row - 1)
                    {
                        for (int l = 0; l < hexes.Count; l++)
                        {
                            if (hexes[l].row == row_start && (hexes[l].column == (col_start + 1)))
                            {
                                Option.hexes_outer_board.Add(hexes[l]);
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


        //p1 = AI
        //p2 = Human
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
            //int[] result = new int[3];
            bool result;
            if (!isplayer1_turn && isplayer2_turn)//Human TURN == p2
            {

                

                int number_hexes_player2_before = player2_hexes.Count;
                if(possible_hexes.Any(hex => hex.row == row_clicked && hex.column == col_clicked))
                {
                    player2_hexes.Add(possible_hexes.Find(hex => hex.row == row_clicked && hex.column == col_clicked));
                    empty_hexes.Remove(hexes_board[index_min_distance]);
                    result = check_is_victory(hexes_board[index_min_distance], 2);
                    //==> Create a new State s that will be the root state for the AI search
                    
                    //Hexagon move, int player, List<Hexagon> player1_hexes, List<Hexagon> player2_hexes, int depth, List<Hexagon> empty_hexes, List<Hexagon> possible_hexes, double value, bool is_game_over
                    Hexagon move = possible_hexes.Find(hex => hex.row == row_clicked && hex.column == col_clicked);
                    int player = 2;
                    List<Hexagon> p1_hexes = new List<Hexagon>(player1_hexes);
                    List<Hexagon> p2_hexes = new List<Hexagon>(player2_hexes);
                    depth_game++;
                    List<Hexagon> empty_hexs = new List<Hexagon>(empty_hexes);


                    List<Hexagon> possible_hexs = set_possible_hexes(player1_hexes, player2_hexes);

                    int horizontal_score = count_horizontal_sequence(move, Option.number_coins_required, player);

                    int right_diagonal_score = count_diagonal_right_sequence(move, Option.number_coins_required, player, horizontal_score);

                    int left_diagonal_score = count_left_diagonal_sequence(move, Option.number_coins_required, player, horizontal_score, right_diagonal_score);

                    var value = Math.Pow(horizontal_score, horizontal_score) + Math.Pow(right_diagonal_score, right_diagonal_score) + Math.Pow(left_diagonal_score, left_diagonal_score);
                    bool is_game_over;
                    if (horizontal_score == 5 || right_diagonal_score == 5 || left_diagonal_score == 5)
                    {
                        is_game_over = true;
                        global_is_game_over = true;
                        MessageBox.Show("Game over, Player 2 won");
                    }
                    else
                    {
                        is_game_over = false;
                    }

                    game_state.move = move;
                    game_state.is_game_over = is_game_over;
                    game_state.player = player;
                    game_state.player1_hexes = p1_hexes;
                    game_state.player2_hexes = p2_hexes;
                    game_state.possible_hexes = possible_hexs;
                    game_state.depth = depth_game;
                    game_state.empty_hexes = empty_hexs;
                    game_state.value = value;

                    //or maybe call game_state.create_state_from_move ?


                }

                if (player2_hexes.Count>number_hexes_player2_before)
                {
                    //Hexagon last_p2_hex = player2_hexes[player2_hexes.Count - 1];
                    isplayer1_turn = true;
                    label2.Text = "Player 1";
                    label2.ForeColor = Color.RoyalBlue;
                    isplayer2_turn = false;
                    possible_hexes = set_possible_hexes(player1_hexes, player2_hexes);
                    
                    textBox1.Text = possible_hexes.Count.ToString();
                    picGrid.Refresh();
                }

                else
                {
                    MessageBox.Show("Impossible move ! Please choose another place");
                    return;
                }

            }

            else//AI Turn == p1
            {
                //result = check_is_victory(hexes_board[index_min_distance], 1);
                int number_hexes_player1_before = player1_hexes.Count;
                if (possible_hexes.Any(hex => hex.row == row_clicked && hex.column == col_clicked))
                {
                    player1_hexes.Add(possible_hexes.Find(hex => hex.row == row_clicked && hex.column == col_clicked));
                    empty_hexes.Remove(hexes_board[index_min_distance]);
                    result = check_is_victory(hexes_board[index_min_distance], 1);
                    //minimax(game_state, 2, true);
                    //game_state = game_state.get_state_after_move(game_state, ai_move);
                }

                if (player1_hexes.Count>number_hexes_player1_before)
                {
                    isplayer1_turn = false;
                    isplayer2_turn = true;
                    label2.Text = "Player 2";
                    label2.ForeColor = Color.Crimson;
                    possible_hexes = set_possible_hexes(player1_hexes, player2_hexes);
                    
                    textBox1.Text = possible_hexes.Count.ToString();
                    picGrid.Refresh();
                }
                else
                {
                    MessageBox.Show("Impossible move ! Please choose another place");
                    return;

                }

            }
            dataGridView1.Rows.Clear();
            //dataGridView1.Refresh();
            //dataGridView1.Rows.Add("-:" + result[0].ToString() + ", /:" + result[1].ToString() + ", \\:" + result[2].ToString());
            

        }

        public List<Hexagon> set_possible_hexes(List<Hexagon> player1_hexes, List<Hexagon> player2_hexes)
        {
            List<Hexagon> neighbors2 = new List<Hexagon>();
            List<Hexagon> possible_hexes = new List<Hexagon>();

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

            for (int i = 0; i < neighbors2.Count; i++)
            {
                List<Hexagon> temp_list = new List<Hexagon>(neighbors2);
                temp_list.RemoveAt(i);
                if (temp_list.Contains(neighbors2[i]) && !player1_hexes.Contains(neighbors2[i]) && !player2_hexes.Contains(neighbors2[i]) && !possible_hexes.Contains(neighbors2[i]))
                {
                    possible_hexes.Add(neighbors2[i]);
                }
            }



            return possible_hexes;

        }

        public int count_horizontal_sequence(Hexagon new_hex, int number_coins_required, int which_player_played)
        {
            int row = new_hex.row;
            int col = new_hex.column;
            int horizontal_sequential = 1;
            for (int i = 1; i <= number_coins_required - 1; i++)
            {
                if (which_player_played == 1)
                {
                    if (player2_hexes.Any(hex => hex.row == row && hex.column == col + i) || Option.hexes_outer_board.Any(hex => hex.row == row && hex.column == col + i) || empty_hexes.Any(hex => hex.row == row && hex.column == col + i))
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
                    if (player1_hexes.Any(hex => hex.row == row && hex.column == col + i) || Option.hexes_outer_board.Any(hex => hex.row == row && hex.column == col + i) || empty_hexes.Any(hex => hex.row == row && hex.column == col + i))
                    {
                        break;
                    }
                    else
                    {
                        horizontal_sequential += 1;
                    }

                }
            }
            if (horizontal_sequential != 5)//if there are not already 5 in 1 direction
            {
                for (int i = 1; i <= number_coins_required - 1; i++)
                {
                    if (which_player_played == 1)
                    {
                        if (player2_hexes.Any(hex => hex.row == row && hex.column == col - i) || Option.hexes_outer_board.Any(hex => hex.row == row && hex.column == col - i) || empty_hexes.Any(hex => hex.row == row && hex.column == col - i))
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
                        if (player1_hexes.Any(hex => hex.row == row && hex.column == col - i) || Option.hexes_outer_board.Any(hex => hex.row == row && hex.column == col - i) || empty_hexes.Any(hex => hex.row == row && hex.column == col - i))
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


            return horizontal_sequential;

        }

        public int count_diagonal_right_sequence(Hexagon new_hex, int number_coins_required, int which_player_played, int horizontal_sequential)
        {
            int right_diagonal_sequential = 1;// /
            if (horizontal_sequential != 5)
            {
                int row = new_hex.row;
                int col = new_hex.column;
                int[] odd_start_addition_upright = new int[4] { 1, 1, 2, 2 };
                int[] even_start_addition_upright = new int[4] { 0, 1, 1, 2 };
                for (int i = 1; i <= number_coins_required - 1; i++)
                {
                    if (which_player_played == 1)
                    {
                        if (row % 2 == 0)
                        {
                            if (player2_hexes.Any(hex => hex.row == row - i && hex.column == col + even_start_addition_upright[i - 1]) || Option.hexes_outer_board.Any(hex => hex.row == row - i && hex.column == col + even_start_addition_upright[i - 1]) || empty_hexes.Any(hex => hex.row == row - i && hex.column == col + even_start_addition_upright[i - 1]))
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
                            if (player2_hexes.Any(hex => hex.row == row - i && hex.column == col + odd_start_addition_upright[i - 1]) || Option.hexes_outer_board.Any(hex => hex.row == row - i && hex.column == col + odd_start_addition_upright[i - 1]) || empty_hexes.Any(hex => hex.row == row - i && hex.column == col + odd_start_addition_upright[i - 1]))
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
                            if (player1_hexes.Any(hex => hex.row == row - i && hex.column == col + even_start_addition_upright[i - 1]) || Option.hexes_outer_board.Any(hex => hex.row == row - i && hex.column == col + even_start_addition_upright[i - 1]) || empty_hexes.Any(hex => hex.row == row - i && hex.column == col + even_start_addition_upright[i - 1]))
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
                            if (player1_hexes.Any(hex => hex.row == row - i && hex.column == col + odd_start_addition_upright[i - 1]) || Option.hexes_outer_board.Any(hex => hex.row == row - i && hex.column == col + odd_start_addition_upright[i - 1]) || empty_hexes.Any(hex => hex.row == row - i && hex.column == col + odd_start_addition_upright[i - 1]))
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
                    int[] odd_start_downleft = new int[4] { 0, 1, 1, 2 };
                    int[] even_start_downleft = new int[4] { 1, 1, 2, 2 };
                    for (int i = 1; i <= number_coins_required - 1; i++)
                    {
                        if (which_player_played == 1)
                        {
                            if (row % 2 == 0)
                            {
                                if (player2_hexes.Any(hex => hex.row == row + i && hex.column == col - even_start_downleft[i - 1]) || Option.hexes_outer_board.Any(hex => hex.row == row + i && hex.column == col - even_start_downleft[i - 1]))// ||hexes_outer_board.Any(hex => hex.row == row + i && hex.column == col - even_start_downleft[i - 1])
                                {

                                    break;
                                }
                                if (empty_hexes.Any(hex => hex.row == row + i && hex.column == col - even_start_downleft[i - 1]))
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
                                if (player2_hexes.Any(hex => hex.row == row + i && hex.column == col - odd_start_downleft[i - 1]))
                                {
                                    break;
                                }

                                if (empty_hexes.Any(hex => hex.row == row + i && hex.column == col - odd_start_downleft[i - 1]))
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
                                if (player1_hexes.Any(hex => hex.row == row + i && hex.column == col - even_start_downleft[i - 1]) || Option.hexes_outer_board.Any(hex => hex.row == row + i && hex.column == col - even_start_downleft[i - 1]) || empty_hexes.Any(hex => hex.row == row + i && hex.column == col - even_start_downleft[i - 1]))
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
                                if (player1_hexes.Any(hex => hex.row == row + i && hex.column == col - odd_start_downleft[i - 1]) || Option.hexes_outer_board.Any(hex => hex.row == row + i && hex.column == col - odd_start_downleft[i - 1]) || empty_hexes.Any(hex => hex.row == row + i && hex.column == col - odd_start_downleft[i - 1]))
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
            return right_diagonal_sequential;
        }

        public int count_left_diagonal_sequence(Hexagon new_hex, int number_coins_required, int which_player_played, int horizontal_sequential, int right_diagonal_sequential)
        {
            int left_diagonal_sequential = 1;
            if (horizontal_sequential != 5 || right_diagonal_sequential != 5)//going up
            {
                int row = new_hex.row;
                int col = new_hex.column;
                int[] odd_start_upleft = new int[4] { 0, 1, 1, 2 };
                int[] even_start_upleft = new int[4] { 1, 1, 2, 2 };

                for (int i = 1; i <= number_coins_required - 1; i++)
                {
                    if (which_player_played == 1)
                    {
                        if (row % 2 == 0)
                        {
                            if (player2_hexes.Any(hex => hex.row == row - i && hex.column == col - even_start_upleft[i - 1]) || Option.hexes_outer_board.Any(hex => hex.row == row - i && hex.column == col - even_start_upleft[i - 1]) || empty_hexes.Any(hex => hex.row == row - i && hex.column == col - even_start_upleft[i - 1]))
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
                            if (player2_hexes.Any(hex => hex.row == row - i && hex.column == col - odd_start_upleft[i - 1]) || Option.hexes_outer_board.Any(hex => hex.row == row - i && hex.column == col - odd_start_upleft[i - 1]) || empty_hexes.Any(hex => hex.row == row - i && hex.column == col - odd_start_upleft[i - 1]))
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
                            if (player1_hexes.Any(hex => hex.row == row - i && hex.column == col - even_start_upleft[i - 1]) || Option.hexes_outer_board.Any(hex => hex.row == row - i && hex.column == col - even_start_upleft[i - 1]) || empty_hexes.Any(hex => hex.row == row - i && hex.column == col - even_start_upleft[i - 1]))
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
                            if (player1_hexes.Any(hex => hex.row == row - i && hex.column == col - odd_start_upleft[i - 1]) || Option.hexes_outer_board.Any(hex => hex.row == row - i && hex.column == col - odd_start_upleft[i - 1]) || empty_hexes.Any(hex => hex.row == row - i && hex.column == col - odd_start_upleft[i - 1]))
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
                    int[] odd_start_downright = new int[4] { 1, 1, 2, 2 };
                    int[] even_start_downright = new int[4] { 0, 1, 1, 2 };
                    for (int i = 1; i <= number_coins_required - 1; i++)
                    {
                        if (which_player_played == 1)
                        {
                            if (row % 2 == 0)
                            {
                                if (player2_hexes.Any(hex => hex.row == row + i && hex.column == col + even_start_downright[i - 1]) || Option.hexes_outer_board.Any(hex => hex.row == row + i && hex.column == col + even_start_downright[i - 1]) || empty_hexes.Any(hex => hex.row == row + i && hex.column == col + even_start_downright[i - 1]))
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
                                if (player2_hexes.Any(hex => hex.row == row + i && hex.column == col + odd_start_downright[i - 1]) || Option.hexes_outer_board.Any(hex => hex.row == row + i && hex.column == col + odd_start_downright[i - 1]) || empty_hexes.Any(hex => hex.row == row + i && hex.column == col + odd_start_downright[i - 1]))
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
                                if (player1_hexes.Any(hex => hex.row == row + i && hex.column == col + even_start_downright[i - 1]) || Option.hexes_outer_board.Any(hex => hex.row == row + i && hex.column == col + even_start_downright[i - 1]) || empty_hexes.Any(hex => hex.row == row + i && hex.column == col + even_start_downright[i - 1]))
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
                                if (player1_hexes.Any(hex => hex.row == row + i && hex.column == col + odd_start_downright[i - 1]) || Option.hexes_outer_board.Any(hex => hex.row == row + i && hex.column == col + odd_start_downright[i - 1]) || empty_hexes.Any(hex => hex.row == row + i && hex.column == col + odd_start_downright[i - 1]))
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
            }
            return left_diagonal_sequential;
        }


        public bool check_is_victory(Hexagon new_hex, int which_player_played)//P1 = 1, P2 = 2
        {
            bool is_winner = false;
            //int[] test = new int[3];
            
            //int number_coins_required = 5;

            int horizontal_sequential = count_horizontal_sequence(new_hex, Option.number_coins_required, which_player_played);

            int right_diagonal_sequential = count_diagonal_right_sequence(new_hex, Option.number_coins_required, which_player_played, horizontal_sequential);

            int left_diagonal_sequential = count_left_diagonal_sequence(new_hex, Option.number_coins_required, which_player_played, horizontal_sequential, right_diagonal_sequential);

            if (horizontal_sequential == 5 || right_diagonal_sequential == 5 || left_diagonal_sequential == 5)
            {
                is_winner = true;
            }
            //test[0] = horizontal_sequential;
            //test[1] = right_diagonal_sequential;
            //test[2] = left_diagonal_sequential;

            return is_winner;
            //return is_winner;

        }


        public static double evaluate(State s)
        {
            return s.value;

        }




        //public static double minimax(State s, int depth, bool maximizing_player)
        //{
        //    double eval;
        //    if (depth == 0 || s.is_game_over)
        //    {

        //        return s.value;
        //    }

        //    State child_state = new State();


        //    if (maximizing_player)
        //    {
        //        double maxEval = double.NegativeInfinity;
        //        for (int i = 0; i < s.possible_hexes.Count; i++)
        //        {
        //            child_state = s.get_state_after_move(s, s.possible_hexes[i]);
        //            eval = minimax(child_state, depth - 1, false);//not s but a copy of it
        //            maxEval = Math.Max(maxEval, eval);
        //        }
        //        return maxEval;
        //    }
        //    else
        //    {
        //        double minEval = double.PositiveInfinity;
        //        for (int i = 0; i < s.possible_hexes.Count; i++)
        //        {
        //            child_state = s.get_state_after_move(s, s.possible_hexes[i]);
        //            eval = minimax(child_state, depth - 1, true);//copy of s
        //            minEval = Math.Min(minEval, eval);
        //        }
        //        return minEval;
        //    }
        //}

        public double minimax(State s, int depth_minimax, bool maximizing_player)
        {
            double eval;
            if (depth_minimax == 0 || s.is_game_over)
            {
                return s.value;
            }

            State child_state = new State();


            if (maximizing_player)
            {
                double maxEval = double.NegativeInfinity;
                for (int i = 0; i < s.possible_hexes.Count; i++)
                {

                    child_state = s.get_state_after_move(s, s.possible_hexes[i]);
                    eval = minimax(child_state, depth_minimax - 1, false);
                    
                    if(eval>maxEval)
                    {
                        ai_move = child_state.move;
                        maxEval = eval;

                    }
                }
                return maxEval;
            }
            else
            {
                double minEval = double.PositiveInfinity;
                for (int i = 0; i < s.possible_hexes.Count; i++)
                {
                    child_state = s.get_state_after_move(s, s.possible_hexes[i]);
                    eval = minimax(child_state, depth_minimax - 1, true);
                    if (eval<minEval)
                    {
                        minEval = eval;
                    }
                    
                }
                return minEval;
            }
        }

        public double minimax_alpha_beta_pruning(State s, int depth_alpha_beta, double alpha, double beta, bool maximizing_player )
        {
            double eval;
            if (depth_alpha_beta == 0 || s.is_game_over)
            {
                return s.value;
            }

            State child_state = new State();


            if (maximizing_player)
            {
                double maxEval = double.NegativeInfinity;
                for (int i = 0; i < s.possible_hexes.Count; i++)
                {

                    child_state = s.get_state_after_move(s, s.possible_hexes[i]);
                    eval = minimax_alpha_beta_pruning(child_state, depth_alpha_beta - 1,alpha,beta, false);

                    if (eval > maxEval)
                    {
                        ai_move = child_state.move;
                        maxEval = eval;

                    }

                    alpha = Math.Max(alpha, eval);
                    if(beta <= alpha)
                    {
                        break;
                    }
                }
                return maxEval;
            }
            else
            {
                double minEval = double.PositiveInfinity;
                for (int i = 0; i < s.possible_hexes.Count; i++)
                {
                    child_state = s.get_state_after_move(s, s.possible_hexes[i]);
                    eval = minimax_alpha_beta_pruning(child_state, depth_alpha_beta - 1, alpha, beta, true);
                    if (eval < minEval)
                    {
                        minEval = eval;
                    }
                    beta = Math.Min(beta, eval);
                    if(beta <= alpha)
                    {
                        break;
                    }

                }
                return minEval;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            //double value = minimax(game_state, 4, true);
            double value = minimax_alpha_beta_pruning(game_state, 6, double.NegativeInfinity, double.PositiveInfinity, true);
            stopWatch.Stop();
            depth_game++;
            //MessageBox.Show("("+ai_move.row.ToString() +","+ ai_move.column.ToString() + "," + "value:"+value.ToString()+")");
            //labe
            label4.Text = "(" + ai_move.row.ToString() + "," + ai_move.column.ToString() + "," + "value:" + value.ToString() + ")";
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
            //MessageBox.Show("RunTime " + elapsedTime);
            dataGridView1.Rows.Clear();
            dataGridView1.Rows.Add(elapsedTime);

            game_state = game_state.get_state_after_move(game_state, ai_move);



        }

        private void button2_Click(object sender, EventArgs e)
        {
            //dataGridView1.Rows.Add("-:" + game_state.move + ", /:" + result[1].ToString() + ", \\:" + result[2].ToString());

        }

    }
}
