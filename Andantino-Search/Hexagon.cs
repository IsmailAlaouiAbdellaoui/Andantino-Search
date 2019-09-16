using System.Drawing;

namespace Andantino_Search
{
    public class Hexagon
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
    }
}
