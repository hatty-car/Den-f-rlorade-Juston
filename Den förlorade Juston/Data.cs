using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Numerics;
using System;

namespace Den_förlorade_Juston
{
    internal class Data
    {
        public static List<Objekt> All = new List<Objekt>();


        public static KeyboardState keyboard;

        public static Texture2D spelarBild, tileSet;

        public static SpriteFont text;

        public static Random rnd = new Random();

        public static Spelare player;

        public static Backgrund backgrund;

        public static Kamera camera;
        public static Viewport viewPort;

        public static StationaryLevelObjekt level1;
    }
}
