using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class BanditBattery : SkinElement
    {
        public BanditBattery()
        {
            name = "Battery enjoyer";
            location = "BanditHatskin1";
            AppearenceName = name;

            type = 1;

            rarity = 3;
            mainThing = "Bandit";

            animated = true;
            frames = 6;
            animationSpeed = 0.125f;
        }
    }
}

