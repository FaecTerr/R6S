using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class Attacker : Operators
    {
        public Attacker(float xpos, float ypos) : base(xpos, ypos)
        {
            team = "Att";
        }
    }
}
