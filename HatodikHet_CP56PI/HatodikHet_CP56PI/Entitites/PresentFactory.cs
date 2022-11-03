using HatodikHet_CP56PI.Abstractions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HatodikHet_CP56PI.Entitites
{
    public class PresentFactory : IToyFactory
    {
        public Color ribbonColor { get; set; }
        public Color boxColor { get; set; }
        public Toy CreateNew()
        {
            return new Present(ribbonColor, boxColor);
        }
    }
}
