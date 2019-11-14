using System;
using System.Collections.Generic;
using System.Linq;
using Modeling3.Models;

namespace Modeling3.Lab4
{
    public class BankAutoBranch : Branch
    {
        public int QueueChanges { get; set; } = 0;

        public BankAutoBranch(IList<(Element element, double percent)> possibleElements) : base(possibleElements) { }

        public override void inAct(EventBase e)
        {
            var first = possibleElements[0].element as Process;
            var second = possibleElements[1].element as Process;

            if (first.IsAnyProcessorFree || 
                !second.IsAnyProcessorFree && first.eventQueue.Count <= second.eventQueue.Count)
            {
                first.inAct(e);
            }
            else
            {
                second.inAct(e);
            }
        }

        public bool RefreshQueues()
        {
            var first = possibleElements[0].element as Process;
            var second = possibleElements[1].element as Process;

            int quesueDiff = first.eventQueue.Count - second.eventQueue.Count;

            if (quesueDiff >= 2)
            {
                second.eventQueue.Enqueue(first.eventQueue.Dequeue());
                QueueChanges++;
                return true;
            }
            else if (quesueDiff <= -2)
            {
                first.eventQueue.Enqueue(second.eventQueue.Dequeue());
                QueueChanges++;
                return true;
            }

            return false;
        }
    }
}
