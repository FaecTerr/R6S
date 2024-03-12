using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    [BaggedProperty("isFatal", false)]
    public class ClusterBomb : Rocky
    {
        public float timer = 2f;

        public ClusterBomb(float xval, float yval) : base(xval, yval)
        {
            
            canPickUp = false;
            _sprite = new SpriteMap(GetPath("Sprites/Devices/LifeLineGrenade.png"), 10, 10, false);
            graphic = _sprite;
            center = new Vec2(5f, 5f);
            collisionOffset = new Vec2(-4f, -4f);
            collisionSize = new Vec2(8f, 8f);
            bouncy = 0.4f;
            weight = 0.4f;
            gravMultiplier = 0.6f;
            friction = 0.05f;

            isGrenade = true;
            jammResistance = true;
        }

        public override void Update()
        {
            base.Update();

            if (time <= 0)
            {
                Level.Add(new SoundSource(position.x, position.y, 360, "SFX/explo/explosion_barrel.wav", "J"));
                DuckNetwork.SendToEveryone(new NMSoundSource(position, 360, "SFX/explo/explosion_barrel.wav", "J"));

                Level.Add(new Explosion(position.x, position.y, 16, 75, "S") { shootedBy = oper });
                Level.Add(new Explosion(position.x, position.y, 48, 130, "N") { shootedBy = oper });

                DuckNetwork.SendToEveryone(new NMExplosion(position, 16, 75, "S", oper));
                DuckNetwork.SendToEveryone(new NMExplosion(position, 48, 130, "N", oper));

                Level.Add(new ExplosionPart(position.x, position.y, true));
                int num = 6;
                if (Graphics.effectsLevel < 2)
                {
                    num = 3;
                }
                for (int i = 0; i < num; i++)
                {
                    float dir = i * 60f + Rando.Float(-10f, 10f);
                    float dist = Rando.Float(20f, 20f);
                    ExplosionPart ins = new ExplosionPart(position.x + (float)(Math.Cos(Maths.DegToRad(dir)) * dist), position.y - (float)(Math.Sin(Maths.DegToRad(dir)) * dist), true);
                    Level.Add(ins);
                }
                Graphics.FlashScreen();
                //SFX.Play("explode", 1f, 0f, 0f, false);
                if (base.isServerForObject)
                {
                    for (int i = 0; i < 40; i++)
                    {
                        float dir = (float)i * 9f - 5f;
                        ATShrapnel shrap = new ATShrapnel();
                        shrap.range = 40f;
                        Bullet bullet = new Bullet(this.position.x + (float)(Math.Cos((double)Maths.DegToRad(dir)) * 6.0), this.position.y - (float)(Math.Sin((double)Maths.DegToRad(dir)) * 6.0), shrap, dir, null, false, -1f, false, true);
                        Level.Add(bullet);
                        this.firedBullets.Add(bullet);
                        if (Network.isActive)
                        {
                            NMFireGun gunEvent = new NMFireGun(null, this.firedBullets, 20, false, 4, false);
                            Send.Message(gunEvent, NetMessagePriority.ReliableOrdered);
                            this.firedBullets.Clear();
                        }
                        Level.Remove(this);
                    }
                }
            }
            else
            {
                time -= 0.01666666f;
            }
        }
    }
}
