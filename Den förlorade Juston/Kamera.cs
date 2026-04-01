using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Den_förlorade_Juston
{
    internal class Kamera: MovingObjekt
    {
        public Kamera(Texture2D texture,Vector2 Velocity, Vector2 Postion): base(texture, Velocity, Postion)
        {
            this.postion = Postion;
            image = texture;
            velocity = Velocity;
        }

        public void Follow(Rectangle target, Vector2 screenSize)
        {
            postion = new Vector2(-target.X + (screenSize.X / 2 - target.Width / 2),
                -target.Y + (screenSize.Y / 2 - target.Height / 2));
        }
    }
}
