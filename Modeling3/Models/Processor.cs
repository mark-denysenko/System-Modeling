using System;

namespace Modeling3.Models
{
    public class Processor : Element
    {
        public double workingTime { get; set; } = 0.0;
        public ProcessState state { get; set; } = ProcessState.Idle;

        public Processor(double delay) : this("processor", delay) { }

        public Processor(string name, double delay) : base(name, delay) { }

        public override void inAct(EventBase e)
        {
            base.inAct(e);

            if (state == ProcessState.Idle)
            {
                state = ProcessState.Work;
                tnext = tcurr + getDelay();
            }
        }

        public override EventBase outAct()
        {
            var e = base.outAct();

            tnext = double.MaxValue;
            state = ProcessState.Idle;

            return e;
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
