using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class BannerAnarchy : SkinElement
    {
        public BannerAnarchy()
        {
            name = "AnarchyBanner";
            location = "bannerAnarchy";
            AppearenceName = "Anarchy";

            type = 2;

            rarity = 2;
            mainThing = "Banner";
        }
    }
}
