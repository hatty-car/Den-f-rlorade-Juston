using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Den_förlorade_Juston
{
    internal class Backgrund: StationaryObjekt
    {
        public Backgrund(Texture2D texture, Vector2 Postion) : base(texture, Postion)
        {
            postion = Postion;
            image = texture;
            
        }
    }
}
