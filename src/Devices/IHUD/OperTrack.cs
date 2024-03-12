using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class OperTrack : PhysicsObject
    {
        public Operators own;
        public float lifetime = 90;
        public ulong netIndex;

        public OperTrack(float x, float y) : base(x, y)
        {
            collisionSize = new Vec2(6, 2);
            collisionOffset = new Vec2(-3, -1);

            enablePhysics = true;            
        }
        public override void Update()
        {
            if(lifetime > 0)
            {
                lifetime -= 0.01666666f;
            }
            else
            {
                Level.Remove(this);
            }

            foreach (OperTrack track in Level.CheckRectAll<OperTrack>(topLeft, bottomRight))
            {
                if(track.lifetime > lifetime)
                {
                    Level.Remove(this);
                }
            }

            base.Update();
        }
    }
}
