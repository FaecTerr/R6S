using System;
using System.Collections.Generic;

namespace DuckGame.R6S
{
    public class BreachForceWave : Thing
    {
        public BreachForceWave(float xpos, float ypos, int dir, float alphaSub, float speed, float speedv, Duck own) : base(xpos, ypos, null)
        {
            this.offDir = (sbyte)dir;
            this.graphic = new Sprite("sledgeForce", 0f, 0f);
            this.center = new Vec2((float)this.graphic.w, (float)this.graphic.h);
            this._alphaSub = alphaSub;
            this._speed = speed;
            this._speedv = speedv;
            this._collisionSize = new Vec2(6f, 26f);
            this._collisionOffset = new Vec2(-3f, -13f);
            this.graphic.flipH = (this.offDir <= 0);
            this.owner = own;
            base.depth = -0.7f;
        }

        public override void Update()
        {
            if (base.alpha > 0.1f)
            {
                /*if(Level.CheckPoint<RainforcableWall>(this.position) != null)
                {
                    RainforcableWall r = Level.CheckPoint<RainforcableWall>(this.position) as RainforcableWall;
                    if(r.rainforced == false && r.Unbreakable == false)
                    {
                        r.health = 0;
                    }
                }
                else if (Level.CheckPoint<RainforcableFloor>(this.position) != null)
                {
                    RainforcableFloor r = Level.CheckPoint<RainforcableFloor>(this.position) as RainforcableFloor;
                    if (r.rainforced == false && r.Unbreakable == false)
                    {
                        r.health = 0;
                    }
                }
                else if (Level.CheckPoint<Block>(this.position) != null)
                {
                    Level.Remove(this);
                }*/
                IEnumerable<PhysicsObject> hits = Level.CheckRectAll<PhysicsObject>(base.topLeft, base.bottomRight);
                foreach (PhysicsObject hit in hits)
                {
                    if (!this._hits.Contains(hit) && hit != this.owner)
                    {
                        if (this.owner != null)
                        {
                            Thing.Fondle(hit, this.owner.connection);
                        }
                        Grenade g = hit as Grenade;
                        if (g != null)
                        {
                            g.PressAction();
                        }
                        hit.hSpeed = ((this._speed - 3f) * (float)this.offDir * 1.5f + (float)this.offDir * 4f) * base.alpha;
                        hit.vSpeed = (this._speedv + -4.5f) * base.alpha;
                        hit.clip.Add(this.owner as MaterialThing);
                        if (!hit.destroyed)
                        {
                            hit.Destroy(new DTImpact(this));
                        }
                        this._hits.Add(hit);
                    }
                }
                foreach (Device d in Level.CheckRectAll<Device>(base.topLeft, base.bottomRight))
                {
                    if (d != null)
                    {
                        d.Hurt(1f);
                    }
                }
                IEnumerable<Door> doors = Level.CheckRectAll<Door>(base.topLeft, base.bottomRight);
                foreach (Door hit2 in doors)
                {
                    if (this.owner != null)
                    {
                        Thing.Fondle(hit2, this.owner.connection);
                    }
                    if (!hit2.destroyed)
                    {
                        hit2.Destroy(new DTImpact(this));
                    }
                }
                IEnumerable<Window> windows = Level.CheckRectAll<Window>(base.topLeft, base.bottomRight);
                foreach (Window hit3 in windows)
                {
                    if (this.owner != null)
                    {
                        Thing.Fondle(hit3, this.owner.connection);
                    }
                    if (!hit3.destroyed)
                    {
                        hit3.Destroy(new DTImpact(this));
                    }
                }

            }
            base.x += (float)this.offDir * this._speed;
            base.y += this._speedv;
            base.alpha -= this._alphaSub;
            if (base.alpha <= 0f)
            {
                Level.Remove(this);
            }
        }

        private float _alphaSub;
        private float _speed;
        private float _speedv;
        private List<Thing> _hits = new List<Thing>();
    }
}
