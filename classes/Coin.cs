using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using lab8_g.interfaces;

namespace lab8_g.classes
{
    internal class Coin : IField
    {
        public Bitmap sprite;
        public int cost;
        public Bitmap cloneBm;
        public int I;
        public int J;

        public Coin(int I,int J, Bitmap bg)
        {
            this.I = I; 
            this.J = J;
            cost = 1;
            Image spriteImg = Image.FromFile("img/coin.png");
            sprite = new Bitmap(spriteImg, 40, 40);
            System.Drawing.Imaging.PixelFormat format =
                bg.PixelFormat;
            Rectangle cloneRect = new Rectangle(J *40,I*40, 40, 40);
            cloneBm = bg.Clone(cloneRect, format);
        }


        public void PickUp()
        {
            Storage.score++;
            Storage.field[I, J] = 0;
            Storage.fieldEntities[I, J] = null;
        }
    }
}
