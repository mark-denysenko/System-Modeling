using Modeling3.NumberGenerators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Modeling3.Models
{
    public class Branch : Element
    {
        protected IList<(Element element, double probability)> possibleElements;

        public Branch(IList<(Element element, int weight)> possibleElements) : base("branch", 0)
        {
            this.possibleElements = WeightToPercent(possibleElements);
        }

        public Branch(IList<(Element element, double percent)> possibleElements) : base("branch", 0)
        {
            this.possibleElements = PercentToPercent(possibleElements);
        }

        public override void inAct()
        {
            GetNextElement().inAct();
        }

        protected Element GetNextElement()
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
