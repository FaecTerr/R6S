using System;
using System.Collections.Generic;


namespace DuckGame.R6S
{
    public class NMChangeState : NMEvent
    {
        public string mode = "";
        public Operators oper;

        public NMChangeState()
        {
        }

        public NMChangeState(string state, Operators op)
        {
            mode = state;
            oper = op;
        }

        public override void Activate()
        {
            if (oper != null && mode != null && mode != "")
            {
                if (mode == "stand")
                {
                    oper.mode = "normal";
                }
                if (mode == "crouch")
                {
                    oper.mode = "crouch";
                }
                if (mode == "slide")
                {
                    oper.mode = "slide";
                }
                if (mode == "reppel")
                {
                    oper.mode = "reppel";
                }

                if (mode == "sprint")
                {
                    oper.sprinting = true;
                }
                if (mode == "stopSprint")
                {
                    oper.sprinting = false;
                }

                if(mode == "injure")
                {
                    oper.Injure();
                }
                if (mode == "resetFrom")
                {
                    oper.resetFromDBNO();
                }

                if (mode == "fire")
                {
                    if(oper.holdObject is GunDev)
                    {
                        (oper.holdObject as GunDev).Fire();
                    }
                }
            }
        }
    }
}