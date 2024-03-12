using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class OilSpitPMM : SkinElement
    {
        public OilSpitPMM()
        {
            name = "Oil spit";
            location = "PMMskin0";
            AppearenceName = name;

            type = 0;

            rarity = 1;
            mainThing = "PMM";
        }
    }
}

