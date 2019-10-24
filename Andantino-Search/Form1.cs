using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Linq;
using System.Diagnostics;
using System.Collections;

namespace Andantino_Search
{
    public partial class Form1 : Form
    {
        //P1 AI
        //P2 Human

        //State GameState.game_state = new State();
        //Stack game_history = new Stack();
        
        bool isplayer1_turn = false;
        bool isplayer2_turn = true;

        int depth_game = 1;

        DateTime Started = DateTime.Now;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            set_labels();

            set_timer();

            GameStatic.all_hexes = new List<Hexagon>();
            GameStatic.hexes_in_board = new List<Hexagon>();
            GameStatic.hexes_board_dict = new Dictionary<Tuple<int, int>, Hexagon>();

            GameState.game_history = new Stack();

            set_all_hexes(Option.first_center_x, Option.first_center_y);
            GameStatic.hexes_in_board = set_hexes_board(GameStatic.all_hexes);

            //var span = new Span<Hexagon>(hexes_board.ToArray());

            
            for (int i = 0; i < GameStatic.hexes_in_board.Count; i++)
            {
                GameStatic.hexes_board_dict.Add(Tuple.Create(GameStatic.hexes_in_board[i].row, GameStatic.hexes_in_board[i].column), GameStatic.hexes_in_board[i]);
            }

            Hexagon player1_hex = GameStatic.hexes_in_board.Find(hex => hex.row == Option.row_init_coin && hex.column == Option.col_init_coin);
            set_initial_state(player1_hex);

            
            txtbox_number_possible_hexes.Text = GameState.game_state.possible_hexes.Count.ToString();

            GameState.game_history.Push(GameState.game_state);
            //MessageBox.Show(GameStatic.hexes_in_board.Count.ToString());
            Zobrist.generate_zobrist_table(false);
            if(Option.player1_type == 1 && Option.player2_type == 1)
            {
                //disable click on board and on ai_move button
                //change the counter
                //while (!Option.is_game_over)
                //{
                //    //if checkbox of which algo to use
                //    AI.negamax(GameState.game_state, Option.depth_of_search, Option.minimum_score, double.PositiveInfinity);
                //    //put pawn of p2 in board
                //    //check game over
                //    //update stack

                //    AI.negamax(GameState.game_state, Option.depth_of_search, Option.minimum_score, double.PositiveInfinity);
                //    //put pawn of p1 in board
                //    //check game over
                //    //update stack

                //}

            }
            

        }

        public void set_labels()
        {
            label2.Text = "00:00";
            label2.Font = Option.font_counter;

            label_time_limit.ForeColor = Option.color_time_limit;
            label_time_limit.Font = Option.font_time_limit;

            label_player_turn.Text = "Player 2";

            label_player_turn.ForeColor = Color.Crimson;



        }

        public void set_timer()
        {
            timer1.Tick += new EventHandler(this.t_Tick);
            timer1.Interval = 1000;
            timer1.Start();
        }

        public void set_initial_state(Hexagon first_hex)
        {
            GameState.game_state.state_player1_hexes = new List<Hexagon>();
            GameState.game_state.state_player1_hexes.Add(first_hex);
            GameState.game_state.empty_hexes = new List<Hexagon>(GameStatic.hexes_in_board);
            GameState.game_state.empty_hexes.Remove(first_hex);
            GameState.game_state.possible_hexes = GameState.game_state.get_neighbors(GameState.game_state.state_player1_hexes[0]);
            GameState.game_state.is_game_over = false;
            GameState.game_state.move = first_hex;
            GameState.game_state.player = 1;
            GameState.game_state.state_player2_hexes = new List<Hexagon>();
            GameState.game_state.value = 1;
            GameState.game_state.depth = depth_game;

        }

        public void set_all_hexes(float center_x, float center_y)
        {
            for (int i = 0; i < 19; i++)
            {
                for (int j = 0; j < 19; j++)
                {
                    //centers.Add(new PointF(center_x, center_y));
                    GameStatic.all_hexes.Add(new Hexagon(j, i, new PointF(center_x, center_y)));
                    //all_hexes.Add(new Hexagon(j, i, new PointF(center_x, center_y)));
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



        private void picGrid_Paint_1(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;


            Drawing.draw_hexes_board(e.Graphics, GameStatic.hexes_in_board);

            //painting player1 coins
            Drawing.draw_player1_hexes(e.Graphics, GameState.game_state.state_player1_hexes);


            //painting player2 coins
            Drawing.draw_player2_hexes(e.Graphics, GameState.game_state.state_player2_hexes);


            Drawing.draw_possible_hexes(e.Graphics, GameState.game_state.possible_hexes,isplayer1_turn);

            txtbox_p1_number_coins.Text = GameState.game_state.state_player1_hexes.Count.ToString();
            txtbox_p2_number_coins.Text = GameState.game_state.state_player2_hexes.Count.ToString();

            Drawing.draw_notation_letters_top(e.Graphics,Option.letters_first_side);
            Drawing.draw_notation_letters_right(e.Graphics, Option.letters_second_side);
            Drawing.draw_notation_numbers_top_left(e.Graphics);
            Drawing.draw_notation_numbers_bottom_left(e.Graphics);

        }



        public List<Hexagon> set_hexes_board(List<Hexagon> hexes)
        {
            List<Hexagon> hexes_board = new List<Hexagon>();
            for (int i = 0; i < Option.hexes_board_size; i++)
            {
                for (int j = 0; j < Option.hexes_board_number_hexes_row; j++)
                {
                    for (int k = 0; k < GameStatic.all_hexes.Count; k++)
                    {
                        if (hexes[k].row == Option.hexes_board_row_start && hexes[k].column == Option.hexes_board_col_start)
                        {
                            hexes_board.Add(hexes[k]);
                        }
                    }
                    //part outer hexes
                    if (j == 0)
                    {
                        for (int l = 0; l < hexes.Count; l++)
                        {
                            if (hexes[l].row == Option.hexes_board_row_start && (hexes[l].column == (Option.hexes_board_col_start - 1)))
                            {
                                Option.hexes_outer_board.Add(hexes[l]);
                            }
                        }
                    }

                    if (j == Option.hexes_board_number_hexes_row - 1)
                    {
                        for (int l = 0; l < hexes.Count; l++)
                        {
                            if (hexes[l].row == Option.hexes_board_row_start && (hexes[l].column == (Option.hexes_board_col_start + 1)))
                            {
                                Option.hexes_outer_board.Add(hexes[l]);
                            }
                        }
                    }
                    //end part outer hexes


                    Option.hexes_board_col_start += 1;

                }
                Option.hexes_board_col_start -= Option.hexes_board_number_hexes_row;
                if (i >= 9)
                {
                    if (i % 2 != 0)
                    {
                        Option.hexes_board_col_start += 1;
                    }
                    Option.hexes_board_number_hexes_row -= 1;
                }
                else
                {
                    if (i % 2 == 0)
                    {
                        Option.hexes_board_col_start -= 1;
                    }
                    Option.hexes_board_number_hexes_row += 1;
                }
                Option.hexes_board_row_start += 1;
            }
            return hexes_board;
        }
        public void picGrid_MouseClick(object sender, MouseEventArgs e)
        {
            //MessageBox.Show(GameState.game_state.depth.ToString());
            List<float> distances = new List<float>();
            for (int i = 0; i < GameStatic.hexes_in_board.Count; i++)
            {
                distances.Add((float)Math.Sqrt(Math.Pow(GameStatic.hexes_in_board[i].center.Y - e.Y, 2) + Math.Pow(GameStatic.hexes_in_board[i].center.X - e.X, 2)));

            }
            float min_distance = distances.Min();
            int index_min_distance = distances.IndexOf(min_distance);
            int row_clicked = GameStatic.hexes_in_board[index_min_distance].row;
            int col_clicked = GameStatic.hexes_in_board[index_min_distance].column;
            Hexagon move = GameStatic.hexes_in_board[index_min_distance];

            //bool result;
            //when you click,
            if (!isplayer1_turn && isplayer2_turn)//Human TURN == p2
            {
                int number_hexes_player2_before = GameState.game_state.state_player2_hexes.Count;
                if(GameState.game_state.possible_hexes.Any(hex=> hex.Equals(move)))
                {
                    depth_game++;
                    GameState.game_state = GameState.game_state.get_state_after_move(GameState.game_state, move);
                    GameState.game_history.Push(GameState.game_state);
                }

                if (GameState.game_state.state_player2_hexes.Count>number_hexes_player2_before)
                {
                    isplayer1_turn = true;
                    label_player_turn.Text = "Player 1";
                    label_player_turn.ForeColor = Color.RoyalBlue;
                    isplayer2_turn = false;                    
                    txtbox_number_possible_hexes.Text = GameState.game_state.possible_hexes.Count.ToString();
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
                int number_hexes_player1_before = GameState.game_state.state_player1_hexes.Count;
                if (GameState.game_state.possible_hexes.Any(hex => hex.row == row_clicked && hex.column == col_clicked))
                {
                    depth_game++;
                    GameState.game_state = GameState.game_state.get_state_after_move(GameState.game_state, move);
                    GameState.game_history.Push(GameState.game_state);
                }

                if (GameState.game_state.state_player1_hexes.Count>number_hexes_player1_before)
                {
                    isplayer1_turn = false;
                    isplayer2_turn = true;
                    label_player_turn.Text = "Player 2";
                    label_player_turn.ForeColor = Color.Crimson;                    
                    txtbox_number_possible_hexes.Text = GameState.game_state.possible_hexes.Count.ToString();
                    picGrid.Refresh();
                }
                else
                {
                    MessageBox.Show("Impossible move ! Please choose another place");
                    return;

                }

            }
            //dataGridView1.Rows.Clear();            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //to do
            //double check that everything works fine ( value + recursion + pruning)
            //transposition table
            //iterative deepening
            //timer
            //all span

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            //double value = AI.minimax(GameState.game_state, Option.depth_of_search, true);
            //double value = AI.minimax_alpha_beta_pruning(GameState.game_state, Option.depth_of_search, Option.minimum_score, double.PositiveInfinity, true);
            double value = AI.negamax(GameState.game_state, Option.depth_of_search, double.NegativeInfinity, double.PositiveInfinity);
            stopWatch.Stop();
            //depth_game++;
            label_ai_move_result.Text = "(" + AI.ai_move.row.ToString() + "," + AI.ai_move.column.ToString() + "," + "value:" + AI.ai_state.value.ToString() + ")";
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTimeString = String.Format("{0:00}:{1:00}:{2:00}",ts.Minutes, ts.Seconds,ts.Milliseconds / 10);
            label_ai_move_stats.Text = elapsedTimeString + ", depth =  " + AI.ai_state.depth;


            //GameState.game_state = GameState.game_state.get_state_after_move(GameState.game_state, ai_move);
            //game_history.Push(GameState.game_state);



        }


        private void button3_Click(object sender, EventArgs e)
        {
            if(GameState.game_history.Count == 1)
            {
                MessageBox.Show("Since it is the beginning of the game, we cannot go any further ! ");
                return;
            }
            else
            {
                GameState.game_history.Pop();
                GameState.game_state = (State)GameState.game_history.Peek();
                if(isplayer1_turn)
                {
                    isplayer1_turn = false;
                    isplayer2_turn = true;
                }
                else
                {
                    isplayer1_turn = true;
                    isplayer2_turn = false;
                }
                picGrid.Refresh();
                
            }

        }

        private void picGrid_Click(object sender, EventArgs e)
        {

        }

        public void t_Tick(object sender, EventArgs e)
        {

            TimeSpan span_counter = TimeSpan.FromSeconds((DateTime.Now - Started).TotalSeconds);
            if(span_counter >= TimeSpan.FromMinutes(Option.time_limit_minutes_tournament))
            {
                label_time_limit.Text = "TIME LIMIT REACHED !";
                label2.ForeColor = Option.color_time_limit;
            }
            var test = String.Format("{0:00}:{1:00}", span_counter.Minutes, span_counter.Seconds);
            label2.Text = test;

        }

        

    

    }

    
}
