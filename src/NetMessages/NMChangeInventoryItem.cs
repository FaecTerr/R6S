using System;
using System.Collections.Generic;


namespace DuckGame.R6S
{
    public class NMChangeInventoryItem : NMEvent
    {
        public int index;
        public Operators oper;
        public int time;

        public NMChangeInventoryItem()
        {
        }

        public NMChangeInventoryItem(int t, int i, Operators op)
        {
            index = i;
            oper = op;
            time = t;
        }

        public override void Activate()
        {
            if (oper != null)
            {
                if (index == -1)
                {
                    oper.holdIndex = 0;
                    oper.holdObject = null;
                    oper.holdObject2 = null;
                }
                else
                {
                    if (index == 0)
                    {
                        oper.KnifeStab(0);
                    }
                    oper.ChangeWeapon(time, index);
                    if (oper.inventory[index] != null)
                    {
                        oper.inventory[index].position = oper.position + oper.handPosition;
                    }
                    //oper.inventory[index].position = oper.position + oper.handPosition;
                }
                foreach (Holdable_plus holdObject in oper.inventory)
                {
                    holdObject.owner = null;
                    holdObject.position = new Vec2(9999999f, -9999999f);
                    holdObject.own = null;
                    holdObject.oper = null;
                    holdObject.opeq = null;


                    //Rework this part
                    /*if (oper.inventory[index] == oper.holdObject)
                    {
                        DevConsole.Log(Convert.ToString(oper.position), Color.White);
                        holdObject.position = oper.position + oper.handPosition;
                        holdObject.offDir = oper.offDir;
                        holdObject._offDir = oper._offDir;
                        holdObject.flipHorizontal = oper.offDir == -1;
                        holdObject.angle = oper.holdAngle;
                        holdObject.depth = 0.6f;
                    }*/
                }
            }
        }
    }
}