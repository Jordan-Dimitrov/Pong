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
        private Rectangle Ball;
        private bool IsLost = false;

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
            _spriteBatch.Draw(_tex, Ball, Color.White);
            _spriteBatch.End();
            base.Draw(gameTime);
        }

       public void BallDrawingMovementAndExit (int xOfPaddle1, int yOfPaddle1, int xOfPaddle2, int yOfPaddle2, int width, int height)
        {
            IsLost = false;
            Ball = new Rectangle(Window.ClientBounds.X / 2, Window.ClientBounds.Y / 2, 20, 20);
            int speedX = 1,  speedY = 1;
            Ball.X += speedX;
            Ball.X += speedY;

            if (Ball.X <= xOfPaddle1 && Ball.Y >= yOfPaddle1 && Ball.Y <= yOfPaddle1 + height)
            {
                speedX = 1;
            }
            if (Ball.X >= xOfPaddle2 && Ball.Y >= yOfPaddle2 && Ball.Y <= yOfPaddle2 + height)
            {
                speedX = -1;
            }

            if (Ball.Y <= 0)
            {
                speedY = -1;
            }
            if (Ball.Y >= Window.ClientBounds.Y + height)
            {
                speedY = -1;
            }

            if (Ball.X <= 0 || Ball.X >= Window.ClientBounds.X + width)
            {
                IsLost = true;
                
            }
            
        }
    }
}
