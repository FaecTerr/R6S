using System;
using System.Collections.Generic;


namespace DuckGame.R6S
{
    public class NMSuggestInfo : NMEvent
    {
        public Operators op;

        public NMSuggestInfo()
        {

        }

        public NMSuggestInfo(Operators oper)
        {
            op = oper;
        }

        public override void Activate()
        {
            if (op != null)
            {
                op.InfoSuggested();
            }
        }
    }
}