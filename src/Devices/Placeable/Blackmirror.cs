using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Reflection;

namespace DuckGame.R6S
{
    public class BlackMirror : Throwable
    {
        public BlackMirror(float xval, float yval) : base(xval, yval)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Devices/BlackMirror.png"), 16, 16, false);
            graphic = _sprite;
            center = new Vec2(8f, 8f);
            collisionOffset = new Vec2(-5f, -3f);
            collisionSize = new Vec2(10f, 6f);
            weight = 0.9f;
            thickness = 0.1f;
            placeable = false;
            scannable = false;

            sticky = true;
            UsageCount = 2;
            index = 44;
            closeDeployment = true;

            setTime = 2.5f;

            descriptionPoints = "Black mirror";
            DeviceCost = 15;

            //placeSound = "SFX/Devices/FuzeChargePlace.wav";
        }

        public override void SetRocky()
        {
            rock = new BlackMirrorAP(position.x, position.y);
            base.SetRocky();
        }
    }
    public class BlackMirrorAP : Rocky
    {
        public bool init;
        public BlackMirrorAP(float xval, float yval) : base(xval, yval)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Devices/BlackMirror.png"), 16, 16, false);
            graphic = _sprite;
            center = new Vec2(8f, 8f);
            collisionOffset = new Vec2(-3f, -3f);
            collisionSize = new Vec2(6f, 6f);
            _sprite.frame = 1;

            weight = 0.9f;
            thickness = 6f;
            placeable = false;
            breakable = true;
            scannable = false;
            zeroSpeed = false;
            bulletproof = true;
            sticky = true;

            DeviceCost = 5;
            destroyedPoints = "Black mirror destroyed";
        }

        public override void Set()
        {
            base.Set();
        }

        public override void DetonateFull()
        {
            base.DetonateFull();
        }

        public override void Update()
        {
            canPick = false;
            canPickUp = false;
            if (!init && setted)
            {
                foreach (BreakableSurface b in Level.CheckLineAll<BreakableSurface>(position + new Vec2(16 * Dir.x, 16 * Dir.y), position))
                {
                    if (b.type != "U")
                    {
                        if (b.height > 6 && Dir.x != 0)
                        {
                            Vec2 prevPos = b.position;
                            b.hitPoses.Add(position.y - b.position.y);
                            b.thicks.Add(6f);
                            b.UpdateBreaking();
                        }
                        if (b.width > 6 && Dir.y != 0)
                        {
                            Vec2 prevPos = b.position;
                            b.hitPoses.Add(position.x - b.position.x);
                            b.thicks.Add(6f);
                            b.UpdateBreaking();
                        }
                    }
                }
                _sprite.centerx += Dir.x * 4;
                _sprite.centery += Dir.y * 4;
                init = true;
            }

            if (stickedTo != null)
            {
                _sprite.frame = 1;
                /*if(stickedTo is BreakableSurface)
                {
                    BreakableSurface b = stickedTo as BreakableSurface;

                    if (b.type == "U")
                    {
                        stickedTo = null;
                    }
                    else
                    {
                        if (b.height > 6 && Dir.x != 0)
                        {
                            Vec2 prevPos = b.position;
                            b.hitPoses.Add(position.y - b.position.y - 2);
                            b.thicks.Add(0.1f);
                            b.hitPoses.Add(position.y - b.position.y + 2);
                            b.thicks.Add(0.1f);
                            b.UpdateBreaking();

                            stickedTo = Level.CheckCircle<BreakableSurface>(prevPos, 2);
                            if (stickedTo != null)
                            {
                                Level.Remove(stickedTo);
                            }
                            stickedTo = null;
                        }
                        if (b.width > 6 && Dir.y != 0)
                        {
                            Vec2 prevPos = b.position;
                            b.hitPoses.Add(position.x - b.position.x - 2);
                            b.thicks.Add(0.1f);
                            b.hitPoses.Add(position.x - b.position.x + 2);
                            b.thicks.Add(0.1f);
                            b.UpdateBreaking();

                            stickedTo = Level.CheckCircle<BreakableSurface>(prevPos, 2);
                            if (stickedTo != null)
                            {
                                Level.Remove(stickedTo);
                            }
                            stickedTo = null;
                        }
                    }
                }*/
            }
            else
            {
                _sprite.frame = 1;
                /*foreach(Block b in Level.current.things[typeof(Block)])
                {
                    stickedTo = b;
                }*/
                /*stickedTo = Level.CheckLine<BreakableSurface>(position + new Vec2(1 * Dir.x * 16, 1 * Dir.y * 16), position + new Vec2(1 * Dir.x * 16, 1 * Dir.y * 16));
                if (stickedTo != null && (stickedTo as BreakableSurface).type != "U")
                {
                    stickedTo = null;                    
                }*/
            }
            base.Update();
        }
        public override bool Hit(Bullet bullet, Vec2 hitPos)
        {
            Vec2 relPos = hitPos - position;
            //DevConsole.Log(Convert.ToString(relPos));
            if (Dir.x != 0)
            {
                if (relPos.y > 0 && relPos.x * offDir < 0)
                {
                    DetonateFull();
                }
            }
            if (Dir.y > 0)
            {
                if (relPos.y < 0 && relPos.x * offDir < 0)
                {
                    DetonateFull();
                }
            }
            if (Dir.y < 0)
            {
                if (relPos.y > 0 && relPos.x * offDir > 0)
                {
                    DetonateFull();
                }
            }
            return base.Hit(bullet, hitPos);
        }

        public override void Draw()
        {
            Graphics.DrawLine(position + new Vec2(16 * Dir.x, 16 * Dir.y), position, Color.White, 1f);            
            base.Draw();
        }
    }

}