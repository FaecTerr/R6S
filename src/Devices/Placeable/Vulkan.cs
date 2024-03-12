using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Devices|Throwable")]
    public class Vulkan : Throwable
    {
        public Vulkan(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Devices/Vulkan.png"), 16, 16, false);
            graphic = _sprite;
            _sprite.frame = 0;
            center = new Vec2(8f, 8f);
            collisionSize = new Vec2(16f, 16f);
            collisionOffset = new Vec2(-8f, -8f);
            setTime = 1.3f;
            weight = 0.9f;
            thickness = 0.1f;
            placeable = true;
            breakable = true;
            zeroSpeed = false;
            scannable = false;
            jammResistance = true;
            sticky = true;
            weightR = 6;
            UsageCount = 3;

            closeDeployment = true;

            DeviceCost = 15;
            descriptionPoints = "Vulkan set";
            placeSound = "SFX/Devices/CanisterPlace.wav";
            pickupSound = "SFX/Devices/CanisterPick.wav";
            index = 36;
        }

        public override void SetRocky()
        {
            rock = new VulkanAP(position.x, position.y);
            base.SetRocky();
        }
    }
    public class VulkanAP : Rocky
    {      
        public VulkanAP(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Devices/Vulkan.png"), 16, 16, false);
            graphic = _sprite;
            _sprite.frame = 0;
            center = new Vec2(8f, 8f);
            collisionSize = new Vec2(10f, 10f);
            collisionOffset = new Vec2(-5f, -5f);
            thickness = 0.4f;
            placeable = false;
            setted = false;
            breakable = true;
            sticky = true;
            bulletproof = true;

            setted = false;
            setFrames = 4;

            DeviceCost = 15;
            descriptionPoints = "Vulkan destroyed";
        }

        public override void Set()
        {
            base.Set();
        }

        public override void DetonateFull()
        {
            float wide = 160;
            float lengthR = wide / 2;
            float lengthL = wide / 2;

            float additionR;
            float additionL;


            foreach (Block b in Level.CheckLineAll<Block>(position, position + new Vec2(wide / 2, 0)))
            {
                if (Math.Abs((position - b.position).length) < lengthR)
                {
                    lengthR = Math.Abs((position - b.position).length);
                }
            }
            foreach (Block b in Level.CheckLineAll<Block>(position, position - new Vec2(wide / 2, 0)))
            {
                if (Math.Abs((position - b.position).length) < lengthL)
                {
                    lengthL = Math.Abs((position - b.position).length);
                }
            }
            additionR = (wide / 2 - lengthR) * 0.6f;
            additionL = (wide / 2 - lengthL) * 0.6f;

            lengthR += additionL;
            lengthL += additionR;


            float w = -lengthL;
            while (w < lengthR && (lengthR + lengthL) > 0)
            {
                LandFire f = new LandFire(position.x + w, position.y, 10f) { oper = oper, doMakeSound = (int)Math.Abs(w) % (12 * 4) < 12 };
                Level.Add(f);
                w += 12;
            }

            /*FluidStream fs = new FluidStream(position.x, position.y, Vec2.Zero, 10);
            fs.holeThickness = 12;
            FluidData f = Fluid.Gas;
            f.amount = 16;
            Level.Add(fs);
            fs.Feed(f);

            for (int i = 0; i < 16; i++)
            {
                Spark s = Spark.New(position.x, position.y, new Vec2((float)Math.Cos(Maths.DegToRad(i * (360/16))), (float)Math.Sin(Maths.DegToRad(i * (360/16)))));
                Level.Add(s);
                //FireSparkle.New(position.x, position.y, new Vec2((float)Math.Cos(Maths.DegToRad(i * (360 / 16))), (float)Math.Sin(Maths.DegToRad(i * (360 / 16)))));
                
            }*/

            /*
            Level.Add(new Explosion(position.x, position.y, 6, 12, "S"));
            DuckNetwork.SendToEveryone(new NMExplosion(position, 6, 12, "S"));
            */
            base.DetonateFull();
            Level.Remove(this);
        }

        public override void Break()
        {
            DetonateFull();
            base.Break();
        }

        public override bool Hit(Bullet bullet, Vec2 hitPos)
        {
            Vec2 relPos = hitPos - position;
            bool detonate = false;
            //DevConsole.Log(Convert.ToString(relPos));
            if (Dir.x != 0)
            {
                if (relPos.y > 0 && relPos.x * offDir < 0 && bullet.start.y > position.y)
                {
                    DetonateFull();
                    detonate = true;
                }
            }
            if(Dir.y > 0)
            {
                if (relPos.y < 0 && relPos.x * offDir < 0 && bullet.start.x * offDir < position.x * offDir)
                {
                    detonate = true;
                    DetonateFull();
                }
            }
            if (Dir.y < 0)
            {
                if (relPos.y > 0 && relPos.x * offDir > 0 && bullet.start.x * offDir < position.x * offDir)
                {
                    detonate = true;
                    DetonateFull();
                }
            }

            if (!detonate)
            {
                Level.Add(new SoundSource(position.x, position.y, 160, "SFX/Devices/metalTing.wav", "J"));
                DuckNetwork.SendToEveryone(new NMSoundSource(position, 160, "SFX/Devices/metalTing.wav", "J"));
            }

            return base.Hit(bullet, hitPos);
        }

        public override void Update()
        {
            base.Update();

            if (setFrames == 2)
            {
                Level.Add(new SoundSource(position.x, position.y, 240, "SFX/Devices/ADSActive.wav", "J"));
                DuckNetwork.SendToEveryone(new NMSoundSource(position, 240, "SFX/Devices/ADSActive.wav", "J"));
            }

            if (setted == true && !jammed)
            {

            }
        }

        public override void Draw()
        {
            base.Draw();

            if (setFrames > 0 && setted)
            {
                setFrames -= 2;
            }
        }
    }
}
