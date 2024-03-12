using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class C4 : Throwable
    {
        public C4(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Devices/C4.png"), 16, 16, false);
            graphic = _sprite;
            _sprite.frame = 0;
            setTime = 0.5f;
            center = new Vec2(8f, 8f);
            collisionSize = new Vec2(12f, 12f);
            collisionOffset = new Vec2(-6f, -6f);
            scannable = false;

            doubleActivation = true;
            inPlace = false;
            UsageCount = 1;

            index = 2;
            weightR = 5;

            delay = 0.5f;
            activationDelay = 0.4f;

            DeviceCost = 10;
            descriptionPoints = "Bomb placed";

            drawTraectory = true;
            useCustomHitMarker = true;
            minimalTimeOfHolding = 1;
            needsToBeGentle = false;

            isSecondary = true;
        }

        public override void Update()
        {
            base.Update();
            if (inPlace || UsageCount <= 0)
            {
                drawTraectory = false;
            }
            else
            {
                drawTraectory = true;
            }
        }

        public override void SetRocky()
        {
            rock = new C4thing(position.x, position.y);
            base.SetRocky();
        }
    }

    public class C4thing : Rocky
    {
        public int timer = 25;
        public C4thing(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Devices/C4.png"), 16, 16, false);
            graphic = _sprite;
            _sprite.frame = 1;
            setTime = 0.5f;
            center = new Vec2(8f, 8f);
            collisionSize = new Vec2(8f, 8f);
            collisionOffset = new Vec2(-4f, -4f);
            scannable = true;
            sticky = true;

            destructingByElectricity = true;
            breakable = true;

            gravMultiplier = 0.8f;
        }

        public override void Update()
        {
            if (timer > 0)
            {
                timer--;
            }
            else
            {
                timer = 45;
                Level.Add(new SoundSource(position.x, position.y, 128, "SFX/Devices/C4idlesound.wav", "J"));
                DuckNetwork.SendToEveryone(new NMSoundSource(position, 128, "SFX/Devices/C4idlesound.wav", "J"));
            }
            base.Update();
        }

        public override void DetonateFull()
        {
            Level.Add(new Explosion(position.x, position.y, 102, 165, "N") { shootedBy = oper});
            Level.Add(new Explosion(position.x, position.y, 28, 65, "E") { shootedBy = oper });
            Level.Add(new Explosion(position.x, position.y, 56, 105, "S") { shootedBy = oper });

            DuckNetwork.SendToEveryone(new NMExplosion(position, 102, 165, "N", oper));
            DuckNetwork.SendToEveryone(new NMExplosion(position, 28, 65, "E", oper));
            DuckNetwork.SendToEveryone(new NMExplosion(position, 56, 105, "S", oper));

            Level.Add(new SoundSource(position.x, position.y, 320, "SFX/explo/big_bomb.wav", "J"));
            DuckNetwork.SendToEveryone(new NMSoundSource(position, 320, "SFX/explo/big_bomb.wav", "J"));

            //SFX.Play("explode", 1f, 0f, 0f, false);
            for (int i = 0; i < 20; i++)
            {
                float dir = i * 18f - 5f;
                ATShrapnel shrap = new ATShrapnel();
                shrap.range = 48f;
                Bullet bullet = new Bullet(position.x + (float)(Math.Cos(Maths.DegToRad(dir)) * 6.0), position.y - (float)(Math.Sin(Maths.DegToRad(dir)) * 6.0), shrap, dir, null, false, -1f, false, true);
                bullet.firedFrom = this;
                firedBullets.Add(bullet);
                Level.Add(bullet);
            }
            base.DetonateFull();
        }
    }
}
