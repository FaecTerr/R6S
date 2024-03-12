using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class ProximityAlarmed : Effect
    {

        public ProximityAlarmed()
        {
            team = "Def";
            type = 2;
            name = "Proximity alarm";

            timer = 0.2f;
            maxTimer = 0.2f;
        }
    }
}
