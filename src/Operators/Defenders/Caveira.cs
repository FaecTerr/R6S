using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Operators|Attack")]
    public class CaveiraOPEQ : OPEQ
    {
        public CaveiraOPEQ(float xpos, float ypos) : base(xpos, ypos)
        {
            description =
"\n\n" +
" \n\n" +
" \n\n" +
" \n\n" +
" \n\n" +
" \n\n" +
"        ";
            name = "CAVEIRA";
            oper = new Caveira(position.x, position.y);
            _sprite = new SpriteMap(GetPath("Sprites/OperatorIcons.png"), 24, 24);
            _sprite.frame = 42;
            graphic = _sprite;
        }
    }
    public class Caveira : Defender
    {
        public Caveira(float xpos, float ypos) : base(xpos, ypos)
        {

            Primary = new List<GunDev>()
            {
                new MP5K(position.x, position.y),
                new SASG11(position.x, position.y)
            };

            Secondary = new List<GunDev>()
            {
                new PMM(position.x, position.y)
            };

            Devices = new List<Device>()
            {
                new Impact(position.x, position.y),
                new ProximityAlarm(position.x, position.y)
            };

            bodyLocation = "Sprites/Operators/capitao.png";
            headLocation = "Sprites/Operators/caveiraHead.png";
            handLocation = "Sprites/Operators/fbiGloves.png";

            injureScream = "SFX/Characters/ScreamAsh.wav";

            Knife = new Knife(position.x, position.y);
            MainDevice = new SilentStep(position.x, position.y);
            Phone = new Phone(position.x, position.y);
            drone = new Drone(position.x, position.y) { UsageCount = 0 };

            Armor = 1;
            Speed = 3;
            team = "Def";
            female = true;

            SetSprites();
            operatorID = 42;

            female = true;
        }
    }
}
