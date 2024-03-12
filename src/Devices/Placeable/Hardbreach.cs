using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Guns|Devices|Deployable")]
    public class HardBreach : Throwable
    {
        public HardBreach(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Devices/Hardbreach.png"), 16, 16, false);
            graphic = _sprite;
            _sprite.frame = 0;
            setTime = 1.2f;
            center = new Vec2(8f, 8f);
            collisionSize = new Vec2(16f, 16f);
            collisionOffset = new Vec2(-8f, -8f);
            //this.breakable = true;
            scannable = false;
            wallSet = true;
            sticky = true;
            closeDeployment = true;

            delay = 8f;
            index = 5;
            UsageCount = 1;

            cantProne = true;

            DeviceCost = 15;
            descriptionPoints = "Thermal charge";

            isSecondary = true;

            index = 13;
        }

        public override void SetRocky()
        {
            rock = new HardBreachAP(position.x, position.y) { activationTimer = delay };
            base.SetRocky();
        }
    }
    public class HardBreachAP : Rocky
    {
        public float activationTimer;
        public int soundFrames;
        public bool startBreach;
        public int BreachFrames = 200;
        public int spriteFrames;
        public StateBinding _spriteBind = new StateBinding("spriteFrames", -1, false, false);
        public StateBinding _breach = new StateBinding("BreachFrames", -1, false, false);

        public HardBreachAP(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Devices/Hardbreach.png"), 16, 16, false);
            graphic = _sprite;
            _sprite.frame = 0;
            setTime = 0.5f;
            center = new Vec2(8f, 8f);
            collisionSize = new Vec2(4f, 2f);
            collisionOffset = new Vec2(-2f, -2f);
            breakable = true;
            scannable = false;
            wallSet = true;
            sticky = true;
            DeviceCost = 20;
            destroyedPoints = "Hard breach charge destroyed";

            destructingByElectricity = true;
        }

        public override void Update()
        {
            if (setted)
            {
                if (!detonate)
                {
                    if (activationTimer > 0)
                    {
                        if (activationTimer >= 8)
                        {
                            Level.Add(new SoundSource(position.x, position.y, 640, "SFX/Devices/ThermiteBreachin.wav", "J"));
                            DuckNetwork.SendToEveryone(new NMSoundSource(position, 640, "SFX/Devices/ThermiteBreachin.wav", "J"));
                        }
                        activationTimer -= 0.01666666f;
                    }
                    else
                    {
                        Detonate();
                    }
                }
                if (Dir.y != 0)
                {
                    collisionSize = new Vec2(4f, 14f);
                    collisionOffset = new Vec2(-2f, -7f);
                }
                if (Dir.x != 0)
                {
                    collisionSize = new Vec2(14f, 4f);
                    collisionOffset = new Vec2(-7f, -2f);
                }
            }
            base.Update();
        }

        public override void Detonate(float delay = 0)
        {
            base.Detonate(delay);
        }

        public override void DetonateFull()
        {
            Level.Add(new SoundSource(position.x, position.y, 640, "SFX/explo/big_bomb.wav", "J"));
            DuckNetwork.SendToEveryone(new NMSoundSource(position, 640, "SFX/explo/big_bomb.wav", "J"));
            DoBreach();
            base.DetonateFull();
        }

        public override void NotDetonated()
        {
            if (!jammed )
            {
                if (Dir.y == 0)
                {
                    float power = 1f;
                    if (time > 3 - 0.016f && time < 3 + 0.016f)
                    {
                        Level.Add(new SoundSource(position.x, position.y, 640, "SFX/Devices/ThermiteBreachin.wav", "J"));
                        DuckNetwork.SendToEveryone(new NMSoundSource(position, 640, "SFX/Devices/ThermiteBreachin.wav", "J"));
                    }
                    Spark spark = Spark.New(position.x + Dir.x * 6, position.y + 5, Vec2.Zero, 0.002f);
                    spark.hSpeed = Rando.Float(-3 * Dir.x * power, 0);
                    spark.vSpeed = Rando.Float(-2, 0);
                    Level.Add(spark);
                    spark = Spark.New(position.x + Dir.x * 6, position.y - 5, Vec2.Zero, 0.002f);
                    Level.Add(spark);
                }
                if(Dir.x == 0)
                {
                    float power = 1f;
                    if (time > 3 - 0.016f && time < 3 + 0.016f)
                    {
                        Level.Add(new SoundSource(position.x, position.y, 640, "SFX/Devices/ThermiteBreachin.wav", "J"));
                        DuckNetwork.SendToEveryone(new NMSoundSource(position, 640, "SFX/Devices/ThermiteBreachin.wav", "J"));
                    }
                    Spark spark = Spark.New(position.x + 5, position.y, Vec2.Zero, 0.002f);
                    spark.hSpeed = Rando.Float(-2, 0);
                    spark.vSpeed = Rando.Float(0, -3 * Dir.y * power);
                    Level.Add(spark);
                    spark = Spark.New(position.x - 5, position.y, Vec2.Zero, 0.002f);
                    Level.Add(spark);
                }
            }
            
            base.NotDetonated();
        }

        public virtual void DoBreach()
        {
            position.x += 6f * offDir;
            foreach (Device d in Level.CheckCircleAll<Device>(this.position, 32f))
            {
                if (Level.CheckLine<Block>(d.position, position) == null && d.setted == true && d != this && (!(d is HandShield) || !(d is BallisticShield) || !(d is FlashShield)))
                    d.Break();
            }
            Explode();
        }
        public virtual void Explode()
        {
            Level.Add(new Explosion(position.x, position.y, 20, 15, "H") { shootedBy = oper });
            Level.Add(new Explosion(position.x, position.y, 20, 15, "S") { shootedBy = oper });
            Level.Add(new Explosion(position.x, position.y, 8, 20, "N") { shootedBy = oper });

            DuckNetwork.SendToEveryone(new NMExplosion(position, 20, 15, "H", oper));
            DuckNetwork.SendToEveryone(new NMExplosion(position, 20, 15, "S", oper));
            DuckNetwork.SendToEveryone(new NMExplosion(position, 8, 20, "N", oper));

            Level.Add(new ExplosionPart(position.x, position.y, true));
            Level.Remove(this);
        }
    }
}
