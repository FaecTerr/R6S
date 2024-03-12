using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //NEEDS REWORK
    //[EditorGroup("Faecterr's|Devices|Deployable")]
    public class VolcanShield : Placeable
    {
        public List<Bullet> firedBullets = new List<Bullet>();
        public VolcanShield(float xpos, float ypos) : base(xpos, ypos)
        {
            this._sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Devices/Volcan.png"), 16, 20, false);
            this.graphic = this._sprite;
            this._sprite.frame = 0;
            this.center = new Vec2(8f, 10f);
            this.collisionSize = new Vec2(12f, 6f);
            this.collisionOffset = new Vec2(-6f, -3f);
            this.setTime = 0.8f;
            this.CheckRect = new Vec2(24f, 0f);
            this.electricible = true;
            this.scannable = false;

            cantProne = true;
        }
        public virtual void Explode()
        {
            for (int i = 0; i < 16; i++)
            {
                LandFire f = new LandFire(this.position.x, this.position.y, 10f);
                f.vSpeed = -Math.Abs(4f - i * 0.5f);
                f.hSpeed = 4f - i * 0.5f;
                Level.Add(f);
            }
            Level.Remove(this);
        }
        public override bool Hit(Bullet bullet, Vec2 hitPos)
        {
            if((hitPos.x > position.x && offDir == -1) || (hitPos.x < position.x && offDir == 1))
            {
                Explode();
            }
            return base.Hit(bullet, hitPos);
        }
        public override void Set()
        {
            base.Set();
            foreach (Duck d in Level.current.things[typeof(Duck)])
            {
                DinamicSFX.Play(d.position, this.position, 1f, 1f, "ShieldPlace.wav");
            }
        }
        public override void Update()
        {
            base.Update();
            if (this.setted == true)
            {
                this.thickness = 10f;
                this._sprite.frame = 1;
                this.collisionSize = new Vec2(12f, 20f);
                this.collisionOffset = new Vec2(-6f, -10f);
                foreach (PhysicsObject po in Level.CheckRectAll<PhysicsObject>(this.topLeft + new Vec2(0f, 8f), this.bottomRight))
                {
                    if (po != null && po != this && !(po is DeployableShield))
                    {
                        if (po.enablePhysics == true && !(po is OPEQ))
                        {
                            if (this.offDir == 1 && po.hSpeed < 0)
                            {
                                po.hSpeed = 1.6f;
                            }
                            if (this.offDir == -1 && po.hSpeed > 0)
                            {
                                po.hSpeed = -1.6f;
                            }
                        }
                        if (po is OPEQ)
                        {
                            OPEQ d = po as OPEQ;
                            Operators f = d.oper as Operators;
                            if (this.team != f.team)
                            {
                                if (offDir == 1 && po.hSpeed < 0)
                                {
                                    po.hSpeed = 1.6f;
                                }
                                if (offDir == -1 && po.hSpeed > 0)
                                {
                                    po.hSpeed = -1.6f;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}