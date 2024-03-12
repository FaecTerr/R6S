using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    [EditorGroup("Faecterr's|Breakables")]
    public class HardWallEHE : Thing
    {
        public float xSize = 10;
        EditorProperty<float> ySize;
        public HardWallEHE()
        {
            _editorName = "Wall - HHE";
            ySize = new EditorProperty<float>(16, this, 4f, 80, 4f);
        }

        public override void Update()
        {
            base.Update();
            if (!(Level.current is Editor))
            {
                if (flipHorizontal)
                {
                    Level.Add(new BreakableSurface(position.x + 4, position.y, 2, ySize) { breakableMode = "E", horizontal = false, vertical = true });

                    Level.Add(new BreakableSurface(position.x - 4.5f, position.y, 1, ySize) { breakableMode = "H", horizontal = false, vertical = true, lightColored = true });
                    Level.Add(new BreakableSurface(position.x - 3.5f, position.y, 1, ySize) { breakableMode = "H", horizontal = false, vertical = true });

                    for (int i = 0; i < (int)((ySize - 6) / 6) + 2; i++)
                    {
                        Level.Add(new BreakableSurface(position.x + 1, bottom + ySize * 0.5f - i * 6 - 3, 8, 1) { breakableMode = "H", horizontal = false, vertical = true });
                    }
                }
                else
                {
                    Level.Add(new BreakableSurface(position.x - 4, position.y, 2, ySize) { breakableMode = "E", horizontal = false, vertical = true });

                    Level.Add(new BreakableSurface(position.x + 4.5f, position.y, 1, ySize) { breakableMode = "H", horizontal = false, vertical = true, lightColored = true });
                    Level.Add(new BreakableSurface(position.x + 3.5f, position.y, 1, ySize) { breakableMode = "H", horizontal = false, vertical = true });

                    for (int i = 0; i < (int)((ySize - 6) / 6) + 2; i++)
                    {
                        Level.Add(new BreakableSurface(position.x - 1, bottom + ySize * 0.5f - i * 6 - 3, 8, 1) { breakableMode = "H", horizontal = false, vertical = true });
                    }
                }

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
            Graphics.DrawRect(position + new Vec2(-xSize / 2, -ySize / 2), position + new Vec2(xSize / 2, ySize / 2), Color.DarkGray, 1f, true, 1);
        }
    }
}
