using System;
using System.Collections.Generic;
using System.Linq;


//Hexagon move, int player, List<Hexagon> state_player1_hexes, List<Hexagon> state_player2_hexes, int depth, List<Hexagon> empty_hexes, List<Hexagon> possible_hexes, double value, bool is_game_over
namespace Andantino_Search
{
    public struct State : IComparable<State>
    {
        public Hexagon move {  get;  set; }// Move that led to this State

        public int player { get; set; }//1 for max and 2 for min // player who needs to make the move

        public List<Hexagon> state_player1_hexes { get; set; }//player

        public List<Hexagon> state_player2_hexes { get; set; }//opponent

        public int depth { get; set; }

        public List<Hexagon> empty_hexes { get; set; }

        public List<Hexagon> possible_hexes { get; set; }

        public double value { get; set; }

        public bool is_game_over { get; set; }

        

        public State(Hexagon move, int player, List<Hexagon> p1_hexes, List<Hexagon> p2_hexes, int depth,
            List<Hexagon> empty_hexes, List<Hexagon> possible_hexes, bool is_over, double value)
        //public State()
        {
            this.move = move;
            this.player = player;//P1 = 1, P2 = 2
            state_player1_hexes = p1_hexes;
            //evaluation_p1 = eva_p1;
            state_player2_hexes = p2_hexes;
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
        //public State()
        //{

        //}

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
                            if (state_player2_hexes.Any(hex => hex.row == row - i && hex.column == col + even_start_addition_upright[i - 1]) || Option.hexes_outer_board.Any(hex => hex.row == row - i && hex.column == col + even_start_addition_upright[i - 1]) || empty_hexes.Any(hex => hex.row == row - i && hex.column == col + even_start_addition_upright[i - 1]))
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
                            if (state_player2_hexes.Any(hex => hex.row == row - i && hex.column == col + odd_start_addition_upright[i - 1]) || Option.hexes_outer_board.Any(hex => hex.row == row - i && hex.column == col + odd_start_addition_upright[i - 1]) || empty_hexes.Any(hex => hex.row == row - i && hex.column == col + odd_start_addition_upright[i - 1]))
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
                            if (state_player1_hexes.Any(hex => hex.row == row - i && hex.column == col + even_start_addition_upright[i - 1]) || Option.hexes_outer_board.Any(hex => hex.row == row - i && hex.column == col + even_start_addition_upright[i - 1]) || empty_hexes.Any(hex => hex.row == row - i && hex.column == col + even_start_addition_upright[i - 1]))
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
                            if (state_player1_hexes.Any(hex => hex.row == row - i && hex.column == col + odd_start_addition_upright[i - 1]) || Option.hexes_outer_board.Any(hex => hex.row == row - i && hex.column == col + odd_start_addition_upright[i - 1]) || empty_hexes.Any(hex => hex.row == row - i && hex.column == col + odd_start_addition_upright[i - 1]))
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
                                if (state_player2_hexes.Any(hex => hex.row == row + i && hex.column == col - even_start_downleft[i - 1]) || Option.hexes_outer_board.Any(hex => hex.row == row + i && hex.column == col - even_start_downleft[i - 1]))// ||hexes_outer_board.Any(hex => hex.row == row + i && hex.column == col - even_start_downleft[i - 1])
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
                                if (state_player2_hexes.Any(hex => hex.row == row + i && hex.column == col - odd_start_downleft[i - 1]))
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
                                if (state_player1_hexes.Any(hex => hex.row == row + i && hex.column == col - even_start_downleft[i - 1]) || Option.hexes_outer_board.Any(hex => hex.row == row + i && hex.column == col - even_start_downleft[i - 1]) || empty_hexes.Any(hex => hex.row == row + i && hex.column == col - even_start_downleft[i - 1]))
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
                                if (state_player1_hexes.Any(hex => hex.row == row + i && hex.column == col - odd_start_downleft[i - 1]) || Option.hexes_outer_board.Any(hex => hex.row == row + i && hex.column == col - odd_start_downleft[i - 1]) || empty_hexes.Any(hex => hex.row == row + i && hex.column == col - odd_start_downleft[i - 1]))
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
                            if (state_player2_hexes.Any(hex => hex.row == row - i && hex.column == col - even_start_upleft[i - 1]) || Option.hexes_outer_board.Any(hex => hex.row == row - i && hex.column == col - even_start_upleft[i - 1]) || empty_hexes.Any(hex => hex.row == row - i && hex.column == col - even_start_upleft[i - 1]))
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
                            if (state_player2_hexes.Any(hex => hex.row == row - i && hex.column == col - odd_start_upleft[i - 1]) || Option.hexes_outer_board.Any(hex => hex.row == row - i && hex.column == col - odd_start_upleft[i - 1]) || empty_hexes.Any(hex => hex.row == row - i && hex.column == col - odd_start_upleft[i - 1]))
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
                            if (state_player1_hexes.Any(hex => hex.row == row - i && hex.column == col - even_start_upleft[i - 1]) || Option.hexes_outer_board.Any(hex => hex.row == row - i && hex.column == col - even_start_upleft[i - 1]) || empty_hexes.Any(hex => hex.row == row - i && hex.column == col - even_start_upleft[i - 1]))
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
                            if (state_player1_hexes.Any(hex => hex.row == row - i && hex.column == col - odd_start_upleft[i - 1]) || Option.hexes_outer_board.Any(hex => hex.row == row - i && hex.column == col - odd_start_upleft[i - 1]) || empty_hexes.Any(hex => hex.row == row - i && hex.column == col - odd_start_upleft[i - 1]))
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
                                if (state_player2_hexes.Any(hex => hex.row == row + i && hex.column == col + even_start_downright[i - 1]) || Option.hexes_outer_board.Any(hex => hex.row == row + i && hex.column == col + even_start_downright[i - 1]) || empty_hexes.Any(hex => hex.row == row + i && hex.column == col + even_start_downright[i - 1]))
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
                                if (state_player2_hexes.Any(hex => hex.row == row + i && hex.column == col + odd_start_downright[i - 1]) || Option.hexes_outer_board.Any(hex => hex.row == row + i && hex.column == col + odd_start_downright[i - 1]) || empty_hexes.Any(hex => hex.row == row + i && hex.column == col + odd_start_downright[i - 1]))
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
                                if (state_player1_hexes.Any(hex => hex.row == row + i && hex.column == col + even_start_downright[i - 1]) || Option.hexes_outer_board.Any(hex => hex.row == row + i && hex.column == col + even_start_downright[i - 1]) || empty_hexes.Any(hex => hex.row == row + i && hex.column == col + even_start_downright[i - 1]))
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
                                if (state_player1_hexes.Any(hex => hex.row == row + i && hex.column == col + odd_start_downright[i - 1]) || Option.hexes_outer_board.Any(hex => hex.row == row + i && hex.column == col + odd_start_downright[i - 1]) || empty_hexes.Any(hex => hex.row == row + i && hex.column == col + odd_start_downright[i - 1]))
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
                    if (state_player2_hexes.Any(hex => hex.row == row && hex.column == col + i) || Option.hexes_outer_board.Any(hex => hex.row == row && hex.column == col + i) || empty_hexes.Any(hex => hex.row == row && hex.column == col + i))
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
                    if (state_player1_hexes.Any(hex => hex.row == row && hex.column == col + i) || Option.hexes_outer_board.Any(hex => hex.row == row && hex.column == col + i) || empty_hexes.Any(hex => hex.row == row && hex.column == col + i))
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
                        if (state_player2_hexes.Any(hex => hex.row == row && hex.column == col - i) || Option.hexes_outer_board.Any(hex => hex.row == row && hex.column == col - i) || empty_hexes.Any(hex => hex.row == row && hex.column == col - i))
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
                        if (state_player1_hexes.Any(hex => hex.row == row && hex.column == col - i) || Option.hexes_outer_board.Any(hex => hex.row == row && hex.column == col - i) || empty_hexes.Any(hex => hex.row == row && hex.column == col - i))
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

        public int[] check_is_victory(Hexagon new_hex, int which_player_played)//P1 = 1, P2 = 2
        {

            int number_coins_required = 5;

            int horizontal_sequential = count_horizontal_sequence(new_hex, number_coins_required, which_player_played);

            int right_diagonal_sequential = count_diagonal_right_sequence(new_hex, number_coins_required, which_player_played, horizontal_sequential);

            int left_diagonal_sequential = count_left_diagonal_sequence(new_hex, number_coins_required, which_player_played, horizontal_sequential, right_diagonal_sequential);

            int[] result = new int[] { horizontal_sequential,right_diagonal_sequential,left_diagonal_sequential };
            return result;


        }

        public List<Hexagon> get_neighbors(Hexagon middle_hex)
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

        public List<Hexagon> set_possible_hexes(List<Hexagon> state_player1_hexes, List<Hexagon> state_player2_hexes)
        {
            List<Hexagon> neighbors2 = new List<Hexagon>();
            List<Hexagon> possible_hexes = new List<Hexagon>();
            //ReadOnlySpan<Hexagon> test = new ReadOnlySpan<Hexagon>(neighbors2.ToArray());
            //test.BinarySearch(possible_hexes[0]);
            //neighbors2.BinarySearch(possible_hexes[0]);

            for (int i = 0; i < state_player1_hexes.Count; i++)//choosing possible hexes
            {
                neighbors2.AddRange(get_neighbors(state_player1_hexes[i]));

            }
            for (int i = 0; i < state_player2_hexes.Count; i++)
            {
                neighbors2.AddRange(get_neighbors(state_player2_hexes[i]));
            }

            for (int i = 0; i < state_player1_hexes.Count; i++)
            {
                int index = neighbors2.FindIndex(hex => hex.Equals(state_player1_hexes[i]));
                if (index >= 0)
                {
                    neighbors2.RemoveAt(index);
                }

            }
            for (int i = 0; i < state_player2_hexes.Count; i++)
            {
                int index = neighbors2.FindIndex(hex => hex.Equals(state_player2_hexes[i]));
                if (index >= 0)
                {
                    neighbors2.RemoveAt(index);
                }

            }

            for (int i = 0; i < neighbors2.Count; i++)
            {
                List<Hexagon> temp_list = new List<Hexagon>(neighbors2);
                temp_list.RemoveAt(i);
                if (temp_list.AsParallel().Contains(neighbors2[i]) && !state_player1_hexes.AsParallel().Contains(neighbors2[i]) && !state_player2_hexes.AsParallel().Contains(neighbors2[i]) && !possible_hexes.AsParallel().Contains(neighbors2[i]))
                {
                    possible_hexes.Add(neighbors2[i]);
                }
            }

            return possible_hexes;

        }

        //public List<Hexagon> set_possible_hexes(List<Hexagon> state_player1_hexes, List<Hexagon> state_player2_hexes)
        //{
        //    //Dictionary<Tuple<int, int>, Hexagon> neighbors2 = new Dictionary<Tuple<int, int>, Hexagon>();
        //    Dictionary<Tuple<int, int>, Hexagon> possible_hexes = new Dictionary<Tuple<int, int>, Hexagon>();
        //    List<Hexagon> poss_hexes = new List<Hexagon>();
        //    List<Hexagon> neighbors2 = new List<Hexagon>();
        //    //ReadOnlySpan<Hexagon> test = new ReadOnlySpan<Hexagon>(neighbors2.ToArray());
        //    //test.BinarySearch(possible_hexes[0]);
        //    //neighbors2.BinarySearch(possible_hexes[0]);

        //    for (int i = 0; i < state_player1_hexes.Count; i++)//choosing possible hexes
        //    {
        //        neighbors2.AddRange(get_neighbors(state_player1_hexes[i]));

        //    }
        //    for (int i = 0; i < state_player2_hexes.Count; i++)
        //    {
        //        neighbors2.AddRange(get_neighbors(state_player2_hexes[i]));
        //    }

        //    for (int i = 0; i < state_player1_hexes.Count; i++)
        //    {
        //        int index = neighbors2.FindIndex(hex => hex.Equals(state_player1_hexes[i]));
        //        if (index >= 0)
        //        {
        //            neighbors2.RemoveAt(index);
        //        }

        //    }
        //    for (int i = 0; i < state_player2_hexes.Count; i++)
        //    {
        //        int index = neighbors2.FindIndex(hex => hex.Equals(state_player2_hexes[i]));
        //        if (index >= 0)
        //        {
        //            neighbors2.RemoveAt(index);
        //        }

        //    }
        //    Dictionary<Tuple<int, int>, Hexagon> neighbors2_dict = new Dictionary<Tuple<int, int>, Hexagon>();
        //    Dictionary<Tuple<int, int>, Hexagon> p1_dict = new Dictionary<Tuple<int, int>, Hexagon>();
        //    Dictionary<Tuple<int, int>, Hexagon> p2_dict = new Dictionary<Tuple<int, int>, Hexagon>();

        //    for (int i = 0; i < state_player1_hexes.Count; i++)
        //    {
        //        p1_dict.Add(Tuple.Create(state_player1_hexes[i].row, state_player1_hexes[i].column), state_player1_hexes[i]);
        //    }

        //    for (int i = 0; i < state_player2_hexes.Count; i++)
        //    {
        //        p2_dict.Add(Tuple.Create(state_player2_hexes[i].row, state_player2_hexes[i].column), state_player2_hexes[i]);
        //    }

        //    for (int i = 0; i < neighbors2.Count; i++)
        //    {
        //        try
        //        {
        //            neighbors2_dict.Add(Tuple.Create(neighbors2[i].row, neighbors2[i].column), neighbors2[i]);
        //        }
        //        catch (Exception)
        //        {


        //        }

        //    }



        //    for (int i = 0; i < neighbors2.Count; i++)
        //    {
        //        //List<Hexagon> temp_list = new List<Hexagon>(neighbors2);
        //        Dictionary<Tuple<int, int>, Hexagon> temp_list = new Dictionary<Tuple<int, int>, Hexagon>(neighbors2_dict);
        //        temp_list.Remove(temp_list.ElementAt(i).Key);
        //        if (temp_list.ContainsKey(neighbors2_dict.ElementAt(i).Key) && !p1_dict.ContainsKey(neighbors2_dict.ElementAt(i).Key) && !p2_dict.ContainsKey(neighbors2_dict.ElementAt(i).Key) && !possible_hexes.ContainsKey(neighbors2_dict.ElementAt(i).Key))
        //        {
        //            possible_hexes.Add(neighbors2_dict.ElementAt(i).Key, neighbors2_dict.ElementAt(i).Value);
        //        }
        //    }
        //    for (int i = 0; i < possible_hexes.Count; i++)
        //    {
        //        poss_hexes.Add(possible_hexes.ElementAt(i).Value);
        //    }
        //    return poss_hexes;

        //}


        public State get_state_after_move(State ancestor_state,Hexagon move)//New state after move from 1 of possible hexes
        {
            State s = new State();
            s.move = move;
            s.state_player1_hexes = new List<Hexagon>(ancestor_state.state_player1_hexes);
            s.state_player2_hexes = new List<Hexagon>(ancestor_state.state_player2_hexes);
            s.empty_hexes = new List<Hexagon>(ancestor_state.empty_hexes);
            s.empty_hexes.Remove(move);
            s.depth = ancestor_state.depth + 1;
            s.possible_hexes = new List<Hexagon>();


            

            if (ancestor_state.player == 1)
            {
                s.player = 2;
            }
            else
            {
                s.player = 1;
            }


            if(s.player == 2)
            {
                s.state_player2_hexes.Add(move);
                s.is_game_over = false;
                int[] result = s.check_is_victory(move, 2);

                s.value = Math.Pow(result[0], result[0]) + Math.Pow(result[1], result[1]) + Math.Pow(result[2], result[2]);
                List<Hexagon> move_neighbors = get_neighbors(move);
                int temp = 0;
                for (int i = 0; i < move_neighbors.Count; i++)
                {
                    if (s.state_player2_hexes.Contains(move_neighbors[i]))
                    {
                        temp += 1;
                    }
                }
                if (temp == 0)
                {
                    s.value -= 50;
                }
                foreach (var item in result)
                {
                    if(item == 5)
                    {
                        s.is_game_over = true;
                    }
                }
                if (!s.is_game_over)
                {
                    s.is_game_over = is_draw(s.empty_hexes);
                }
                //if (!s.is_game_over)
                //{
                //    s.is_game_over = s.base_case_surrounding(s.state_player1_hexes, s.state_player2_hexes);
                //}
                if (!s.is_game_over)
                {
                    if (s.state_player1_hexes.Count > 5)
                    {
                        for (int i = 0; i < s.state_player1_hexes.Count; i++)
                        {
                            if (out_of_boundaries(s.state_player1_hexes[i], s.state_player2_hexes))
                            {
                                s.is_game_over = true;
                                break;
                            }
                        }
                    }

                }
            }
            else//player1
            {
                s.state_player1_hexes.Add(move);
                s.is_game_over = false;
                int[] result = s.check_is_victory(move, 1);
                //s.value = Math.Pow(result[0], result[0]) + Math.Pow(result[1], result[1]) + Math.Pow(result[2], result[2]);
                List<Hexagon> move_neighbors = get_neighbors(move);
                //int temp = 0;
                //for (int i = 0; i < move_neighbors.Count; i++)
                //{
                //    if(s.state_player1_hexes.Contains(move_neighbors[i]))
                //    {
                //        temp += 1;
                //    }
                //}
                //if(temp == 0)
                //{
                //    s.value -= 50;
                //}
                s.value = get_fitness(result, move_neighbors, s);
                foreach (var item in result)
                {
                    if(item == 5)
                    {
                        s.is_game_over = true;
                    }
                }
                if(!s.is_game_over)
                {
                    s.is_game_over = is_draw(s.empty_hexes);
                }

                //if(!s.is_game_over)
                //{
                //    s.is_game_over = s.base_case_surrounding(s.state_player2_hexes, s.state_player1_hexes);
                //}
                if (!s.is_game_over)
                {
                    if (s.state_player2_hexes.Count > 5)
                    {
                        for (int i = 0; i < s.state_player2_hexes.Count; i++)
                        {
                            if (out_of_boundaries(s.state_player2_hexes[i], s.state_player1_hexes))
                            {
                                s.is_game_over = true;
                                break;
                            }
                        }
                    }
                }
                
            }
            

            if (!s.is_game_over)
            {
                //calculate possible hexes
                s.possible_hexes = s.set_possible_hexes(s.state_player1_hexes, s.state_player2_hexes);
                //s.possible_hexes = s.set_possible_hexes(p1_dict, p2_dict);

            }




            return s;


            //define function is_game_over that checks victory or if all_hexes_taken
            //if 2nd case, then draw
        }

        public double get_fitness(int[] result, List<Hexagon> move_neighbors, State s)
        {
            double fitness_value;
            fitness_value = Math.Pow(result[0], result[0]) + Math.Pow(result[1], result[1]) + Math.Pow(result[2], result[2]);
            int temp = 0;
            for (int i = 0; i < move_neighbors.Count; i++)
            {
                if (s.state_player1_hexes.Contains(move_neighbors[i]))
                {
                    temp += 1;
                }
            }
            if (temp == 0)
            {
                fitness_value -= 50;
            }
            return fitness_value;
        }

        public int CompareTo(State other)
        {
            if(other.value == value)
            {
                return 0;
            }
            if(other.value> value)
            {
                return 1;//with 1 here it sorts from smallest to biggest
            }
            else
            {
                return -1;
            }
        }

        public bool check_is_trapped(Hexagon middle, List<Hexagon> previous, List<Hexagon> hex_players, List<Hexagon> hexes_board)
        {
            //bool is_trapped = false;
            if (get_neighbors(middle).Count<6)
            {
                return true;
            }
            if(hex_players.Contains(middle) || previous.Contains(middle))
            {
                return false;
            }
            else
            {
                previous.Add(middle);
                List<Hexagon> temp = get_neighbors(middle);
                for (int i = 0; i < temp.Count; i++)
                {
                    //Option.number_calls_flood_fill += 1;
                    if(check_is_trapped(temp[i],previous,hex_players,hexes_board))
                    {
                        return true;
                    }
                }
            }
            return false;

        }
        public bool out_of_boundaries(Hexagon hex, List<Hexagon> hexes_player)
        {
            List<Hexagon> temp1 = new List<Hexagon>();
            if (!check_is_trapped(hex, temp1, hexes_player, GameStatic.hexes_in_board))
            {
                //MessageBox.Show("Game Over");
                
                return true;
            }
            else
                return false;
        }

        public bool base_case_surrounding(List<Hexagon> hexes_player, List<Hexagon> other_player_hexes)
        {
            int temp;
            for (int j = 0; j < hexes_player.Count; j++)
            {
                
                List<Hexagon> move_neighbors = GameState.game_state.get_neighbors(hexes_player[j]);
                temp = 1;
                for (int i = 0; i < move_neighbors.Count; i++)
                {

                    if (!other_player_hexes.Contains(move_neighbors[i]))
                    {
                        break;
                    }
                    else
                    {
                        temp += 1;
                    }
                }
                if (temp == move_neighbors.Count)
                {

                    return true;
                }
            }
            return false;
            
        }

        public bool is_draw(List<Hexagon> empty_hexes)
        {
            if(empty_hexes.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


    }
}
