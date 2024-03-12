using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Operators|Attack")]
    public class ZofiaOPEQ : OPEQ
    {
        public ZofiaOPEQ(float xpos, float ypos) : base(xpos, ypos)
        {
            description =
"KS79 LIFELINE makes use of \n\n" +
"electronically triggered \n\n" +
"projectile technology that \n\n" +
"can fire both concussion and \n\n" +
"impact ammunition. The concussion \n\n" +
"ammunition delivers a 170-decibel\n\n" +
"shockwave that impairs hearing\n\n" +
" and causes a dizzying effect.  ";

            name = "ZOFIA";
            oper = new Zofia(position.x, position.y);
            _sprite = new SpriteMap(GetPath("Sprites/OperatorIcons.png"), 24, 24);
            _sprite.frame = 27;
            graphic = _sprite;
        }
    }
    public class Zofia : Attacker
    {
        public Zofia(float xpos, float ypos) : base(xpos, ypos)
        {
            Primary = new List<GunDev>()
            {
                new R4C(position.x, position.y),
                new LMGE105(position.x, position.y)
            };

            Secondary = new List<GunDev>()
            {
                new RG15(position.x, position.y)
            };

            Devices = new List<Device>()
            {
                new Breach(position.x, position.y),
                new Claymore(position.x, position.y)
            };

            bodyLocation = "Sprites/Operators/grom.png";
            headLocation = "Sprites/Operators/zofiaHead.png";
            handLocation = "Sprites/Operators/GromHand.png";

            injureScream = "SFX/Characters/ScreamZofia.wav";

            Knife = new Knife(position.x, position.y);
            MainDevice = new LifeLine(position.x, position.y);
            Phone = new Phone(position.x, position.y);
            drone = new Drone(position.x, position.y) { UsageCount = 2 };

            Armor = 2;
            Speed = 2;
            team = "Att"; 
            female = true;


            SetSprites();

            operatorID = 27;
        }
    }
}
