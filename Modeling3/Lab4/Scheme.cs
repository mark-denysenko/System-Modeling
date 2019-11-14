using Modeling3.Lab4;
using Modeling3.Models;
using System;
using System.Collections.Generic;

namespace Modeling3
{
    partial class Scheme
    {
        public static void Lab4_Test_Example()
        {
            var creator = new Creator(2d);

            var process1 = new Process("process1", 0.6);
            var process2 = new Process("process2", 0.3);
            var process3 = new Process("process3", 0.4);
            var process4 = new Process("process4", 0.1, processors: 2);

            var despose1 = new Despose("despose1");

            var branch1 = new Branch(new List<(Element element, double percent)>
            {
                (despose1, 0.42),
                (process2, 0.15),
                (process3, 0.13),
                (process4, 0.30)
            });

            creator.nextElement = process1;
            process1.nextElement = branch1;
            process2.nextElement = process1;
            process3.nextElement = process1;
            process4.nextElement = process1;

            var model = new SimulationModel(new List<Element>
            {
                creator,
                process1,
                process2,
                process3,
                process4,
                despose1
            },
            SimulateTime);

            model.Simulate(false);
            model.PrintResultStatistic();
        }

        public static void Lab4_Bank()
        {
            const double customerDelayIn = 0.5;
            const double customerServiceDelay = 1;
            const double initialCustomerIn = 0.1;
            const int initialQueueForCarLines = 2;
            const int windowMaxQueue = 3;

            var q1 = new Queue<EventBase>(initialQueueForCarLines);
            var q2 = new Queue<EventBase>(initialQueueForCarLines);
            for(int i = 0; i < initialQueueForCarLines; i++)
            {
                q1.Enqueue(new EventBase { createTime = 0.0 });
                q2.Enqueue(new EventBase { createTime = 0.0 });
            }

            var creator = new Creator(customerDelayIn)
            {
                distribution = NumberGenerators.Distributions.EXPONENTIAL,
                tnext = initialCustomerIn
            };

            var window1 = new BankProcess("window1", customerServiceDelay, windowMaxQueue)
            {
                //queue = initialQueueForCarLines,
                eventQueue = q1
                //tnext = NumberGenerators.RandomNumberGenerators.Normal(1d, 0.3)
            };
            window1.inAct(new EventBase { createTime = 0.0 });

            var window2 = new BankProcess("window2", customerServiceDelay, windowMaxQueue)
            {
                //queue = initialQueueForCarLines,
                eventQueue = q2
                //tnext = NumberGenerators.RandomNumberGenerators.Normal(1d, 0.3)
            };
            window2.inAct(new EventBase { createTime = 0.0 });

            var bankExit = new BankDesposer("exit");

            var windowChoice = new BankAutoBranch(new List<(Element element, double percent)>
            {
                (window1, 0.5),
                (window2, 0.5)
            });

            window1.windowsChoose = windowChoice;
            window2.windowsChoose = windowChoice;

            creator.nextElement = windowChoice;
            window1.nextElement = bankExit;
            window2.nextElement = bankExit;

            var model = new SimulationModel(new List<Element>
            {
                creator,
                window1,
                window2,
                bankExit
            },
            SimulateTime);

            model.Simulate(false);
            model.PrintResultStatistic();
            bankExit.printResult();
            Console.WriteLine("Average clinets in bank: " + (window1.avgClientInBank + window2.avgClientInBank) / SimulateTime);
            Console.WriteLine("Number of line changes: " + windowChoice.QueueChanges);
        }

        public static void Lab4_Hospital()
        {
            const double customerDelayIn = 0.5;
            const double customerServiceDelay = 1.7;
            const double initialCustomerIn = 0.1;
            const int initialQueueForCarLines = 2;
            const int windowMaxQueue = 5;

            var q1 = new Queue<EventBase>(initialQueueForCarLines);
            var q2 = new Queue<EventBase>(initialQueueForCarLines);
            for (int i = 0; i < initialQueueForCarLines; i++)
            {
                q1.Enqueue(new EventBase { createTime = 0.0 });
                q2.Enqueue(new EventBase { createTime = 0.0 });
            }

            var creator = new Creator(customerDelayIn)
            {
                distribution = NumberGenerators.Distributions.EXPONENTIAL,
                tnext = initialCustomerIn
            };

            var window1 = new Process("window1", customerServiceDelay, windowMaxQueue)
            {
                //queue = initialQueueForCarLines,
                eventQueue = q1
                //tnext = NumberGenerators.RandomNumberGenerators.Normal(1d, 0.3)
            };
            window1.inAct(new EventBase { createTime = 0.0 });

            var window2 = new Process("window2", customerServiceDelay, windowMaxQueue)
            {
                //queue = initialQueueForCarLines,
                eventQueue = q2
                //tnext = NumberGenerators.RandomNumberGenerators.Normal(1d, 0.3)
            };
            window2.inAct(new EventBase { createTime = 0.0 });

            var bankExit = new Despose("exit");

            var windowChoice = new BankAutoBranch(new List<(Element element, double percent)>
            {
                (window1, 0.5),
                (window2, 0.5)
            });

            creator.nextElement = windowChoice;
            window1.nextElement = bankExit;
            window2.nextElement = bankExit;

            var model = new SimulationModel(new List<Element>
            {
                creator,
                window1,
                window2,
                bankExit
            },
            SimulateTime);

            model.Simulate(false);
            model.PrintResultStatistic();

        }
    }
}
