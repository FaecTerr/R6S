using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    [EditorGroup("Faecterr's|Gamemode")]
    public class GMSTH : GamemodeScripter
    {
        public GMSTH()
        {
            isTH = true;

                preparationPhase = 0;
                actionPhase = 600;

            if (phaseTime.Count > 3)
            {
                phaseTime[2] = 0;
                phaseTime[3] = 600;
            }
            
        }
    }
}
