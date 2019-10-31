using Modeling3.NumberGenerators;
using System;

namespace Modeling3.Models
{
    public abstract class Element
    {
        public virtual int id { get; set; }
        public virtual string name { get; set; }
        public virtual double tcurr { get; set; } = 0.0;
        public virtual double tnext { get; set; } = 0.0;
        public virtual double delayMean { get; set; }
        public virtual double delayDev { get; set; }
        public virtual int quantity { get; private set; }
        public ProcessState state { get; set; } = ProcessState.Idle;
        public Distributions distribution { get; set; } = Distributions.EXPONENTIAL;

        public Element nextElement { get; set; }

        private static int nextId = 0;

        public Element(): this(1.0) { }

        public Element(double delay) : this("anonymous", delay) { }

        public Element(string nameOfElement, double delay = 1.0)
        {
            delayMean = delay;
            id = nextId;
            name = nameOfElement + "_" + id;

            nextId++;
        }

        public virtual void inAct() { }

        public virtual void outAct()
        {
            quantity++;
        }

        public virtual void doStatistics(double delta) { }

        public virtual void printResult()
        {
            Console.WriteLine(name + " quantity = " + quantity);
        }

        public virtual void printInfo()
        {
            Console.WriteLine(name + " state = " + state + " quantity = " + quantity + " tnext = " + tnext);
        }

        public double getDelay()
        {
            double delay = delayMean;

            if (Distributions.EXPONENTIAL == distribution)
            {
                delay = RandomNumberGenerators.Exponential(delayMean);
            }
            else if (Distributions.NORMAL == distribution)
            {
                delay = RandomNumberGenerators.Normal(delayMean, delayDev);
            }
            else if (Distributions.UNIFORM == distribution)
            {
                delay = RandomNumberGenerators.Uniform(delayMean, delayDev);
            }

            return delay;
        }
    }
}
