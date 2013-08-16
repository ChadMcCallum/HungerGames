using System;
using System.Collections.Generic;
using System.Linq;

namespace Players
{
    public class HuntIfHungry : IPlayer
    {
        private int startingFood = 0;
        private int food = Int32.MinValue;
        private int change = 0;

        public char[] HuntChoices(int roundNumber, int currentFood, double currentReputation, int m, double[] playerReputations)
        {
            if (food == Int32.MinValue)
            {
                food = 300*(playerReputations.Length + 1);
                startingFood = food;
            }
            var c = 'h';
            if (change < 0 || food > startingFood)
            {
                c = 's';
            }
            var result = new List<char>();
            for (var i = 0; i < playerReputations.Length; i++)
            {
                result.Add(c);
            }
            return result.ToArray();
        }

        public void HuntOutcomes(int[] foodEarnings)
        {
            change = foodEarnings.Sum();
            food += foodEarnings.Sum();
        }

        public void RoundEnd(int award, int m, int numberOfHunters)
        {
            change += award;
            food += award;
        }
    }
}