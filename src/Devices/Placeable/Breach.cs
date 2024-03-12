using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Guns|Devices|Deployable")]
    public class Breach : Throwable
    {
        public Breach(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Devices/Breach.png"), 16, 16, false);
            graphic = _sprite;
            _sprite.frame = 0;
            setTime = 0.8f;
            center = new Vec2(8f, 8f);
            collisionSize = new Vec2(16f, 16f);
            collisionOffset = new Vec2(-8f, -8f);
            breakable = true;
            scannable = false;
            wallSet = true;
            sticky = true;
            doubleActivation = true;
            closeDeployment = true;
            index = 8;
            UsageCount = 3;

            descriptionPoints = "Breach charge";
            DeviceCost = 20;

            cantProne = true;
            isSecondary = true;

            placeSound = "SFX/Devices/BreachChargePlaced.wav";
        }

        public override void SetRocky()
        {
            rock = new BreachAP(position.x, position.y);
            base.SetRocky();
        }
    }
    public class BreachAP : Rocky
    {
        public int soundFrames;
        public bool startBreach;
        public int BreachFrames = 150;
        public int spriteFrames;
        public StateBinding _spriteBind = new StateBinding("spriteFrames", -1, false, false);
        public StateBinding _breach = new StateBinding("BreachFrames", -1, false, false);

        public BreachAP(float xpos, float ypos) : base(xpos, ypos)
        {
            this._sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Devices/Breach.png"), 16, 16, false);
            this.graphic = this._sprite;
            this._sprite.frame = 0;
            this.setTime = 0.5f;
            this.center = new Vec2(8f, 8f);
            this.collisionSize = new Vec2(14f, 14f);
            this.collisionOffset = new Vec2(-7f, -7f);

            breakable = true;
            scannable = false;
            destructingByElectricity = true;

            wallSet = true;
            sticky = true;

            destroyedPoints = "Breach destroyed";
            DeviceCost = 10;
        }

        public override void DetonateFull()
        {
            
            DoBreach();
            base.DetonateFull();
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
            Level.Add(new Explosion(position.x, position.y, 26, 80, "S") { shootedBy = oper });
            Level.Add(new Explosion(position.x, position.y, 44, 50, "N") { shootedBy = oper });

            DuckNetwork.SendToEveryone(new NMExplosion(position, 26, 80, "S", oper));
            DuckNetwork.SendToEveryone(new NMExplosion(position, 44, 50, "N", oper));

            Level.Add(new SoundSource(position.x, position.y, 380, "SFX/explo/breach_charge.wav", "J"));

            Level.Add(new ExplosionPart(position.x, position.y, true));
        }
    }
}
