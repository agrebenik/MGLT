using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;


namespace MGLT
{
    public class MGLG : Game
    {
        GraphicsDeviceManager GameGraphics;
        SpriteBatch GameSpriteBatch;

        public MGLG(int w = 1920, int h = 1080)
        {
            GameGraphics = new GraphicsDeviceManager(this);
            GameGraphics.PreferredBackBufferWidth = w;
            GameGraphics.PreferredBackBufferHeight = h;
            GameGraphics.IsFullScreen = true;
            IsMouseVisible = true;

            Content.RootDirectory = "Content";
            MGLM.Game = this;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            MGLM.GameSpriteBatch = new SpriteBatch(GraphicsDevice);
            GameSpriteBatch = MGLM.GameSpriteBatch;
            Window.IsBorderless = true;
        }

        protected override void UnloadContent() { }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            MGLM.NowState = Keyboard.GetState();

            if (MGLM.Released(Keys.P))
            {
                Window.IsBorderless = !Window.IsBorderless;
            }

            List<MGLSprite> toDelete = new List<MGLSprite>();

            foreach (MGLSprite sprite in MGLM.SpriteAddQueue)
            {
                MGLM.Sprites.Add(sprite);
            }

            if (MGLM.SpriteAddQueue.Count > 0)
            {
                MGLM.SpriteAddQueue = new List<MGLSprite>();
            }

            MGLM.UpdateCycleInProgress = true;
            foreach (MGLSprite sprite in MGLM.Sprites)
            {
                if (sprite.QueuedForDeletion())
                {
                    toDelete.Add(sprite);
                    continue;
                }
                sprite.Update(gameTime);
            }
            MGLM.UpdateCycleInProgress = false;

            foreach (MGLSprite sprite in toDelete)
            {
                MGLM.Sprites.Remove(sprite);
            }

            base.Update(gameTime);

            MGLM.PastState = MGLM.NowState;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            GameSpriteBatch.Begin();
            foreach (MGLSprite sprite in MGLM.Sprites)
            {
                sprite.Draw(gameTime);
            }
            GameSpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
