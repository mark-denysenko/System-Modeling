using System;
using System.Collections.Generic;
using System.Linq;
using Modeling3.Models;

namespace Modeling3.Lab4
{
    public class BankAutoBranch : Branch
    {
        public BankAutoBranch(IList<(Element element, double percent)> possibleElements) : base(possibleElements) { }

        public override void inAct(EventBase e)
        {
            var first = possibleElements[0].element as Process;
            var second = possibleElements[1].element as Process;

            if (first.eventQueue.Count == second.eventQueue.Count || first.eventQueue.Count < second.eventQueue.Count)
            {
                first.inAct(e);
            }
            else
            {
                second.inAct(e);
            }
        }
    }
}
