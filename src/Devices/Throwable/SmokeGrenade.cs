using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    /// <summary>
    /// SmokeGrenade itself, inventory variant
    /// </summary>
    //[EditorGroup("Faecterr's|Grenades")]
    [BaggedProperty("isFatal", false)]    
    public class SmokeGrenade : Throwable
    {
        public float Time;

        public SmokeGrenade(float xval, float yval) : base(xval, yval)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Devices/SmokeGrenade.png"), 16, 16, false);
            graphic = _sprite;
            center = new Vec2(7f, 7f);
            collisionOffset = new Vec2(-4f, -5f);
            collisionSize = new Vec2(8f, 12f);

            Timer = 1f;
            bouncy = 0.4f;
            friction = 0.05f;

            isGrenade = true;
            index = 11;
            UsageCount = 2;

            DeviceCost = 15;
            descriptionPoints = "Smoke grenade";
            drawTraectory = true;

            isSecondary = true;
        }

        public override void SetRocky()
        {
            rock = new SmokeGrenadeAP(position.x, position.y);
            base.SetRocky();
        }
    }
    public class SmokeGrenadeAP : Rocky
    {
        private int ammo = 17;
        private int waitFrame = 20;
        public float Time;

        public SmokeGrenadeAP(float xval, float yval) : base(xval, yval)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Devices/SmokeGrenade.png"), 16, 16, false);
            graphic = _sprite;
            center = new Vec2(7f, 7f);
            collisionOffset = new Vec2(-4f, -5f);
            collisionSize = new Vec2(8f, 12f);
            
            bouncy = 0.4f;
            friction = 0.05f;

            jammResistance = true;
            isGrenade = true;
            catchableByADS = true;
            breakable = false;
        }

        public override void DetonateFull()
        {

        }

        public override void Update()
        {
            if(waitFrame <= 0)
            {
                if(ammo > 12)
                {
                    if(ammo > 16)
                    {
                        Level.Add(new SoundSource(position.x, position.y, 360, "SFX/Devices/SmokeOpen.wav", "J"));
                        DuckNetwork.SendToEveryone(new NMSoundSource(position, 360, "SFX/Devices/SmokeOpen.wav", "J"));
                    }
                    waitFrame = 10;
                    Level.Add(new SmokeGR(position.x, position.y, 1.5f - 0.1f * (18 - ammo)) { order = 1});
                }
                else
                {
                    waitFrame = 30;
                    Level.Add(new SmokeGR(position.x, position.y, 3.5f) { order = 0.3f - 0.03f * ammo});
                }
                ammo--;
            }
            else
            {
                waitFrame--;
            }
            if(ammo <= 0)
            {
                Level.Remove(this);
            }
            base.Update();
        }
    }
}

