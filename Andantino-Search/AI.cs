using System;
using System.Threading.Tasks;

namespace Andantino_Search
{
    public static class AI
    {
        public static Hexagon ai_move { get; set; }
        public static State ai_state { get; set; }
        public static double minimax(State s, int depth_minimax, bool maximizing_player)
        {
            double eval;
            if (depth_minimax == 0)
            {
                //ai_move = s.move;
                return s.value;
            }

            State child_state = new State();


            if (maximizing_player)
            {
                double maxEval = double.NegativeInfinity;
                for (int i = 0; i < s.possible_hexes.Count; i++)
                {

                    child_state = s.get_state_after_move(s, s.possible_hexes[i]);
                    string file_directory = Util.check_folder_file_directory();
                    //Util.log_minimax(file_directory, "Recursive call for min player with depth: " + (depth_minimax - 1));
                    //Util.log_minimax(file_directory, "Ancestor move: (" + s.move.row + "," + s.move.column + ")");
                    //Util.log_minimax(file_directory, "Information about the cild");
                    //Util.log_minimax(file_directory, "move of child state: (" + child_state.move.row + "," + child_state.move.column + "), value: " + child_state.value);
                    //Util.log_minimax(file_directory, "depth: " + child_state.depth);
                    //Util.log_minimax(file_directory, "# possible states: " + child_state.possible_hexes.Count);
                    //Util.log_minimax(file_directory, "\n\n");
                    eval = minimax(child_state, depth_minimax - 1, false);

                    if (eval > maxEval)
                    {

                        maxEval = eval;
                        if (s.move.row == GameState.game_state.move.row && s.move.column == GameState.game_state.move.column)
                        {
                            ai_move = child_state.move;
                            ai_state = child_state;
                        }
                        //ai_move = child_state.move;
                        //ai_state = child_state;
                        //Util.log_minimax(file_directory, "move chosen in condition: (" + child_state.move.row + "," + child_state.move.column + ")");
                        //Util.log_minimax(file_directory, "Recursive call for min player with depth: " + (depth_minimax - 1));
                        //Util.log_minimax(file_directory, "Ancestor move: (" + s.move.row + "," + s.move.column + ")");
                        //Util.log_minimax(file_directory, "Information about the cild");
                        //Util.log_minimax(file_directory, "move of child state: (" + child_state.move.row + "," + child_state.move.column + "), value: " + child_state.value);
                        //Util.log_minimax(file_directory, "depth: " + child_state.depth);
                        //Util.log_minimax(file_directory, "# possible states: " + child_state.possible_hexes.Count);
                        //Util.log_minimax(file_directory, "\n\n");
                        //ai_move = s.move;

                    }
                }
                //ai_move = s.move;
                return maxEval;
            }
            else
            {
                double minEval = double.PositiveInfinity;
                for (int i = 0; i < s.possible_hexes.Count; i++)
                {
                    child_state = s.get_state_after_move(s, s.possible_hexes[i]);
                    //string file_directory = Util.check_folder_file_directory();
                    //Util.log_minimax(file_directory, "Recursive call for max player with depth: " + (depth_minimax + 1));
                    //Util.log_minimax(file_directory, "Ancestor move: (" + s.move.row + "," + s.move.column + ")");
                    //Util.log_minimax(file_directory, "Information about the cild");
                    //Util.log_minimax(file_directory, "move of child state: (" + child_state.move.row + "," + child_state.move.column + "), value: " + child_state.value);
                    //Util.log_minimax(file_directory, "depth: " + child_state.depth);
                    //Util.log_minimax(file_directory, "# possible states: " + child_state.possible_hexes.Count);
                    //Util.log_minimax(file_directory, "\n\n");
                    eval = minimax(child_state, depth_minimax - 1, true);
                    if (eval < minEval)
                    {

                        minEval = eval;
                        //ai_move = child_state.move;
                        if (s.move.row == GameState.game_state.move.row && s.move.column == GameState.game_state.move.column)
                        {
                            ai_move = child_state.move;
                            ai_state = child_state;
                        }
                        //Util.log_minimax(file_directory, "move chosen in condition: (" + child_state.move.row + "," + child_state.move.column + ")");
                        //Util.log_minimax(file_directory, "Recursive call for max player with depth: " + (depth_minimax + 1));
                        //Util.log_minimax(file_directory, "Ancestor move: (" + s.move.row + "," + s.move.column + ")");
                        //Util.log_minimax(file_directory, "Information about the cild");
                        //Util.log_minimax(file_directory, "move of child state: (" + child_state.move.row + "," + child_state.move.column + "), value: " + child_state.value);
                        //Util.log_minimax(file_directory, "depth: " + child_state.depth);
                        //Util.log_minimax(file_directory, "# possible states: " + child_state.possible_hexes.Count);
                        //Util.log_minimax(file_directory, "\n\n");
                        //ai_state = child_state;
                    }

                }
                //ai_move = s.move;
                return minEval;
            }
        }
        public static double minimax_alpha_beta_pruning(State s, int depth_alpha_beta, double alpha, double beta, bool maximizing_player)
        {
            double eval;
            if (depth_alpha_beta == 0 || s.is_game_over)
            {
                return s.value;
            }

            State child_state;


            if (maximizing_player)
            {
                double maxEval = double.NegativeInfinity;
                for (int i = 0; i < s.possible_hexes.Count; i++)
                {

                    child_state = s.get_state_after_move(s, s.possible_hexes[i]);
                    eval = minimax_alpha_beta_pruning(child_state, depth_alpha_beta - 1, alpha, beta, false);

                    if (eval > maxEval)
                    {
                        if (s.move.row == GameState.game_state.move.row && s.move.column == GameState.game_state.move.column)
                        {
                            ai_move = child_state.move;
                            ai_state = child_state;
                        }
                        //ai_move = child_state.move;
                        //maxEval = eval;

                    }

                    alpha = Math.Max(alpha, eval);
                    if (beta <= alpha)
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
                        //ai_move = child_state.move;
                        if (s.move.row == GameState.game_state.move.row && s.move.column == GameState.game_state.move.column)
                        {
                            ai_move = child_state.move;
                            ai_state = child_state;
                        }
                    }
                    beta = Math.Min(beta, eval);
                    if (beta <= alpha)
                    {
                        break;
                    }

                }
                return minEval;
            }

        }

        public static double minimax_alpha_beta_pruning_parallel(State s, int depth_alpha_beta, double alpha, double beta, bool maximizing_player)
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
                Parallel.For(0, s.possible_hexes.Count, (i, parallel_loop_state)
                     =>
                {
                    child_state = s.get_state_after_move(s, s.possible_hexes[i]);

                    eval = minimax_alpha_beta_pruning(child_state, depth_alpha_beta - 1, alpha, beta, false);

                    if (eval > maxEval)
                    {
                        ai_move = child_state.move;
                        maxEval = eval;

                    }

                    alpha = Math.Max(alpha, eval);
                    if (beta <= alpha)
                    {
                        parallel_loop_state.Break();
                    }
                });

                return maxEval;
            }
            else
            {
                double minEval = double.PositiveInfinity;
                Parallel.For(0, s.possible_hexes.Count, (i, pls)
                      =>
                {
                    child_state = s.get_state_after_move(s, s.possible_hexes[i]);
                    eval = minimax_alpha_beta_pruning(child_state, depth_alpha_beta - 1, alpha, beta, true);
                    if (eval < minEval)
                    {
                        minEval = eval;
                        ai_move = child_state.move;
                    }
                    beta = Math.Min(beta, eval);
                    if (beta <= alpha)
                    {
                        pls.Break();
                    }

                });
                return minEval;
            }

        }

        public static double negamax(State s, int depth_negamax, double alpha, double beta)
        {
            if (depth_negamax == 0)
            {
                return s.value;
            }
            double score = alpha;
            for (int i = 0; i < s.possible_hexes.Count; i++)
            {
                State child_state = s.get_state_after_move(s, s.possible_hexes[i]);
                double value = -1 * negamax(child_state, depth_negamax - 1, -beta, -alpha);
                if (value > score)
                {
                    score = value;
                    if (s.move.row == GameState.game_state.move.row && s.move.column == GameState.game_state.move.column)
                    {
                        ai_move = child_state.move;
                        ai_state = child_state;
                    }
                    //ai_move = child_state.move;
                    //ai_state = child_state;
                }
                if (score > alpha)
                {
                    alpha = score;
                }
                if (score >= beta)
                {
                    break;
                }
            }
            return (score);
        }
    }
}
