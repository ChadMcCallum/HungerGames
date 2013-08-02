using System;
using System.Collections.Generic;

namespace Players
{
    public class Alternating : IPlayer
    {
        private char lastPlayed = 's';

        public char[] HuntChoices(int roundNumber, int currentFood, double currentReputation, int m, double[] playerReputations)
        {
            var result = new List<char>();
            for (var i = 0; i < playerReputations.Length; i++)
            {
                lastPlayed = lastPlayed == 's' ? 'h' : 's';
                result.Add(lastPlayed);
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