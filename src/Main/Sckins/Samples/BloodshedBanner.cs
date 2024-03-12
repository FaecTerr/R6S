using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class BloodshedBanner : SkinElement
    {
        public BloodshedBanner()
        {
            name = "Bloodshed";
            location = "banner4";
            AppearenceName = "Ther ll b BLOODSHED";

            type = 2;

            rarity = 2;
            mainThing = "Banner";
        }
    }
}