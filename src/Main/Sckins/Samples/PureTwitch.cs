using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class PureTwitch : SkinElement
    {
        public PureTwitch()
        {
            name = "Pure";
            location = "twitchHatskin0";
            AppearenceName = name;

            type = 1;

            rarity = 1;
            mainThing = "Twitch";
        }
    }
}

