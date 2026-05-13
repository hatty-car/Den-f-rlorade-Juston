using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Den_förlorade_Juston
{
    internal class Ui
    {
        string namn, time;
        double death;
        public Ui(string Namn, string Time, double Death)
        {
            namn = Namn;
            time = Time;
            death = Death;
        }
    }
}
