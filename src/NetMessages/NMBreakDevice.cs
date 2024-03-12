using System;
using System.Collections.Generic;


namespace DuckGame.R6S
{
    public class NMBreakDevice : NMEvent
    {
        public Device device;

        public NMBreakDevice()
        {

        }

        public NMBreakDevice(Device d)
        {
            device = d;
        }

        public override void Activate()
        {
            if (device != null)
            {
                device.Break();
            }
        }
    }
}