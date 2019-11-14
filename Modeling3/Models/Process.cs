using System;
using System.Collections.Generic;
using System.Linq;

namespace Modeling3.Models
{
    public class Process : Element
    {
        public int maxqueue { get; set; } = int.MaxValue;
        public int failure { get; private set; } = 0;
        public double meanQueue { get; private set; } = 0.0;

        public double workingTime { get; set; } = 0.0;
        public int workingMaxQueue { get; private set; } = 0;

        public bool IsAnyProcessorFree => processors.Any(p => p.state == ProcessState.Idle);

        public Queue<EventBase> eventQueue;

        public override double tnext
        {
            get => processors.Min(procc => procc.tnext);
            set
            {
                var proc = processors.First(p => p.tnext == tcurr);
                proc.tnext = value;
            }
        }

        public override double tcurr
        {
            get => processors.FirstOrDefault()?.tcurr ?? default(double);
            set
            {
                foreach(var proc in processors)
                {
                    proc.tcurr = value;
                }
            }
        }

        protected IEnumerable<Processor> processors;

        public Process(double delay): this("process", delay) { }

        public Process(string name, double delay, int maxQueue = 10, int processors = 1): base(name, delay)
        {
            this.maxqueue = maxQueue;
            eventQueue = new Queue<EventBase>(maxQueue);

            var procList = new List<Processor>(processors);
            for(int i = 0; i < processors; i++)
            {
                procList.Add(new Processor(name, delay));
            }

            this.processors = procList;
        }

        public override void inAct(EventBase e)
        {
            var freeProcc = processors.FirstOrDefault(proc => proc.state == ProcessState.Idle);

            if(freeProcc != null)
            {
                freeProcc.inAct(e);
            }
            else
            {
                if (eventQueue.Count < maxqueue)
                {
                    eventQueue.Enqueue(e);
                    workingMaxQueue = workingMaxQueue < eventQueue.Count ? eventQueue.Count : workingMaxQueue;
                }
                else
                {
                    failure++;
                }
            }
        }

        public override EventBase outAct()
        {
            base.outAct();

            var finishedProcceses = processors.Where(proc => proc.tnext <= tnext).ToList();
            EventBase e = null;

            if (finishedProcceses.Any())
            {
                foreach (var proc in finishedProcceses)
                {
                    e = proc.outAct();

                    if (eventQueue.Count > 0)
                    {
                        proc.inAct(eventQueue.Dequeue());
                    }
                    nextElement.inAct(e);

                }
            }

            return e;
        }

        public override void printInfo()
        {
            base.printInfo();
            Console.WriteLine("failure = " + failure);
        }

        public override void doStatistics(double delta)
        {
            meanQueue = meanQueue + eventQueue.Count * delta;

            if (processors.Any(p => p.state == ProcessState.Work))
            {
                workingTime += delta * processors.Count(p => p.state == ProcessState.Work) / processors.Count();
            }
        }
    }
}
