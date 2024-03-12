using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Operators|Attack")]
    public class CapitaoOPEQ : OPEQ
    {
        public CapitaoOPEQ(float xpos, float ypos) : base(xpos, ypos)
        {
            description =
"Armed with a crossbow, Capitao \n\n" +
"can either block the enemy's view \n\n" +
"with a smoke arrow, or drive them \n\n" +
"out of position or kill them \n\n" +
"with choking arrows  \n\n" +
" \n\n" +
"        ";

            name = "CAPITAO";
            oper = new Capitao(position.x, position.y);
            _sprite = new SpriteMap(GetPath("Sprites/OperatorIcons.png"), 24, 24);
            _sprite.frame = 23;
            graphic = _sprite;
        }
    }
    public class Capitao : Attacker
    {
        public Capitao(float xpos, float ypos) : base(xpos, ypos)
        {
            Primary = new List<GunDev>()
            {
                new COMMANDO552(position.x, position.y),
                new RP41(position.x, position.y)
            };

            Secondary = new List<GunDev>()
            {
                new P226(position.x, position.y)
            };

            Devices = new List<Device>()
            {
                new FlashbangGrenade(position.x, position.y),
                new Claymore(position.x, position.y)
            };

            bodyLocation = "Sprites/Operators/capitao.png";
            headLocation = "Sprites/Operators/capitaoHead.png";
            handLocation = "Sprites/Operators/CapitaoGloves.png";

            injureScream = "SFX/Characters/ScreamCapitao.wav";

            Knife = new Knife(position.x, position.y);
            MainDevice = new Crossbow(position.x, position.y);
            Phone = new Phone(position.x, position.y);
            drone = new Drone(position.x, position.y) { UsageCount = 2 };

            Armor = 1;
            Speed = 3;
            team = "Att";


            SetSprites();

            operatorID = 23;
        }
    }
}
