using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class SpottedEffect : Effect
    {
        public SpottedEffect()
        {
            team = "Att";
            type = 1;
            name = "Exposed";
            timer = 10f;
            maxTimer = 10f;

            removeOnEnd = true;
        }
    }
}
