using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class WoundedPulse : SkinElement
    {
        public WoundedPulse()
        {
            name = "Wounded";
            location = "pulseHatskin1";
            AppearenceName = name;

            type = 1;

            rarity = 2;
            mainThing = "Pulse";
        }
    }
}

