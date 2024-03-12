using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Operators|Attack")]
    public class DokkaebiOPEQ : OPEQ
    {
        public DokkaebiOPEQ(float xpos, float ypos) : base(xpos, ypos)
        {
            description =
" \n\n" +
" \n\n" +
" \n\n" +
" \n\n" +
" \n\n" +
" \n\n" +
"        ";

            name = "DOKKAEBI";
            oper = new Dokkaebi(position.x, position.y);
            _sprite = new SpriteMap(GetPath("Sprites/OperatorIcons.png"), 24, 24);
            _sprite.frame = 29;
            graphic = _sprite;
        }
    }
    public class Dokkaebi : Attacker
    {
        public Dokkaebi(float xpos, float ypos) : base(xpos, ypos)
        {
            Primary = new List<GunDev>()
            {
                new Mk14(position.x, position.y)
            };

            Secondary = new List<GunDev>()
            {
                new SMG12(position.x, position.y)
                
            };

            Devices = new List<Device>()
            {
                new SmokeGrenade(position.x, position.y),
                new FlashbangGrenade(position.x, position.y)
            };
            
            bodyLocation = "Sprites/Operators/707th.png";
            headLocation = "Sprites/Operators/dokkaebiHead.png";
            handLocation = "Sprites/Operators/FBIGloves.png";

            injureScream = "SFX/Characters/ScreamDokkaebi.wav";

            Knife = new Knife(position.x, position.y);
            MainDevice = new HackingDevice(position.x, position.y);
            Phone = new Tablet(position.x, position.y);
            drone = new Drone(position.x, position.y) { UsageCount = 2 };

            Armor = 2;
            Speed = 2;
            team = "Att"; 
            female = true;


            SetSprites();

            operatorID = 29;
        }
    }
}
