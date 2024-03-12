using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    public class FireBolt : Device
    {
        public float wide = 120;

        public FireBolt(float xpos, float ypos) : base(xpos, ypos)
        {
            gravMultiplier = 0.3f;
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Devices/Bolt.png"), 20, 7, false);
            graphic = _sprite;
            center = new Vec2(10f, 3.5f);
            collisionSize = new Vec2(12f, 1f);
            collisionOffset = new Vec2(-6f, -0.5f);
            weight = 0.6f;
            thickness = 0.1f;

            jammResistance = true;
        }
        public virtual void Explode()
        {
            float lengthR = wide / 2;
            float lengthL = wide / 2;

            float additionR;
            float additionL;


            foreach (Block b in Level.CheckLineAll<Block>(position, position + new Vec2(wide/2, 0)))
            {
                if(Math.Abs((position - b.position).length) < lengthR)
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
            while(w < lengthR && (lengthR + lengthL) > 0)
            {
                LandFire f = new LandFire(position.x + w, position.y, 10f) { oper = oper, doMakeSound = (int)Math.Abs(w) % (12 * 4) < 12 };
                Level.Add(f);
                w += 12;
            }

            Level.Remove(this);
        }
        public override void OnSoftImpact(MaterialThing with, ImpactedFrom from)
        {
            if (with != null)
            {
                if (with is Block || with is DeployableShieldAP)
                {
                    /*if(from == ImpactedFrom.Top)
                    {
                        position.y += 8;
                    }
                    if(from == ImpactedFrom.Bottom)
                    {
                        position.y -= 3;
                    }*/
                    Explode();
                }
            }
            base.OnSoftImpact(with, from);
        }
    }
}