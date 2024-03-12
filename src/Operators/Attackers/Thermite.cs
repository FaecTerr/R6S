using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Operators|Attack")]
    public class ThermiteOPEQ : OPEQ
    {
        public ThermiteOPEQ(float xpos, float ypos) : base(xpos, ypos)
        {
            description =
"Thermite is an important backline \n\n" +
"support for an attacking team. \n\n" +
"As one of the only hard-breachers \n\n" +
"in the game, losing Thermite \n\n" +
"early in the round will hinder \n\n" +
"your team significantly. \n\n" +
"        ";

            name = "THERMITE";
            oper = new Thermite(position.x, position.y);
            _sprite = new SpriteMap(GetPath("Sprites/OperatorIcons.png"), 24, 24);
            _sprite.frame = 19;
            graphic = _sprite;
        }
    }
    public class Thermite : Attacker
    {
        public Thermite(float xpos, float ypos) : base(xpos, ypos)
        {
            Primary = new List<GunDev>()
            {
                new xi556(position.x, position.y),
                new M1014(position.x, position.y)
            };

            Secondary = new List<GunDev>()
            {
                new FiveSeven(position.x, position.y),
                new Meusoc(position.x, position.y)
            };

            Devices = new List<Device>()
            {
                new FlashbangGrenade(position.x, position.y),
                new Claymore(position.x, position.y)
            };

            bodyLocation = "Sprites/Operators/fbi.png";
            headLocation = "Sprites/Operators/thermiteHat.png";
            handLocation = "Sprites/Operators/FBIThermiteHand.png";

            injureScream = "SFX/Characters/ScreamThermite.wav";

            Knife = new Knife(position.x, position.y);
            MainDevice = new ThermalBreach(position.x, position.y);
            Phone = new Phone(position.x, position.y);
            drone = new Drone(position.x, position.y) { UsageCount = 2 };

            Armor = 2;
            Speed = 2;
            team = "Att";

            SetSprites();

            operatorID = 19;
        }
    }
}
