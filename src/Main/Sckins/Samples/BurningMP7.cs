using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class BurningMP7 : SkinElement
    {
        public BurningMP7()
        {
            name = "Burning (MP7)";
            location = "MP7skin2";
            AppearenceName = "Burning";

            type = 0;

            rarity = 3;
            mainThing = "MP7";
            animationSpeed = 0.125f;

            animated = true;
            trackType = 2;
        }
    }
}

