using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class MontagneFull : ExPoD
    {
        public MontagneFull(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Devices/HandShield.png"), 32, 32, false);
            graphic = this._sprite;
            _sprite.frame = 0;
            center = new Vec2(16f, 16f);
            setTime = 0.8f;
            weight = 0.9f;
            thickness = 0.1f;
            placeable = false;
            breakable = true;
            zeroSpeed = false;

            index = 13;
            requiresTakeOut = false;
        }

        public override void PocketActivation()
        {
            base.PocketActivation();
            if (user != null)
            {
                if (user.MainGun != null /*&& oper.holdObject == this */)
                {
                    if (user.MainGun is HandShield && user.local)
                    {
                        HandShield h = user.MainGun as HandShield;

                        if (user.holdObject == h)
                        {
                            h.opened = !h.opened;

                            DuckNetwork.SendToEveryone(new NMUpdateShieldState(h, h.opened));

                            if (h.opened == true)
                            {
                                Level.Add(new SoundSource(h.position.x, h.position.y, 300, "SFX/Devices/MontyShieldOpen.wav", "J"));
                                DuckNetwork.SendToEveryone(new NMSoundSource(h.position, 300, "SFX/Devices/MontyShieldOpen.wav", "J"));
                            }
                            else
                            {
                                Level.Add(new SoundSource(h.position.x, h.position.y, 300, "SFX/Devices/MontyShieldClose.wav", "J"));
                                DuckNetwork.SendToEveryone(new NMSoundSource(h.position, 300, "SFX/Devices/MontyShieldClose.wav", "J"));
                            }
                        }
                        else
                        {
                            user.TakeOutInventoryItem(1);
                        }
                    }
                }
            }
        }
    }
}
