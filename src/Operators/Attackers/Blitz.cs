using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Operators|Attack")]
    public class BlitzOPEQ : OPEQ
    {
        public BlitzOPEQ(float xpos, float ypos) : base(xpos, ypos)
        {
            name = "BLITZ";

            description =
    "G52 is tactical shield \n\n" +
    "which Blitz using. This \n\n" +
    "shield can flash your \n\n" +
    "enemies. Your legs or \n\n" +
    "head is still reachcable \n\n" +
    "for enemies. \n\n" +
    "";


            oper = new Blitz(position.x, position.y);
            _sprite = new SpriteMap(GetPath("Sprites/OperatorIcons.png"), 24, 24);
            _sprite.frame = 1;
            graphic = _sprite;
        }
    }
    public class Blitz : Attacker
    {
        public Blitz(float xpos, float ypos) : base(xpos, ypos)
        {
            Primary = new List<GunDev>()
            {
                new FlashShield(position.x, position.y)
            };

            Secondary = new List<GunDev>()
            {
                new P12(position.x, position.y),
            };

            Devices = new List<Device>()
            {
                new SmokeGrenade(position.x, position.y),
                new HardBreach(position.x, position.y)
            };

            bodyLocation = "Sprites/Operators/gsg9.png";
            headLocation = "Sprites/Operators/blitzHat.png";
            handLocation = "Sprites/Operators/GSGGloves.png";

            injureScream = "SFX/Characters/ScreamBlitz.wav";

            Knife = new Knife(position.x, position.y);
            MainDevice = new Flash(position.x, position.y);
            Phone = new Phone(position.x, position.y);
            drone = new Drone(position.x, position.y) { UsageCount = 2 };

            Armor = 2;
            Speed = 2;
            team = "Att";

            SetSprites();

            operatorID = 1;
        }
    }
}
