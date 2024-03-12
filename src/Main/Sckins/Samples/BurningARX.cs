using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class BurningARX200 : SkinElement
    {
        public BurningARX200()
        {
            name = "Burning (ARX200)";
            location = "ARX200skin0";
            AppearenceName = "Burning";

            type = 0;

            rarity = 3;
            mainThing = "ARX200";
            animationSpeed = 0.125f;

            animated = true;
            trackType = 2;
        }
    }
}

