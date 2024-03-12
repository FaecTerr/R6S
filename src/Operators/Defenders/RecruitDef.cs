using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Operators")]
    public class RecruitDef : Operators
    {
        public RecruitDef(float xpos, float ypos) : base(xpos, ypos)
        {
            Primary = new List<GunDev>()
            {
                new MP5K(position.x, position.y),
                new M870(position.x, position.y)
            };

            Secondary = new List<GunDev>()
            {
                new SMG11(position.x, position.y),
                new P9(position.x, position.y)
            };

            Devices = new List<Device>()
            {
                new C4(position.x, position.y),
                new ProximityAlarm(position.x, position.y)
            };
            _sprite = new SpriteMap(GetPath("Sprites/Operators/mute.png"), 32, 32, false);
            _head = new SpriteMap(GetPath("Sprites/Operators/recruitHat.png"), 32, 32, false);
            _hand = new SpriteMap(GetPath("Sprites/Operators/SASGloves.png"), 8, 8, false);

            Knife = new Knife(position.x, position.y);
            MainDevice = new DeployableShield(position.x, position.y) { UsageCount = 2 };
            Phone = new Phone(position.x, position.y);
            drone = new Drone(position.x, position.y) { UsageCount = 0 };

            Armor = 2;
            Speed = 2;
            team = "Def";

            operatorID = -2;

            SetSprites();
        }
    }
    public class RecruitDefOPEQ : OPEQ
    {
        public RecruitDefOPEQ(float xpos, float ypos) : base(xpos, ypos)
        {
            description =
"MP5K / M870 \n\n" +
" \n\n" +
"SMG11 / P9 \n\n" +
" \n\n" +
"C4 / Proximity alarm \n\n" +
" \n\n" +
"Main device : Deployable Shield (x2) ";

            name = "RECRUIT";
            oper = new RecruitDef(position.x, position.y);
            _sprite = new SpriteMap(GetPath("Sprites/OperatorIcons.png"), 24, 24);
            _sprite.frame = 78;
            graphic = _sprite;
        }
    }
}
