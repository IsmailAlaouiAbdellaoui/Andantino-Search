using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Linq;
using System.Diagnostics;
using System.Collections;
using System.Threading.Tasks;
using System.Threading;

namespace Andantino_Search
{
    public partial class Form1 : Form
    {
        //P1 AI
        //P2 Human

       


        int depth_game = 1;

        public static int depth_deepening;
        bool is_iterative_depth = false;

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
            if(Option.isplayer2_turn)
            {
                Hexagon player1_hex = GameStatic.hexes_in_board.Find(hex => hex.row == Option.row_init_coin && hex.column == Option.col_init_coin);
                set_initial_state(player1_hex);
            }
            else
            {
                Hexagon player2_hex = GameStatic.hexes_in_board.Find(hex => hex.row == Option.row_init_coin && hex.column == Option.col_init_coin);
                set_initial_state(player2_hex);
            }
            
            txtbox_number_possible_hexes.Text = GameState.game_state.possible_hexes.Count.ToString();

            GameState.game_history.Push(GameState.game_state);
            if(Option.i_play_second)
            {
                make_ai_move(is_iterative_depth);
                depth_game++;
                if(is_iterative_depth)
                {
                    GameState.game_state = GameState.game_state.get_state_after_move(GameState.game_state, AI.get_ai_move_iterative());
                }
                else
                {
                    GameState.game_state = GameState.game_state.get_state_after_move(GameState.game_state, AI.ai_move);
                }
                Option.isplayer1_turn = true;
                Option.isplayer2_turn = false;
                label_player_turn.Text = "Player 1";
                label_player_turn.ForeColor = Color.RoyalBlue;
            }
            


            
            //Zobrist.generate_zobrist_table(false);
            
            

        }

        public void set_labels()
        {
            label2.Text = "00:00";
            label2.Font = Option.font_counter;

            label_time_limit.ForeColor = Option.color_time_limit;
            label_time_limit.Font = Option.font_time_limit;

            if(Option.isplayer2_turn)
            {
                label_player_turn.Text = "Player 2";
                label_player_turn.ForeColor = Color.Crimson;
            }
            else
            {
                label_player_turn.Text = "Player 1";
                label_player_turn.ForeColor = Color.RoyalBlue;
            }
            



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
            GameState.game_state.state_player2_hexes = new List<Hexagon>();
            if (Option.isplayer2_turn)
            {
                
                GameState.game_state.state_player1_hexes.Add(first_hex);
            }
            else
            {
                
                GameState.game_state.state_player2_hexes.Add(first_hex);
            }
            
            GameState.game_state.empty_hexes = new List<Hexagon>(GameStatic.hexes_in_board);
            GameState.game_state.empty_hexes.Remove(first_hex);
            if(Option.isplayer2_turn)
            {
                GameState.game_state.possible_hexes = GameState.game_state.get_neighbors(GameState.game_state.state_player1_hexes[0]);
            }
            else
            {
                GameState.game_state.possible_hexes = GameState.game_state.get_neighbors(GameState.game_state.state_player2_hexes[0]);
            }
            
            GameState.game_state.is_game_over = false;
            GameState.game_state.move = first_hex;
            if(Option.isplayer1_turn == false && Option.isplayer2_turn == true)
            {
                GameState.game_state.player = 1;
            }
            else
            {
                GameState.game_state.player = 2;
            }
            
            //GameState.game_state.state_player2_hexes = new List<Hexagon>();
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

            
            if(GameState.game_state.state_player1_hexes.Count>0)
            {
                //painting player1 coins
                Drawing.draw_player1_hexes(e.Graphics, GameState.game_state.state_player1_hexes);
            }
            
            if(GameState.game_state.state_player2_hexes.Count > 0)
            {
                //painting player2 coins
                Drawing.draw_player2_hexes(e.Graphics, GameState.game_state.state_player2_hexes);
            }

            


            Drawing.draw_possible_hexes(e.Graphics, GameState.game_state.possible_hexes,Option.isplayer1_turn);

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
            if (!Option.isplayer1_turn && Option.isplayer2_turn)//Human TURN == p2
            {
                int number_hexes_player2_before = GameState.game_state.state_player2_hexes.Count;
                if(GameState.game_state.possible_hexes.Any(hex=> hex.Equals(move)))
                {
                    depth_game++;
                    GameState.game_state = GameState.game_state.get_state_after_move(GameState.game_state, move);
                    if (GameState.game_state.is_game_over)
                    {
                        MessageBox.Show("Game Over. Player 2 (Red) wins !");
                    }

                    

                    GameState.game_history.Push(GameState.game_state);

                    make_ai_move(is_iterative_depth);
                    depth_game++;
                    if (is_iterative_depth)
                    {
                        GameState.game_state = GameState.game_state.get_state_after_move(GameState.game_state, AI.get_ai_move_iterative());
                    }
                    else
                    {
                        GameState.game_state = GameState.game_state.get_state_after_move(GameState.game_state, AI.ai_move);

                    }
                    //stop timer
                    if (GameState.game_state.is_game_over)
                    {
                        MessageBox.Show("Game Over. Player 1 (Blue) wins !");
                    }
                    GameState.game_history.Push(GameState.game_state);
                }

                if (GameState.game_state.state_player1_hexes.Count>number_hexes_player2_before)
                {
                    //Option.isplayer1_turn = true;
                    //label_player_turn.Text = "Player 1";
                    //label_player_turn.ForeColor = Color.RoyalBlue;
                    //Option.isplayer2_turn = false;

                    Option.isplayer1_turn = false;
                    Option.isplayer2_turn = true;
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

            else//AI Turn == p1
            {
                int number_hexes_player1_before = GameState.game_state.state_player1_hexes.Count;
                if (GameState.game_state.possible_hexes.Any(hex => hex.row == row_clicked && hex.column == col_clicked))
                {
                    depth_game++;
                    GameState.game_state = GameState.game_state.get_state_after_move(GameState.game_state, move);
                    if (GameState.game_state.is_game_over)
                    {
                        MessageBox.Show("Game Over. Player 1 (Blue) wins !");
                    }

                    GameState.game_history.Push(GameState.game_state);


                    make_ai_move(is_iterative_depth);
                    depth_game++;
                    if(is_iterative_depth)
                    {
                        GameState.game_state = GameState.game_state.get_state_after_move(GameState.game_state, AI.get_ai_move_iterative());
                    }
                    else
                    {
                        GameState.game_state = GameState.game_state.get_state_after_move(GameState.game_state, AI.ai_move);

                    }

                    if (GameState.game_state.is_game_over)
                    {
                        MessageBox.Show("Game Over. Player 2 (Red) wins !");
                    }

                    GameState.game_history.Push(GameState.game_state);
                }

                if (GameState.game_state.state_player1_hexes.Count>number_hexes_player1_before)
                {
                    //Option.isplayer1_turn = false;
                    //Option.isplayer2_turn = true;
                    //label_player_turn.Text = "Player 2";
                    //label_player_turn.ForeColor = Color.Crimson;

                    Option.isplayer1_turn = true;
                    Option.isplayer2_turn = false;
                    label_player_turn.Text = "Player 1";
                    label_player_turn.ForeColor = Color.RoyalBlue;

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
            
            make_ai_move(is_iterative_depth);





        }
       
        public async  void make_ai_move(bool is_iterative_deepening)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            double value;
            //value = AI.negamax(GameState.game_state, Option.depth_of_search, double.NegativeInfinity, double.PositiveInfinity);
            //value = AI.pvs(GameState.game_state, 3, double.NegativeInfinity, double.PositiveInfinity);

            if(is_iterative_deepening)
            {
                run_iterative_deepening(0);
            }
            else
            {
                if(checkBox1.Checked)
                {
                    value = AI.minimax(GameState.game_state, Option.depth_of_search, true);
                }
                if(checkBox2.Checked)
                {
                    value = AI.minimax_alpha_beta_pruning(GameState.game_state, Option.depth_of_search, double.NegativeInfinity, double.PositiveInfinity, true);
                }
                if(checkBox3.Checked)
                {
                    value = AI.negamax(GameState.game_state, Option.depth_of_search, double.NegativeInfinity, double.PositiveInfinity);
                }
                if(checkBox4.Checked)
                {
                    value = AI.pvs(GameState.game_state, Option.depth_of_search, double.NegativeInfinity, double.PositiveInfinity);
                }

            }
            if(!checkBox1.Checked && !checkBox2.Checked && !checkBox3.Checked && !checkBox4.Checked)
            {
                value = AI.negamax(GameState.game_state, Option.depth_of_search, double.NegativeInfinity, double.PositiveInfinity);
            }
            



            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTimeString = string.Format("{0:00}:{1:00}:{2:00}", ts.Minutes, ts.Seconds, ts.Milliseconds / 10);

            if (is_iterative_depth)
            {
                label_ai_move_result.Text = "(" + AI.get_ai_move_iterative().row.ToString() + "," + AI.get_ai_move_iterative().column.ToString() + "," + "value:" + AI.ai_state_iterative_deepening.value.ToString() + ")";
                label_ai_move_stats.Text = elapsedTimeString + ", depth of iterative deepening =  " + depth_deepening.ToString();

            }
            else
            {
                label_ai_move_result.Text = "(" + AI.ai_move.row.ToString() + "," + AI.ai_move.column.ToString() + "," + "value:" + AI.ai_state.value.ToString() + ")";
                label_ai_move_stats.Text = elapsedTimeString + ", depth of iterative deepening =  " + depth_deepening.ToString();
            }


        }

        
        public static Task CancellableSearch(CancellationToken ct,int search_type)
        {
            //https://arghya.xyz/articles/task-cancellation/
            return Task.Factory.StartNew(async () => {
                int iterative_depth = Option.depth_of_search;
                while (true)
                {
                    if(search_type == 0)//Negamax
                    {
                        double x = await AI.negamax_id(GameState.game_state, iterative_depth, double.NegativeInfinity, double.PositiveInfinity, ct);
                    }
                    if(search_type == 1)
                    {
                        double x = await AI.pvs_id(GameState.game_state, iterative_depth, double.NegativeInfinity, double.PositiveInfinity, ct);
                    }
                    if(search_type == 2)
                    {
                        double x = await AI.minimax_alpha_beta_pruning_id(GameState.game_state, Option.depth_of_search, double.NegativeInfinity, double.PositiveInfinity, true,ct);
                    }
                    AI.set_ai_move_iterative(AI.ai_move);
                    string info = "(" + AI.get_ai_move_iterative().row.ToString() + "," + AI.get_ai_move_iterative().column.ToString() + "," + "value:" + AI.ai_state_iterative_deepening.value.ToString() + ")";
                    AI.ai_state_iterative_deepening = AI.ai_state;
                    iterative_depth += 1;
                    depth_deepening = iterative_depth;
                }

            }, ct);
        }

        public async void run_iterative_deepening(int search_type)
        {
            CancellationTokenSource source = new CancellationTokenSource();
            var task = CancellableSearch(source.Token,search_type);
            try
            {
                task.Wait(Option.iterative_deepening_time_limit_milliseconds);
            }
            catch (AggregateException ae)
            {
                if (ae.InnerExceptions.Any(e => e is TaskCanceledException))
                    MessageBox.Show("Task cancelled exception detected");
                else
                    throw;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                source.Dispose();
            }

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
                if(Option.isplayer1_turn)
                {
                    Option.isplayer1_turn = false;
                    Option.isplayer2_turn = true;
                }
                else
                {
                    Option.isplayer1_turn = true;
                    Option.isplayer2_turn = false;
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                checkBox2.Enabled = false;
                checkBox3.Enabled = false;
                checkBox4.Enabled = false;
            }
            else
            {
                checkBox2.Enabled = true;
                checkBox3.Enabled = true;
                checkBox4.Enabled = true;
            }

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox2.Checked)
            {
                checkBox1.Enabled = false;
                checkBox3.Enabled = false;
                checkBox4.Enabled = false;
            }
            else
            {
                checkBox1.Enabled = true;
                checkBox3.Enabled = true;
                checkBox4.Enabled = true;
            }

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox3.Checked)
            {
                checkBox1.Enabled = false;
                checkBox2.Enabled = false;
                checkBox4.Enabled = false;
            }
            else
            {
                checkBox1.Enabled = true;
                checkBox2.Enabled = true;
                checkBox4.Enabled = true;
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox4.Checked)
            {
                checkBox1.Enabled = false;
                checkBox2.Enabled = false;
                checkBox3.Enabled = false;
            }
            else
            {
                checkBox1.Enabled = true;
                checkBox2.Enabled = true;
                checkBox3.Enabled = true;
            }
        }
    }


}
