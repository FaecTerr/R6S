using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Reflection;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Grenades")]
    [BaggedProperty("isFatal", false)]
    public class FragGrenade : Throwable
    {
        SinWave _pulse = 1f;
        public FragGrenade(float xval, float yval) : base(xval, yval)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Devices/FragGrenade.png"), 16, 16, false);
            graphic = _sprite;
            center = new Vec2(7f, 7f);
            collisionOffset = new Vec2(-4f, -5f);
            collisionSize = new Vec2(8f, 12f);
            isGrenade = true;
            Timer = 2.5f;
            bouncy = 0.4f;
            friction = 0.05f;
            UsageCount = 2;
            sticky = false;
            index = 9;

            DeviceCost = 5;
            descriptionPoints = "Frag grenade";
            drawTraectory = true; 
            minimalTimeOfHolding = 0.25f;
            needsToBeGentle = false;

            isSecondary = true;
        }

        public override void SetRocky()
        {
            rock = new FragGrenadeAP(position.x, position.y);
            base.SetRocky();
        }

        public override void Update()
        {
            if(oper != null)
            {
                if(oper.holdObject == this)
                {
                    Timer -= 0.01666666f;
                    oper._aim.alpha = (float)Math.Sin(Timer % 2.5f * 3 * Math.PI) * 0.5f + 0.5f;
                    if(Timer <= 0)
                    {
                        Throw();
                    }
                }
                else
                {
                    Timer = 2.5f;
                }
            }
            else
            {
                Timer = 2.5f;
            }
            base.Update();
        }

        public override void Throw()
        {
            oper._aim.alpha = 1f;
            base.Throw();
        }
    }
    public class FragGrenadeAP : Rocky
    {
        public FragGrenadeAP(float xval, float yval) : base(xval, yval)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Devices/FragGrenade.png"), 16, 16, false);
            graphic = _sprite;
            center = new Vec2(8f, 8f);
            collisionOffset = new Vec2(-4f, -4f);
            collisionSize = new Vec2(8f, 8f);
            isGrenade = true;
            bouncy = 0.4f;
            friction = 0.05f;
            sticky = false;

            breakable = true;

            jammResistance = true;
            isGrenade = true;
            breakable = false;
            catchableByADS = true;
        }

        public override void DetonateFull()
        {
           
        }

        public override void Update()
        {
            if(time > 0)
            {
                time -= 0.01666666f;
            }
            else
            {
                Explode();
            }
            base.Update();
        }

        public override void OnDrawLayer(Layer layer)
        {
            if (layer == Layer.Foreground && !(Level.current is Editor))
            {
                foreach (Operators o in Level.CheckCircleAll<Operators>(position, 48))
                {
                    if (o.local)
                    {
                        SpriteMap _dir = new SpriteMap(GetPath("Sprites/GUI/GrenadeAware.png"), 13, 13);

                        _dir.center = new Vec2(6.5f, 18f);

                        /*if (position.x > o.position.x)
                        {
                            _dir.angle = (float)Math.Atan(-(position.x - o.position.x) / (position.y - o.position.y));
                        }
                        else
                        {
                            _dir.angle = 180 + (float)Math.Atan(-(position.x - o.position.x) / (position.y - o.position.y));
                        }*/

                        _dir.angleDegrees = 360 - Maths.PointDirection(o.position, position) + 90;

                        SpriteMap _gren = new SpriteMap(GetPath("Sprites/GUI/GrenadeAware.png"), 13, 13);
                        _gren.frame = 1;

                        if ((o.position - position).length < 30)
                        {
                            _dir.color = Color.Red;
                            _gren.color = Color.Red;
                        }

                        _gren.CenterOrigin();


                        Vec2 pos = Level.current.camera.position + Level.current.camera.size / 2;

                        Graphics.Draw(_dir, pos.x, pos.y);
                        Graphics.Draw(_gren, pos.x, pos.y);
                    }
                }
            }


            base.OnDrawLayer(layer);
        }

        public virtual void Explode()
        {
            Level.Add(new Explosion(position.x, position.y, 48, 185, "N") { shootedBy = oper });
            Level.Add(new Explosion(position.x, position.y, 32, 40, "S") { shootedBy = oper });
            Level.Add(new Explosion(position.x, position.y, 14, 20, "E") { shootedBy = oper });

            if (oper != null && oper.local)
            {
                DuckNetwork.SendToEveryone(new NMExplosion(position, 48, 185, "N", oper));
                DuckNetwork.SendToEveryone(new NMExplosion(position, 32, 40, "S", oper));
                DuckNetwork.SendToEveryone(new NMExplosion(position, 14, 20, "E", oper));
            }

            Level.Add(new SoundSource(position.x, position.y, 640, "SFX/explo/frag_grenad.wav", "J"));
            DuckNetwork.SendToEveryone(new NMSoundSource(position, 640, "SFX/explo/frag_grenade.wav", "J"));

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
            if (isServerForObject)
            {
                for (int i = 0; i < 40; i++)
                {
                    float dir = i * 9f - 5f;
                    ATShrapnel shrap = new ATShrapnel();
                    shrap.range = 48f;
                    Bullet bullet = new Bullet(position.x + (float)(Math.Cos(Maths.DegToRad(dir)) * 6.0), position.y - (float)(Math.Sin(Maths.DegToRad(dir)) * 6.0), shrap, dir, null, false, -1f, false, true);
                    Level.Add(bullet);
                    firedBullets.Add(bullet);
                    if (Network.isActive)
                    {
                        NMFireGun gunEvent = new NMFireGun(null, firedBullets, 20, false, 4, false);
                        Send.Message(gunEvent, NetMessagePriority.ReliableOrdered);
                        firedBullets.Clear();
                    }
                    Level.Remove(this);
                }
            }
        }
    }
}
 