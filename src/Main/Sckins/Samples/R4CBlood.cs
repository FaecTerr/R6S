using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class R4CBlood : SkinElement
    {
        public R4CBlood()
        {
            name = "Blood track";
            location = "R4Cskin0";
            AppearenceName = name;

            type = 0;

            rarity = 1;
            mainThing = "R4C";
        }
    }
}

