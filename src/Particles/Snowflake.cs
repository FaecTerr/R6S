using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    public class Snowflake : Thing
    {
        public Vec2 prevPos;
        public Vec2 startPos;

        private int len = Rando.Int(4, 7);
        private List<Vec2> _prevPositions = new List<Vec2>();

        public Vec2 _travelVec = new Vec2(0, 0);

        SinWave waving = 0.32f;
        SinWave floating = 0.45f;

        public Snowflake(float xval, float yval) : base(xval, yval)
        {
            collisionSize = new Vec2(0f, 0f);
            graphic = null;
            layer = Layer.Foreground;
            _visibleInGame = true;
            waving = Rando.Float(-1, 1);
            floating = Rando.Float(-1, 1);
            
            startPos = new Vec2(xval, yval);
            position = startPos;
            prevPos = startPos;
        }
        public override void Update()
        {            
            Vec2 camPos = Level.current.camera.position;
            Vec2 Unit = Level.current.camera.size / new Vec2(320, 180);

            prevPos = position;
            _prevPositions.Insert(0, prevPos);
            _travelVec = new Vec2(-1 * (waving + 1.15f) * Unit.x, (floating + 0.95f) * Unit.y) * new Vec2(0.7f, 0.5f);
            position += _travelVec;          


            if(Level.current.camera.position.x + position.x < Level.current.camera.position.x - 10 && isInitialized)
            {
                Level.Remove(this);
            }

            while (_prevPositions.Count > len)
            {
                _prevPositions.RemoveAt(len);
            }

            base.Update();
        }
        public override void Draw()
        {
            base.Draw();

            Vec2 camPos = Level.current.camera.position;

            Graphics.DrawLine(camPos + position, camPos + prevPos, Color.White * (0.6f), 1f, 0.9f);

            Vec2 virtualCurPos = position;

            for (int i = 0; i < _prevPositions.Count; i++)
            {
                //Graphics.DrawLine(camPos + virtualCurPos, camPos + _prevPositions[i], Color.White * (0.6f - 0.07f * i), 0.4f - 0.03f * i, 0.9f);
                virtualCurPos = _prevPositions[i];
            }
        }
    }
}
