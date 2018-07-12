using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handmade.Classes
{
    public class TheClassWhichHasConstructorWithParameters: IAnInterfaceForTheClassWhichHasConstructorWithParameters
    {
        public A[] A { get; set; }
        public C C { get; set; }
        public TheClassWhichHasConstructorWithParameters(A[] a,C c)
        {
            A = a;
            C = c;
        }
    }
}
