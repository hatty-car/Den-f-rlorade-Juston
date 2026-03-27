using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Den_förlorade_Juston
{
    internal class MovingLevelObjekt:LevelObjekt
    {
        internal Vector2 velocity;
        public MovingLevelObjekt(Texture2D texture, Vector2 Velocity, Vector2 Postion, Rectangle Hitbox): base (texture, Postion)
        {
            postion = Postion;
            image = texture;
            hitBox = Hitbox;
            velocity = Velocity;
        }
    }
}
