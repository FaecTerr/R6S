using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    public class GasSmoke : Thing
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
        public Operators oper;
        public string team = "Def";

        public Sprite sprite1;
        public Sprite sprite2;

        public Sprite graphic1;
        public Sprite graphic2;

        public GasSmoke(float xpos, float ypos, float stayTime = 1f) : base(xpos, ypos)
        {
            velocity = new Vec2(0f, 0f);
            xscale = 1;
            yscale = xscale;
            angle = Maths.DegToRad(Rando.Float(360f));
            fastGrowTimer = 0.95f;
            Timer = stayTime;
            angleIncrement = Rando.Float(0.02f, 0.03f) * 0.05f;
            scaleDecrement = 0.002f;
            move = new Vec2(0f, 0f);

            GraphicList graphicList = new GraphicList();
            graphic1 = new Sprite("smoke", 0.0f, 0.0f);
            graphic1.alpha = 0.3f;
            graphic1.depth = 1f;
            graphic1.CenterOrigin();
            graphic1.color = Color.Yellow;
            graphicList.Add(graphic1);

            graphic2 = new Sprite("smokeBack", 0.0f, 0.0f);
            graphic2.depth = 0.1f;
            graphic2.alpha = 0.3f;
            graphic2.color = Color.Green;
            graphic2.CenterOrigin();
            graphicList.Add(graphic2);

            sprite1 = new Sprite("smoke", 0.0f, 0.0f);
            sprite1.alpha = 0.3f;
            sprite1.depth = 1f;
            sprite1.CenterOrigin();
            sprite1.color = Color.Yellow;

            sprite2 = new Sprite("smokeBack", 0.0f, 0.0f);
            sprite2.depth = 0.1f;
            sprite2.alpha = 0.3f;
            sprite2.color = Color.Green;
            sprite2.CenterOrigin();

            sprite1.angle = Rando.Float(1, 3);

            graphic = graphicList;
            center = new Vec2(0.0f, 0.0f);
            depth = 1f;

            //center = new Vec2(22, 21);
            collisionSize = new Vec2(32, 32);
            collisionOffset = new Vec2(-16, -16);
        }

        public override void Update()
        {
            foreach (Operators f in Level.CheckCircleAll<Operators>(position, 19 * xscale))
            {
                if (f.poisonFrames <= 0)
                {                
                    if (f != oper && Level.CheckLine<Block>(f.position, position) == null && !(f is Smoke))
                    {
                        f.poisonFrames = 35;
                        f.lastDamageFrom = oper;
                    }
                }
            }

            Vec2 motion = new Vec2(0f, 8f);
            if(Level.CheckRect<Block>(topLeft + motion, bottomRight + motion) == null)
            {
                position += new Vec2(0f, 0.1f);
            }

            if (Timer > 0)
            {
                Timer -= 0.01f;
            }
            else
            {
                xscale -= scaleDecrement;
                scaleDecrement += 0.0001f;
            }
            if (fastGrowTimer > 0.0)
            {
                fastGrowTimer -= 0.05f;
                xscale += 0.085f;
            }
            yscale = xscale;
            position += move;
            position += velocity;
            velocity *= new Vec2(0.8f, 0.8f);

            if (xscale < 0.1)
            {
                Level.Remove(this);
            }
        }

        public override void Draw()
        {
            base.Draw();

            sprite1.angle -= angleIncrement;
            sprite2.angle = sprite1.angle;

            sprite2.scale = sprite1.scale = graphic.scale;
            sprite2.alpha = sprite1.alpha = 0.3f;

            sprite2.flipH = sprite1.flipH = true;

            Graphics.Draw(sprite1, position.x, position.y);
            Graphics.Draw(sprite2, position.x, position.y);



            graphic1.angle += angleIncrement;
            graphic2.angle = graphic1.angle;

            graphic2.scale = graphic1.scale = graphic.scale;
            graphic2.alpha = graphic1.alpha = 0.3f;

            Graphics.Draw(graphic1, position.x, position.y);
            Graphics.Draw(graphic2, position.x, position.y);


            if (DevConsole.showCollision)
            {
                Graphics.DrawCircle(position, 19 * xscale, Color.Green, 1f, 1f, 32);
            }

        }
    }

}
