using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    public class ImpactWave : Thing
    {
        public float destination;
        public float range = 0;
        public float speed=1f;
        public ImpactWave(float xval, float yval, float radius, float sp = 1f) : base(xval, yval)
        {
            destination = radius;
            speed = sp;
            this.collisionSize = new Vec2(0f, 0f);
            this.graphic = null;
            this.layer = Layer.Foreground;
            this._visibleInGame = true;
        }
        public override void Update()
        {
            base.Update();
            range += speed;
            if(range> destination)
            {
                Level.Remove(this);
            }
        }
        public override void Draw()
        {
            Graphics.DrawCircle(position, range, Color.White, 2f, 1f, 32);
            base.Draw();
        }
    }
}
