using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    [EditorGroup("Faecterr's|Breakables")]
    public class SoftFloorUEU : Thing
    {
        public float ySize = 10;
        EditorProperty<float> xSize;
        public SoftFloorUEU()
        {
            _editorName = "Floor - UEU";
            xSize = new EditorProperty<float>(16, this, 4f, 80, 4f);

        }

        public override void Update()
        {
            base.Update();
            if (!(Level.current is Editor))
            {
                Level.Add(new BreakableSurface(position.x, position.y - 6.5f, xSize, 3) { breakableMode = "U", horizontal = true, vertical = false });
                Level.Add(new BreakableSurface(position.x, position.y - 4.5f, xSize, 1) { breakableMode = "E", horizontal = true, vertical = false });
                Level.Add(new BreakableSurface(position.x, position.y - 3.5f, xSize, 1) { breakableMode = "U", horizontal = true, vertical = false });
                Level.Add(new BreakableSurface(position.x, position.y - 2.5f, xSize, 1) { breakableMode = "E", horizontal = true, vertical = false });
                Level.Add(new BreakableSurface(position.x, position.y - 1.5f, xSize, 1) { breakableMode = "U", horizontal = true, vertical = false });

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
            Graphics.DrawRect(position + new Vec2(-xSize / 2, -ySize / 2 - 3), position + new Vec2(xSize / 2, ySize / 2 - 3), Color.LightGray, 1f, true, 1);
        }
    }
}
