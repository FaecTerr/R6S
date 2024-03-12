using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class BindCategory : Thing
    {
        public bool picked;
        public bool locked;
                
        public string bind;
       
        public BindCategory(float xpos, float ypos) : base(xpos, ypos)
        {
            depth = 0.4f;
            layer = Layer.Foreground;
        }

        public override void Draw()
        {
            base.Draw();

            int l = bind.Length;
            string text = bind;

            BitmapFont f = new BitmapFont("biosFont", 8, -1);
            //Graphics.DrawStringOutline(text, position + new Vec2(76, -20), Color.White, Color.Black, 0.5f, null, 1f);
            f.DrawOutline(text, position + new Vec2(76, -20), Color.White, Color.Black, 0.5f);
            
            Graphics.DrawLine(position + new Vec2(-10, -16), position + new Vec2(68, -16), Color.White, 2f, 0.1f);
            Graphics.DrawLine(position + new Vec2(80 + 8 * l, -16), position + new Vec2(280, -16), Color.White, 2f, 0.1f);
        }
    }
}
