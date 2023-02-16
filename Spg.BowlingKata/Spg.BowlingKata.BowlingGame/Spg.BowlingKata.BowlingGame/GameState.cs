using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.BowlingKata.BowlingGame
{
    public class GameState
    {
        public int RollNumber { get; set; }     // 1-20
        public int SumThronPins { get; set; }   // 0-10
        public int StrikeRolls { get; set; }    // 2,1,0
    }
}
