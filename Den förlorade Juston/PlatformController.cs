using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Den_förlorade_Juston
{
    internal class PlatformController
    {

        int horizontalRayCount;
        int verticalRayCount;
        int tileSize;

        int boundaryLeft;
        int boundaryRight;
        int boundaryUp;
        int boundaryDown;

        int horizontalRaySpacing;
        int verticalRaySpacing;

        bool[,] collisionMap;
        public CollisionInfo collisions;
        public RaycastOrigins raycastOrigins;

        public PlatformController()
        {

        }

        public void Initialize(Rectangle boundingBox, int numberOfHorizontalRays, int numberOfVerticalRays, int tileSize)
        {
            horizontalRayCount = numberOfHorizontalRays;
            verticalRayCount = numberOfVerticalRays;
            this.tileSize = tileSize;

            CalculateRaycastOrigins(boundingBox);
        }

        public void SetCollisionMap(bool[,] collisionMap)
        {
            this.collisionMap = collisionMap;

            boundaryLeft = 0;
            boundaryRight = collisionMap.GetLength(1) * tileSize - 1;
            boundaryUp = 0;
            boundaryDown = collisionMap.GetLength(0) * tileSize - 1;
        }

        public Vector2 CalculateVelocity(Vector2 velocity, Rectangle boundingBox)
        {
            UpdateRaycastOrigins(boundingBox);
            collisions.Reset();

            if (velocity.X != 0)
                velocity = HorizontalCollisions(velocity);

            Vector2 velocityOffset = new Vector2(velocity.X, 0);

            if (velocity.Y != 0)
                velocity = VerticalCollisions(velocity, velocityOffset);

            return velocity;
        }

        public Vector2 HorizontalCollisions(Vector2 velocity)
        {
            float directionX = Math.Sign(velocity.X);
            float rayLength = Math.Abs(velocity.X);
            Vector2 checkPoint;

            for (int i = 0; i < horizontalRayCount; i++)
            {
                Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.topLeft.ToVector2() : raycastOrigins.topRight.ToVector2();
                rayOrigin += Vector2.UnitY * (horizontalRaySpacing * i);
                checkPoint = rayOrigin;

                int amount = 0;

                while (Math.Abs(checkPoint.X - rayOrigin.X) < rayLength)
                {
                    amount++;
                    checkPoint += Vector2.UnitX * directionX;

                    if (CheckCollision(checkPoint))
                    {
                        amount--;
                        velocity.X = amount * directionX;

                        collisions.left = (directionX == -1);
                        collisions.right = (directionX == 1);

                        break;
                    }
                }
            }

            return velocity;
        }

        public Vector2 VerticalCollisions(Vector2 velocity, Vector2 velocityOffset)
        {
            float directionY = Math.Sign(velocity.Y);
            float rayLength = Math.Abs(velocity.Y);
            Vector2 checkPoint;

            for (int i = 0; i < verticalRayCount; i++)
            {
                Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.topLeft.ToVector2() : raycastOrigins.botLeft.ToVector2();
                rayOrigin += Vector2.UnitX * (verticalRaySpacing * i);
                checkPoint = rayOrigin + velocityOffset;

                int amount = 0;

                while (Math.Abs(checkPoint.Y - rayOrigin.Y) < rayLength)
                {
                    amount++;
                    checkPoint += Vector2.UnitY * directionY;

                    if (CheckCollision(checkPoint))
                    {
                        amount--;
                        velocity.Y = amount * directionY;

                        collisions.above = (directionY == -1);
                        collisions.below = (directionY == 1);

                        break;
                    }
                }
            }

            return velocity;
        }

        public void UpdateRaycastOrigins(Rectangle boundingBox)
        {
            raycastOrigins.topLeft = new Point(boundingBox.X, boundingBox.Y);
            raycastOrigins.topRight = new Point(boundingBox.X + boundingBox.Width - 1, boundingBox.Y);
            raycastOrigins.botLeft = new Point(boundingBox.X, boundingBox.Y + boundingBox.Height - 1);
            raycastOrigins.botRight = new Point(boundingBox.X + boundingBox.Width - 1, boundingBox.Y + boundingBox.Height - 1);
        }

        public void CalculateRaycastOrigins(Rectangle boundingBox)
        {
            horizontalRayCount = Math.Clamp(horizontalRayCount, 2, int.MaxValue);
            verticalRayCount = Math.Clamp(verticalRayCount, 2, int.MaxValue);

            horizontalRaySpacing = (boundingBox.Height - 1) / (horizontalRayCount - 1);
            verticalRaySpacing = (boundingBox.Width - 1) / (verticalRayCount - 1);
        }

        bool CheckCollision(Vector2 point)
        {
            if (point.X < boundaryLeft || point.X > boundaryRight || point.Y < boundaryUp || point.Y > boundaryDown)
                return false;

            return collisionMap[(int)point.Y / tileSize, (int)point.X / tileSize];
        }

        public struct RaycastOrigins
        {
            public Point
                topLeft,
                topRight,
                botLeft,
                botRight;
        }

        public struct CollisionInfo
        {
            public bool
                above,
                below,
                left,
                right;

            public void Reset()
            {
                above = below = left = right = false;
            }
        }

    }
}

