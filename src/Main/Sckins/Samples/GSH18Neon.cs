using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class GSH18Neon : SkinElement
    {
        public GSH18Neon()
        {
            name = "Neon (GSH18)";
            location = "GSHskin0";
            AppearenceName = "Neon";

            type = 0;

            rarity = 2;
            mainThing = "GSH";

            animated = true;
            animationSpeed = 0.125f;
        }
    }
}

