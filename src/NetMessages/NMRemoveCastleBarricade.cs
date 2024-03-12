using System;
using System.Collections.Generic;


namespace DuckGame.R6S
{
    public class NMRemoveCastleBarricade : NMEvent
    {
        public CastleBarricadeAP barricade;

        public NMRemoveCastleBarricade()
        {
        }

        public NMRemoveCastleBarricade(CastleBarricadeAP castle)
        {
            barricade = castle;
        }

        public override void Activate()
        {
            if (barricade != null)
            {
                barricade.OnPickUp();
            }
        }
    }
}