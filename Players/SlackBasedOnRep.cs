using System.Collections.Generic;

namespace Players
{
    public class SlackBasedOnRep : IPlayer
    {
        public char[] HuntChoices(int roundNumber, int currentFood, double currentReputation, int m, double[] playerReputations)
        {
            var result = new List<char>();
            foreach (var rep in playerReputations)
            {
                result.Add(rep >= 0.5 ? 'h' : 's');
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