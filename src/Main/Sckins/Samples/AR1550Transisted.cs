using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class AR1550Transisted : SkinElement
    {
        public AR1550Transisted()
        {
            name = "Transisted";
            location = "AR1550skin0";
            AppearenceName = name;

            type = 0;

            rarity = 3;
            mainThing = "AR1550";

            animated = true;
            frames = 3;
            animationSpeed = 0.02f;

            trackType = 4;
        }
    }
}

