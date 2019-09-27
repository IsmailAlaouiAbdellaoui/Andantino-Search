using System.Drawing;


namespace Andantino_Search
{
    public struct Option
    {
        public static float size = 20f;

        public static float first_center_x = 20f;
        public static float first_center_y = first_center_x + 30f;

        public static Brush color_player1 = Brushes.RoyalBlue;
        public static Pen color_possible_hexes_p1 = Pens.DarkBlue;


        public static Brush color_player2 = Brushes.Crimson;        
        public static Pen color_possible_hexes_p2 = Pens.Crimson;

        public static Pen color_board = Pens.LightGray;

        public static PointF center_coin_init = new PointF(10f, 10f);

        public static int row_init_coin = 9;
        public static int col_init_coin = 9;
        public static float radius_coins = 10f;
    }
}
