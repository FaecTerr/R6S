using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Operators|Defense")]
    public class ThunderbirdOPEQ : OPEQ
    {
        public ThunderbirdOPEQ(float xpos, float ypos) : base(xpos, ypos)
        {
            description =
" \n\n" +
" \n\n" +
" \n\n" +
" \n\n" +
" \n\n" +
" \n\n" +
"       ";
            name = "THUNDERBIRD";
            oper = new Thunderbird(position.x, position.y);
            _sprite = new SpriteMap(GetPath("Sprites/OperatorIcons.png"), 24, 24);
            _sprite.frame = 54;
            graphic = _sprite;
        }
    }
    public class Thunderbird : Defender
    {
        public Thunderbird(float xpos, float ypos) : base(xpos, ypos)
        {
            Primary = new List<GunDev>()
            {
                new Spear308(position.x, position.y) { muzzle = 1 },
                new SIX12SD(position.x, position.y)
            };

            Secondary = new List<GunDev>()
            {
                new P226(position.x, position.y),
                new Bearing9(position.x, position.y)
            };

            Devices = new List<Device>()
            {
                new Impact(position.x, position.y),
                new C4(position.x, position.y)
            };

            bodyLocation = "Sprites/Operators/fbi.png";
            headLocation = "Sprites/Operators/ThunderbirdHead.png";
            handLocation = "Sprites/Operators/FBIHand.png";

            injureScream = "SFX/Characters/ScreamEla.wav";

            Knife = new Knife(position.x, position.y);
            MainDevice = new KonaStation(position.x, position.y);
            Phone = new Phone(position.x, position.y);
            drone = new Drone(position.x, position.y) { UsageCount = 0 };

            Armor = 3;
            Speed = 1;
            team = "Def";
            female = true;


            SetSprites();

            operatorID = 54;
        }
    }
}
