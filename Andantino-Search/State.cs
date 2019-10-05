using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//Hexagon move, int player, List<Hexagon> player1_hexes, List<Hexagon> player2_hexes, int depth, List<Hexagon> empty_hexes, List<Hexagon> possible_hexes, double value, bool is_game_over
namespace Andantino_Search
{
    public struct State
    {
        public Hexagon move {  get;  set; }// Move that led to this State

        public int player { get; set; }//1 for max and 2 for min // player who needs to make the move

        public List<Hexagon> player1_hexes { get; set; }//player
        //public int evaluation_p1 { get; set; }//player

        public List<Hexagon> player2_hexes { get; set; }//opponent
        //public int evaluation_p2 { get; set; }//opponent

        public int depth { get; set; }

        public List<Hexagon> empty_hexes { get; set; }

        public List<Hexagon> possible_hexes { get; set; }

        public double value { get; set; }

        public bool is_game_over { get; set; }

        public State(Hexagon move, int player, List<Hexagon> p1_hexes, int eva_p1, List<Hexagon> p2_hexes, int depth,
            List<Hexagon> empty_hexes, List<Hexagon> possible_hexes, bool is_over, double value)
        //public State()
        {
            this.move = move;
            this.player = player;//P1 = 1, P2 = 2
            player1_hexes = p1_hexes;
            //evaluation_p1 = eva_p1;
            player2_hexes = p2_hexes;
            //evaluation_p2 = eva_p2;
            this.depth = depth;
            this.empty_hexes = empty_hexes;
            this.possible_hexes = possible_hexes;
            is_game_over = is_over;
            this.value = value;

            //int horizontal_score = count_horizontal_sequence(move, Option.number_coins_required, player);

            //int right_diagonal_score = count_diagonal_right_sequence(move, Option.number_coins_required, player, horizontal_score);

            //int left_diagonal_score = count_left_diagonal_sequence(move, Option.number_coins_required, player, horizontal_score, right_diagonal_score);

            //double value_temp = Math.Pow(horizontal_score, horizontal_score) + Math.Pow(right_diagonal_score, right_diagonal_score) + Math.Pow(left_diagonal_score, left_diagonal_score);
            //value = value_temp;// maybe useless ? The value will be computed outside of this, maybe in get_state_after_move


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

        public bool check_is_victory(Hexagon new_hex, int which_player_played)//P1 = 1, P2 = 2
        {
            bool is_winner = false;
            //int[] test = new int[3];

            int number_coins_required = 5;

            int horizontal_sequential = count_horizontal_sequence(new_hex, number_coins_required, which_player_played);

            int right_diagonal_sequential = count_diagonal_right_sequence(new_hex, number_coins_required, which_player_played, horizontal_sequential);

            int left_diagonal_sequential = count_left_diagonal_sequence(new_hex, number_coins_required, which_player_played, horizontal_sequential, right_diagonal_sequential);

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

        private List<Hexagon> get_neighbors(Hexagon middle_hex)
        {
            List<Hexagon> neighbors = new List<Hexagon>();
            int column_hex = middle_hex.column;
            int row_hex = middle_hex.row;

            int left_neighbor_row = row_hex;
            int left_neighbor_col = column_hex - 1;

            try
            {
                neighbors.Add(GameStatic.hexes_board_dict[Tuple.Create(left_neighbor_row, left_neighbor_col)]);
            }
            catch (Exception)
            {

                
            }

            int right_neighbor_row = row_hex;
            int right_neighbor_col = column_hex + 1;

            try
            {
                neighbors.Add(GameStatic.hexes_board_dict[Tuple.Create(right_neighbor_row, right_neighbor_col)]);
            }
            catch (Exception)
            {

               
            }


            int upper_left_neighbor_row = row_hex - 1;

            int upper_right_neighbor_row = upper_left_neighbor_row;

            int bottom_left_neighbor_row = row_hex + 1;

            int bottom_right_neighbor_row = bottom_left_neighbor_row;

            //dumb values that will be changed anyway
            int upper_left_neighbor_col = column_hex;
            int upper_right_neighbor_col = column_hex + 1;
            int bottom_left_neighbor_col = column_hex;
            int bottom_right_neighbor_col = column_hex + 1;
            //dummy values

            if (row_hex % 2 != 0)
            {
                upper_left_neighbor_col = column_hex;
                upper_right_neighbor_col = column_hex + 1;
                bottom_left_neighbor_col = column_hex;
                bottom_right_neighbor_col = column_hex + 1; 
            }
            else
            {
                upper_left_neighbor_col = column_hex - 1;
                upper_right_neighbor_col = column_hex;
                bottom_left_neighbor_col = column_hex - 1;
                bottom_right_neighbor_col = column_hex;
            }


            try
            {
                neighbors.Add(GameStatic.hexes_board_dict[Tuple.Create(upper_left_neighbor_row, upper_left_neighbor_col)]);
            }
            catch (Exception)
            {


            }

            try
            {
                neighbors.Add(GameStatic.hexes_board_dict[Tuple.Create(upper_right_neighbor_row, upper_right_neighbor_col)]);
            }
            catch (Exception)
            {


            }

            try
            {
                neighbors.Add(GameStatic.hexes_board_dict[Tuple.Create(bottom_left_neighbor_row, bottom_left_neighbor_col)]);
            }
            catch (Exception)
            {


            }

            try
            {
                neighbors.Add(GameStatic.hexes_board_dict[Tuple.Create(bottom_right_neighbor_row, bottom_right_neighbor_col)]);
            }
            catch (Exception)
            {


            }

            return neighbors;
        }

        public List<Hexagon> set_possible_hexes(List<Hexagon> player1_hexes, List<Hexagon> player2_hexes)
        {
            List<Hexagon> neighbors2 = new List<Hexagon>();
            List<Hexagon> possible_hexes = new List<Hexagon>();
            ReadOnlySpan<Hexagon> test = new ReadOnlySpan<Hexagon>(neighbors2.ToArray());
            test.BinarySearch(possible_hexes[0]);
            neighbors2.BinarySearch(possible_hexes[0]);

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
                if (temp_list.AsParallel().Contains(neighbors2[i]) && !player1_hexes.AsParallel().Contains(neighbors2[i]) && !player2_hexes.AsParallel().Contains(neighbors2[i]) && !possible_hexes.AsParallel().Contains(neighbors2[i]))
                {
                    possible_hexes.Add(neighbors2[i]);
                }
            }



            return possible_hexes;

        }


        public State get_state_after_move(State ancestor_state,Hexagon move)//New state after move from 1 of possible hexes
        {
            State s = new State();
            s.move = move;
            s.player1_hexes = new List<Hexagon>(ancestor_state.player1_hexes);
            s.player2_hexes = new List<Hexagon>(ancestor_state.player2_hexes);
            s.empty_hexes = new List<Hexagon>(ancestor_state.empty_hexes);
            s.empty_hexes.Remove(move);
            s.depth = ancestor_state.depth + 1;
            s.possible_hexes = new List<Hexagon>();
            
            if(ancestor_state.player == 1)
            {
                s.player = 2;
            }
            else
            {
                s.player = 1;
            }


            if(s.player == 2)
            { 
                s.player2_hexes.Add(move);
                s.is_game_over = s.check_is_victory(move, 2);
                

            }
            else//player1
            {
                s.player1_hexes.Add(move);
                s.is_game_over = s.check_is_victory(move, 1);

                
            }
            if (!s.is_game_over)// || not draw
            {
                //calculate possible hexes
                s.possible_hexes = s.set_possible_hexes(s.player1_hexes, s.player2_hexes);

            }

            int horizontal_score = s.count_horizontal_sequence(move, Option.number_coins_required, s.player);

            int right_diagonal_score = s.count_diagonal_right_sequence(move, Option.number_coins_required, s.player, horizontal_score);

            int left_diagonal_score = s.count_left_diagonal_sequence(move, Option.number_coins_required, s.player, horizontal_score, right_diagonal_score);

            s.value = Math.Pow(horizontal_score, horizontal_score) + Math.Pow(right_diagonal_score, right_diagonal_score) + Math.Pow(left_diagonal_score, left_diagonal_score);


            return s;

            //cloning is useless since we now create a new state ?

            //define function is_game_over that checks victory or if all_hexes_taken
            //if 2nd case, then draw
        }


        

    }
}
