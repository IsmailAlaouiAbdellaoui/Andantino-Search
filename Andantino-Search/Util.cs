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
        private ulong Get64BitRandom(ulong minValue, ulong maxValue)
        {
            //code taken from https://social.msdn.microsoft.com/Forums/vstudio/en-US/cb9c7f4d-5f1e-4900-87d8-013205f27587/64-bit-strong-random-function?forum=csharpgeneral
            // Get a random array of 8 bytes. 
            byte[] buffer = new byte[sizeof(ulong)];
            r.NextBytes(buffer);
            return BitConverter.ToUInt64(buffer, 0) % (maxValue - minValue + 1) + minValue;
        }


    }
}
