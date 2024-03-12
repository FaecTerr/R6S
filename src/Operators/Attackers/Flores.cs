using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Operators|Attack")]
    public class FloresOPEQ : OPEQ
    {
        public FloresOPEQ(float xpos, float ypos) : base(xpos, ypos)
        {
            description =
" \n\n" +
"  \n\n" +
" \n\n" +
" \n\n" +
" \n\n" +
" \n\n" +
"        ";

            name = "FLORES";
            oper = new Flores(position.x, position.y);
            _sprite = new SpriteMap(GetPath("Sprites/OperatorIcons.png"), 24, 24);
            _sprite.frame = 55;
            graphic = _sprite;
        }
    }
    public class Flores : Attacker
    {
        public Flores(float xpos, float ypos) : base(xpos, ypos)
        {
            Primary = new List<GunDev>()
            {
                new AR33(position.x, position.y),
                new AR1550(position.x, position.y)
            };

            Secondary = new List<GunDev>()
            {
                new GSH(position.x, position.y)
            };

            Devices = new List<Device>()
            {
                new SmokeGrenade(position.x, position.y),
                new Claymore(position.x, position.y)
            };

            bodyLocation = "Sprites/Operators/gign.png";
            headLocation = "Sprites/Operators/floresHead.png";
            handLocation = "Sprites/Operators/SASGloves.png";

            injureScream = "SFX/Characters/ScreamMute.wav";

            Knife = new Knife(position.x, position.y);
            MainDevice = new RateroDrone(position.x, position.y);
            Phone = new RHPhone(position.x, position.y);
            drone = new Drone(position.x, position.y) { UsageCount = 2 };

            Armor = 1;
            Speed = 3;
            team = "Att";

            SetSprites();

            operatorID = 55;
        }
    }
}
