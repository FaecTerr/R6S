using System;
using System.Collections.Generic;


namespace DuckGame.R6S
{
    public class NMLoserDefuser : NMEvent
    {
        public Operators op;

        public NMLoserDefuser()
        {
        }

        public NMLoserDefuser(Operators oper)
        {
            op = oper;
        }

        public override void Activate()
        {
            op.HasDefuser = false;
        }
    }
}