using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    [EditorGroup("Faecterr's|Breakables")]
    public class SoftFloorSES : Thing
    {
        public float ySize = 12;
        EditorProperty<float> xSize;
        EditorProperty<float> hatchSize;
        EditorProperty<float> hatchOffset;

        public SoftFloorSES()
        {
            _editorName = "Floor - SES";
            xSize = new EditorProperty<float>(16, this, 4f, 80, 4f);
            hatchSize = new EditorProperty<float>(16, this, 4f, 80, 4f);
            hatchOffset = new EditorProperty<float>(0, this, -40f, 40, 2f);
        }

        public override void Update()
        {
            base.Update();
            if (!(Level.current is Editor))
            {
                Level.Add(new BreakableSurface(position.x, position.y - 6.5f, xSize, 3) { breakableMode = "S", horizontal = true, vertical = false });
                Level.Add(new BreakableSurface(position.x, position.y - 4, xSize, 2) { breakableMode = "S", horizontal = true, vertical = false, lightColored = true });
                Level.Add(new BreakableSurface(position.x, position.y - 1.5f, xSize, 3) { breakableMode = "S", horizontal = true, vertical = false });

                Level.Add(new BreakableSurface(position.x, position.y, xSize, 2) { breakableMode = "E", horizontal = true, vertical = false });

                Level.Add(new SurfaceStationary(position.x, position.y - 2) { collisionSize = new Vec2(xSize, 12), horizontal = true});

                Level.Add(new HatchLeftover(position.x + hatchOffset, position.y) { collisionSize = new Vec2(hatchSize, 16), collisionOffset = new Vec2(-hatchSize * 0.5f, -8f)});

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

                Graphics.DrawRect(position + new Vec2(-xSize / 2, -7), position + new Vec2(xSize / 2, 1), Color.Red * 0.5f, 1f, false, 1);
                Graphics.DrawRect(position + new Vec2(-xSize / 2, -8), position + new Vec2(xSize / 2, 2), Color.Orange * 0.5f, 0.99f, true, 1);

                Graphics.DrawDottedRect(position + new Vec2(hatchOffset + hatchSize * -0.5f, -8), position + new Vec2(hatchOffset + hatchSize * 0.5f, 8), Color.BlueViolet, 1f, 1f, 8);
            }
            base.Draw();
        }
    }

    [EditorGroup("Faecterr's|Breakables")]
    public class HardFloorSES : Thing
    {
        public float ySize = 12;
        EditorProperty<float> xSize;
        public HardFloorSES()
        {
            _editorName = "Floor - HHH";
            xSize = new EditorProperty<float>(16, this, 4f, 80, 4f);
        }

        public override void Update()
        {
            base.Update();
            if (!(Level.current is Editor))
            {
                Level.Add(new BreakableSurface(position.x, position.y - 4.5f, xSize, 3) { breakableMode = "H", horizontal = true, vertical = false, lightColored = true });
                Level.Add(new BreakableSurface(position.x, position.y - 2f, xSize, 2) { breakableMode = "H", horizontal = true, vertical = false });
                Level.Add(new BreakableSurface(position.x, position.y + 0.5f, xSize, 3) { breakableMode = "H", horizontal = true, vertical = false, lightColored = true });
                
                Level.Add(new HatchLeftover(position.x, position.y));

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
            Graphics.DrawRect(position + new Vec2(-xSize / 2, -ySize / 2 - 2), position + new Vec2(xSize / 2, ySize / 2 - 2), Color.Gray, 1f, false, 1);
            Graphics.DrawRect(position + new Vec2(-xSize / 2, -ySize / 2 - 2), position + new Vec2(xSize / 2, ySize / 2 - 2), Color.SteelBlue, 0.99f, true, 1);
        }
    }
}
