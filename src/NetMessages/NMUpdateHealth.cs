using System;
using System.Collections.Generic;


namespace DuckGame.R6S
{
    public class NMUpdateHealth : NMEvent
    {
        public ulong netIndex = 999;
        public int h = -1;

        public NMUpdateHealth()
        {
        }

        public NMUpdateHealth(ulong net, int health)
        {
            netIndex = net; 
            h = health;
        }

        public override void Activate()
        {
            if(netIndex != 999 && h >= 0)
            { 
                foreach(Operators op in Level.current.things[typeof(Operators)])
                {
                    if (op.netIndex == netIndex)
                    {
                        op.Health = h;
                    }
                }
            }
            //DevConsole.Log(oper.name + "'s health is " + Convert.ToString(h));

        }
    }
}