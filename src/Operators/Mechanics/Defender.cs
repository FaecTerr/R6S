using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class Defender : Operators
    {
        public Defender(float xpos, float ypos) : base(xpos, ypos)
        {
            team = "Def";
        }
    }
}
