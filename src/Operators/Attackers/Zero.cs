using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Operators|Attack")]
    public class ZeroOPEQ : OPEQ
    {
        public ZeroOPEQ(float xpos, float ypos) : base(xpos, ypos)
        {
            description = "";


            name = "ZERO";
            oper = new Zero(position.x, position.y);
            _sprite = new SpriteMap(GetPath("Sprites/OperatorIcons.png"), 24, 24);
            _sprite.frame = 51;
            graphic = _sprite;
        }
    }
    public class Zero : Attacker
    {
        public Zero(float xpos, float ypos) : base(xpos, ypos)
        {
            Primary = new List<GunDev>()
            {
                new MP7(position.x, position.y)
            };

            Secondary = new List<GunDev>()
            {
                new FiveSeven(position.x, position.y) { muzzle = 1 }
            };

            Devices = new List<Device>()
            {
                new Breach(position.x, position.y),
                new Claymore(position.x, position.y)
            };

            bodyLocation = "Sprites/Operators/sam.png";
            headLocation = "Sprites/Operators/SamHead.png";
            handLocation = "Sprites/Operators/GromHand.png";

            injureScream = "SFX/Characters/ScreamKaid.wav";

            Knife = new Knife(position.x, position.y);
            MainDevice = new ArgusLauncher(position.x, position.y);
            Phone = new Phone(position.x, position.y);
            drone = new Drone(position.x, position.y) { UsageCount = 2 };

            Armor = 2;
            Speed = 2;
            team = "Att";

            SetSprites();

            operatorID = 51;
        }
    }
}
