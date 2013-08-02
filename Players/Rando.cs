using System;
using System.Collections.Generic;

namespace Players
{
    class Rando : IPlayer
    {
        static Random _random = new Random();

        public char[] HuntChoices(int roundNumber, int currentFood, double currentReputation, int m, double[] playerReputations)
        {
            var result = new List<char>();
            for (var i = 0; i < playerReputations.Length; i++)
            {
                result.Add(_random.Next()%2 == 0 ? 's' : 'h');
            }
            return result.ToArray();
        }

        public void HuntOutcomes(int[] foodEarnings)
        {
            
        }

        public void RoundEnd(int award, int m, int numberOfHunters)
        {
            
        }
    }
}
