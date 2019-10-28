using Modeling3.NumberGenerators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Modeling3.Models
{
    public class Branch : Element
    {
        private IList<(Element element, double probability)> possibleElements;

        public Branch(IList<(Element element, int weight)> possibleElements) : base("branch", 0)
        {
            this.possibleElements = WeightToPercent(possibleElements);
        }

        public Branch(IList<(Element element, double percent)> possibleElements) : base("branch", 0)
        {
            this.possibleElements = PercentToPercent(possibleElements);
        }

        public override int quantity => nextElement?.quantity ?? 0;

        public override int id
        {
            get => nextElement?.id ?? -1;
            set
            {
                if (nextElement != null)
                    nextElement.id = value;
            }
        }

        public override string name
        {
            get => nextElement?.name;
            set
            {
                if (nextElement != null)
                    nextElement.name = value;
            }
        }

        public override double delayMean
        {
            get => nextElement?.delayMean ?? default(double);
            set
            {
                if (nextElement != null)
                    nextElement.delayMean = value;
            }
        }

        public override double tnext
        {
            get => nextElement?.tnext ?? default(double);
            set
            {
                if (nextElement != null)
                    nextElement.tnext = value;
            }
        }

        public override double tcurr
        {
            get => nextElement?.tcurr ?? default(double);
            set
            {
                if (nextElement != null)
                    nextElement.tcurr = value;
            }
        }

        public override void inAct()
        {
            GetNextElement().inAct();
            //if (nextElement is null)
            //{
            //}

            //nextElement.inAct();
        }

        public override void outAct()
        {
            nextElement?.outAct();

            nextElement = null;
        }

        public override void printInfo()
        {
            nextElement?.printInfo();
        }

        public override void printResult()
        {
            nextElement?.printResult();
        }

        public override void doStatistics(double delta)
        {
            nextElement?.doStatistics(delta);
        }

        private Element GetNextElement()
        {
            double diceRoll = RandomNumberGenerators.Uniform(0, 1);

            double cumulative = 0.0;
            for (int i = 0; i < possibleElements.Count; i++)
            {
                cumulative += possibleElements[i].probability;
                if (diceRoll < cumulative)
                {
                    return possibleElements[i].element;
                }
            }

            return possibleElements.FirstOrDefault().element;
        }

        private IList<(Element element, double probability)> WeightToPercent(IList<(Element element, int weight)> possibleElements)
        {
            int totalWeight = possibleElements.Sum(el => el.weight);

            return possibleElements.Select(element => (element.element, (double)element.weight / totalWeight)).ToList();
        }

        private IList<(Element element, double probability)> PercentToPercent(IList<(Element element, double percent)> possibleElements)
        {
            double totalPercent = possibleElements.Sum(el => el.percent);

            return possibleElements.Select(element => (element.element, element.percent / totalPercent)).ToList();
        }
    }
}
