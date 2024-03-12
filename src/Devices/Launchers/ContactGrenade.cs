using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[BaggedProperty("isFatal", false)]
    public class ContactGrenade : Device
    {
        public List<Bullet> firedBullets = new List<Bullet>();
        public bool used;
        public ContactGrenade(float xval, float yval) : base(xval, yval)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Devices/LifeLineGrenade.png"), 10, 10, false);
            graphic = _sprite;
            _sprite.frame = 1;
            center = new Vec2(5f, 5f);
            collisionOffset = new Vec2(-3f, -3f);
            collisionSize = new Vec2(6f, 6f);

            gravMultiplier = 0.6f;
            thickness = 0.6f;
            weight = 0.9f;
            
            canPickUp = false;

            catchableByADS = true;
            destructingByElectricity = false;
            destructingByHands = false;
            bulletproof = true;
            
            grounded = false;
        }


        public override void Update()
        {
            base.Update();
            angle = (float)Math.Atan(hSpeed / -vSpeed) - (float)Math.PI / 2;

            if (grounded && hSpeed == 0 && vSpeed == 0)
            {
                Explode();
            }
        }


        public virtual void Explode()
        {
            for (int i = 0; i < 20; i++)
            {
                float dir = i * 18f - 5f;
                ATShrapnel shrap = new ATShrapnel();
                shrap.range = 48f;
                Bullet bullet = new Bullet(position.x + (float)(Math.Cos(Maths.DegToRad(dir)) * 6.0), position.y - (float)(Math.Sin(Maths.DegToRad(dir)) * 6.0), shrap, dir, null, false, -1f, false, true);
                bullet.firedFrom = this;
                firedBullets.Add(bullet);
                Level.Add(bullet);
            }

            if (!used)
            {
                used = true;
                Level.Add(new Explosion(position.x, position.y, 32, 50, "N") { shootedBy = oper });
                Level.Add(new Explosion(position.x, position.y, 26, 0, "S") { shootedBy = oper });

                DuckNetwork.SendToEveryone(new NMExplosion(position, 32, 50, "N", oper));
                DuckNetwork.SendToEveryone(new NMExplosion(position, 26, 0, "S", oper));

                Level.Add(new SoundSource(position.x, position.y, 320, "SFX/explo/explosion_barrel.wav", "J"));
                DuckNetwork.SendToEveryone(new NMSoundSource(position, 320, "SFX/explo/explosion_barrel.wav", "J"));
            }
            
            Level.Remove(this);
        }

        public override void OnImpact(MaterialThing with, ImpactedFrom from)
        {
            base.OnSolidImpact(with, from);
            if (with != null)
            {
                if (with is Block)
                {
                    Explode();
                }
            }
        }
    }
}
