using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Den_förlorade_Juston
{
    internal class Spelare: MovingObjekt
    {   
        public bool alive, facing;
        Rectangle sourceRect = new Rectangle(0, 0, 32, 64);
        public Rectangle boundingBox = new Rectangle(0, 0, 0, 0);
        float frameTimer;
        int frame, deadCounter;
        bool isGrounded, isJumping;
        float gravity, speed;
        int jumpStrenght = 60;
        public PlatformController Controller;

        public Spelare(Texture2D Image,Vector2 Velocity, Vector2 Position): base(Image, Velocity, Position)
        {
            image = Image;
            postion = Position;
            velocity = Velocity;
          

            isGrounded = true;
            gravity = 0f;
            speed = 10;
            frameTimer = 0;
            alive = facing = true;
            frame = deadCounter = 0;

            boundingBox.Location = Position.ToPoint();
            boundingBox.Width = 192;
            boundingBox.Height = 64;

          

            Controller = new PlatformController();
            Controller.Initialize(boundingBox, 5, 3, 64);
            //Controller.SetCollisionMap(Data.level1.collisionMap);
        }
        public override void Update(GameTime gameTime)
        {
            Data.keyboard = Keyboard.GetState();
            if (Data.keyboard.IsKeyDown(Keys.D))
            {
                velocity.X = speed;
                facing = true;
            }
            else if (Data.keyboard.IsKeyDown(Keys.A))
            {
                velocity.X = -speed;
                facing = false;
            }
            else
            {
                velocity.X = 0;
            }
            if (isGrounded)
            {
                jumpStrenght = 60;
            }
            if (Data.keyboard.IsKeyDown(Keys.Space) && isJumping == false && jumpStrenght > 0)
            {
                gravity = 0;
                jumpStrenght -= 5;
                velocity.Y = -20;
            }
            else
            {
                gravity = 2;
                jumpStrenght = 0;
            }

            //velocity.Y += gravity;
            if (Controller.collisions.below == true)
            {
                isGrounded = true;
            }
            else
            {
                isGrounded = false;
            }
            postion += velocity;
            boundingBox.Location = postion.ToPoint();

            sourceRect.X = 32 * (frame % 6);
            sourceRect.Y = 64 * (frame / 6);

            frameTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (frameTimer > 0.15f)
            {
           
                    frameTimer = 0;
                    frame++;
                if (velocity.X != 0)
                {
                    if (frame > 5)
                    {
                        frame = 2;
                    }
                }
                else
                {
                    if (frame > 1)
                    {
                        frame = 0;
                    }
                }
                    
                
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (facing == true)
                spriteBatch.Draw(image, postion, sourceRect, Color.White, 0.0f, new Vector2(0, 0), 3f, SpriteEffects.None, 0f);
            else
                spriteBatch.Draw(image, postion, sourceRect, Color.White, 0.0f, new Vector2(0, 0), 3f, SpriteEffects.FlipHorizontally, 0f);
        }
    }
}
