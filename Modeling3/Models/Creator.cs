using System;

namespace Modeling3.Models
{
    public class Creator : Element
    {
        public Creator(double delay): base("creator", delay) { }

        public override EventBase outAct()
        {
            base.outAct();

            tnext = tcurr + getDelay();

            var newEvent = new EventBase { createTime = tcurr };
            nextElement.inAct(newEvent);

            return newEvent;
        }
    }
}
