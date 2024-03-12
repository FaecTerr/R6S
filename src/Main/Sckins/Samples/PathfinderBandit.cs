using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class PathfinderBandit : SkinElement
    {
        public PathfinderBandit()
        {
            name = "Pathfinder (Bandit)";
            location = "banditHatskin0";
            AppearenceName = "Pathfinder";

            type = 1;

            rarity = 0;
            mainThing = "Bandit";
        }
    }
}

