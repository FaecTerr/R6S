using System;
using System.Collections.Generic;


namespace DuckGame.R6S
{
    public class NMPickUpDefuser : NMEvent
    {
        public byte OperID;
        public Vec2 pos;

        public NMPickUpDefuser()
        {
        }

        public NMPickUpDefuser(Vec2 p, byte ID)
        {
            OperID = ID;
            pos = p;
        }

        public override void Activate()
        {
            Profile p = DuckNetwork.profiles[OperID];
            Operators oper = null;
            if (p != null && p.duck != null && p.duck.HasEquipment(typeof(OPEQ)))
            {
                OPEQ opeq = p.duck.GetEquipment(typeof(OPEQ)) as OPEQ;
                if(opeq != null && opeq.oper != null && opeq.oper.team == "Att")
                {
                    oper = opeq.oper;
                    oper.HasDefuser = true;
                }
            }

            foreach (Operators op in Level.current.things[typeof(Operators)])
            {
                if (op != oper)
                {
                    op.HasDefuser = false;
                    DuckNetwork.SendToEveryone(new NMLoserDefuser(op));
                }
            }

            foreach (IDP idp in Level.CheckPointAll<IDP>(pos))
            {
                if (idp != null)
                {
                    Level.Remove(idp);
                }
            }
        }
    }
}