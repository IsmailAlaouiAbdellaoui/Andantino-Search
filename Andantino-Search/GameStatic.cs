using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andantino_Search
{
    public ref struct GameStatic
    {
        public static List<Hexagon> all_hexes  { get; set; }//span
        public static List<Hexagon> hexes_in_board { get; set; }//span
        //public State grand_ancestor_state { get; set; }    //the very first state of every game
    }
}
