using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    [EditorGroup("Faecterr's|Breakables")]
    public class SoftWallESE : Thing
    {
        public float xSize = 10;
        EditorProperty<float> ySize;
        public SoftWallESE()
        {
            _editorName = "Wall - ESE";
            ySize = new EditorProperty<float>(16, this, 4f, 80, 4f);
        }

        public override void Update()
        {
            base.Update();
            if (!(Level.current is Editor))
            {
                Level.Add(new BreakableSurface(position.x + 4, position.y, 2, ySize) { breakableMode = "E", horizontal = false, vertical = true });
                Level.Add(new BreakableSurface(position.x - 4, position.y, 2, ySize) { breakableMode = "E", horizontal = false, vertical = true });

                Level.Add(new BreakableSurface(position.x + 2f, position.y, 2, ySize) { breakableMode = "S", horizontal = false, vertical = true });
                Level.Add(new BreakableSurface(position.x - 2f, position.y, 2, ySize) { breakableMode = "S", horizontal = false, vertical = true });
                Level.Add(new BreakableSurface(position.x, position.y, 2, ySize) { breakableMode = "S", horizontal = false, vertical = true, lightColored = true });

                Level.Add(new SurfaceStationary(position.x, position.y) { collisionSize = new Vec2(10, ySize), vertical = true});
                Level.Remove(this);
            }
            else
            {
                collisionSize = new Vec2(xSize, ySize);
            }
        }

        public override void Draw()
        {
            if (Level.current is Editor)
            {
                collisionSize = new Vec2(xSize, ySize);
                collisionOffset = new Vec2(-xSize * 0.5f, -ySize * 0.5f);
            }
            base.Draw();
            Graphics.DrawRect(position + new Vec2(-xSize / 2, -ySize / 2), position + new Vec2(xSize / 2, ySize / 2), Color.Red, 1f, false, 1);
            Graphics.DrawRect(position + new Vec2(-xSize / 2, -ySize / 2), position + new Vec2(xSize / 2, ySize / 2), Color.Orange, 0.99f, true, 1);
        }
    }
}
