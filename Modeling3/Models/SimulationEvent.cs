using System;

namespace Modeling3.Models
{
    public abstract class SimulationEvent
    {
        public double createTime { get; set; }
        public double workTime { get; set; }
        public double finishTime { get; set; }
    }
}
