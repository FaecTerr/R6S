using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Operators|Attack")]
    public class NokkOPEQ : OPEQ
    {
        public NokkOPEQ(float xpos, float ypos) : base(xpos, ypos)
        {
            description =
"\n\n" +
" \n\n" +
" \n\n" +
" \n\n" +
" \n\n" +
" \n\n" +
"        ";
            name = "NOKK";
            oper = new Nokk(position.x, position.y);
            _sprite = new SpriteMap(GetPath("Sprites/OperatorIcons.png"), 24, 24);
            _sprite.frame = 39;
            graphic = _sprite;
        }
    }
    public class Nokk : Attacker
    {
        public Nokk(float xpos, float ypos) : base(xpos, ypos)
        {

            Primary = new List<GunDev>()
            {
                new FMG(position.x, position.y),
                new M590A1(position.x, position.y)
            };

            Secondary = new List<GunDev>()
            {
                new D50(position.x, position.y),
                new FiveSeven(position.x, position.y)
            };

            Devices = new List<Device>()
            {
                new FragGrenade(position.x, position.y),
                new Claymore(position.x, position.y)
            };

            bodyLocation = "Sprites/Operators/fbi.png";
            headLocation = "Sprites/Operators/NokkHead.png";
            handLocation = "Sprites/Operators/NokkHand.png";

            injureScream = "SFX/Characters/ScreamEla.wav";

            Knife = new Knife(position.x, position.y);
            MainDevice = new HEL(position.x, position.y);
            Phone = new Phone(position.x, position.y);
            drone = new Drone(position.x, position.y) { UsageCount = 2 };

            Armor = 2;
            Speed = 2;
            team = "Att";
            female = true;

            SetSprites();
            operatorID = 39;
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
