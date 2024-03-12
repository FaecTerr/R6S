using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Devices|Deployable")]
    public class DeployableShield : Placeable
    {
        public DeployableShield(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Devices/Shield.png"), 16, 16, false);
            graphic = this._sprite;
            _sprite.frame = 0;
            center = new Vec2(8f, 8f);
            collisionSize = new Vec2(12f, 6f);
            collisionOffset = new Vec2(-6f, -3f);
            setTime = 0.8f;
            CheckRect = new Vec2(24f, 0f);
            electricible = true;
            scannable = false;

            breakable = true;
            bulletproof = true;
            destructingByHands = false;
            health = 1000;

            DeviceCost = 10;
            descriptionPoints = "Shield deployed";

            destroyedPoints = "Shield destroyed";

            UsageCount = 1;

            cantProne = true;
            isSecondary = true; 

            placeSound = "SFX/Devices/ShieldPlaceM.wav";
        }
        public override void Set()
        {
            base.Set();
        }

        public override void SetAfterPlace()
        {
            afterPlace = new DeployableShieldAP(position.x, position.y);
            base.SetAfterPlace();
        }

        public override void Update()
        {
            if(oper != null && oper.female)
            {
                placeSound = "SFX/Devices/ShieldPlaceF.wav";
            }
            base.Update();
        }
    }


    public class DeployableShieldAP : Device
    {
        public DeployableShieldAP(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Devices/DPshield.png"), 16, 20, false);
            graphic = _sprite;
            _sprite.frame = 0;
            center = new Vec2(8f, 13f);
            collisionSize = new Vec2(14f, 14f);
            collisionOffset = new Vec2(-7f, -7f);
            electricible = true;
            scannable = false;

            jammResistance = true;

            breakable = true;
            bulletproof = true;
            destructingByHands = false;
        }

        public override void Set()
        {
            base.Set();

            //Level.Add(new SoundSource(position.x, position.y, 200, "SFX/ShieldPlace.wav", "J"));

        }
        public override void Update()
        {
            base.Update();
            thickness = 10f;
            _sprite.frame = 1;
            foreach (PhysicsObject po in Level.CheckRectAll<PhysicsObject>(topLeft + new Vec2(0f, 8f), bottomRight))
            {
                if (po != null && po != this && !(po is DeployableShieldAP))
                {
                    if (po.enablePhysics == true && !(po is Operators))
                    {
                        if (po is Device)
                        {
                            if ((po as Device).mainDevice != null && po is Throwable)
                            {
                                if ((po as Throwable).isGrenade)
                                {
                                    if (position.x < po.position.x && po.hSpeed < 0)
                                    {
                                        po.hSpeed = 0.6f;
                                    }
                                    if (position.x > po.position.x && po.hSpeed > 0)
                                    {
                                        po.hSpeed = 0.6f;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (position.x < po.position.x && po.hSpeed < 0)
                            {
                                po.hSpeed = 0.6f;
                            }
                            if (position.x > po.position.x && po.hSpeed > 0)
                            {
                                po.hSpeed = 0.6f;
                            }
                        }
                    }
                    if (po is Operators)
                    {
                        Operators op = po as Operators;
                        if (op.grounded)
                        {
                            if (position.x < op.position.x)
                            {
                                op.hSpeed = 0.7f + Math.Abs(op.hSpeed) * 0.7f;
                            }
                            if (position.x > op.position.x)
                            {
                                op.hSpeed = -0.7f - Math.Abs(op.hSpeed) * 0.7f;
                            }
                        }
                        else
                        {
                            op.unableToMove = 10;
                            op.hSpeed = op.offDir * 0.6f;
                            op.vSpeed = -0.1f;
                        }
                    }
                }
            }
        }

        public override bool Hit(Bullet bullet, Vec2 hitPos)
        {
            Level.Add(new SoundSource(position.x, position.y, 160, "SFX/Devices/metalTing.wav", "J"));
            DuckNetwork.SendToEveryone(new NMSoundSource(position, 160, "SFX/Devices/metalTing.wav", "J"));

            return base.Hit(bullet, hitPos);
        }
    }
}