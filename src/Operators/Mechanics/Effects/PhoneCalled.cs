using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class PhoneCalled : Effect
    {
        private int time;

        public PhoneCalled()
        {
            team = "Att";
            type = 1;
            name = "Phone called";
            timer = 16;
            maxTimer = 16;
        }

        public override void TimerOut()
        {
            if (owner != null)
            {
                /*if (owner.holdIndex == 5)
                {
                    owner.ChangeWeapon(30, 1);
                }*/
            }
            base.TimerOut();
        }

        public override void Update()
        {
            if (owner != null)
            {
                if(owner.Phone != null)
                {
                    (owner.Phone as Phone).unable = 30;
                }

                if(time != (int)(timer / 2))
                {
                    time = (int)(timer / 2);

                    Level.Add(new SoundSource(owner.position.x, owner.position.y, 320, "SFX/Devices/PhoneCall.wav", "J"));
                    DuckNetwork.SendToEveryone(new NMSoundSource(owner.position, 320, "SFX/Devices/PhoneCall.wav", "J"));
                }
                
                if(charge > 5)
                {
                    TimerOut();
                    timer = 0;
                    //owner.effects.Remove(this);
                }
            }
            base.Update();
        }
    }
}

