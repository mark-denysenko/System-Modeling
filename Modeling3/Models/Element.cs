using Modeling3.NumberGenerators;
using System;

namespace Modeling3.Models
{
    public abstract class Element
    {
        public virtual string name { get; set; }
        public virtual double tcurr { get; set; } = 0.0;
        public virtual double tnext { get; set; } = 0.0;
        public virtual double delayMean { get; set; }
        public virtual double delayDev { get; set; }
        public virtual int quantity { get; private set; }
        public Distributions distribution { get; set; } = Distributions.EXPONENTIAL;

        public Element nextElement { get; set; }
        public EventBase currentEvent { get; set; }

        public Element(): this(1.0) { }

        public Element(double delay) : this("anonymous", delay) { }

        public Element(string nameOfElement, double delay = 1.0)
        {
            delayMean = delay;
            name = nameOfElement;
        }

        public virtual void inAct(EventBase e = null)
        {
            currentEvent = e;
        }

        public virtual EventBase outAct()
        {
            quantity++;
            return currentEvent;
        }

        public virtual void doStatistics(double delta) { }

        public virtual void printResult()
        {
            Console.WriteLine(name + " quantity = " + quantity);
        }

        public virtual void printInfo()
        {
            Console.WriteLine(name + " quantity = " + quantity + " tnext = " + tnext);
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
