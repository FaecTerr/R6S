using System;
using System.Collections.Generic;


namespace DuckGame.R6S
{
    public class NMSelectedOper : NMEvent
    {
        public int operatorID;

        public Vec2 position;
        public ulong netIndex;
        public string name;

        public NMSelectedOper()
        {
        }

        public NMSelectedOper(int selected, ulong index = 255, string n = "")
        {
            operatorID = selected;
            name = n;
            netIndex = index;
        }

        public override void Activate()
        {
            if (netIndex < 255)
            {
                Duck d = DuckNetwork.profiles[(int)netIndex].duck;
                if (d != null)
                {
                    foreach (GamemodeScripter gm in Level.current.things[typeof(GamemodeScripter)])
                    {
                        //position = gm.position;
                        OPEQ op = new OPEQ(position.x, position.y);
                        op = PlayerStats.GetOpeqByID(operatorID, position);

                        if (op != null)
                        {
                            gm.selectedOperators.Add(op);

                            //op.oper.MainGun = null;

                            op.oper.name = d.profile.name;

                            op.duckOwner = d;
                            op.oper.duckOwner = d;
                            op.owner = d;
                            d.Equip(op);                            

                            //owner.Equip(op);
                            //op.oper.duckOwner = owner;
                            //op.duckOwner = owner;
                            //owner.position = op.oper.position;

                            op.oper.InitializeInv();

                            op.netIndex = netIndex;
                        }
                        //DevConsole.Log(owner.profile.lastKnownName, Color.White);
                    }
                }
            }
        }
    }
}