using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class BannerEyes : SkinElement
    {
        public BannerEyes()
        {
            name = "ObserversBanner";
            location = "banner7";
            AppearenceName = "Observers";

            type = 2;

            rarity = 2;
            mainThing = "Banner";
        }
    }
}
