using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Operators|Attack")]
    public class AshOPEQ : OPEQ
    {
        public AshOPEQ(float xpos, float ypos) : base(xpos, ypos)
        {
            description =
"Ash is capable of ranged soft \n\n" +
"breaching thanks to her modified\n\n" +
"M120 CREM Breaching Rounds. \n\n" +
" \n\n" +
" \n\n" +
" \n\n" +
"        ";

            name = "ASH";
            oper = new Ash(position.x, position.y);
            _sprite = new SpriteMap(GetPath("Sprites/OperatorIcons.png"), 24, 24);
            _sprite.frame = 17;
            graphic = _sprite;
        }
    }
    public class Ash : Attacker
    {
        public Ash(float xpos, float ypos) : base(xpos, ypos)
        {
            Primary = new List<GunDev>()
            {
                new R4C(position.x, position.y) { grip = 1 },
                new G36(position.x, position.y)
            };

            Secondary = new List<GunDev>()
            {
                new FiveSeven(position.x, position.y),
                new Meusoc(position.x, position.y)
            };

            Devices = new List<Device>()
            {
                new FlashbangGrenade(position.x, position.y),
                new Breach(position.x, position.y)
            };

            bodyLocation = "Sprites/Operators/fbi.png";
            headLocation = "Sprites/Operators/ashHat.png";
            handLocation = "Sprites/Operators/FBIGloves.png";

            injureScream = "SFX/Characters/ScreamAsh.wav";

            Knife = new Knife(position.x, position.y);
            MainDevice = new GrenadeCannon(position.x, position.y);
            Phone = new Phone(position.x, position.y);
            drone = new Drone(position.x, position.y) { UsageCount = 2 };

            Armor = 1;
            Speed = 3;
            team = "Att";
            female = true;

            SetSprites();

            operatorID = 17;
        }
    }
}
