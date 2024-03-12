using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Guns|Devices|Traps")]
    public class Claymore : Placeable
    {
        public Claymore(float xpos, float ypos) : base(xpos, ypos)
        {
            this._sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Devices/Claymore.png"), 12, 8, false);
            this.graphic = this._sprite;
            this._sprite.frame = 1;
            this.center = new Vec2(6f, 4f);
            this.collisionSize = new Vec2(10f, 6f);
            this.collisionOffset = new Vec2(-5f, -4f);
            setTime = 0.7f;
            breakable = true;
            placeable = true;
            CheckRect = new Vec2(10f, 2f);
            UsageCount = 1;
            index = 12;

            DeviceCost = 15;

            destroyedPoints = "Claymore destroyed";

            cantProne = true;
            isSecondary = true;

            placeSound = "SFX/Devices/ClaymorePlace.wav";
        }

        public override void SetAfterPlace()
        {
            afterPlace = new ClaymoreAP(position.x, position.y);
            base.SetAfterPlace();
        }
    }

    public class ClaymoreAP : Device
    {
        public List<Bullet> firedBullets = new List<Bullet>();
        protected Sprite _sightHit;
        public bool deactivate;
        public ClaymoreAP(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Devices/Claymore.png"), 12, 8, false);
            graphic = this._sprite;
            _sprite.frame = 1;
            center = new Vec2(6f, 4f);
            collisionSize = new Vec2(10f, 6f);
            collisionOffset = new Vec2(-5f, -4f);
            _sightHit = new Sprite("laserSightHit", 0f, 0f);
            _sightHit.CenterOrigin();
            setTime = 0.7f;
            breakable = true;
            destructingByElectricity = true;

            Level.Add(new SoundSource(position.x, position.y, 160, "SFX/Devices/Claymore.wav", "J"));
            DuckNetwork.SendToEveryone(new NMSoundSource(position, 160, "SFX/Devices/Claymore.wav", "J"));
        }  

        public virtual void Explode()
        {
            Level.Add(new Explosion(position.x, position.y, 32, 140, "N") { shootedBy = oper });
            Level.Add(new Explosion(position.x, position.y, 14, 30, "S") { shootedBy = oper });

            DuckNetwork.SendToEveryone(new NMExplosion(position, 32, 140, "N", oper));
            DuckNetwork.SendToEveryone(new NMExplosion(position, 14, 30, "S", oper));

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
            SFX.Play("explode", 1f, 0f, 0f, false);
        }

        public override void Set()
        {
            base.Set();
            //Sound
        }

        public override void Update()
        {
            base.Update();
            _sightHit.scale = new Vec2(0.5f, 0.5f);
            if (setted == true)
            {
                foreach (Operators d in Level.CheckRectAll<Operators>(topLeft + new Vec2(0f, -16f), bottomRight))
                {
                    if (d.team != team && !jammed && !deactivate)
                    {
                        Explode();
                    }
                }
                Upstairs upstairs = Level.CheckRect<Upstairs>(topLeft, bottomRight);
                if(upstairs != null)
                {
                    deactivate = true;
                }
                else
                {
                    deactivate = false;
                }
            }
        }

        public override void Draw()
        {
            base.Draw();
            if (setted == true && !jammed)
            {
                for (int i = 0; i < 3; i++)
                {
                    ATTracer tracer = new ATTracer();
                    tracer.range = 18f;
                    float a = 115f - i * 25f;
                    Vec2 pos = Offset(default(Vec2));
                    tracer.penetration = 0f;
                    Bullet b = new Bullet(pos.x, pos.y, tracer, a, owner, false, -1f, true, true);
                    _sightHit.alpha = 0.3f;
                    _sightHit.color = Color.Red;
                    if (deactivate)
                    {
                        _sightHit.color = Color.Blue;
                    }
                    Graphics.Draw(_sightHit, b.end.x, b.end.y);
                }
            }
        }
    }
}
