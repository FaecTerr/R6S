using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Operators|Attack")]
    public class MaverickOPEQ : OPEQ
    {
        public MaverickOPEQ(float xpos, float ypos) : base(xpos, ypos)
        {
            description =
"The Maverick's breach torch can \n\n" +
"help the team get through in \n\n" +
"places where other hardbreachers \n\n" +
"can't do anything. \n\n" +
" \n\n" +
" \n\n" +
"        ";

            name = "MAVERICK";
            oper = new Maverick(position.x, position.y);
            _sprite = new SpriteMap(GetPath("Sprites/OperatorIcons.png"), 24, 24);
            _sprite.frame = 21;
            graphic = _sprite;
        }
    }
    public class Maverick : Attacker
    {
        public Maverick(float xpos, float ypos) : base(xpos, ypos)
        {
            Primary = new List<GunDev>()
            {
                new M4(position.x, position.y),
                new AR1550(position.x, position.y)
            };

            Secondary = new List<GunDev>()
            {
                new FiveSeven(position.x, position.y)
            };

            Devices = new List<Device>()
            {
                new FragGrenade(position.x, position.y),
                new Claymore(position.x, position.y)
            };

            bodyLocation = "Sprites/Operators/maverick.png";
            headLocation = "Sprites/Operators/maverickHat.png";
            handLocation = "Sprites/Operators/MaverickHand.png";

            Knife = new Knife(position.x, position.y);
            MainDevice = new MVG(position.x, position.y);
            Phone = new Phone(position.x, position.y);
            drone = new Drone(position.x, position.y) { UsageCount = 2 };

            Armor = 1;
            Speed = 3;
            team = "Att";


            SetSprites();

            operatorID = 21;
        }
    }
}
