using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Players
{
    public class FailHunts : IPlayer
    {
        private Dictionary<int, HuntOutcome> _outcomes = new Dictionary<int, HuntOutcome>(); 
        private Random _random = new Random();
        public char[] HuntChoices(int roundNumber, int currentFood, double currentReputation, int m, double[] playerReputations)
        {
            var results = new List<char>();
            var likelyHunts = GetProjectedHunts(m, playerReputations);
            if (likelyHunts >= 0.5)
            {
                for (var i = 0; i < playerReputations.Length; i++) results.Add('s');
                return results.ToArray();
            }
            
            foreach (var rep in playerReputations)
            {
                if (((_random.Next() % 100) / 100.0) < rep) results.Add('h');
                else results.Add('s');
            }
            return results.ToArray();
        }

        private int GetProjectedHunts(int m, double[] playerReputations)
        {
            return _outcomes.ContainsKey(m) ? (int)_outcomes[m].averageAwardChance : 0;
        }

        private class HuntOutcome
        {
            public int numberOfHunts;
            public double averageAwardChance;
        }

        public void HuntOutcomes(int[] foodEarnings)
        {
            
        }

        public void RoundEnd(int award, int m, int numberOfHunters)
        {
            if (!_outcomes.ContainsKey(m))
            {
                _outcomes[m] = new HuntOutcome
                    {
                        numberOfHunts = 1,
                        averageAwardChance = award > 0 ? 1.0 : 0.0
                    };
            }
            else _outcomes[m] = new HuntOutcome
                {
                    numberOfHunts = _outcomes[m].numberOfHunts+1,
                    averageAwardChance =
                    (((double)_outcomes[m].numberOfHunts) * _outcomes[m].averageAwardChance + (award > 0 ? 1.0 : 0.0))
                    / ((double)_outcomes[m].numberOfHunts + 1)
                };
        }
    }
}
