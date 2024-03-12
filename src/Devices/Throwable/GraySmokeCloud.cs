using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    /// <summary>
    /// Vision breaker variant(involved in smokegrenadeworkingmaintenance) of active smoke grenade
    /// </summary>
    public class SmokeGR : Thing
    {
        public float Timer
        {
            get;
            set;
        }

        public Vec2 move;
        float angleIncrement;
        float scaleDecrement;
        float fastGrowTimer;
        public float order;

        public SmokeGR(float xpos, float ypos, float stayTime = 1f) : base(xpos, ypos)
        {
            velocity = new Vec2(0f, 0f);
            xscale = 0.35f;
            yscale = xscale;
            angle = Maths.DegToRad(Rando.Float(360f));
            fastGrowTimer = 2f;
            Timer = stayTime;
            angleIncrement = Maths.DegToRad(Rando.Float(3f) - 1f);
            scaleDecrement = 0.003f;
            move = new Vec2(0f, 0f);

            GraphicList graphicList = new GraphicList();
            Sprite graphic1 = new Sprite("smoke", 0.0f, 0.0f);
            graphic1.alpha = 1f;
            graphic1.depth = (Depth)1f;
            graphic1.CenterOrigin();
            graphicList.Add(graphic1);

            Sprite graphic2 = new Sprite("smokeBack", 0.0f, 0.0f);
            graphic2.depth = (Depth)0.1f;
            graphic2.alpha = 1f;
            graphic2.CenterOrigin();
            graphicList.Add(graphic2);

            graphic = graphicList;
            center = new Vec2(0.0f, 0.0f);
            depth = 1f;
            layer = Layer.Blocks;

            collisionSize = new Vec2(32, 32);
            collisionOffset = new Vec2(-16, -16);
        }

        public override void Update()
        {
            angle += angleIncrement;

            graphic.angle += angleIncrement;

            if (Timer > 0)
            {
                Timer -= 0.01f;
            }
            else
            {
                xscale -= scaleDecrement;
                scaleDecrement += 0.0001f;
            }

            collisionSize = new Vec2(32, 32) * new Vec2(xscale, xscale);
            collisionOffset = new Vec2(-16, -16) * new Vec2(xscale, xscale);

            if (fastGrowTimer > 0.0)
            {
                fastGrowTimer -= 0.05f;
                xscale += 0.05f;
            }

            Vec2 motion = new Vec2(0f, 8f);
            if (Level.CheckRect<Block>(topLeft + motion, bottomRight + motion) == null)
            {
                position += new Vec2(0f, 0.1f);
            }
            yscale = xscale;

            position += move;
            position += velocity;
            velocity *= new Vec2(0.9f, 0.9f);

            if (xscale < 0.1)
            {
                Level.Remove(this);
            }
        }
    }
}
