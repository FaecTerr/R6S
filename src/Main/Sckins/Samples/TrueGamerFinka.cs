using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class TrueGamerFinka : SkinElement
    {
        public TrueGamerFinka()
        {
            name = "TrueGamerFinka";
            location = "finkaHeadskin0";
            AppearenceName = "True gamer";

            type = 1;

            rarity = 1;
            mainThing = "Finka";

            animated = true;
            frames = 9;
            animationSpeed = 0.125f;
        }
    }
}

