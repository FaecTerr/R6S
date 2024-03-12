using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class MenuSign : Thing
    {
        public MenuSign()
        {
            layer = Layer.Foreground;
        }
        public override void Draw()
        {
            base.Draw();
            SpriteMap _sprite = new SpriteMap(GetPath("Sprites/GUI/F2Menu"), 116, 54);
            _sprite.CenterOrigin();
            _sprite.depth = 1f;
            Graphics.Draw(_sprite, 2, 160, 20);
        }
    }
}
