using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_PART1_TEST
{
    public class Player
    {
        public string playerName {  get; set; }

        public Player() { }

        public Player(string playName)
        {
            this.playerName = playName;
        }
        public override string ToString()
        {
            string msg = "";
            msg += $"Player: {playerName}";
            return msg;
        }
    }
}
