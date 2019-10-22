using System;
using System.Collections.Generic;

namespace Andantino_Search
{
    public ref struct GameStatic
    {
        public static List<Hexagon> all_hexes  { get; set; }//span

        //public Span<Hexagon> hexes_board_span { get; set; }
        public static List<Hexagon> hexes_in_board { get; set; }//span

        //public static Memory<Hexagon> hexes_board_memory { get; set; }

        public static Dictionary<Tuple<int, int>, Hexagon> hexes_board_dict { get; set; }//row,col


        //public State grand_ancestor_state { get; set; }    //the very first state of every game

    }
}
