using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using System.ComponentModel;
using static System.Formats.Asn1.AsnWriter;
using System.IO;
using System.Runtime.ConstrainedExecution;


namespace Den_förlorade_Juston
{
    public class Game1 : Game
    {
        Spelare spelare;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Kamera camera;
        RenderTarget2D mainTarget;
        Texture2D bakgrundSkog, bakgrund2, bakgrundBy, bakgrund4, bakgrundKyrka;
        SpriteFont deathTracker, duVan, namn, leaderboard;
        Vector2 deathTrackerPosition, deathTrackerVelocity;
        string Namn = "";
        string time = "";

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
            _graphics.PreferredBackBufferWidth = 1080;
            _graphics.PreferredBackBufferHeight = 1060;
            _graphics.ApplyChanges();

            mainTarget = new RenderTarget2D(GraphicsDevice, 1080, 1080);

            Data.viewPort = new Viewport();
            Data.viewPort = GraphicsDevice.Viewport;

            Data.Win = false;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Data.spelarBild = Content.Load<Texture2D>("playerSheet");
            Data.tileSet = Content.Load<Texture2D>("Tilemap");
            bakgrundSkog = Content.Load<Texture2D>("forestBackground");
            bakgrund2 = Content.Load<Texture2D>("secondBackground");
            bakgrundBy = Content.Load<Texture2D>("villageBackground");
            bakgrund4 = Content.Load<Texture2D>("fourthBackground");
            deathTracker = Content.Load<SpriteFont>("deathCount");
            duVan = Content.Load<SpriteFont>("klar");
            namn = Content.Load<SpriteFont>("namn");
            leaderboard = Content.Load<SpriteFont>("leaderboard");

            Data.level1 = new StationaryLevelObjekt(Data.tileSet, 290, 25, 64, 8);
            Data.level2 = new SpikeTilemap(Data.tileSet, 290, 25, 64, 7);
            Data.level3 = new Leveldecoration(Data.tileSet, 290, 24, 64, 7);
            Data.All.Add(new Spelare(Data.spelarBild, new Vector2(0f,  0f), new Vector2(17000, 1165)));
       
           

            Data.viewPort.Width = 300;
            Data.viewPort.Height = 1400;
            Data.camera = new Kamera(Data.viewPort);

            // TODO: use this.Content to load your game content here
        }

        KeyboardState prevInput = Keyboard.GetState();
        bool isWriting = true;

        protected override void Update(GameTime gameTime)
        {
            Vector2 temp = Data.All[0].postion;
            temp.Y = 1164;
            Data.camera.position = temp;
            Data.camera.UpdateCamera(Data.viewPort);

            if (Data.Win && isWriting)
            {
                KeyboardState currentInput = Keyboard.GetState();

                if (currentInput == prevInput)
                {
                    prevInput = currentInput;
                    base.Update(gameTime);
                    return;
                }

                foreach (Keys key in currentInput.GetPressedKeys())
                {
                    if (prevInput.IsKeyDown(key))
                    {
                        continue;
                    }

                    if (key.ToString() == "Space")
                    {
                        Namn += " ";
                    }
                    else if (key.ToString() == "Back")
                    {
                        Namn = Namn.Substring(0, Namn.Length - 1);
                    }
                    else if (key.ToString() == "Enter")
                    {
                        isWriting = false;
                    }
                    else
                    {
                        Namn += key.ToString();
                    }

                }

                prevInput = currentInput;
                base.Update(gameTime);
                return;
            }
            if (Data.Win)
            {
                Data.save.Add(new Ui(Namn, time, Data.deathCount));
            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            deathTrackerPosition = Data.All[0].postion;
         

            // TODO: Add your update logic here

            for (int i = 0; i < Data.All.Count; i++)
            {
                Data.All[i].Update(gameTime);
            }

            if (Data.All[0].postion.X >= 18000)
            {
                Data.Win = true;
                Data.All[0].postion = new Vector2(20000, 1100);
                
            }

            prevInput = Keyboard.GetState();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            GraphicsDevice.SetRenderTarget(mainTarget);

            // TODO: Add your drawing code here
            _spriteBatch.Begin(transformMatrix: Data.camera.transform, samplerState: SamplerState.PointClamp);
            //Data.backgrund.Draw(_spriteBatch);
            _spriteBatch.Draw(bakgrundSkog, new Vector2(0, 430), Color.White);
            _spriteBatch.Draw(bakgrundSkog, new Vector2(3840, 430), Color.White);
            _spriteBatch.Draw(bakgrund2, new Vector2(7680, 430), Color.White);
            _spriteBatch.Draw(bakgrundBy, new Vector2(8640, 430), Color.White);
            _spriteBatch.Draw(bakgrundBy, new Vector2(12480, 430), Color.White);
            _spriteBatch.Draw(bakgrund4, new Vector2(16320, 430), Color.White);

            

            Data.level1.Draw(_spriteBatch);
            Data.level2.Draw(_spriteBatch);
            Data.level3.Draw(_spriteBatch);
          
            if (!Data.Win)
            {
                time = gameTime.TotalGameTime.ToString();
            }
            string[] temp = time.Split('.');
            
            _spriteBatch.DrawString(deathTracker, "Deaths: " + Data.deathCount.ToString() + "\n" + "Time: " + temp[0], new Vector2(deathTrackerPosition.X + 362, 475), Color.White);
            _spriteBatch.DrawString(duVan, "Du Vann!!",new Vector2(20000, 475), Color.Yellow);
            _spriteBatch.DrawString(namn,"Namn: " + Namn, new Vector2(20000, 675), Color.White);
            _spriteBatch.DrawString(leaderboard,"Leaderboard", new Vector2(24000, 475), Color.White);

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
        private void SaveData()
        {
            TextWriter tw = new StreamWriter("leaderboard.txt");
            for (int i = 0; i < Data.save.Count; i++) 
            {

            }
            tw.Close();
        }
        private void LoadData()
        {
            string[] lines;
            if (File.Exists("leaderboard.txt"))
            {
                lines = File.ReadAllLines("leaderboard.txt");

                for (int i = 0; i < lines.Length; i++)
                {
                  
                }
            }
        }
    }
}
