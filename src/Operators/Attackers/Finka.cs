using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Operators|Attack")]
    public class FinkaOPEQ : OPEQ
    {
        public FinkaOPEQ(float xpos, float ypos) : base(xpos, ypos)
        {
            description =
" \n\n" +
" \n\n" +
" \n\n" +
" \n\n" +
" \n\n" +
" \n\n" +
"        ";

            name = "FINKA";
            oper = new Finka(position.x, position.y);
            _sprite = new SpriteMap(GetPath("Sprites/OperatorIcons.png"), 24, 24);
            _sprite.frame = 31;
            graphic = _sprite;
        }
    }
    public class Finka : Attacker
    {
        public Finka(float xpos, float ypos) : base(xpos, ypos)
        {
            Primary = new List<GunDev>()
            {
                new Spear308(position.x, position.y),
                new SASG11(position.x, position.y)
            };

            Secondary = new List<GunDev>()
            {
                new PMM(position.x, position.y)

            };

            Devices = new List<Device>()
            {
                new FragGrenade(position.x, position.y),
                new HardBreach(position.x, position.y)
            };

            bodyLocation = "Sprites/Operators/CBRN.png";
            headLocation = "Sprites/Operators/finkaHead.png";
            handLocation = "Sprites/Operators/FBIGloves.png";

            injureScream = "SFX/Characters/ScreamDokkaebi.wav";

            Knife = new Knife(position.x, position.y);
            MainDevice = new BuffFinka(position.x, position.y);
            Phone = new NHPhone(position.x, position.y);
            drone = new Drone(position.x, position.y) { UsageCount = 2 };

            Armor = 2;
            Speed = 2;
            team = "Att";
            female = true;


            SetSprites();

            operatorID = 31;
        }
    }
}
