using System.Collections.Generic;
using System.Linq;

namespace Players
{
    public class Karlee : IPlayer
    {
        private bool stopHunting = false;

        public char[] HuntChoices(int roundNumber, int currentFood, double currentReputation, int m, double[] playerReputations)
        {
            var result = new List<char>();
            var c = 's';
            if (!stopHunting && currentReputation < playerReputations.Max())
            {
                c = 'h';
            }
            else
            {
                stopHunting = true; 
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