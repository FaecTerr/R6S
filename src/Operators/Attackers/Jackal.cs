using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Operators|Attack")]
    public class JackalOPEQ : OPEQ
    {
        public JackalOPEQ(float xpos, float ypos) : base(xpos, ypos)
        {
            description =
"As a tracker, Jackal can scan \n\n" +
"the tracks left by defenders \n\n" +
"and give their position to the team \n\n" +
" \n\n" +
" \n\n" +
" \n\n" +
"        ";

            name = "JACKAL";
            oper = new Jackal(position.x, position.y);
            _sprite = new SpriteMap(GetPath("Sprites/OperatorIcons.png"), 24, 24);
            _sprite.frame = 25;
            graphic = _sprite;
        }
    }
    public class Jackal : Attacker
    {
        public Jackal(float xpos, float ypos) : base(xpos, ypos)
        {
            Primary = new List<GunDev>()
            {
                new C7E(position.x, position.y),
                new PDW9(position.x, position.y)
            };

            Secondary = new List<GunDev>()
            {
                new GSH(position.x, position.y),
                new ITA12S(position.x, position.y)
            };

            Devices = new List<Device>()
            {
                new SmokeGrenade(position.x, position.y),
                new Claymore(position.x, position.y)
            };

            bodyLocation = "Sprites/Operators/jackal.png";
            headLocation = "Sprites/Operators/jackalHead.png";
            handLocation = "Sprites/Operators/jackalGloves.png";

            injureScream = "SFX/Characters/ScreamJackal.wav";

            Knife = new Knife(position.x, position.y);
            MainDevice = new JackalTracker(position.x, position.y);
            Phone = new Phone(position.x, position.y);
            drone = new Drone(position.x, position.y) { UsageCount = 2 };

            Armor = 2;
            Speed = 2;
            team = "Att";


            SetSprites();

            operatorID = 25;
        }
    }
}
