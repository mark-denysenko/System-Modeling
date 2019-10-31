using Modeling3.Models;
using System;

namespace Modeling3.Lab4
{
    public class BankProcess : Process
    {
        public BankProcess(double delay) : base(delay)
        {
        }

        public BankProcess(string name, double delay, int maxQueue = int.MaxValue, int processors = 1) : base(name, delay, maxQueue, processors)
        {
        }
    }
}
