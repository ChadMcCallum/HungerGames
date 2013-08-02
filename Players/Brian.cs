using System.Collections.Generic;
using System.Linq;

namespace Players
{
    public class Brian : IPlayer
    {
        public char[] HuntChoices(int roundNumber, int currentFood, double currentReputation, int m, double[] playerReputations)
        {
            var result = new List<char>();
            var likelyHunters = playerReputations.Count(p => p >= 0.6);
            var likelyHunts = likelyHunters*playerReputations.Length;
            var c = 's';
            if (m > likelyHunts)
            {
                c = 'h';
            }
            for (var i = 0; i < playerReputations.Length; i++)
            {
                result.Add(c);
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