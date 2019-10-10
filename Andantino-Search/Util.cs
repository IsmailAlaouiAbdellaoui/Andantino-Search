
using System.IO;

namespace Andantino_Search
{
    public static class Util
    {
        public static void log_minimax(string file_directory, string info)
        {

            using (StreamWriter file = new StreamWriter(file_directory, true))
            {
                file.WriteLine(info);
            }

        }
        public static string check_folder_file_directory()
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

    }
}
