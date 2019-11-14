using Modeling3.Models;
using System;
using System.Linq;

namespace Modeling3.Lab4
{
    public class BankProcess : Process
    {
        public BankAutoBranch windowsChoose;
        public int changedQueue = 0;
        public double avgClientInBank = 0;

        public BankProcess(double delay) : base(delay)
        {
        }

        public BankProcess(string name, double delay, int maxQueue = int.MaxValue, int processors = 1) : base(name, delay, maxQueue, processors)
        {
        }

        public override void doStatistics(double delta)
        {
            if(windowsChoose.RefreshQueues())
            {
                changedQueue++;
            }

            avgClientInBank += (eventQueue.Count + processors.Count(p => p.state == ProcessState.Work)) * delta;
            base.doStatistics(delta);
        }
    }
}
