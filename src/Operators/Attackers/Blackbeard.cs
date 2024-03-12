using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Operators|Attack")]
    public class BlackbeardOPEQ : OPEQ
    {
        public BlackbeardOPEQ(float xpos, float ypos) : base(xpos, ypos)
        {
            description =
"\n\n" +
" \n\n" +
" \n\n" +
" \n\n" +
" \n\n" +
" \n\n" +
"        ";
            name = "BLACKBEARD";
            oper = new Blackbeard(position.x, position.y);
            _sprite = new SpriteMap(GetPath("Sprites/OperatorIcons.png"), 24, 24);
            _sprite.frame = 43;
            graphic = _sprite;
        }
    }
    public class Blackbeard : Attacker
    {
        public Blackbeard(float xpos, float ypos) : base(xpos, ypos)
        {

            Primary = new List<GunDev>()
            {
                new xi556(position.x, position.y),
                new HK417(position.x, position.y)
            };

            Secondary = new List<GunDev>()
            {
                new D50(position.x, position.y)
            };

            Devices = new List<Device>()
            {
                new FlashbangGrenade(position.x, position.y),
                new Claymore(position.x, position.y)
            };

            bodyLocation = "Sprites/Operators/valkyrie.png";
            headLocation = "Sprites/Operators/BlackbeardHead.png";
            handLocation = "Sprites/Operators/fbiGloves.png";

            injureScream = "SFX/Characters/ScreamJager.wav";

            Knife = new Knife(position.x, position.y);
            MainDevice = new BlackbeardShield(position.x, position.y);
            Phone = new Phone(position.x, position.y);
            drone = new Drone(position.x, position.y) { UsageCount = 2 };

            Armor = 2;
            Speed = 2;
            team = "Att";

            SetSprites();
            operatorID = 43;
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
