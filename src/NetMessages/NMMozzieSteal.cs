using System;
using System.Collections.Generic;


namespace DuckGame.R6S
{
    public class NMMozzieSteal : NMEvent
    {
        DroneAP _drone;
        Operators oper;
        public NMMozzieSteal()
        {
        }

        public NMMozzieSteal(DroneAP device, Operators op)
        {
            _drone = device;
            oper = op;
        }

        public override void Activate()
        {
            if (_drone != null)
            {
                _drone.team = "Def";
                if (oper != null)
                {
                    _drone.oper = oper;
                    if (oper.duckOwner != null)
                    {
                        _drone.own = oper.duckOwner;
                    }
                }
                _drone.UpdatePhones();
            }
        }
    }
}