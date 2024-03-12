using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class VineBanner : SkinElement
    {
        public VineBanner()
        {
            name = "VineBanner";
            location = "banner5";
            AppearenceName = "Jungles";

            type = 2;

            rarity = 2;
            mainThing = "Banner";
        }
    }
}