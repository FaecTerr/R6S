using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class SleepyMonky : SkinElement
    {
        public SleepyMonky()
        {
            name = "Sleepy";
            location = "echoHeadskin0";
            AppearenceName = name;

            type = 1;

            rarity = 1;
            mainThing = "Echo";
        }
    }
}

