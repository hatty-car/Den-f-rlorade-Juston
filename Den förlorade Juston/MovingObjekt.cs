using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Den_förlorade_Juston
{
    internal class MovingObjekt: Objekt
    {
        internal Vector2 velocity;
        public MovingObjekt(Texture2D texture, Vector2 Velocity, Vector2 Postion): base(texture, Postion)
        {
            postion = Postion;
            image = texture;
            velocity = Velocity;
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
