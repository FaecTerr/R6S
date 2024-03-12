using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    [EditorGroup("Faecterr's|Scripting")]
    public class Spawn : Thing
    {
        public SpriteMap _sprite;
        public EditorProperty<string> team;
        public EditorProperty<string> location;
        public string _team;
        public string _location;
        public Operators oper;

        public Spawn(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Spawn.png"), 32, 32, false);
            graphic = _sprite;
            center = new Vec2(16, 16f);
            collisionSize = new Vec2(30f, 30f);
            collisionOffset = new Vec2(-15f, -15f);
            team = new EditorProperty<string>("");
            location = new EditorProperty<string>("");
        }

        public override void Update()
        {
            base.Update();
            if(!(Level.current is Editor))
            {
                _sprite.alpha = 0;
                _team = team;
                _location = location;
            }
            if(_team == "Def")
            {
                _sprite.frame = 1;
            }
            if(_team == "Att")
            {
                _sprite.frame = 0;
            }
        }

        public override void Draw()
        {
            if (_team == "Def")
            {
                _sprite.frame = 1;
            }
            if (_team == "Att")
            {
                _sprite.frame = 0;
            }
            base.Draw();
            _sprite.flipH = offDir == -1;
        }
    }
}
