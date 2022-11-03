using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HatodikHet_CP56PI.Entitites
{
    public class BallFactory
    {
        public Toy CreateNew()
        {
            return new Ball();
        }
    }
}
