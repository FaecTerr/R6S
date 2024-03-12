using System;
using System.Collections.Generic;


namespace DuckGame.R6S
{
    public class NMSendInfo : NMEvent
    {
        public Operators oper;
        public ulong index;
        public string n;
        public Duck d;

        public NMSendInfo()
        {

        }

        public NMSendInfo(Operators op, string name, ulong netIndex, Duck duck)
        {
            oper = op;
            index = netIndex;
            n = name;
            d = duck;
        }

        public override void Activate()
        {
            if (oper != null)
            {
                oper.netIndex = index;
                oper.name = n;

                int operatorID = oper.operatorID;

                Vec2 position = new Vec2(-999999f, -999999f);

                OPEQ op = PlayerStats.GetOpeqByID(operatorID, position);

                op.netIndex = index;
                oper.opeq = op;
                op.oper = oper;
                oper.opeq.netIndex = index;

                if (d != null)
                {
                    d.Fondle(oper);
                }


                foreach(Duck duck in Level.current.things[typeof(Duck)])
                {
                    if(duck.netProfileIndex == index && oper.duckOwner != duck && !duck.profile.localPlayer && !oper.local)
                    {
                        //oper.opeq.duckOwner = duck;
                        //oper.duckOwner = duck;
                        //d = duck;
                    }
                }

                /*if (d != null)
                {
                    oper.duckOwner = d;
                    oper.opeq.duckOwner = d;
                }*/
            }
        }
    }
}