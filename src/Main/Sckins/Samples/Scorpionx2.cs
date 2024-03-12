using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class Scorpionx2 : SkinElement
    {
        public Scorpionx2()
        {
            name = "Scorpion blood";
            location = "scorpionskin0";
            AppearenceName = name;

            type = 0;

            rarity = 3;
            mainThing = "Scorpion";

            animated = true;
            frames = 3 * 6;
            animationSpeed = 0.1f;
            trackType = 5;
        }
    }
}

