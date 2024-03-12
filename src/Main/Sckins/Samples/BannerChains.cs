using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class BannerChains : SkinElement
    {
        public BannerChains()
        {
            name = "ChainsBanner";
            location = "bannerChains";
            AppearenceName = "Chains";

            type = 2;

            rarity = 2;
            mainThing = "Banner";
        }
    }
}
