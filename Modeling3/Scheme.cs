using Modeling3.Models;
using System;
using System.Collections.Generic;

namespace Modeling3
{
    public partial class Scheme
    {
        public static double SimulateTime { get; set; } = 10_000d;

        public static void Lab3()
        {
            const double creatorDelay = 1.0f;
            const double processDelay = 2.5f;
            const int processMaxQueue = 5;

            var creator = new Creator(creatorDelay);

            var process1 = new Process("process1", processDelay, processMaxQueue, 2);
            var process2 = new Process("process2", processDelay * 3, processMaxQueue, 2);
            var process3 = new Process("process3", processDelay * 7, processMaxQueue, 1);
            var process4 = new Process("process4", processDelay * 10, processMaxQueue, 1);

            var despose1 = new Despose("despose1");
            var despose2 = new Despose("despose2");

            var branch1 = new Branch(new List<(Element element, int weight)> { (process1, 4), (despose2, 1) });
            var branch2 = new Branch(new List<(Element element, int weight)> { (process2, 3), (process3, 2) });

            creator.nextElement = branch1;
            process1.nextElement = branch2;
            process2.nextElement = despose1;
            process3.nextElement = process4;
            process4.nextElement = despose2;

            var model = new SimulationModel(new List<Element>
            {
                creator,
                process1,
                process2,
                process3,
                process4,
                despose1,
                despose2
            });

            model.Simulate(SimulateTime);
        }
    }
}
