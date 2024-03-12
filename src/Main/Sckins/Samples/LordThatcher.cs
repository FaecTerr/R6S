using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class LordThatcher : SkinElement
    {
        public LordThatcher()
        {
            name = "Gentleman";
            location = "thatcherHatskin0";
            AppearenceName = name;

            type = 1;

            rarity = 3;
            mainThing = "Thatcher";
        }
    }
}

