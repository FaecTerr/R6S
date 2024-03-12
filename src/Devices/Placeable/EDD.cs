using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Guns|Devices|Traps")]
    public class EDD : Placeable
    {
        public EDD(float xval, float yval) : base(xval, yval)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Devices/EDD.png"), 12, 12, false);
            graphic = _sprite;
            setTime = 1.8f;
            center = new Vec2(6f, 6f);
            collisionOffset = new Vec2(-6f, -6f);
            collisionSize = new Vec2(12f, 12f);
            weight = 0.9f;
            thickness = 0.1f;
            //placeable = true;
            breakable = true;
            zeroSpeed = false;
            doorPlacement = true;
            
            index = 6;
            UsageCount = 8;

            DeviceCost = 5;

            destroyedPoints = "Trap destroyed";

            cantProne = true;

            placeSound = "SFX/Devices/EDDPlace.wav";
        }

        public override void Update()
        {
            base.Update();
            setted = false;
        }

        public override void SetAfterPlace()
        {
            afterPlace = new EDDAP(position.x, position.y);
            base.SetAfterPlace();
        }

    }
    public class EDDAP : Device
    {
        float vanish = 1;
        public EDDAP(float xval, float yval) : base(xval, yval)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Devices/EDD.png"), 12, 12, false);
            graphic = _sprite;
            setTime = 1;
            center = new Vec2(6f, 6f);
            collisionOffset = new Vec2(-6f, -6f);
            collisionSize = new Vec2(12f, 12f);
            _sprite.frame = 1;
            _sprite.alpha = 0.2f;
            weight = 0.9f;
            thickness = 0.1f;

            placeable = true;
            breakable = true;
            scannable = true;
            zeroSpeed = false;

            DeviceCost = 5;

            destroyedPoints = "Trap destroyed";
        }


        public override void Update()
        {
            base.Update();
            if(vanish > 0)
            {
                vanish -= 0.01f;
            }

            _sprite.alpha = vanish;
            alpha = vanish;
            gravMultiplier = 0;

            if (jammed)
            {
                vanish = 1;
            }

            if (oper != null && !jammed)
            {
                foreach (Operators duck in Level.CheckCircleAll<Operators>(position, 12))
                {
                    if (duck.team != team)
                    {
                        Explode();
                    }
                }
            }
        }
        public virtual void Explode()
        {
            Level.Add(new Explosion(position.x, position.y, 8, 0, "S") { shootedBy = oper });
            Level.Add(new Explosion(position.x, position.y, 32, 65, "N") { shootedBy = oper });

            DuckNetwork.SendToEveryone(new NMExplosion(position, 8, 0, "S", oper));
            DuckNetwork.SendToEveryone(new NMExplosion(position, 32, 65, "N", oper));

            Level.Add(new SoundSource(position.x, position.y, 320, "SFX/explo/explosion_barrel.wav", "J"));
            DuckNetwork.SendToEveryone(new NMSoundSource(position, 320, "SFX/explo/explosion_barrel.wav", "J"));

            Level.Add(new ExplosionPart(position.x, position.y, true));
            int num = 6;
            if (Graphics.effectsLevel < 2)
            {
                num = 3;
            }
            for (int i = 0; i < num; i++)
            {
                float dir = i * 60f + Rando.Float(-10f, 10f);
                float dist = Rando.Float(20f, 20f);
                ExplosionPart ins = new ExplosionPart(position.x + (float)(Math.Cos(Maths.DegToRad(dir)) * dist), position.y - (float)(Math.Sin(Maths.DegToRad(dir)) * dist), true);
                Level.Add(ins);
            }
            Graphics.FlashScreen();
            //SFX.Play("explode", 1f, 0f, 0f, false);
            Level.Remove(this);
        }
    }
}
