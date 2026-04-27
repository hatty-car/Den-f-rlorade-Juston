using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using System.ComponentModel;


namespace Den_förlorade_Juston
{
    public class Game1 : Game
    {
        Spelare spelare;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Kamera camera;
        StationaryLevelObjekt level1;
        RenderTarget2D mainTarget;
        Texture2D bakgrundSkog;
        Texture2D bakgrund2;

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

            mainTarget = new RenderTarget2D(GraphicsDevice, 1920, 1080);

            Data.viewPort = new Viewport();
            Data.viewPort = GraphicsDevice.Viewport;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Data.spelarBild = Content.Load<Texture2D>("playerSheet");
            Data.tileSet = Content.Load<Texture2D>("Tilemap");
            bakgrundSkog = Content.Load<Texture2D>("forestBackground");
            bakgrund2 = Content.Load<Texture2D>("secondBackground");

            Data.level1 = new StationaryLevelObjekt(Data.tileSet, 145, 25, 64, 7);
            Data.level2 = new SpikeTilemap(Data.tileSet, 145, 24, 64, 7);
            Data.level3 = new Leveldecoration(Data.tileSet, 145, 24, 64, 7);
            Data.All.Add(new Spelare(Data.spelarBild, new Vector2(0f,  0f), new Vector2(200, 1165)));
       
           

            Data.viewPort.Width = 300;
            Data.viewPort.Height = 1400;
            Data.camera = new Kamera(Data.viewPort);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            Vector2 temp = Data.All[0].postion;
            temp.Y = 1164;
            Data.camera.position = temp;
            Data.camera.UpdateCamera(Data.viewPort);
         

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
            GraphicsDevice.SetRenderTarget(mainTarget);

            // TODO: Add your drawing code here
            _spriteBatch.Begin(transformMatrix: Data.camera.transform, samplerState: SamplerState.PointClamp);
            //Data.backgrund.Draw(_spriteBatch);
            _spriteBatch.Draw(bakgrundSkog, new Vector2(0, 400), Color.White);
            _spriteBatch.Draw(bakgrundSkog, new Vector2(3840, 400), Color.White);
            _spriteBatch.Draw(bakgrund2, new Vector2(7680, 400), Color.White);
            Data.level1.Draw(_spriteBatch);
            Data.level2.Draw(_spriteBatch);
            Data.level3.Draw(_spriteBatch);
            for (int i = 0; i < Data.All.Count; i++)
            {
                Data.All[i].Draw(_spriteBatch);
            }
            _spriteBatch.End();
            GraphicsDevice.SetRenderTarget(null);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            _spriteBatch.Draw(mainTarget, new Vector2(0, 0), null, Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0f);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
