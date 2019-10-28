using Modeling3.NumberGenerators;
using System;

namespace Modeling3.Models
{
    public class Despose : Element
    {
        public Despose(string name): base(name, 0)
        {
            distribution = Distributions.None;
            tnext = double.MaxValue;
        }

        public override void inAct()
        {
            outAct();
        }
    }
}
