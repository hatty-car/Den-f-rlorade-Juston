using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Den_förlorade_Juston
{
    internal class Objekt
    {
        internal Vector2 postion;
        internal Texture2D image;

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(image, postion, Color.White);
        }
        public virtual void Update(GameTime gameTime)
        {
          
        }
        public Objekt(Texture2D texture, Vector2 Postion)
        {
            postion = Postion;
            image = texture;
           
        }
    }
}
