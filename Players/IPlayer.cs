using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Players
{
    public interface IPlayer
    {
        char[] HuntChoices(int roundNumber, int currentFood, double currentReputation, int m, double[] playerReputations);
        void HuntOutcomes(int[] foodEarnings);
        void RoundEnd(int award, int m, int numberOfHunters);
    }
}
