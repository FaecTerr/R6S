using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class SlavaKoroluTachanka : SkinElement
    {
        public SlavaKoroluTachanka()
        {
            name = "Slava Korolu";
            location = "tachankaHatskin0";
            AppearenceName = name;

            type = 1;

            rarity = 3;
            mainThing = "Tachankin";

            animated = true;
            frames = 4;

        }
    }
}

