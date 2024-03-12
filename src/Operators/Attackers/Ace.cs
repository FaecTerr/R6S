using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Operators|Attack")]
    public class AceOPEQ : OPEQ
    {
        public AceOPEQ(float xpos, float ypos) : base(xpos, ypos)
        {
            description =
" \n\n" +
" \n\n" +
" \n\n" +
" \n\n" +
" \n\n" +
" \n\n" +
"        ";

            name = "ACE";
            oper = new Ace(position.x, position.y);
            _sprite = new SpriteMap(GetPath("Sprites/OperatorIcons.png"), 24, 24);
            _sprite.frame = 53;
            graphic = _sprite;
        }
    }
    public class Ace : Attacker
    {
        public Ace(float xpos, float ypos) : base(xpos, ypos)
        {
            Primary = new List<GunDev>()
            {
                new AK12(position.x, position.y) { grip = 1 },
                new M590A1(position.x, position.y)
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
            headLocation = "Sprites/Operators/aceHead.png";
            handLocation = "Sprites/Operators/FBIGloves.png";

            injureScream = "SFX/Characters/ScreamBlitz.wav";

            Knife = new Knife(position.x, position.y);
            MainDevice = new Selma(position.x, position.y);
            Phone = new Phone(position.x, position.y);
            drone = new Drone(position.x, position.y) { UsageCount = 2 };

            Armor = 2;
            Speed = 2;
            team = "Att";

            SetSprites();

            operatorID = 53;
        }
    }
}
