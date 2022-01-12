using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MGLT
{
    public static class MGLM
    {
        public static SpriteBatch GameSpriteBatch = null;
        public static List<MGLSprite> Sprites = new List<MGLSprite>();
        public static List<MGLSprite> SpriteAddQueue = new List<MGLSprite>();

        public static Random R = new Random();

        public static Game Game;

        public static KeyboardState NowState;
        public static KeyboardState PastState;

        public static bool UpdateCycleInProgress = false;

        public static Dictionary<string, Texture2D> Textures = new Dictionary<string, Texture2D>();

        public static Texture2D BlankTexture; 

        public static void Initialize(GraphicsDevice device)
        {
            BlankTexture = new Texture2D(device, 1, 1);
            Color[] data = new Color[1];
            data[0] = Color.White;
            BlankTexture.SetData(data);
        }

        public static long MS()
        {
            return DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        }

        public static void Add(MGLSprite spr)
        {
            if (spr.Texture != null)
            {
                //Console.WriteLine("Adding " + spr.Texture.Name);
            }
            if (UpdateCycleInProgress)
            {
                SpriteAddQueue.Add(spr);
                return;
            }
            Sprites.Add(spr);
        }

        public static bool Pressed(Keys key)
        {
            return NowState.IsKeyDown(key) && PastState.IsKeyUp(key);
        }

        public static bool Released(Keys key)
        {
            return NowState.IsKeyUp(key) && PastState.IsKeyDown(key);
        }

        public static bool HasTexture(string name)
        {
            return Textures.ContainsKey(name);
        }

        public static Texture2D Load(string name)
        {
            if (name == "")
            {
                return null;
            }

            if (HasTexture(name))
            {
                return Textures[name];
            }
            Texture2D newTexture = Game.Content.Load<Texture2D>(name);
            Textures.Add(name, newTexture);
            return newTexture;
        }

        public static string Random(this string[] list, params object[] args)
        {
            return list[R.Next(0, list.Length)];
        }
    }
}
