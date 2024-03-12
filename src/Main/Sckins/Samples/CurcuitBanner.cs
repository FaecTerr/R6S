using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class CurcuitBanner : SkinElement
    {
        public CurcuitBanner()
        {
            name = "CurcuitBanner";
            location = "banner2";
            AppearenceName = "Curcuit";

            type = 2;

            rarity = 2;
            mainThing = "Banner";
        }
    }
}