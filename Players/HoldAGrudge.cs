using System.Collections.Generic;
using System.Linq;

namespace Players
{
    public class HoldAGrudge : IPlayer
    {
        private int lastTotal = 1;

        public char[] HuntChoices(int roundNumber, int currentFood, double currentReputation, int m, double[] playerReputations)
        {
            var c = 's';
            if (lastTotal > 0)
            {
                c = 'h';
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
            lastTotal = foodEarnings.Sum();
        }

        public void RoundEnd(int award, int m, int numberOfHunters)
        {
            lastTotal += award;
        }
    }
}