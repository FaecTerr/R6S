using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class BurningPDW : SkinElement
    {
        public BurningPDW()
        {
            name = "Burning (GSH18)";
            location = "PDW9skin0";
            AppearenceName = "Burning";

            type = 0;

            rarity = 3;
            mainThing = "PDW9";

            animated = true;
            trackType = 2;
        }
    }
}

