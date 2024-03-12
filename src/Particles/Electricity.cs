using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    public class Electricity : Thing
    {
        public Vec2 prevPos;
        public Vec2 startPos;
        private List<Vec2> _prevPositions = new List<Vec2>();
        public float range = 64f;
        public Vec2 _travelVec = new Vec2(Rando.Float(-6f, 6f), Rando.Float(-6f, 6f));


        public Electricity(float xval, float yval, float radius, float alp = 1f) : base(xval, yval)
        {
            alpha = alp;
            if (alpha <= 0f)
            {
                Level.Remove(this);
            }
            collisionSize = new Vec2(0f, 0f);
            graphic = null;
            startPos = new Vec2(xval, yval);
            prevPos = startPos;
            range = radius;
            layer = Layer.Foreground;
            _visibleInGame = true;
        }
        public override void Update()
        {
            base.Update();
            if (_prevPositions.Count == 0)
            {
                _prevPositions.Insert(0, position);
            }
            Vec2 prev = position;
            position += _travelVec;
            _travelVec = new Vec2(Rando.Float(range / -10f, range / 10f), Rando.Float(range / -10f, range / 10f));
            _prevPositions.Insert(0, position);
            alpha -= 0.1f;
            if (alpha <= 0f)
            {
                Level.Remove(this);
            }
        }
        public override void Draw()
        {
            base.Draw();
            Vec2 prev = Vec2.Zero;
            bool hasPrev = false;
            float a = 1f;
            foreach (Vec2 v in _prevPositions)
            {
                if (a > 0 && alpha > 0)
                {
                    if (!hasPrev)
                    {
                        hasPrev = true;
                        prev = v;
                    }
                    else
                    {
                        a *= alpha;
                        Graphics.DrawLine(v, prev, Color.Aqua * a, 1f, 0.9f);
                        a -= 0.1f;
                    }
                }
            }
        }
    }
}
