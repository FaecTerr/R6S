using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    public class TRAX : Throwable
    {
        public TRAX(float xval, float yval) : base(xval, yval)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Devices/TRAX.png"), 16, 16, false);
            graphic = _sprite;
            center = new Vec2(8f, 8f);
            collisionOffset = new Vec2(-4f, -3f);
            collisionSize = new Vec2(8f, 6f);

            scannable = true;

            UsageCount = 4;
            index = 35;
            setTime = 0.5f;
            delay = 0.5f;

            weightR = 5;
            DeviceCost = 15;
            descriptionPoints = "TRAX deployed";

            drawTraectory = true;
            canPickUp = false;

            pickupSound = "SFX/Devices/TRAXpickup.wav"; 
            placeSound = "SFX/Devices/TRAXspawn1.wav"; 
            minimalTimeOfHolding = 0.5f;
            needsToBeGentle = false;
        }
        public override void Update()
        {
            base.Update();
        }

        public override void SetRocky()
        {
            rock = new TRAXAP(position.x, position.y);
            base.SetRocky();
        }
    }
    public class TRAXAP : Rocky
    {
        public int cooldown = 20;
        public int MoreLeft = 3;
        public int MoreRight = 3;
        public int Damage = 10;

        public TRAXAP(float xval, float yval) : base(xval, yval)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Devices/Trax.png"), 16, 16, false);
            graphic = _sprite;
            center = new Vec2(8f, 13f);
            collisionOffset = new Vec2(-8f, -2f);
            collisionSize = new Vec2(16f, 4f);

            grounded = false;
            breakable = true;
            bulletproof = false;
            destructingByHands = true;
                        
            bouncy = 0.1f;
            friction = 0.2f;
            health = 40;
        }
        public override void Update()
        {
            base.Update();


            if (framesSinceGrounded == 2)
            {
                if (MoreRight == MoreLeft && MoreRight == 3)
                {
                    Level.Add(new SoundSource(position.x, position.y, 480, "SFX/Devices/TRAXspawn1.wav", "J"));
                    DuckNetwork.SendToEveryone(new NMSoundSource(position, 480, "SFX/Devices/TRAXspawn1.wav", "J"));
                }
            }
            if (grounded)
            {
                setted = true;
                angle = 0;
                _sprite.frame = 1;
            }

            if (setted && grounded)
            {
                canPickUp = false;
                if (cooldown > 0)
                {
                    cooldown--;
                }
                if (isServerForObject)
                {
                    if (MoreLeft > 0 && cooldown <= 0)
                    {
                        TRAXAP tl = new TRAXAP(position.x, position.y - 3);
                        tl.hSpeed = -2.5f;
                        tl.vSpeed = -0.5f;
                        tl.MoreLeft = MoreLeft - 1;
                        tl.MoreRight = 0;
                        tl.oper = oper;
                        tl.team = team;
                        MoreLeft = 0;
                        cooldown = 20;
                        Level.Add(tl);
                        canPickUp = false;
                        canPick = false;

                        int i = Rando.Int(2);
                        if (i == 0)
                        {
                            Level.Add(new SoundSource(position.x, position.y, 480, "SFX/Devices/TRAXspawn2.wav", "J"));
                            DuckNetwork.SendToEveryone(new NMSoundSource(position, 480, "SFX/Devices/TRAXspawn2.wav", "J"));
                        }
                        if (i == 1)
                        {
                            Level.Add(new SoundSource(position.x, position.y, 480, "SFX/Devices/TRAXspawn3.wav", "J"));
                            DuckNetwork.SendToEveryone(new NMSoundSource(position, 480, "SFX/Devices/TRAXspawn3.wav", "J"));
                        }
                        if (i == 2)
                        {
                            Level.Add(new SoundSource(position.x, position.y, 480, "SFX/Devices/TRAXspawn4.wav", "J"));
                            DuckNetwork.SendToEveryone(new NMSoundSource(position, 480, "SFX/Devices/TRAXspawn4.wav", "J"));
                        }
                    }
                    if (MoreRight > 0 && cooldown <= 0)
                    {
                        TRAXAP tl = new TRAXAP(position.x, position.y - 3);
                        tl.hSpeed = 2.5f;
                        tl.vSpeed = -0.5f;
                        tl.MoreLeft = 0;
                        tl.MoreRight = MoreRight - 1;
                        tl.oper = oper;
                        tl.team = team;
                        MoreRight = 0;
                        cooldown = 20;
                        Level.Add(tl);
                        canPickUp = false;
                        canPick = false; 
                        
                        int i = Rando.Int(2);
                        if (i == 0)
                        {
                            Level.Add(new SoundSource(position.x, position.y, 480, "SFX/Devices/TRAXspawn2.wav", "J"));
                            DuckNetwork.SendToEveryone(new NMSoundSource(position, 480, "SFX/Devices/TRAXspawn2.wav", "J"));
                        }
                        if (i == 1)
                        {
                            Level.Add(new SoundSource(position.x, position.y, 480, "SFX/Devices/TRAXspawn3.wav", "J"));
                            DuckNetwork.SendToEveryone(new NMSoundSource(position, 480, "SFX/Devices/TRAXspawn3.wav", "J"));
                        }
                        if (i == 2)
                        {
                            Level.Add(new SoundSource(position.x, position.y, 480, "SFX/Devices/TRAXspawn4.wav", "J"));
                            DuckNetwork.SendToEveryone(new NMSoundSource(position, 480, "SFX/Devices/TRAXspawn4.wav", "J"));
                        }
                    }
                }

                foreach (Operators op in Level.CheckRectAll<Operators>(topLeft, bottomRight))
                {
                    if(cooldown <= 0 && op.team != team && Math.Abs(op.hSpeed) > 0.2f && op.cutFrames <= 0)
                    {
                        //op.GetDamage(Damage);
                        cooldown = 30;
                        op.cutFrames = 30;
                        op.unableToSprint = 30;
                    }
                }
            }
        }
    }
}
