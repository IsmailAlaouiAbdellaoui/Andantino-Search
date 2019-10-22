

using System;
using System.Collections.Generic;

namespace Andantino_Search
{
    public class Zobrist
    {
        public static Dictionary<int, ulong> zobrist_dict { get; set; }

        public static Random random_seed = new Random();

        public static void generate_zobrist_table(bool do_log)
        {
            Zobrist.zobrist_dict = new Dictionary<int, ulong>();
            string dir = Util.check_folder_file_zobrist_directory();
            for (int i = 0; i < 542; i++)
            {
                Zobrist.zobrist_dict.Add(i, Util.Get64BitRandom(Option.minimum_random_value_zobrist, ulong.MaxValue, random_seed));
                if(do_log)
                {
                    Util.log_info(dir, Zobrist.zobrist_dict[i].ToString());
                }
                
            }
        }

    }
}
