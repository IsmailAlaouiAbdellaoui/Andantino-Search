using System;
using System.IO;

namespace Andantino_Search
{
    public static class Util
    {
        public static void log_info(string file_directory, string info)
        {

            using (StreamWriter file = new StreamWriter(file_directory, true))
            {
                file.WriteLine(info);
            }

        }
        public static string check_folder_file_minimax_directory()
        {
            string current_directory = Directory.GetCurrentDirectory();
            string minimax_folder = "\\log_minimax";
            string complete_directory = current_directory + minimax_folder;
            string file_directory = complete_directory + "\\log_minimax.txt";
            if (!Directory.Exists(complete_directory))
            {
                Directory.CreateDirectory(complete_directory);
                if (!File.Exists(file_directory))
                {
                    File.Create(file_directory);
                }

            }
            return file_directory;
        }

        public static string check_folder_file_alphabeta_directory()
        {
            string current_directory = Directory.GetCurrentDirectory();
            string minimax_folder = "\\log_alphabeta";
            string complete_directory = current_directory + minimax_folder;
            string file_directory = complete_directory + "\\log_alphabeta.txt";
            if (!Directory.Exists(complete_directory))
            {
                Directory.CreateDirectory(complete_directory);
                if (!File.Exists(file_directory))
                {
                    File.Create(file_directory);
                }

            }
            return file_directory;
        }



        public static string check_folder_file_negamax_directory()
        {
            string current_directory = Directory.GetCurrentDirectory();
            string minimax_folder = "\\log_negamax";
            string complete_directory = current_directory + minimax_folder;
            string file_directory = complete_directory + "\\log_negamax.txt";
            if (!Directory.Exists(complete_directory))
            {
                Directory.CreateDirectory(complete_directory);
                if (!File.Exists(file_directory))
                {
                    File.Create(file_directory);
                }

            }
            return file_directory;
        }

        public static string check_folder_file_pvs_directory()
        {
            string current_directory = Directory.GetCurrentDirectory();
            string minimax_folder = "\\log_pvs";
            string complete_directory = current_directory + minimax_folder;
            string file_directory = complete_directory + "\\log_pvs.txt";
            if (!Directory.Exists(complete_directory))
            {
                Directory.CreateDirectory(complete_directory);
                if (!File.Exists(file_directory))
                {
                    File.Create(file_directory);
                }

            }
            return file_directory;
        }


        public static string check_folder_file_zobrist_directory()
        {
            string current_directory = Directory.GetCurrentDirectory();
            string minimax_folder = "\\log_zobrist";
            string complete_directory = current_directory + minimax_folder;
            string file_directory = complete_directory + "\\log_zobrist.txt";
            if (!Directory.Exists(complete_directory))
            {
                Directory.CreateDirectory(complete_directory);
                if (!File.Exists(file_directory))
                {
                    File.Create(file_directory);
                }

            }
            return file_directory;

        }
        public static ulong Get64BitRandom(ulong minValue, ulong maxValue,Random r)
        {
            //code taken from https://social.msdn.microsoft.com/Forums/vstudio/en-US/cb9c7f4d-5f1e-4900-87d8-013205f27587/64-bit-strong-random-function?forum=csharpgeneral
            // Get a random array of 8 bytes. 
            byte[] buffer = new byte[sizeof(ulong)];
            r.NextBytes(buffer);
            return BitConverter.ToUInt64(buffer, 0) % (maxValue - minValue + 1) + minValue;
        }

        public static string check_folder_file_state_directory()
        {
            string current_directory = Directory.GetCurrentDirectory();
            string minimax_folder = "\\saved_states";
            string complete_directory = current_directory + minimax_folder;
            string file_directory = complete_directory + "\\saved_states.txt";
            if (!Directory.Exists(complete_directory))
            {
                Directory.CreateDirectory(complete_directory);
                if (!File.Exists(file_directory))
                {
                    File.Create(file_directory);
                }

            }
            return file_directory;

        }
        public static void save_state(string file_directory,State s)
        {
            //Hexagon move, int player, List< Hexagon > p1_hexes, List< Hexagon > p2_hexes, int depth,
            //    List< Hexagon > empty_hexes, List<Hexagon> possible_hexes, bool is_over, double value
            int col = s.move.column;
            int row = s.move.row;
            float x_move = s.move.center.X;
            float y_move = s.move.center.Y;
            int player = s.player;
            //loop for p1 hexes
            //loop for p2 hexes
            int depth = s.depth;
            //loop for empty hexes
            //loop for possible hexes
            bool is_over = s.is_game_over;
            double score = s.value;
            //using (StreamWriter file = new StreamWriter(file_directory, true))
            //{
            //    file.WriteLine(info);
            //}



        }
        
        public static State load_state(string file_directory,string file_name)
        {
            State saved_state = new State();
            return saved_state;
        }


    }
}
