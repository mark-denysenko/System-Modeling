using System;
using System.Collections.Generic;
using System.Linq;
using Modeling3.Models;

namespace Modeling3.Lab4
{
    public class BankAutoBranch : Branch
    {
        public BankAutoBranch(IList<(Element element, double percent)> possibleElements) : base(possibleElements) { }

        public override void inAct()
        {
            var first = possibleElements[0].element as Process;
            var second = possibleElements[1].element as Process;

            if (first.queue == second.queue || first.queue < second.queue)
            {
                first.inAct();
            }
            else
            {
                second.inAct();
            }
        }
    }
}
