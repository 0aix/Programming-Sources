using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace TaylorMacLaurinSeries
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D texture;
        float width;
        float height;
        float o_X;
        float o_Y;
        float scale = 40f;
        float[] function;
        int time = 0;
        float n = 1;
        float d = 1;
        int limit = 800;
        bool run = false;
        float[] sin;
        float[] cos;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            width = GraphicsDevice.Viewport.Width;
            height = GraphicsDevice.Viewport.Height;
            o_X = width / 2f;
            o_Y = height / 2f;
            function = new float[800];
            sin = new float[800];
            cos = new float[800];
            for (int i = 0; i < 800; i++)
            {
                sin[i] = (float)Math.Sin(X(i));
                cos[i] = (float)Math.Cos(X(i));
            }
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            texture = new Texture2D(GraphicsDevice, 1, 1);
            texture.SetData<Color>(new Color[] { Color.White });
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
                run = true;
            if (run && n < 40)
                time += gameTime.ElapsedGameTime.Milliseconds;
            else if (run)
            {
                function = new float[800];
                n = 0;
                d = 1;
                time = 0;
                run = false;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            DrawLine(new Vector2(0, o_Y), new Vector2(width, o_Y));
            DrawLine(new Vector2(o_X + 2, 0), new Vector2(o_X + 2, height));
            if (run)
            {
                for (int i = 0; i < width - 1; i++)
                {
                    if (n % 2 == 1)
                    {
                        DrawLineX(new Vector2(i, Y(sin[i])), new Vector2(i + 1, Y(sin[i + 1])));
                    }
                    else
                    {
                        DrawLineX(new Vector2(i, Y(cos[i])), new Vector2(i + 1, Y(cos[i + 1])));
                    }
                }
            }
            if (time < limit)
            {
                for (int i = 0; i < width - 1; i++)
                {
                    DrawLine(new Vector2(i, Y(function[i] + ApproxSin(X(i)) * (float)time / (float)limit)), new Vector2(i + 1, Y(function[i + 1] + ApproxSin(X(i + 1)) * (float)time / (float)limit)));
                }
            }
            else
            {
                for (int i = 0; i < width; i++)
                {
                    function[i] += ApproxSin(X(i));
                }
                for (int i = 0; i < width - 1; i++)
                {
                    DrawLine(new Vector2(i, Y(function[i])), new Vector2(i + 1, Y(function[i + 1])));
                }
                n += 2;
                d *= -1 * n * (n - 1);
                time = 0;
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        void DrawLine(Vector2 A, Vector2 B)
        {
            Vector2 C = B - A;
            spriteBatch.Draw(texture, new Rectangle((int)A.X, (int)A.Y, (int)C.Length() + 3, 4), null, Color.Black, (float)Math.Atan2(C.Y, C.X), new Vector2(0, 0), SpriteEffects.None, 0);
        }

        void DrawLineX(Vector2 A, Vector2 B)
        {
            Vector2 C = B - A;
            spriteBatch.Draw(texture, new Rectangle((int)A.X, (int)A.Y, (int)C.Length() + 3, 4), null, Color.White, (float)Math.Atan2(C.Y, C.X), new Vector2(0, 0), SpriteEffects.None, 0);
        }

        float X(float x)
        {
            return (x - o_X) / scale;
        }

        float Y(float y)
        {
            return y * -1 * scale + o_Y;
        }

        float ApproxSin(float x)
        {
            return (float)Math.Pow(x, n) / d;
        }
    }
}
