using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andantino_Search
{
    public struct State
    {
        List<Hexagon> player1_hexes;
        List<Hexagon> player2_hexes;
        int evaluation_p1;
        int evaluation_p2;

        List<Hexagon> empty_hexes;
        //public List<Hexagon> possible_hexes;
        public List<Hexagon> possible_hexes { get; set; }


    }
}
