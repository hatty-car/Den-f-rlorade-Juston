using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Den_förlorade_Juston
{
    internal class Kamera: MovingObjekt
    {
        public Kamera(Texture2D texture,Vector2 Velocity, Vector2 Postion): base(texture, Velocity, Postion)
        {
            postion = Postion;
            image = texture;
            velocity = Velocity;
        }
    }
}
