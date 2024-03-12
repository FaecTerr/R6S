using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    [EditorGroup("Faecterr's|Furniture")]
    public class CrawlingBugs : Thing
    {
        public bool init;
        public float offset = Rando.Float(0, 180);

        public CrawlingBugs(float xval, float yval) : base(xval, yval)
        {
            center = new Vec2(2f, 1f);
            collisionOffset = new Vec2(-2f, -1f);
            collisionSize = new Vec2(4f, 2f);
            hugWalls = WallHug.Floor;

            depth = -0.6f;

        }
        public override void Update()
        {
            base.Update();

            if (!init)
            {
                init = true;
                //Level.Add(new SoundSource(position.x, position.y, 320, "SFX/Music/Consulate.wav", "J") { showTime = 153 });
            }
        }

        public override void Draw()
        {
            offset += 0.1f;
            float l = offset;
            for (int i = 1; i < 4; i += 2)
            {
                l = i * 3 + offset * (float)Math.Pow(0.97f,i) * 0.2f;
                Graphics.DrawLine(position + new Vec2((float)Math.Cos(l * 2) * 24, 0), position + new Vec2((float)Math.Cos(l * 2) * 24 + 3, 0), Color.Black, 1f);
                Graphics.DrawLine(position + new Vec2((float)Math.Cos(l * 2 + Math.PI * 0.66666f) * 24, 0), position + new Vec2((float)Math.Cos(l * 2 + Math.PI * 0.66666f) * 24 + 3, 0), Color.Black, 1f);
                Graphics.DrawLine(position + new Vec2((float)Math.Cos(l * 2 + Math.PI * 1.33333f) * 24, 0), position + new Vec2((float)Math.Cos(l * 2 + Math.PI * 1.33333f) * 24 + 3, 0), Color.Black, 1f);
            }
            for (int i = 2; i < 4; i += 2)
            {
                l = -(i * 3 + offset * (float)Math.Pow(0.97f, i) * 0.2f);
                Graphics.DrawLine(position + new Vec2((float)Math.Cos(l * 2) * 24, 1), position + new Vec2((float)Math.Cos(l * 2) * 24 + 3, 1), Color.Black, 1f);
                Graphics.DrawLine(position + new Vec2((float)Math.Cos(l * 2 + Math.PI * 0.66666f) * 24, 1), position + new Vec2((float)Math.Cos(l * 2 + Math.PI * 0.66666f) * 24 + 3, 1), Color.Black, 1f);
                Graphics.DrawLine(position + new Vec2((float)Math.Cos(l * 2 + Math.PI * 1.33333f) * 24, 1), position + new Vec2((float)Math.Cos(l * 2 + Math.PI * 1.33333f) * 24 + 3, 1), Color.Black, 1f);
            }

            base.Draw();
        }
    }
}
