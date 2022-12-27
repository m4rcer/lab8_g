using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lab8_g.interfaces;

namespace lab8_g.classes
{
    internal class Hero : IField
    {
        public Bitmap sprite;
        public Bitmap cloneBm;
        public int I;
        public int J;
        private Bitmap bg;

        public Hero(int I, int J, Bitmap bg) 
        {
            this.I = I;
            this.J = J;
            this.bg = bg;
            Image spriteImg = Image.FromFile("img/hero.png");
            sprite = new Bitmap(spriteImg, 40, 40);
            System.Drawing.Imaging.PixelFormat format =
                bg.PixelFormat;
            Rectangle cloneRect = new Rectangle(J * 40, I * 40, 40, 40);
            cloneBm = bg.Clone(cloneRect, format);
        }


        public void Move(int i, int j)
        {
            Storage.field[I, J] = 0;
            Storage.fieldEntities[I, J] = null;
            Storage.field[i, j] = 4;
            Storage.fieldEntities[i, j] = this;
            this.I = i;
            this.J = j;
            System.Drawing.Imaging.PixelFormat format =
                bg.PixelFormat;
            Rectangle cloneRect = new Rectangle(j * 40, i * 40, 40, 40);
            cloneBm = bg.Clone(cloneRect, format);
        }
    }
}
