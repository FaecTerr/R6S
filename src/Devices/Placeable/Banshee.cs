using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Devices|Throwable")]
    public class Banshee : Throwable
    {
        public Banshee(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Devices/Banshi.png"), 8, 16, false);
            graphic = _sprite;
            _sprite.frame = 0;
            center = new Vec2(4f, 8f);
            collisionSize = new Vec2(8f, 16f);
            collisionOffset = new Vec2(-4f, -8f);
            setTime = 1.3f;
            weight = 0.9f;
            thickness = 0.1f;

            placeable = true;
            breakable = true;
            zeroSpeed = false;
            scannable = false;

            sticky = true;
            weightR = 6;
            UsageCount = 3;

            closeDeployment = true;

            DeviceCost = 15;
            descriptionPoints = "Banshee set";
            placeSound = "SFX/Devices/CanisterPlace.wav";
            pickupSound = "SFX/Devices/CanisterPick.wav";
            index = 50;
        }

        public override void SetRocky()
        {
            rock = new BansheeAP(position.x, position.y);
            base.SetRocky();
        }
    }
    public class BansheeAP : Rocky
    {
        public int openStage;
        float range = 64;
        public BansheeAP(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Devices/Banshi.png"), 8, 16, false);
            graphic = _sprite;
            _sprite.frame = 0;
            center = new Vec2(4f, 8f);
            collisionSize = new Vec2(6f, 6f);
            collisionOffset = new Vec2(-3f, -3f);
            thickness = 0.4f;

            placeable = false;
            setted = false;
            breakable = true;
            sticky = true;
            bulletproof = true;

            setted = false;
            setFrames = 64;

            DeviceCost = 15;
            descriptionPoints = "Banshee destroyed";
        }

        public override void Set()
        {
            base.Set();
        }        

        public override void Update()
        {
            base.Update();

            if (setted == true && !jammed)
            {
                bool open = false;
                foreach(Operators oper in Level.CheckCircleAll<Operators>(position, range))
                {
                    if(oper.team != team)
                    {
                        open = true;
                        oper.hSpeed *= 0.85f;
                    }
                }
                if (open)
                {
                    openStage++;
                }
                else
                {
                    openStage--;
                }

                if (openStage > 40)
                {
                    bulletproof = false;
                    if(openStage > 80)
                    {
                        openStage = 80;
                    }
                }
                else
                {
                    bulletproof = true;
                }
                _sprite.frame = openStage / 20;
            }
        }

        public override void Draw()
        {
            base.Draw(); 
            if (setFrames > 0 && setted)
            {
                //text = "Grenade defense is now active";

                Graphics.DrawCircle(position, range - setFrames, Color.White, 2f, 1f, 32);

                setFrames -= 2;
            }
        }
    }
}
