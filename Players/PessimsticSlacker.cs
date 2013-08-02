using System.Collections.Generic;
using System.Linq;

namespace Players
{
    public class PessimsticSlacker : IPlayer
    {
        public char[] HuntChoices(int roundNumber, int currentFood, double currentReputation, int m, double[] playerReputations)
        {
            var result = new List<char>();
            var likelyHunters = playerReputations.Count(p => p >= 0.5);
            var likelyHunts = likelyHunters * playerReputations.Length + 1;
            var c = 'h';
            //if we're not likely to hit the bonus, fuck it.
            if (m > likelyHunts)
            {
                c = 's';
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