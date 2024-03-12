using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class JackalParty : SkinElement
    {
        public JackalParty()
        {
            name = "Jackal party";
            location = "JackalHeadskin1";
            AppearenceName = name;

            type = 1;

            rarity = 3;
            mainThing = "Jackal";

            animated = true;
            frames = 4;
            animationSpeed = 0.08f;
        }
    }
}

