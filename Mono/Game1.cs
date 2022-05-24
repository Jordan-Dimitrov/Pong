using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

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
        private int speedX, speedY;
        private Random rnd = new Random();
        private int _player1Score, _player2Score;
        private SpriteFont _font;

        private Rectangle _score1Rect, _score2Rect;
        private bool _gameWon = false;
        private const int _MaxScore = 10;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
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
            Ball = new Rectangle(Window.ClientBounds.Width / 2, Window.ClientBounds.Height / 2, 20, 20);
            int randX = rnd.Next(0, 2);
            speedX = randX == 0 ? -3 : 3; speedY = 0;
            _font = Content.Load<SpriteFont>("Roboto");
            
            _score1Rect = new Rectangle(0, 2, 0, 0);
            _score2Rect = new Rectangle(0, 2, 0, 0);

            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if(_gameWon) 
            {
                if(Keyboard.GetState().IsKeyDown(Keys.R)) 
                {
                    _gameWon = false;
                    _player1Score = 0;
                    _player2Score = 0;
                }

                return;                
            }

            _score1Rect.Size = _font.MeasureString(_player1Score.ToString()).ToPoint();
            _score1Rect.X = _transform.X + _score1Rect.Width;

            if (_transform.Y <= _score1Rect.Bottom + 2)
                _transform.Y = _score1Rect.Bottom + 2;
            else if (_transform.Y >= Window.ClientBounds.Height - _transform.Height)
                _transform.Y = Window.ClientBounds.Height - _transform.Height;

            _score2Rect.Size = _font.MeasureString(_player2Score.ToString()).ToPoint();
            _score2Rect.X = _transform1.X - _score2Rect.Width;

            if (_transform1.Y <= _score2Rect.Bottom + 2)
                _transform1.Y = _score2Rect.Bottom + 2;
            else if (_transform1.Y >= Window.ClientBounds.Height - _transform.Height)
                _transform1.Y = Window.ClientBounds.Height - _transform.Height;

            if (Keyboard.GetState().IsKeyDown(Keys.W))
                _transform.Y -= 10;
            else if (Keyboard.GetState().IsKeyDown(Keys.S))
                _transform.Y += 10;
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                _transform1.Y += 10;
            else if (Keyboard.GetState().IsKeyDown(Keys.Up))
                _transform1.Y -= 10;

            BallDrawingMovementAndExit(_transform.X, _transform.Y, _transform1.X, _transform1.Y, 50, 200);

            if(_player1Score >= _MaxScore || _player2Score >= _MaxScore)
                _gameWon = true;

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();

            if(!_gameWon) 
            {
                _spriteBatch.Draw(_tex, _transform, Color.White);
                _spriteBatch.Draw(_tex, _transform1, Color.White);
                _spriteBatch.Draw(_tex, Ball, Color.White);
                
                _spriteBatch.DrawString(_font, _player1Score.ToString(), _score1Rect.Location.ToVector2(), Color.White);
                _spriteBatch.DrawString(_font, _player2Score.ToString(), _score2Rect.Location.ToVector2(), Color.White);
            }
            else
            {
                int playerNum = _player1Score >= _MaxScore ? 1 : 2;
                Vector2 textSize = _font.MeasureString($"Player {playerNum} has won!");
                Vector2 textPos = new Vector2(Window.ClientBounds.Width / 2 - textSize.X / 2, Window.ClientBounds.Height / 2 - textSize.Y / 2);
                _spriteBatch.DrawString(_font, $"Player {playerNum} has won!\nPress R to restart!", textPos, Color.White);                
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }

       public void BallDrawingMovementAndExit (int xOfPaddle1, int yOfPaddle1, int xOfPaddle2, int yOfPaddle2, int width, int height)
        {
            bool hasBallHitWall = false;
            
            if (Ball.X <= xOfPaddle1 + width && Ball.Y >= yOfPaddle1 && Ball.Y <= yOfPaddle1 + height)
            {
                Ball.X = xOfPaddle1 + width +  5;
                speedX = 2;
                int randY = rnd.Next(0, 2);
                if (randY == 0)
                    speedY = -3;
                else
                    speedY = 3;
            }
            if (Ball.X + Ball.Width >= xOfPaddle2 && Ball.Y >= yOfPaddle2 && Ball.Y <= yOfPaddle2 + height)
            {
                Ball.X = xOfPaddle2 - 25;
                speedX = -3;
                int randY = rnd.Next(0, 2);
                if (randY == 0)
                    speedY = -3;
                else
                    speedY = 3;
            }

            if (Ball.Y <= _score1Rect.Bottom)
                speedY = 3;

            if (Ball.Y + Ball.Width >= Window.ClientBounds.Height)
                speedY = -3;

            if (Ball.X <= 0) {
                hasBallHitWall = true;
                _player2Score++;
            } 
            else if (Ball.X >= Window.ClientBounds.Width - Ball.Height)
            {
                hasBallHitWall = true;
                _player1Score++;
            }

            if(hasBallHitWall)
            {
                Ball.X = Window.ClientBounds.Width / 2;
                Ball.Y = Window.ClientBounds.Height / 2;

                int randX = rnd.Next(0, 2);
                if (randX == 0)
                    speedX = -3;
                else
                    speedX = 3;

                int randY = rnd.Next(0, 2);
                speedY = randY == 0 ? -3 : 3;                
            }
                
            Ball.X += speedX;
            Ball.Y += speedY;

        }
    }
}
