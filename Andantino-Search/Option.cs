using System.Collections.Generic;
using System.Drawing;


namespace Andantino_Search
{
    public struct Option
    {
        public static float size = 20f;

        public static float first_center_x = 20f;
        public static float first_center_y = first_center_x + 30f;

        public static int hexes_board_row_start = 0;
        public static int hexes_board_col_start = 5;
        public static int hexes_board_number_hexes_row = 10;
        public static int hexes_board_size = 19;

        public static Brush color_player1 = Brushes.RoyalBlue;
        public static Pen color_possible_hexes_p1 = Pens.DarkBlue;


        public static Brush color_player2 = Brushes.Crimson;        
        public static Pen color_possible_hexes_p2 = Pens.Crimson;

        public static Pen color_board = Pens.LightGray;

        public static PointF center_coin_init = new PointF(10f, 10f);

        public static int row_init_coin = 9;
        public static int col_init_coin = 9;
        public static float radius_coins = 10f;

        public static int number_coins_required = 5;

        public static Font font_coordinates = new Font("Microsoft Sans Serif", 8.25f);

        public static List<Hexagon> hexes_outer_board = new List<Hexagon>();

        public static int depth_of_search = 5;

        public static double minimum_score = 3;

        public static Font font_counter = new Font("Microsoft Sans Serif", 25f);

        public static Font font_time_limit = new Font("Microsoft Sans Serif", 18f);
        public static Color color_time_limit = Color.Crimson;
        public static int time_limit_minutes_tournament = 10;

        public static ulong minimum_random_value_zobrist = 1; //if 0 then exception

        public static int player1_type;
        public static int player2_type;
    }
}
