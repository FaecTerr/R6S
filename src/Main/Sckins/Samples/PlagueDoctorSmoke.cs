using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class PlagueDoctorSmoke : SkinElement
    {
        public PlagueDoctorSmoke()
        {
            name = "Plague Doctor";
            location = "smokeHatskin0";
            AppearenceName = name;

            type = 1;

            rarity = 2;
            mainThing = "Smoke";
        }
    }
}

