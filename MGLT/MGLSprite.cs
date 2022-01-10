using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGLT
{
    public class MGLSprite
    {
        public float X;
        public float Y;
        public Texture2D Texture { get; set; }
        public bool Render;

        private bool _delete;
        public MGLSprite(MGLPoint position, Texture2D texture)
        {
            X = position.X;
            Y = position.Y;
            Texture = texture;
            Render = true;
            _delete = false;
        }

        public bool QueuedForDeletion()
        {
            return _delete;
        }

        public void QueueForDeletion()
        {
            _delete = true;
        }

        public MGLSprite(MGLPoint position, string texture) : this(position, MGLM.Load(texture)) { }

        public MGLSprite(string texture) : this((0, 0), MGLM.Load(texture)) { }

        public virtual void Draw(GameTime time)
        {
            if (Texture == null || !Render || _delete)
            {
                return;
            }
            MGLM.GameSpriteBatch.Draw(Texture, new MGLPoint(X, Y), Color.White);
        }

        public virtual void Update(GameTime time) { }
    }
}
