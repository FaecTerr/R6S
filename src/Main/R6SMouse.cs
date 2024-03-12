using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class R6SMouse : IAutoUpdate
    {
        public SpriteMap _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Aim.png"), 17, 17);
        public void Update()
        {
            _sprite.position = Mouse.positionScreen;
            
        }
    }
}
