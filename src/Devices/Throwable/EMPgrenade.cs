using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Grenades")]
    [BaggedProperty("isFatal", false)]

    public class EMPgrenade : Throwable
    {
        public Electricity el;
        public EMPgrenade(float xval, float yval) : base(xval, yval)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Devices/EMP.png"), 16, 16, false);
            graphic = _sprite;
            center = new Vec2(7f, 7f);
            collisionOffset = new Vec2(-4f, -5f);
            collisionSize = new Vec2(8f, 12f);
            bouncy = 0.4f;
            friction = 0.05f;
            UsageCount = 3;
            index = 9;

            drawTraectory = true;
            minimalTimeOfHolding = 0.25f;
            needsToBeGentle = false;
        }

        public override void SetRocky()
        {
            rock = new EMPgrenadeAP(position.x, position.y);
            base.SetRocky();
        }
    }

    public class EMPgrenadeAP : Rocky
    {
        public Electricity el;

        public EMPgrenadeAP(float xval, float yval) : base(xval, yval)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Devices/EMP.png"), 16, 16, false);
            graphic = _sprite;
            center = new Vec2(7f, 7f);
            collisionOffset = new Vec2(-4f, -4f);
            collisionSize = new Vec2(8f, 8f);
            bouncy = 0.4f;
            friction = 0.05f;

            isGrenade = true;
            detonate = true;
            catchableByADS = true;
            breakable = false;

            canPickUp = false;

            time = 2f;
        }

        public override void DetonateFull()
        {
            DuckNetwork.SendToEveryone(new NMSoundSource(position, 720, "SFX/Devices/EMP.wav", "J"));
            Level.Add(new SoundSource(position.x, position.y, 720, "SFX/Devices/EMP.wav", "J"));
            Level.Add(new EMP(position.x, position.y, 14f, 80f) { team = team});
            base.DetonateFull();
        }

        
        public override void Update()
        {
            base.Update();
            angle += hSpeed;

            if(el == null)
            {
                el = new Electricity(x, y, 16f);
                Level.Add(el);
            }
            if(ElectroFrames <= 0)
            {
                Level.Add(new Electricity(x, y, 32f, alpha));
                ElectroFrames = 4;
            }
            if(ElectroFrames > 0)
            {
                ElectroFrames--;
            }
        }
    }
}