using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Operators|Attack")]
    public class ThatcherOPEQ : OPEQ
    {
        public ThatcherOPEQ(float xpos, float ypos) : base(xpos, ypos)
        {
            description =
"The Thatcher's EMP grenade is \n\n" +
"able to disable all electronic \n\n" +
"devices of defenders who have \n\n" +
"fallen into the range of action. \n\n" +
" \n\n" +
" \n\n" +
"        ";
            name = "THATCHER";
            oper = new Thatcher(this.position.x, this.position.y);
            _sprite = new SpriteMap(GetPath("Sprites/OperatorIcons.png"), 24, 24);
            _sprite.frame = 9;
            graphic = _sprite;
        }
    }
    public class Thatcher : Attacker
    {
        public Thatcher(float xpos, float ypos) : base(xpos, ypos)
        {
            Primary = new List<GunDev>()
            {
                new L85A2(this.position.x, this.position.y),
                new AR33(position.x, position.y)
                
            };

            Secondary = new List<GunDev>()
            {
                new P226(this.position.x, this.position.y)
            };

            Devices = new List<Device>()
            {
                new Claymore(this.position.x, this.position.y),
                new Breach(this.position.x, this.position.y)
            };

            bodyLocation = "Sprites/Operators/mute.png";
            headLocation = "Sprites/Operators/thatcherHat.png";
            handLocation = "Sprites/Operators/SASHand.png";

            injureScream = "SFX/Characters/ScreamThatcher.wav";

            Knife = new Knife(this.position.x, this.position.y);
            MainDevice = new EMPgrenade(this.position.x, this.position.y);
            Phone = new Phone(this.position.x, this.position.y);
            drone = new Drone(position.x, position.y) { UsageCount = 2 };

            Armor = 2;
            Speed = 2;
            team = "Att";


            SetSprites();

            operatorID = 9;
        }
    }
}
