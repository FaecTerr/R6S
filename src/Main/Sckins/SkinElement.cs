using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class SkinElement
    {
        public string name; //Also ID
        public string location;
        public int type; //0 - Gun, 1 - Headgear, 2 - Banner
        public string AppearenceName; //Replaces name when being dropped

        public int rarity; //0 - common, 1 - rare, 2 - epic, 3 - legendary
        public string mainThing;

        public bool animated;
        public int frames = 4;
        public float animationSpeed = 0.25f;

        public bool customWinSound;
        public string winSound;

        public bool customReloadEffect;
        public string reloadEffect;

        public bool taunt;
        public string tauntPath;

        public int trackType = 0;
    }
}
