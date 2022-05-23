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

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();
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
