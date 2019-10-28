using System;
using System.Collections.Generic;
using System.Text;

namespace Modeling3.Models
{
    public class Processor : Element
    {
        public double workingTime { get; set; } = 0.0;

        public Processor(double delay) : this("processor", delay) { }

        public Processor(string name, double delay) : base(name, delay) { }

        public override void inAct()
        {
            if (state == ProcessState.Idle)
            {
                state = ProcessState.Work;
                tnext = tcurr + getDelay();
            }
        }

        public override void outAct()
        {
            base.outAct();
            tnext = double.MaxValue;
            state = ProcessState.Idle;
        }

        public override void printInfo()
        {
            base.printInfo();
        }

        public override void doStatistics(double delta)
        {
            if (state == ProcessState.Work)
            {
                workingTime += delta;
            }
        }
    }
}
