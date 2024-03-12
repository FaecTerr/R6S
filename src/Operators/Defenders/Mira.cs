using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Operators|Attack")]
    public class MiraOPEQ : OPEQ
    {
        public MiraOPEQ(float xpos, float ypos) : base(xpos, ypos)
        {
            description =
"\n\n" +
" \n\n" +
" \n\n" +
" \n\n" +
" \n\n" +
" \n\n" +
"        ";
            name = "MIRA";
            oper = new Mira(position.x, position.y);
            _sprite = new SpriteMap(GetPath("Sprites/OperatorIcons.png"), 24, 24);
            _sprite.frame = 44;
            graphic = _sprite;
        }
    }
    public class Mira : Defender
    {
        public Mira(float xpos, float ypos) : base(xpos, ypos)
        {

            Primary = new List<GunDev>()
            {
                new Vector45ACP(position.x, position.y),
                new M590A1(position.x, position.y)
            };

            Secondary = new List<GunDev>()
            {
                new ITA12S(position.x, position.y)
            };

            Devices = new List<Device>()
            {
                new C4(position.x, position.y),
                new ProximityAlarm(position.x, position.y)
            };

            bodyLocation = "Sprites/Operators/jackal.png";
            headLocation = "Sprites/Operators/miraHead.png";
            handLocation = "Sprites/Operators/fbiGloves.png";

            injureScream = "SFX/Characters/ScreamAsh.wav";

            Knife = new Knife(position.x, position.y);
            MainDevice = new BlackMirror(position.x, position.y);
            Phone = new Phone(position.x, position.y);
            drone = new Drone(position.x, position.y) { UsageCount = 0 };

            Armor = 3;
            Speed = 1;
            team = "Def";
            female = true;

            SetSprites();
            operatorID = 44;

            female = true;
        }
    }
}
