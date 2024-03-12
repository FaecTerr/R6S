using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class GlazBadTime : SkinElement
    {
        public GlazBadTime()
        {
            name = "Badtime (glaz)";
            location = "glazHatskin1";
            AppearenceName = name;

            type = 1;

            rarity = 2;
            mainThing = "Glaz";

            animated = true;
            frames = 4;
            animationSpeed = 0.125f;
        }
    }
}

