using System;
using Modeling3.Models;
using System.Collections.Generic;

namespace Modeling3
{
    public class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("======================= Lab 3 =======================");
            //Scheme.Lab3();

            Console.WriteLine("======================= Lab 4 =======================");

            Console.WriteLine("\n----- Test Example -----");
            //Scheme.Lab4_Test_Example();

            Console.WriteLine("\n----- Bank Simulation -----");
            Scheme.Lab4_Bank();

            Console.WriteLine("\n----- Hospital Simulation -----");
            Scheme.Lab4_Hospital();

            Console.ReadKey();
        }
    }
}
