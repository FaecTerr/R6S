using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Guns|Devices|Deployable")]
    public class ThermalBreach : Throwable
    {
        public ThermalBreach(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Devices/ThermalBreach.png"), 16, 16, false);
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
            doubleActivation = true;
            closeDeployment = true;

            delay = 3f;
            index = 5;
            UsageCount = 2;

            cantProne = true;

            DeviceCost = 30;
            descriptionPoints = "Thermal charge";

            index = 19;
        }

        public override void SetRocky()
        {
            rock = new ThermalBreachAP(position.x, position.y);
            base.SetRocky();
        }
    }
    public class ThermalBreachAP : Rocky
    {
        public int soundFrames;
        public bool startBreach;
        public int BreachFrames = 200;
        public int spriteFrames;
        public StateBinding _spriteBind = new StateBinding("spriteFrames", -1, false, false);
        public StateBinding _breach = new StateBinding("BreachFrames", -1, false, false);

        public ThermalBreachAP(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Devices/ThermalBreach.png"), 16, 16, false);
            graphic = _sprite;
            _sprite.frame = 0;
            setTime = 0.5f;
            center = new Vec2(8f, 8f);
            collisionSize = new Vec2(4f, 4f);
            collisionOffset = new Vec2(-2f, -2f);
            breakable = true;
            scannable = false;
            wallSet = true;
            sticky = true;

            DeviceCost = 20;
            destroyedPoints = "Exotermic charge destroyed";

            destructingByElectricity = true;
        }

        public override void Update()
        {
            if (setted)
            {
                if(Dir.y != 0)
                {
                    collisionSize = new Vec2(4f, 14f);
                    collisionOffset = new Vec2(-2f, -7f);
                }
                if(Dir.x != 0)
                {
                    collisionSize = new Vec2(14f, 4f);
                    collisionOffset = new Vec2(-7f, -2f);
                }
            }
            base.Update();
        }

        public override void Detonate(float delay = 0)
        {
            Level.Add(new SoundSource(position.x, position.y, 640, "SFX/Devices/ThermiteBreachin.wav", "J"));
            DuckNetwork.SendToEveryone(new NMSoundSource(position, 640, "SFX/Devices/ThermiteBreachin.wav", "J"));

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
            if (!jammed && Dir.y == 0 && time > 0.3f && time < 2.5f)
            {
                float power = 1f;
                if(time < 1.5)
                {
                    power = (time - 0.4f)/2;
                }
                Spark spark = Spark.New(position.x + Dir.x*6, position.y + 5f - time * 5, Vec2.Zero, 0.002f);
                spark.hSpeed = Rando.Float(-3*Dir.x*power, 0);
                spark.vSpeed = Rando.Float(-2, 0);
                Level.Add(spark);
            }
            base.NotDetonated();
        }

        public virtual void DoBreach()
        {
            this.position.x += 6f * offDir;
            foreach (Device d in Level.CheckCircleAll<Device>(this.position, 32f))
            {
                if (Level.CheckLine<Block>(d.position, position) == null && d.setted == true && d != this && (!(d is HandShield) || !(d is BallisticShield) || !(d is FlashShield)))
                    d.Break();
            }
            Explode();
        }
        public virtual void Explode()
        {
            Level.Add(new Explosion(position.x, position.y, 44, 50, "H") { shootedBy = oper });
            Level.Add(new Explosion(position.x, position.y, 44, 80, "S") { shootedBy = oper });
            Level.Add(new Explosion(position.x, position.y, 36, 50, "N") { shootedBy = oper });

            DuckNetwork.SendToEveryone(new NMExplosion(position, 44, 50, "H", oper));
            DuckNetwork.SendToEveryone(new NMExplosion(position, 44, 80, "S", oper));
            DuckNetwork.SendToEveryone(new NMExplosion(position, 36, 50, "N", oper));

            foreach (SurfaceStationary sf in Level.CheckCircleAll<SurfaceStationary>(position, 44))
            {
                sf.Breach(1000000);
            }

            Level.Add(new ExplosionPart(position.x, position.y, true));
            Level.Remove(this);
        }
    }
}
