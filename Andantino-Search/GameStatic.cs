using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andantino_Search
{
    public struct GameStatic
    {
        public static List<Hexagon> all_hexes  { get; set; }
        public static List<Hexagon> hexes_in_board { get; set; }
        //public State grand_ancestor_state { get; set; }    //the very first state of every game
    }
}
