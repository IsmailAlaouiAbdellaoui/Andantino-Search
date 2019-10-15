using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;
using System.IO;
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

        static bool global_is_game_over = false;
        //private static Hexagon ai_move;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label_player_turn.Text = "Player 2";

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

            label_player_turn.ForeColor = Color.Crimson;
            txtbox_number_possible_hexes.Text = GameState.game_state.possible_hexes.Count.ToString();

            GameState.game_history.Push(GameState.game_state);

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
                    for (int k = 0; k < GameStatic.all_hexes.Count; k++)
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
            dataGridView1.Rows.Clear();            

        }


        

        private void button1_Click(object sender, EventArgs e)
        {
            //to do
            //negamax
            //double check that everything works fine ( value + recursion + pruning)
            //transposition table
            //iterative deepening
            //timer
            //undo
            //all span

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            //double value = AI.minimax(GameState.game_state, 4, true);
            double value = AI.minimax_alpha_beta_pruning(GameState.game_state, 5, double.NegativeInfinity, double.PositiveInfinity, true);
            //double value = AI.negamax(GameState.game_state, 3, double.NegativeInfinity, double.PositiveInfinity);
            stopWatch.Stop();
            //depth_game++;
            label_ai_move_result.Text = "(" + AI.ai_move.row.ToString() + "," + AI.ai_move.column.ToString() + "," + "value:" + AI.ai_state.value.ToString() + ")";
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
            dataGridView1.Rows.Clear();
            dataGridView1.Rows.Add(elapsedTime);
            dataGridView1.Rows.Add("state depth " + AI.ai_state.depth);
            //GameState.game_state = GameState.game_state.get_state_after_move(GameState.game_state, ai_move);
            //game_history.Push(GameState.game_state);



        }

        private void button2_Click(object sender, EventArgs e)
        {
            //dataGridView1.Rows.Add("-:" + GameState.game_state.move + ", /:" + result[1].ToString() + ", \\:" + result[2].ToString());

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
    }

    
}
