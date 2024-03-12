using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    public class GuMine : Throwable
    {
        public int regenerations = 7;

        public GuMine(float xval, float yval) : base(xval, yval)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Devices/GuMine.png"), 8, 6, false);
            graphic = _sprite;
            center = new Vec2(4f, 3f);
            collisionOffset = new Vec2(-4f, -3f);
            collisionSize = new Vec2(8f, 6f);

            scannable = true;

            UsageCount = 1;
            index = 24;
            setTime = 0.5f;
            delay = 0.5f;

            weightR = 7;
            DeviceCost = 5;
            descriptionPoints = "Gu mine placed";

            CooldownTime = 25;
            Cooldown = 0.5f;

            drawTraectory = true; 
            minimalTimeOfHolding = 0.25f;
            needsToBeGentle = false;
        }
        public override void Update()
        {
            base.Update();
            if(regenerations > 0)
            {
                if(Cooldown >= 1)
                {
                    UsageCount++;
                    regenerations--;
                    Cooldown = 0;
                }
                else
                {
                    Cooldown += 0.01666666f / CooldownTime;
                }
            }
        }

        public override void SetRocky()
        {
            rock = new GuMineAP(position.x, position.y);
            base.SetRocky();
        }
    }
    public class GuMineAP : Rocky
    {
        public bool wasGrounded = false;
        public SinWave _pulse = 0.02f;
        public GuMineAP(float xval, float yval) : base(xval, yval)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Devices/GuMine.png"), 8, 6, false);
            graphic = _sprite;
            center = new Vec2(4f, 3f);
            collisionOffset = new Vec2(-4f, -3f);
            collisionSize = new Vec2(8f, 6f);

            grounded = false;
            breakable = true;
            bulletproof = false;
            destructingByHands = true;

            bouncy = 0.4f;
            //friction = 1;
        }
        public override void Update()
        {
            base.Update();

            alpha = _pulse * 0.05f + 0.1f;

            if (jammed)
            {
                alpha = _pulse * 0.05f + 0.95f;
            }

            if (!grounded)
            {
                setted = false;
            }
            else
            {
                if (!wasGrounded)
                {
                    wasGrounded = true;

                    Level.Add(new SoundSource(position.x, position.y, 240, "SFX/Devices/LesionMineSet.wav", "J"));
                    DuckNetwork.SendToEveryone(new NMSoundSource(position, 240, "SFX/Devices/LesionMineSet.wav", "J"));
                }
                angle *= 0.8f;

                setted = true;
                hSpeed *= 0.8f;
                vSpeed *= 0.1f;
            }

            foreach(Operators op in Level.CheckRectAll<Operators>(topLeft, bottomRight))
            {
                if(op.team != team)
                {
                    if (!op.HasEffect("Intoxicated"))
                    {
                        op.effects.Add(new Intoxicated());
                    }
                    
                    if(oper != null)
                    {
                        if (oper.local)
                        {
                            PlayerStats.renown += 5;
                            PlayerStats.Save();
                            Level.Add(new RenownGained() { description = "Trap activated", amount = 5 });
                        }
                    }
                    Level.Remove(this);
                }
            }
        }
    }
}
