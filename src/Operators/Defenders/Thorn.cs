using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Operators|Defense")]
    public class ThornOPEQ : OPEQ
    {
        public ThornOPEQ(float xpos, float ypos) : base(xpos, ypos)
        {
            description =
" \n\n" +
" \n\n" +
" \n\n" +
" \n\n" +
" \n\n" +
" \n\n" +
"       ";

            name = "THORN";
            oper = new Thorn(position.x, position.y);
            _sprite = new SpriteMap(GetPath("Sprites/OperatorIcons.png"), 24, 24);
            _sprite.frame = 62;
            graphic = _sprite;
        }
    }
    public class Thorn : Defender
    {
        public Thorn(float xpos, float ypos) : base(xpos, ypos)
        {
            Primary = new List<GunDev>()
            {
                new UZK50G(position.x, position.y),
                new M870(position.x, position.y)
            };

            Secondary = new List<GunDev>()
            {
                new P226(position.x, position.y),
                new CZ75(position.x, position.y)
            };

            Devices = new List<Device>()
            {
                new BarbedWire(position.x, position.y),
                new DeployableShield(position.x, position.y)
            };

            bodyLocation = "Sprites/Operators/gis.png";
            headLocation = "Sprites/Operators/thornHead.png";
            handLocation = "Sprites/Operators/AlibiHand.png";

            injureScream = "SFX/Characters/ScreamEla.wav";

            Knife = new Knife(position.x, position.y);
            MainDevice = new Razorbloom(position.x, position.y);
            Phone = new Phone(position.x, position.y);
            drone = new Drone(position.x, position.y) { UsageCount = 0 };

            Armor = 1;
            Speed = 3;
            team = "Def";
            female = true;


            SetSprites();

            operatorID = 62;
        }
    }
}
