using System;
using System.Collections.Generic;
using System.Linq;

namespace Modeling3.Models
{
    public class SimulationModel
    {
        private IEnumerable<Element> elements;
        private double tnext = 0.0;
        private double tcurr = 0.0;

        public double SimulateTime { get; private set; }

        public SimulationModel(IEnumerable<Element> elements, double simulateTime)
        {
            this.elements = elements;
            SimulateTime = simulateTime;
        }

        public void Simulate(bool withStepInfo)
        {
            while (tcurr < SimulateTime)
            {
                tnext = double.MaxValue;

                foreach (var e in elements)
                {
                    if (e.tnext < tnext)
                    {
                        tnext = e.tnext;
                    }
                }

                double deltaTime = tnext - tcurr;
                tcurr = tnext;

                foreach (var e in elements)
                {
                    e.doStatistics(deltaTime);
                    e.tcurr = tcurr;

                    if (e.tnext == tcurr)
                    {
                        e.outAct();
                    }
                }

                if (withStepInfo)
                {
                    PrintElementsInfo();
                }
            }
        }

        public void PrintElementsInfo()
        {
            foreach (var e in elements)
            {
                e.printInfo();
            }
        }

        public void PrintResultStatistic()
        {
            int maxQueue = 0;
            double avgQueue = 0.0f;
            double avgHighload = 0.0;

            foreach (var e in elements)
            {
                if (e is Process proc)
                {
                    if(maxQueue < proc.workingMaxQueue)
                    {
                        maxQueue = proc.workingMaxQueue;
                    }
                    avgQueue += proc.meanQueue / tcurr;
                    avgHighload += proc.workingTime / SimulateTime;

                    Console.WriteLine(e.name);
                    Console.WriteLine("Mean length of queue = " + proc.meanQueue / tcurr);
                    Console.WriteLine("Failure probability  = " + proc.failure / (double)proc.quantity);
                    Console.WriteLine("Working time = " + proc.workingTime / SimulateTime);
                }
            }

            Console.WriteLine("Max queue = " + maxQueue);
            Console.WriteLine("Avg queue = " + avgQueue / elements.Where(el => el is Process).Count());
            Console.WriteLine("Avg highload = " + avgHighload / elements.Where(el => el is Process).Count());
        }
    }
}
