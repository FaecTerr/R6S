using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Guns|Devices|Weapon")]
    public class Crossbow : Launchers
    {
        public Crossbow(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Devices/Crossbow.png"), 34, 10, false);
            graphic = _sprite;
            _sprite.frame = 0;
            center = new Vec2(17f, 5f);
            collisionSize = new Vec2(34f, 10f);
            collisionOffset = new Vec2(-17f, -5f);
            _canRaise = true;
            weight = 0.9f;
            thickness = 0.1f;
            placeable = false;
            breakable = false;
            zeroSpeed = false;

            maxMode = 1;

            Missiles1 = 2;
            Missiles2 = 2;

            index = 23;
        }

        public override void SetMissile()
        {
            missile = new FireBolt(position.x + 10 * offDir, position.y);
            missile1 = new SmokeBolt(position.x + 10 * offDir, position.y);
            base.SetMissile();
        }
    }
}
