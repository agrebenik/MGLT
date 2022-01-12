using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MGLT
{
    static class MGLDebug
    {
        public static void DrawLine(Vector2 point1, Vector2 point2, Color color, int lineWidth = 1)
        {

            float angle = (float)Math.Atan2(point2.Y - point1.Y, point2.X - point1.X);
            float length = Vector2.Distance(point1, point2);

            MGLM.GameSpriteBatch.Draw(MGLM.BlankTexture, point1, null, color,
                angle, Vector2.Zero, new Vector2(length, lineWidth),
                SpriteEffects.None, 0f);
        }
    }
}
