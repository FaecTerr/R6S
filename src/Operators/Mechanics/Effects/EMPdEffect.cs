using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class EMPdEffect : Effect
    {
        public EMPdEffect()
        {
            team = "Att";
            type = 1;
            name = "EMP'd";
            timer = 15f;
            maxTimer = 15f;
        }
    }
}
