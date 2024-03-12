using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class BloodsplatDoc : SkinElement
    {
        public BloodsplatDoc()
        {
            name = "Blood splat";
            location = "docHatskin1";
            AppearenceName = name;

            type = 1;

            rarity = 1;
            mainThing = "Doc";
        }
    }
}

