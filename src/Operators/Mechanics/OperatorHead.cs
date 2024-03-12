using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class OperatorHead : MaterialThing
    {
        //public SpriteMap _sprite;
        public Operators oper;

        public OperatorHead(float xpos, float ypos) : base(xpos, ypos)
        {
            weight = 0f;
            flammable = 0f;
            center = new Vec2(16, 16);
            collisionSize = new Vec2(12, 12f);
            collisionOffset = new Vec2(-6f, -6f);
            //_enablePhysics = false;
            thickness = 0;
            SetSprites();
        }

        public virtual void SetSprites()
        {
            //_sprite.center = new Vec2(16, 16);
            collisionSize = new Vec2(10, 10f);
            collisionOffset = new Vec2(-5f, -5f);
            //graphic = _sprite;
            //base.graphic = _sprite;
        }

        public override void Update()
        {
            base.Update();
            if (oper != null)
            {
                velocity = oper.velocity;
                position = oper.headPosition + oper.position;
            }
        }
    }
}
