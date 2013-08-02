using System;
using System.Collections.Generic;
using System.Threading;

namespace Players
{
    class Rando : IPlayer
    {
        private Random _random;

        public Rando(Random r)
        {
            _random = r;
        }

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
