using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Mono
{

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _tex;
        private Rectangle _transform;
        private Rectangle _transform1;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // 1111 1111
            // 255 = ff
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _tex = new Texture2D(_graphics.GraphicsDevice, 1, 1);
            uint[] data = new uint[] { 0xffffffff };
            _tex.SetData(data);
            _transform = new Rectangle(0, 100, 50, 200);
            _transform1 = new Rectangle(750, 100, 50, 200);
            // TODO: use this.Content to load your game content 
            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (_transform.Y <= 0)
            {
                _transform.Y = 200;
            }
            else if (_transform1.Y <= 0)
            {
                _transform1.Y = 200;
            }
            else if (_transform.Y >= Window.ClientBounds.Height - 200)
            {
                _transform.Y = 200;
            }
            else if (_transform1.Y >= Window.ClientBounds.Height - 200)
            {
                _transform1.Y = 200;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.W))
                _transform.Y -= 10;
            else if (Keyboard.GetState().IsKeyDown(Keys.S))
                _transform.Y += 10;
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                _transform1.Y += 10;
            else if (Keyboard.GetState().IsKeyDown(Keys.Up))
                _transform1.Y -= 10;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();
            _spriteBatch.Draw(_tex, _transform, Color.White);
            _spriteBatch.Draw(_tex, _transform1, Color.White);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
