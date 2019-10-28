using System;

namespace Modeling3.NumberGenerators
{
    public static class RandomNumberGenerators
    {
        private static Random Random = new Random(DateTime.UtcNow.Millisecond);

        public static double Exponential(double timeMean)
        {
            return -timeMean * Math.Log(Random.NextDouble());
        }

        public static double Uniform(double timeMin, double timeMax)
        {
            return timeMin + Random.NextDouble() * (timeMax - timeMin);
        }

        public static double Normal(double timeMean, double timeDeviation)
        {
            return timeMean + timeDeviation * NextGaussian();
        }

        private static double NextGaussian()
        {
            double u1 = 1.0 - Random.NextDouble(); //uniform(0,1] random doubles
            double u2 = 1.0 - Random.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                         Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)

            return randStdNormal;
        }
    }
}
