using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Devices|Weapon")]
    public class LifeLine : Launchers
    {
        public LifeLine(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Devices/GrenadeCannon.png"), 22, 14, false);
            graphic = _sprite;
            _sprite.frame = 1;
            center = new Vec2(11f, 7f);
            collisionSize = new Vec2(20f, 12f);
            collisionOffset = new Vec2(-10f, -6f);

            _canRaise = true;
            weight = 0.9f;
            thickness = 0.1f;

            placeable = false;
            breakable = false;
            zeroSpeed = false;
            scannable = false;

            Missiles1 = 2;
            Missiles2 = 2;

            maxMode = 1;

            index = 27;

            jammResistance = true;
        }

        public override void SetMissile()
        {
            missile = new ContactGrenade(position.x + 10 * offDir, position.y - 3);
            missile1 = new ShockGrenade(position.x, position.y);

            base.SetMissile();
        }        
    }
}
