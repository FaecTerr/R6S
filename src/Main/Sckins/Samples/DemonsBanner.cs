using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class DemonsBanner : SkinElement
    {
        public DemonsBanner()
        {
            name = "DemonsBanner";
            location = "banner6";
            AppearenceName = "Demons";

            type = 2;

            rarity = 2;
            mainThing = "Banner";
        }
    }
}