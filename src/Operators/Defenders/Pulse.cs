using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Operators|Defense")]
    public class PulseOPEQ : OPEQ
    {
        public PulseOPEQ(float xpos, float ypos) : base(xpos, ypos)
        {
            description =
"Pulse is a roamer and intel\n\n" +
"Operator for a defending team. \n\n" +
"Thanks to his HB-5 Cardiac \n\n" +
"Sensor, Pulse can track \n\n" +
"attackers through surfaces, \n\n" +
"gathering valuable intel \n\n" +
"on their position.         ";

            name = "PULSE";
            oper = new Pulse(this.position.x, this.position.y);
            _sprite = new SpriteMap(GetPath("Sprites/OperatorIcons.png"), 24, 24);
            _sprite.frame = 16;
            graphic = _sprite;
        }
    }
    public class Pulse : Defender
    {
        public Pulse(float xpos, float ypos) : base(xpos, ypos)
        {
            Primary = new List<GunDev>()
            {
                new UMP45(position.x, position.y),
                new M1014(position.x, position.y)
            };

            Secondary = new List<GunDev>()
            {
                new FiveSeven(position.x, position.y),
                new Meusoc(position.x, position.y)
            };

            Devices = new List<Device>()
            {
                new C4(position.x, position.y),
                new DeployableShield(position.x, position.y)
            };

            bodyLocation = "Sprites/Operators/fbi.png";
            headLocation = "Sprites/Operators/pulseHat.png";
            handLocation = "Sprites/Operators/FBIHand.png";

            injureScream = "SFX/Characters/ScreamPulse.wav";

            Knife = new Knife(position.x, position.y);
            MainDevice = new PulseScanner(position.x, position.y);
            Phone = new NHPhone(position.x, position.y);
            drone = new Drone(position.x, position.y) { UsageCount = 0 };

            Armor = 1;
            Speed = 3;
            team = "Def";

            SetSprites();

            operatorID = 16;
        }
    }
}
