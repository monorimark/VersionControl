using HatodikHet_CP56PI.Abstractions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HatodikHet_CP56PI.Entitites
{
    public class Car : Toy
    {
        protected override void DrawImage(Graphics g)
        {
            Image imgFile = Image.FromFile("Images/car.png");
            g.DrawImage(imgFile, new Rectangle(0, 0, Width, Height));
        }
    }
}
