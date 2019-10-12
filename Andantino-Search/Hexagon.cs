using System.Drawing;
using System;

namespace Andantino_Search
{
    public struct Hexagon : IComparable<Hexagon>
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

        public int CompareTo(Hexagon other)
        {
            if(other.column == column && other.row == row)
            {
                return 0;
            }
            if(other.row < row)
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }
    }
}
