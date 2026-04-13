using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Den_förlorade_Juston
{
    internal class Kamera: MovingObjekt
    {

        public float zoom;
        public Vector2 position;
        public Rectangle bounds;
        public Rectangle visibleArea;
        public Matrix transform;
        public Kamera(Texture2D texture, Vector2 Velocity, Vector2 Postion, Viewport viewPort): base(texture, Velocity, Postion)
        {
            postion = Vector2.Zero;
        }

        private void UpdateVisibleArea()
        {
            var inverseViewMatrix = Matrix.Invert(transform);

            var tl = Vector2.Transform(Vector2.Zero, inverseViewMatrix);
            var tr = Vector2.Transform(new Vector2(bounds.X, 0), inverseViewMatrix);
            var bl = Vector2.Transform(new Vector2(0, bounds.Y), inverseViewMatrix);
            var br = Vector2.Transform(new Vector2(bounds.Width, bounds.Height), inverseViewMatrix);

            var min = new Vector2(
                MathHelper.Min(tl.X, MathHelper.Min(tr.X, MathHelper.Min(bl.X, br.X))),
                MathHelper.Min(tl.Y, MathHelper.Min(tr.Y, MathHelper.Min(bl.Y, br.Y))));
            var max = new Vector2(
                MathHelper.Max(tl.X, MathHelper.Max(tr.X, MathHelper.Max(bl.X, br.X))),
                MathHelper.Max(tl.Y, MathHelper.Max(tr.Y, MathHelper.Max(bl.Y, br.Y))));
            visibleArea = new Rectangle((int)min.X, (int)min.Y, (int)(max.X - min.X), (int)(max.Y - min.Y));
        }

        private void UpdateMatrix()
        {
            transform = Matrix.CreateTranslation(new Vector3(-position.X, -position.Y, 0)) *
                    Matrix.CreateScale(zoom) *
                    Matrix.CreateTranslation(new Vector3(bounds.Width * 0.5f, bounds.Height * 0.5f, 0));
            UpdateVisibleArea();
        }

        public void MoveCamera(Vector2 movePosition)
        {
            Vector2 newPosition = postion + movePosition;
            postion = newPosition;
        }

        public void UpdateCamera(Viewport bounds)
        {
            this.bounds = bounds.Bounds;
            UpdateMatrix();
        }

        public void Follow(Rectangle target, Vector2 screenSize)
        {
            //postion = new Vector2(-target.X + (screenSize.X / 2 - target.Width / 2),
              //  -target.Y + (screenSize.Y / 2 - target.Height / 2));
        }
    }
}
