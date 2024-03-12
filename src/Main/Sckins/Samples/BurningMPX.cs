using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class BurningMPX : SkinElement
    {
        public BurningMPX()
        {
            name = "Burning (MPX)";
            location = "MPXskin1";
            AppearenceName = "Burning";

            type = 0;

            rarity = 3;
            mainThing = "MPX";

            animated = true;
            trackType = 2;
        }
    }
}

