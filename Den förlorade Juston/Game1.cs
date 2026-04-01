 using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;



namespace Den_förlorade_Juston
{
    public class Game1 : Game
    {
        Spelare spelare;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Kamera camera;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Data.spelarBild = Content.Load<Texture2D>("playerSheet");

            Data.All.Add(new Spelare(Data.spelarBild, new Vector2(0f,  0f), new Vector2(100, 500)));

            Data.viewPort.Width = 1920;
            Data.viewPort.Height = 1080;
            Data.camera = new Kamera(Data.camera.postion, Data.viewPort);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Data.camera.postion = Data.player.postion;
            Data.camera.MoveCamera(Data.viewPort);
            //camera.Follow(Data.player.postion, new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight));

            // TODO: Add your update logic here

            for (int i = 0; i < Data.All.Count; i++)
            {
                Data.All[i].Update(gameTime);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            //Data.backgrund.Draw(_spriteBatch);
            for (int i = 0; i < Data.All.Count; i++)
            {
                Data.All[i].Draw(_spriteBatch);
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
