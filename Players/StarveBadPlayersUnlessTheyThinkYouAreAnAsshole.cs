using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Players
{
    class StarveBadPlayersUnlessTheyThinkYouAreAnAsshole : IPlayer
    {
        private Random _random = new Random();

        public char[] HuntChoices(int roundNumber, int currentFood, double currentReputation, int m, double[] playerReputations)
        {
            var results = new List<char>();
            foreach (var rep in playerReputations)
            {
                if (((((_random.Next() % 100) / 100.0) + currentReputation) / 2.0) < rep) results.Add('h');
                else results.Add('s');
            }
            return results.ToArray();
        }

        public void HuntOutcomes(int[] foodEarnings)
        {
        }

        public void RoundEnd(int award, int m, int numberOfHunters)
        {
        }
    }
}
