using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    public class SmokeBolt : Device
    {
        public SmokeBolt(float xpos, float ypos) : base(xpos, ypos)
        {
            gravMultiplier = 0.3f;
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Devices/Bolt.png"), 20, 7, false);
            graphic = _sprite;
            center = new Vec2(10f, 3.5f);
            collisionSize = new Vec2(12f, 1f);
            collisionOffset = new Vec2(-6f, -0.5f);
            weight = 0.6f;
            _sprite.frame = 1;
            thickness = 0.1f;

            jammResistance = true;
        }

        public override void Update()
        {
            if(Level.CheckRect<Block>(topLeft, bottomRight) != null)
            {
                Explode();
            }
            base.Update();
        }

        public override void OnSoftImpact(MaterialThing with, ImpactedFrom from)
        {
            if (with != null)
            {
                if (with is Block || with is DeployableShieldAP)
                {
                    Explode();
                }
            }
            base.OnSoftImpact(with, from);
        }

        public virtual void Explode()
        {
            Level.Add(new SmokeGR(x, y, 12f));
            Level.Remove(this);
        }
    }
}