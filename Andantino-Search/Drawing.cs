using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;


namespace Andantino_Search
{
    public struct Drawing
    {
        public static void draw_hexes_board(Graphics g, List<Hexagon> hexes_board)
        {
            for (int i = 0; i < hexes_board.Count; i++)
            {
                PointF[] hexes_points = new PointF[6];
                for (int j = 0; j < 6; j++)
                {
                    hexes_points[j] = Pointy_hex_corner(hexes_board[i].center, Option.size, j);
                }
                g.DrawPolygon(Option.color_board, hexes_points);
                draw_coordinates_board(g, hexes_board[i]);

            }
        }

        public static void draw_player1_hexes(Graphics g, List<Hexagon> player1_hexes)
        {
            if (player1_hexes.Count > 0)
            {
                for (int i = 0; i < player1_hexes.Count; i++)
                {
                    PointF center_hex = player1_hexes[i].center;
                    g.FillEllipse(Option.color_player1, center_hex.X - Option.radius_coins, center_hex.Y - Option.radius_coins, 2 * Option.radius_coins, 2 * Option.radius_coins);

                }
            }
        }

        public static void draw_player2_hexes(Graphics g, List<Hexagon> player2_hexes)
        {
            if (player2_hexes.Count > 0)
            {
                for (int i = 0; i < player2_hexes.Count; i++)
                {
                    PointF center_hex = player2_hexes[i].center;
                    g.FillEllipse(Option.color_player2, center_hex.X - Option.radius_coins, center_hex.Y - Option.radius_coins, 2 * Option.radius_coins, 2 * Option.radius_coins);

                }
            }

        }

        public static void draw_possible_hexes(Graphics g, List<Hexagon> possible_hexes, bool isplayer1_turn)
        {
            if (possible_hexes.Count > 0)
            {
                for (int i = 0; i < possible_hexes.Count; i++)
                {
                    PointF[] hex_points = new PointF[6];
                    for (int j = 0; j < 6; j++)
                    {
                        hex_points[j] = Pointy_hex_corner(possible_hexes[i].center, Option.size, j);

                    }
                    if (isplayer1_turn)
                    {
                        g.DrawPolygon(Option.color_possible_hexes_p1, hex_points);
                    }
                    else
                    {
                        g.DrawPolygon(Option.color_possible_hexes_p2, hex_points);
                    }
                }
            }
        }

        public static void draw_all_hexes(List<PointF> hexes_centers, Graphics g, List<Hexagon> all_hexes)
        {
            for (int i = 0; i < hexes_centers.Count; i++)
            {
                if (hexes_centers[i].X != 0f && hexes_centers[i].Y != 0f)
                {
                    PointF[] hexes_points = new PointF[6];
                    for (int j = 0; j < 6; j++)
                    {
                        hexes_points[j] = Pointy_hex_corner(hexes_centers[i], Option.size, j);
                    }
                    g.DrawPolygon(Pens.Black, hexes_points);
                    using (StringFormat sf = new StringFormat())
                    {
                        sf.Alignment = StringAlignment.Center;
                        sf.LineAlignment = StringAlignment.Center;
                        float x = hexes_centers[i].X;
                        float y = hexes_centers[i].Y;
                        string label = "(" + all_hexes[i].row.ToString() + ", " +
                            all_hexes[i].column.ToString() + ")";
                        g.DrawString(label, Option.font_coordinates,
                            Brushes.Black, x, y, sf);
                    }
                }
            }

        }

        public static void draw_hexes_outer_border(Graphics g, List<Hexagon> hexes_outer_board)
        {
            for (int k = 0; k < hexes_outer_board.Count; k++)
            {
                PointF[] hexes_points = new PointF[6];
                for (int l = 0; l < 6; l++)
                {
                    hexes_points[l] = Pointy_hex_corner(hexes_outer_board[k].center, Option.size, l);
                }
                g.DrawPolygon(Pens.Purple, hexes_points);
                g.FillPolygon(Brushes.LightBlue, hexes_points);
            }
        }

        public static void draw_coordinates_board(Graphics g, Hexagon hex)
        {
            using (StringFormat sf = new StringFormat())
            {
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;
                float x = hex.center.X;
                float y = hex.center.Y;
                string label = "(" + hex.row.ToString() + ", " +
                    hex.column.ToString() + ")";
                g.DrawString(label, Option.font_coordinates,
                    Brushes.Black, x, y, sf);
            }
        }

        public static PointF Pointy_hex_corner(PointF center, float size, int i)
        {
            int angle_deg = (60 * i) - 30;
            double angle_rad = Math.PI / 180 * angle_deg;
            return new PointF(center.X + (size * (float)Math.Cos(angle_rad)), center.Y + (size * (float)Math.Sin(angle_rad)));
        }

    }
}
