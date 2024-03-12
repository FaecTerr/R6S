using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class NyanDoc : SkinElement
    {
        public NyanDoc()
        {
            name = "Nyan UwU";
            location = "docHatskin0";
            AppearenceName = name;

            type = 1;

            rarity = 2;
            mainThing = "Doc";

            animated = true;
            frames = 10;
            animationSpeed = 0.1f;
        }
    }
}

