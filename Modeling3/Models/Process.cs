using System;
using System.Collections.Generic;
using System.Linq;

namespace Modeling3.Models
{
    public class Process : Element
    {
        public int queue { get; set; } = 0;
        public int maxqueue { get; set; } = int.MaxValue;
        public int failure { get; private set; } = 0;
        public double meanQueue { get; private set; } = 0.0;

        public double workingTime { get; set; } = 0.0;
        public int workingMaxQueue { get; private set; } = 0;

        public override double tnext
        {
            get => processors.Min(procc => procc.tnext);
            set
            {
                var proc = processors.First(p => p.tnext == tcurr);
                proc.tnext = value;
            }
        }

        private IEnumerable<Processor> processors;

        public Process(double delay): this("process", delay, 5, 1) { }

        public Process(string name, double delay, int maxQueue, int processors): base(name, delay)
        {
            this.maxqueue = maxQueue;

            var procList = new List<Processor>(processors);
            for(int i = 0; i < processors; i++)
            {
                procList.Add(new Processor(name, 0));
            }

            this.processors = procList;
        }

        public override void inAct()
        {
            var freeProcc = processors.FirstOrDefault(proc => proc.state == ProcessState.Idle);

            if(freeProcc != null)
            {
                freeProcc.inAct();
            }
            else
            {
                if (queue < maxqueue)
                {
                    queue = queue + 1;
                    workingMaxQueue = workingMaxQueue < queue ? queue : workingMaxQueue;
                }
                else
                {
                    failure++;
                }
            }

            //if (state == ProcessState.Idle)
            //{
            //    state = ProcessState.Work;
            //    tnext = tcurr + getDelay();
            //}
            //else
            //{
            //    if (queue < maxqueue)
            //    {
            //        queue = queue + 1;
            //        workingMaxQueue = workingMaxQueue < queue ? queue : workingMaxQueue;
            //    }
            //    else
            //    {
            //        failure++;
            //    }
            //}
        }

        public override void outAct()
        {
            base.outAct();

            var finishedProcc = processors.FirstOrDefault(proc => proc.tnext == tnext);
            if (finishedProcc != null)
            {
                finishedProcc.outAct();

                //tnext = double.MaxValue;
                //state = ProcessState.Idle;

                if (queue > 0)
                {
                    //queue = queue - 1;
                    //state = ProcessState.Work;
                    //tnext = tcurr + getDelay();

                    finishedProcc.inAct();
                }
            }

            nextElement.inAct();
        }

        public override void printInfo()
        {
            base.printInfo();
            Console.WriteLine("failure = " + failure);
        }

        public override void doStatistics(double delta)
        {
            meanQueue = meanQueue + queue * delta;

            if (processors.Any(p => p.state == ProcessState.Work))
            {
                workingTime += delta * processors.Count(p => p.state == ProcessState.Work) / processors.Count();
            }
        }
    }
}
