using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MGLT
{
    public class MGLLine : MGLSprite
    {
        public MGLPoint Point1;
        public MGLPoint Point2;
        public Color Color;
        public int Width;
        public MGLLine(MGLPoint point1, MGLPoint point2, Color color, int width = 1) : base("")
        {
            Point1 = point1;
            Point2 = point2;
            Color = color;
            Width = width;
        }

        public override void Draw(GameTime time)
        {
            MGLDebug.DrawLine(Point1, Point2, Color, Width);
        }
    }
}
