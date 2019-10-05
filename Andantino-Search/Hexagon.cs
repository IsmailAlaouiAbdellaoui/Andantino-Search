using System.Drawing;
using System.Collections.Generic;

namespace Andantino_Search
{
    public struct Hexagon
    {
        public int column { get; set; }
        public int row { get; set; }
        public PointF center { get; set; }
        public Hexagon(int col, int r, PointF cent)
        {
            column = col;
            row = r;
            center = cent;

        }

        //public int CompareTo(Hexagon hex)
        //{

        //}
    }
}
