using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Operators|Defense")]
    public class MuteOPEQ : OPEQ
    {
        public MuteOPEQ(float xpos, float ypos) : base(xpos, ypos)
        {
            description =
"Signal disruptor is a useful\n\n" +
"useful device if you want \n\n" +
"to deprive the enemy \n\n" +
"of important intelligence. \n\n" +
" \n\n" +
" \n\n" +
"        ";

            name = "MUTE";
            oper = new Mute(this.position.x, this.position.y);
            _sprite = new SpriteMap(GetPath("Sprites/OperatorIcons.png"), 24, 24);
            _sprite.frame = 10;
            graphic = _sprite;
        }
    }
    public class Mute : Operators
    {
        public Mute(float xpos, float ypos) : base(xpos, ypos)
        {
            Primary = new List<GunDev>()
            {
                new MP5K(this.position.x, this.position.y),
                new M590A1(this.position.x, this.position.y)
            };

            Secondary = new List<GunDev>()
            {
                new SMG11(this.position.x, this.position.y),
                new P226(this.position.x, this.position.y)
            };

            Devices = new List<Device>()
            {
                new C4(this.position.x, this.position.y),
                new ProximityAlarm(this.position.x, this.position.y)
            };

            bodyLocation = "Sprites/Operators/mute.png";
            headLocation = "Sprites/Operators/muteHat.png";
            handLocation = "Sprites/Operators/SASMuteHand.png";

            injureScream = "SFX/Characters/ScreamMute.wav";

            Knife = new Knife(this.position.x, this.position.y);
            MainDevice = new Jammer(this.position.x, this.position.y);
            Phone = new Phone(this.position.x, this.position.y);
            drone = new Drone(position.x, position.y) { UsageCount = 0 };

            this.Armor = 2;
            this.Speed = 2;
            this.team = "Def";

            SetSprites();

            operatorID = 10;
        }
    }
}
