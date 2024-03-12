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
    public class Candela : Throwable
    {
        public int ammo = 8;
        public Candela(float xval, float yval) : base(xval, yval)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Devices/Candela.png"), 16, 16, false);
            graphic = _sprite;
            center = new Vec2(8f, 8f);
            collisionOffset = new Vec2(-7f, -7f);
            collisionSize = new Vec2(14f, 14f);
            Timer = 1.5f;
            bouncy = 0.4f;
            friction = 0.05f;
            weightR = 8;

            drawTraectory = true;
            index = 37;
            UsageCount = 3;

            placeSound = "SFX/Devices/CandelaBreath.wav"; 
            minimalTimeOfHolding = 0.25f;
            needsToBeGentle = false;
        }

        public override void SetRocky()
        {
            rock = new CandelaAP(position.x, position.y);
            base.SetRocky();
        }
    }

    public class CandelaAP : Rocky
    {
        public int ammo = 8;
        public int chargeFrames = 150;
        public float moveDir;
        int dirChangeLock = 0;
        public CandelaAP(float xval, float yval) : base(xval, yval)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Devices/Candela.png"), 16, 16, false);
            graphic = _sprite;
            _sprite.frame = 1;
            center = new Vec2(8f, 8f);
            collisionOffset = new Vec2(-3f, -3f);
            collisionSize = new Vec2(6f, 6f);
            bouncy = 0.4f;
            friction = 0.05f;

            weight = 1;
            thickness = 0.6f;

            breakable = true;
            
            isGrenade = true;
        }

        public override void Initialize()
        {
            moveDir = Math.Sign(hSpeed) * 1.5f;
            base.Initialize();
        }

        public override void OnImpact(MaterialThing with, ImpactedFrom from)
        {
            if(with != null)
            {
                if((with is Block || with is DeployableShieldAP) && (from == ImpactedFrom.Right || from == ImpactedFrom.Left) && !grounded)
                {
                    //moveDir = -moveDir;
                }
            }
            base.OnImpact(with, from);
        }

        public override void Update()
        {
            if(Level.CheckLine<Block>(position - new Vec2((float)Math.Abs(moveDir)), position + new Vec2((float)Math.Abs(moveDir))) != null && dirChangeLock <= 0)
            {
                moveDir *= -1;
                dirChangeLock = 30;
            }
            if(dirChangeLock > 0)
            {
                dirChangeLock--;
            }

            angle += moveDir * 0.12f;
            hSpeed = moveDir;
            if(chargeFrames > 0)
            {
                chargeFrames--;
            }
            else
            {
                if (ammo > 0)
                {
                    chargeFrames = 10;
                    QuickFlash();
                    Flash();
                    if (ammo == 8)
                    {
                        Level.Add(new SoundSource(position.x, position.y, 480, "SFX/Devices/CandelaExplosion.wav", "J"));
                        DuckNetwork.SendToEveryone(new NMSoundSource(position, 480, "SFX/Devices/CandelaExplosion.wav", "J"));
                    }
                    ammo--;
                    moveDir = 0;
                }
                else
                {
                    Level.Remove(this);
                }
            }
            base.Update();
        }


        public override void DetonateFull()
        {

        }

        public virtual void QuickFlash()
        {
            //Graphics.flashAdd = 1.3f;
        }

        public virtual void Flash()
        {
            Level.Add(new Flashlight(position.x, position.y, 4f, 240f, 0.99f));
        }
    }
}