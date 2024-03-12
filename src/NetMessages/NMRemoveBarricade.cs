using System;
using System.Collections.Generic;


namespace DuckGame.R6S
{
    public class NMRemoveBarricade : NMEvent
    {
        public WoodenBarricadeAP barricade;

        public NMRemoveBarricade()
        {
        }

        public NMRemoveBarricade(WoodenBarricadeAP barricad)
        {
            barricade = barricad;
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