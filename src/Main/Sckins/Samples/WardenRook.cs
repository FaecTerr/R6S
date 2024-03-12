using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class WardenRook : SkinElement
    {
        public WardenRook()
        {
            name = "Warden";
            location = "rookHatskin0";
            AppearenceName = name;

            type = 1;

            rarity = 3;
            mainThing = "Rook";
        }
    }
}

