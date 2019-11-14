using Modeling3.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modeling3.Lab4
{
    public class BankDesposer: Despose
    {
        public EventBase lastEvent;

        public double sumIntervalsBetweenCustomers;
        public double sumCustomersTimeInBank;

        public BankDesposer(string name) : base(name)
        {
        }

        public override void inAct(EventBase e)
        {
            base.inAct(e);

            if (lastEvent != null)
            {
                sumIntervalsBetweenCustomers += e.finishTime - lastEvent.finishTime;
            }
            sumCustomersTimeInBank += e.finishTime - e.createTime;

            lastEvent = e;
        }

        public override void printResult()
        {
            Console.WriteLine($"Average intervals = {sumIntervalsBetweenCustomers / quantity}");
            Console.WriteLine($"Average customer time in bank = {sumCustomersTimeInBank / quantity}");
        }
    }
}
