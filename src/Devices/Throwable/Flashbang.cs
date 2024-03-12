using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Reflection;

namespace DuckGame.R6S
{  
    public class FlashbangGrenade : Throwable
    {
        public FlashbangGrenade(float xval, float yval) : base(xval, yval)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Devices/Flashbang.png"), 16, 16, false);
            graphic = _sprite;
            center = new Vec2(7f, 7f);
            collisionOffset = new Vec2(-4f, -5f);
            collisionSize = new Vec2(8f, 12f);
            isGrenade = true;
            Timer = 1.2f;
            bouncy = 0.4f;
            friction = 0.05f;
            UsageCount = 3;
            sticky = false;
            index = 10;

            DeviceCost = 5;
            descriptionPoints = "Flashbang"; 
            minimalTimeOfHolding = 0.5f;
            needsToBeGentle = false;

            isSecondary = true;
        }

        public override void SetRocky()
        {
            rock = new FlashbangGrenadeAP(position.x, position.y);
            base.SetRocky();
        }
    }
    public class FlashbangGrenadeAP : Rocky
    {
        public FlashbangGrenadeAP(float xval, float yval) : base(xval, yval)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Devices/Flashbang.png"), 16, 16, false);
            graphic = _sprite;
            center = new Vec2(7f, 7f);
            collisionOffset = new Vec2(-4f, -5f);
            collisionSize = new Vec2(8f, 12f);
            isGrenade = true;
            time = 1.2f;
            bouncy = 0.4f;
            friction = 0.05f;
            sticky = false;

            isGrenade = true;
            catchableByADS = true;

            breakable = false;
        }

        public override void DetonateFull()
        {
            Flash();
            
            base.DetonateFull();
        }

        public virtual void QuickFlash()
        {
            Graphics.flashAdd = 1.3f;
        }

        public virtual void Flash()
        {
            Level.Add(new Flashlight(position.x, position.y));
            DuckNetwork.SendToEveryone(new NMFlash(position));

            Level.Add(new SoundSource(position.x, position.y, 480, "SFX/Devices/flashGrenadeExplode.wav", "J"));
            DuckNetwork.SendToEveryone(new NMSoundSource(position, 480, "SFX/Devices/flashGrenadeExplode.wav", "J"));

            foreach (Terrorist t in Level.CheckCircleAll<Terrorist>(position, 128))
            {
                t.flashedFrames = (int)(300 * ((128 - (t.position - position).length) / 128)) + 120;
            }
        }

        public override void OnDrawLayer(Layer layer)
        {
            if (layer == Layer.Foreground && !(Level.current is Editor))
            {
                foreach (Operators o in Level.CheckCircleAll<Operators>(position, 128))
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
                        _gren.frame = 2;

                        _gren.CenterOrigin();


                        Vec2 pos = Level.current.camera.position + Level.current.camera.size / 2;


                        if (Level.CheckLine<Block>(o.position, position) == null)
                        {
                            Graphics.Draw(_dir, pos.x, pos.y);
                            Graphics.Draw(_gren, pos.x, pos.y);
                        }
                    }
                }
            }


            base.OnDrawLayer(layer);
        }
    }
}