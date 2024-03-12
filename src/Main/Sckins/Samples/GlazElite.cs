using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class GlazElite : SkinElement
    {
        public GlazElite()
        {
            name = "Elite (glaz)";
            location = "glazHatskin2";
            AppearenceName = "Lethal citizen";

            type = 1;

            rarity = 4;
            mainThing = "Glaz";

            customWinSound = true;
            winSound = "";

            customReloadEffect = true;
            reloadEffect = "";

            taunt = true;
            tauntPath = "";
        }
    }
}

