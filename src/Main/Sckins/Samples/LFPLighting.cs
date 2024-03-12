using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class LFPLighting : SkinElement
    {
        public LFPLighting()
        {
            name = "LightingLFP";
            location = "LFP586skin1";
            AppearenceName = "Lighting";

            type = 0;

            rarity = 2;
            mainThing = "LFP";
            animationSpeed = 0.125f;

            animated = true;
            trackType = 6;
        }
    }
}

