using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Operators|Attack")]
    public class ClashOPEQ : OPEQ
    {
        public ClashOPEQ(float xpos, float ypos) : base(xpos, ypos)
        {
            description =
"\n\n" +
" \n\n" +
" \n\n" +
" \n\n" +
" \n\n" +
" \n\n" +
"        ";
            name = "CLASH";
            oper = new Clash(position.x, position.y);
            _sprite = new SpriteMap(GetPath("Sprites/OperatorIcons.png"), 24, 24);
            _sprite.frame = 48;
            graphic = _sprite;
        }
    }
    public class Clash : Defender
    {
        public Clash(float xpos, float ypos) : base(xpos, ypos)
        {

            Primary = new List<GunDev>()
            {
                new CCEShield(position.x, position.y)
            };

            Secondary = new List<GunDev>()
            {
                new SMG12(position.x, position.y),
                new CZ75(position.x, position.y)
            };

            Devices = new List<Device>()
            {
                new Impact(position.x, position.y),
                new BarbedWire(position.x, position.y)
            };

            bodyLocation = "Sprites/Operators/capitao.png";
            headLocation = "Sprites/Operators/clashHead.png";
            handLocation = "Sprites/Operators/fbiGloves.png";

            injureScream = "SFX/Characters/ScreamAsh.wav";

            Knife = new Knife(position.x, position.y);
            MainDevice = new CCEblast(position.x, position.y);
            Phone = new Phone(position.x, position.y);
            drone = new Drone(position.x, position.y) { UsageCount = 0 };

            Armor = 3;
            Speed = 1;
            team = "Def";
            female = true;

            SetSprites();
            operatorID = 48;
        }
    }
}
