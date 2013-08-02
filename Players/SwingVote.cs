using System;
using System.Collections.Generic;

namespace Players
{
    public class SwingVote : IPlayer
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
                    if (r.Next(100) <= rep * 100)
                        likelyHunts++;
                }
            }
            var neededHunts = m - likelyHunts;
            if (neededHunts > 0 && neededHunts < playerReputations.Length)
            {
                //a hunting we will go
            }
            else
            {
                neededHunts = 0;
            }

            for (var i = 0; i < playerReputations.Length; i++)
            {
                result.Add(i < neededHunts ? 'h' : 's');
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