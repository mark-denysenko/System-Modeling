using System;

namespace Modeling3.Models
{
    public class Creator : Element
    {
        public Creator(double delay): base("creator", delay) { }

        public override void outAct()
        {
            base.outAct();

            tnext = tcurr + getDelay();

            nextElement.inAct();
        }
    }
}
