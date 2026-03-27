using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Den_förlorade_Juston
{
    internal class StationaryLevelObjekt:LevelObjekt
    {
        public StationaryLevelObjekt(Texture2D texture, Vector2 Postion, Rectangle Hitbox) : base(texture, Postion)
        {
            postion = Postion;
            image = texture;
            hitBox = Hitbox;
        }
    }
}
