using HatodikHet_CP56PI.Abstractions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HatodikHet_CP56PI.Entitites
{
    public class Present : Toy
    {
        public SolidBrush ribbonColor { get; private set; }
        public SolidBrush boxColor { get; private set; }
        public Present(Color ribbon, Color box)
        {
            ribbonColor = new SolidBrush(ribbon);
            boxColor = new SolidBrush(box);
        }
        protected override void DrawImage(Graphics g)
        {
            g.FillRectangle(boxColor, 0, 0, Width, Height);
            g.FillRectangle(ribbonColor, 0, 20, Width, Height / 5);
            g.FillRectangle(ribbonColor, 20, 0, Width / 5, Height);
        }
    }
}
