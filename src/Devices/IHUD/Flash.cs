using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class Flash : Device
    {
        public Flash(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Devices/FlashShield.png"), 32, 32, false);
            graphic = _sprite;
            _sprite.frame = 0;
            center = new Vec2(16f, 16f);
            collisionSize = new Vec2(12f, 18f);
            collisionOffset = new Vec2(-6f, -9f);
            setTime = 0.8f;
            weight = 0.9f;
            thickness = 0.1f;
            placeable = false;
            breakable = true;
            zeroSpeed = false;
            index = 1;

            UsageCount = 3;
            ShowCounter = true;

            CooldownTime = 10;
            requiresTakeOut = false;
        }
        public override void PocketActivation()
        {
            base.PocketActivation();
            if (user != null && user.MainGun != null)
            {
                if (user.MainGun is FlashShield && user.holdObject == user.MainGun)
                {
                    FlashShield h = user.MainGun as FlashShield;
                    if (!user.HasEffect("Jammed") && Cooldown <= 0 && UsageCount > 0 && user.holdObject != null)
                    {
                        UsageCount--;
                        h.Flash();
                        Cooldown = 1;
                        DuckNetwork.SendToEveryone(new NMFlash(user.holdObject.position));
                    }
                }
            }
        }
        public override void Update()
        {
            base.Update();

            if (Cooldown > 0)
            {
                Cooldown -= 0.01666666f / CooldownTime;
            }

            if (user != null)
            {
                if (Cooldown > 0.0014f && Cooldown < 0.0032f)
                {
                    Level.Add(new SoundSource(user.position.x, user.position.y, 90, "SFX/Devices/FlashReload.wav", "J"));
                    DuckNetwork.SendToEveryone(new NMSoundSource(user.position, 90, "SFX/Devices/FlashReload.wav", "J"));
                }
            }
        }
    }
}
