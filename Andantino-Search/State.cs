using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andantino_Search
{
    public struct State
    {
        public Hexagon move {  get;  set; }

        public int player;//1 for max and 0 for min

        public List<Hexagon> player1_hexes;
        public int evaluation_p1;

        public List<Hexagon> player2_hexes;
        public int evaluation_p2;

        public int depth;

        public List<Hexagon> empty_hexes;

        public List<Hexagon> possible_hexes;

        public double value;

        public bool is_game_over;

        public State(Hexagon move, int player, List<Hexagon> p1_hexes, int eva_p1, List<Hexagon> p2_hexes, int eva_p2, int depth,
            List<Hexagon> empty_hexes, List<Hexagon> possible_hexes, bool is_over, double value)
        {
            this.move = move;
            this.player = player;
            player1_hexes = p1_hexes;
            evaluation_p1 = eva_p1;
            player2_hexes = p2_hexes;
            evaluation_p2 = eva_p2;
            this.depth = depth;
            this.empty_hexes = empty_hexes;
            this.possible_hexes = possible_hexes;
            is_game_over = is_over;
            this.value = value;//dumb value because of c# requirement

            int horizontal_score = count_horizontal_sequence(move, Option.number_coins_required, player);

            int right_diagonal_score = count_diagonal_right_sequence(move, Option.number_coins_required, player, horizontal_score);

            int left_diagonal_score = count_left_diagonal_sequence(move, Option.number_coins_required, player, horizontal_score, right_diagonal_score);

            double value_temp = Math.Pow(horizontal_score, horizontal_score) + Math.Pow(right_diagonal_score, right_diagonal_score) + Math.Pow(left_diagonal_score, left_diagonal_score);
            value = value_temp;


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

        

    }
}
