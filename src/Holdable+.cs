using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class Holdable_plus : Holdable
    {
        public Operators user;
        public Operators oper;
        public OPEQ opeq;
        public Duck own;
        public InputProfile input;
        public bool requiresTakeOut = true;
        public int takenSlot;
        public Holdable_plus(float xpos, float ypos) : base(xpos, ypos)
        {

        }

        public override void Update()
        {
            if(owner != null)
            {
                if(owner is Duck)
                {
                    own = owner as Duck;
               
                    if (own.HasEquipment(typeof(OPEQ)))
                    {
                        opeq = own.GetEquipment(typeof(OPEQ)) as OPEQ;
                        oper = opeq.oper;
                        user = oper;
                    }
                    else
                    {
                        opeq = null;
                        oper = null;
                    }
                }
                if(owner is Operators)
                {
                    oper = owner as Operators;
                    opeq = oper.opeq;
                    user = oper;
                    if (oper.duckOwner != null)
                    {
                        own = oper.duckOwner;
                    }
                }
                else
                {
                    oper = null;
                    opeq = null;
                    own = null;
                    
                }
            }
            if(own != null)
            {
                input = own.inputProfile;
            }
            else
            {
                input = null;
            }
            if(user != null && user.holdObject == this)
            {
                //DevConsole.Log(Convert.ToString(takenSlot));
            }
            
            base.Update();

        }
    }
}
