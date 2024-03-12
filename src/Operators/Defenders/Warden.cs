using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Operators|Attack")]
    public class WardenOPEQ : OPEQ
    {
        public WardenOPEQ(float xpos, float ypos) : base(xpos, ypos)
        {
            description =
"\n\n" +
" \n\n" +
" \n\n" +
" \n\n" +
" \n\n" +
" \n\n" +
"        ";
            name = "WARDEN";
            oper = new Warden(position.x, position.y);
            _sprite = new SpriteMap(GetPath("Sprites/OperatorIcons.png"), 24, 24);
            _sprite.frame = 38;
            graphic = _sprite;
        }
    }
    public class Warden : Defender
    {
        public Warden(float xpos, float ypos) : base(xpos, ypos)
        {

            Primary = new List<GunDev>()
            {
                new MPX(position.x, position.y),
                new M1014(position.x, position.y)
            };

            Secondary = new List<GunDev>()
            {
                new RG15(position.x, position.y),
                new SMG12(position.x, position.y)
            };

            Devices = new List<Device>()
            {
                new C4(position.x, position.y),
                new Impact(position.x, position.y)
            };

            bodyLocation = "Sprites/Operators/warden.png";
            headLocation = "Sprites/Operators/wardenHead.png";
            handLocation = "Sprites/Operators/fbiGloves.png";

            injureScream = "SFX/Characters/ScreamKapkan.wav";

            Knife = new Knife(position.x, position.y);
            MainDevice = new Glance(position.x, position.y);
            Phone = new Phone(position.x, position.y);
            drone = new Drone(position.x, position.y) { UsageCount = 0 };

            Armor = 2;
            Speed = 2;
            team = "Def";
            female = true;

            SetSprites();
            operatorID = 38;
        }
    }
}
