using System.Collections.Generic;

namespace Players
{
    public class SlackIfLowerRep : IPlayer
    {
        public char[] HuntChoices(int roundNumber, int currentFood, double currentReputation, int m, double[] playerReputations)
        {
            var result = new List<char>();
            for (var i = 0; i < playerReputations.Length; i++)
            {
                result.Add(playerReputations[i] > currentReputation ? 's' : 'h');
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