using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Den_förlorade_Juston
{
    internal class StationaryLevelObjekt
    {
        int sizeX, sizeY, tileSize, tileColumns;
        Rectangle srcRect;
        Texture2D image;

        public int[,] map =
        {
           {0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,2},
           {12,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,14},
           {12,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,14},
           {12,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,36,14}};


        public bool[,] collisionMap;

        public StationaryLevelObjekt(Texture2D image, int sizeX, int sizeY, int tileSize, int tileColumns)
        {
            this.image = image;
            this.sizeX = sizeX;
            this.sizeY = sizeY;
            this.tileSize = tileSize;
            this.tileColumns = tileColumns;

            srcRect = new Rectangle(0, 0, tileSize, tileSize);
            CreateCollisonMap();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int x = 0; x < sizeX; x++)
            {
                for (int y = 0; y < sizeY; y++)
                {
                    srcRect.X = (map[y, x] % tileColumns) * tileSize;
                    srcRect.Y = (map[y, x] / tileColumns) * tileSize;
                    spriteBatch.Draw(image, new Vector2(x-5, y+14) * tileSize, srcRect, Color.White);
                    //spriteBatch.Draw(image, new Vector2(x, y) * tileSize, srcRect, Color.White,1f, Vector2.One,1f, SpriteEffects.None, 0f);
                }
            }
        }

        public void CreateCollisonMap()
        {
            collisionMap = new bool[sizeY, sizeX];

            for (int x = 0; x < sizeX; x++)
            {
                for (int y = 0; y < sizeY; y++)
                {
                    if (map[y, x] >= 12 && map[y, x] <= 12 ||
                       map[y, x] >= 13 && map[y, x] <= 13 ||
                            map[y, x] >= 1 && map[y, x] <= 1 ||
                            map[y, x] >= 14 && map[y, x] <= 14 ||
                            map[y, x] >= 26 && map[y, x] <= 26 ||
                            map[y, x] >= 24 && map[y, x] <= 24 ||
                            map[y, x] >= 0 && map[y, x] <= 0 ||
                            map[y, x] >= 2 && map[y, x] <= 2 ||
                            map[y, x] >= 25 && map[y, x] <= 25)
                    {
                        collisionMap[y, x] = true;
                    }
                    else
                        collisionMap[y, x] = false;
                }
            }
        }

    }
}

