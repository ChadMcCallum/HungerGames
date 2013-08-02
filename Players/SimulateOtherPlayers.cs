using System;
using System.Collections.Generic;
using System.Linq;

namespace Players
{
    public class SimulateOtherPlayers : IPlayer
    {
        Random r = new Random();

        public char[] HuntChoices(int roundNumber, int currentFood, double currentReputation, int m, double[] playerReputations)
        {
            var result = new List<char>();
            var likelyHunts = 0;
            foreach (var rep in playerReputations)
            {
                for (var i = 0; i < playerReputations.Length; i++)
                {
                    if (r.Next(100) <= rep*100)
                        likelyHunts++;
                }
            }
            var c = 'h';
            //if we're likely to get the bonus anyways, slack
            if (m < likelyHunts)
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