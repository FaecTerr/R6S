using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class JammedEffect : Effect
    {

        public JammedEffect()
        {
            team = "Def";
            type = 2;
            name = "Jammed";
            timer = 0.2f;
            maxTimer = 0.2f;
        }
    }
}
