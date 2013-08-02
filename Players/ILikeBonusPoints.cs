using System.Collections.Generic;

namespace Players
{
    public class ILikeBonusPoints : IPlayer
    {
        private bool gotBonus = false;

        public char[] HuntChoices(int roundNumber, int currentFood, double currentReputation, int m, double[] playerReputations)
        {
            var c = 's';
            if (gotBonus)
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
            
        }

        public void RoundEnd(int award, int m, int numberOfHunters)
        {
            gotBonus = (award > 0);
        }
    }
}