using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Operators|Attack")]
    public class HibanaOPEQ : OPEQ
    {
        public HibanaOPEQ(float xpos, float ypos) : base(xpos, ypos)
        {
            description =
"\n\n" +
" \n\n" +
" \n\n" +
" \n\n" +
" \n\n" +
" \n\n" +
"        ";
            name = "HIBANA";
            oper = new Hibana(position.x, position.y);
            _sprite = new SpriteMap(GetPath("Sprites/OperatorIcons.png"), 24, 24);
            _sprite.frame = 45;
            graphic = _sprite;
        }
    }
    public class Hibana : Attacker
    {
        public Hibana(float xpos, float ypos) : base(xpos, ypos)
        {

            Primary = new List<GunDev>()
            {
                //Type-89
                new xi556(position.x, position.y),
                new M870(position.x, position.y)
            };

            Secondary = new List<GunDev>()
            {
                new Bearing9(position.x, position.y),
                new P229(position.x, position.y)
            };

            Devices = new List<Device>()
            {
                new FlashbangGrenade(position.x, position.y),
                new Breach(position.x, position.y)
            };

            bodyLocation = "Sprites/Operators/echo.png";
            headLocation = "Sprites/Operators/hibanaHead.png";
            handLocation = "Sprites/Operators/fbiGloves.png";

            injureScream = "SFX/Characters/ScreamJager.wav";

            Knife = new Knife(position.x, position.y);
            MainDevice = new XKairos(position.x, position.y);
            Phone = new Phone(position.x, position.y);
            drone = new Drone(position.x, position.y) { UsageCount = 2 };

            Armor = 1;
            Speed = 3;
            team = "Att";

            SetSprites();
            operatorID = 45;
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
