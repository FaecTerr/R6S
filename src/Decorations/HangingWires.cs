using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    [EditorGroup("Faecterr's|Furniture")]
    public class HangingWires : Thing
    {
        public SpriteMap _sprite;
        public bool init;
        public SinWave _waving = Rando.Float(0.08f, 0.13f);
        public int waitFrames = Rando.Int(2, 6);

        public HangingWires(float xval, float yval) : base(xval, yval)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Decorations/HangingWires.png"), 12, 18, false);
            base.graphic = _sprite;
            center = new Vec2(6f, 2f);
            collisionOffset = new Vec2(-6f, -2f);
            collisionSize = new Vec2(12f, 16f);
            graphic = _sprite;
            hugWalls = WallHug.Ceiling;
            depth = -0.9f;
            _waving.value = Rando.Float(-1f, 1);
        }
        public override void Update()
        {
            base.Update();
            if (waitFrames > 0)
            {
                waitFrames--;
            }
            else
            {
                waitFrames = Rando.Int(12, 16);
                float angl = Rando.Float(90);
                float xp = 2 * (float)Math.Cos(angle) - 11 * (float)Math.Sin(angle);
                float yp = 11 * (float)Math.Cos(angle) + 2 * (float)Math.Sin(angle);
                    
                for (int i = 0; i < 4; i++)
                {
                    Level.Add(new WireParticle(position.x - xp, position.y  + yp,
                        new Vec2((float)Math.Cos(angl + i * 90) * 1, (float)Math.Sin(angl + i * 90) * 1), 32f, 1f));
                }
            }
        }

        public override void Draw()
        {
            angle = _waving * 0.1f;
            base.Draw();
        }
    }

    public class WireParticle : Thing
    {
        public Vec2 prevPos;
        public Vec2 startPos;
        private List<Vec2> _prevPositions = new List<Vec2>();
        public float range = 16f;
        public Vec2 _travelVec = new Vec2(Rando.Float(-4f, 4f), Rando.Float(-4f, 4f));

        public WireParticle(float xval, float yval, Vec2 dir, float radius, float alp = 1f) : base(xval, yval)
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
            layer = Layer.Game;
            _visibleInGame = true;
            _travelVec = dir;
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


            _travelVec.y *= 0.975f;
            _travelVec.y += 0.32f;
            _travelVec.x *= 0.975f;

            _prevPositions.Insert(0, position);

            if (alpha > 1)
            {
                alpha = 1;
            }
            alpha -= 0.05f;

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
                        Graphics.DrawLine(v, prev, Color.LightYellow * a, 0.7f * a, -0.8f);
                        a -= 0.05f;
                    }
                }
            }
        }
    }
}
