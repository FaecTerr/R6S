using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Operators")]
    public class BreakableAdder : Thing
    {
        EditorProperty<float> xSize;
        EditorProperty<float> ySize;
        public BreakableAdder()
        {
            _editorName = "Breakable";
            xSize = new EditorProperty<float>(16, this, 0.1f, 320, 0.1f);
            ySize = new EditorProperty<float>(16, this, 0.1f, 320, 0.1f);
        }

        public override void Update()
        {
            base.Update();
            if(!(Level.current is Editor))
            {
                Level.Add(new BreakableSurface(position.x, position.y, xSize, ySize));
                Level.Remove(this);
            }
        }

        public override void Draw()
        {

            base.Draw();
            Graphics.DrawRect(topLeft + new Vec2(-xSize/2, -ySize/2), bottomRight + new Vec2(xSize / 2, ySize / 2), Color.Red, 1f, true, 1);
        }
    }
}
