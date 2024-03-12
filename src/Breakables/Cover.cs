using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class CoverThing : Block
    {
        public float health = 500;
        public float coverTime;
        public float covering;
        public bool isCovered;

        public Vec2 collisionTarget;

        public CoverThing(float x, float y) : base(x, y)
        {

        }

        public override void Update()
        {

            base.Update();
        }


    }
}
