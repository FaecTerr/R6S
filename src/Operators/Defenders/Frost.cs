using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Operators|Attack")]
    public class FrostOPEQ : OPEQ
    {
        public FrostOPEQ(float xpos, float ypos) : base(xpos, ypos)
        {
            description =
"\n\n" +
" \n\n" +
" \n\n" +
" \n\n" +
" \n\n" +
" \n\n" +
"        ";
            name = "FROST";
            oper = new Frost(position.x, position.y);
            _sprite = new SpriteMap(GetPath("Sprites/OperatorIcons.png"), 24, 24);
            _sprite.frame = 40;
            graphic = _sprite;
        }
    }
    public class Frost : Defender
    {
        public Frost(float xpos, float ypos) : base(xpos, ypos)
        {

            Primary = new List<GunDev>()
            {
                new C1(position.x, position.y),
                new M870(position.x, position.y)
            };

            Secondary = new List<GunDev>()
            {
                new P9(position.x, position.y),
                new ITA12S(position.x, position.y)
            };

            Devices = new List<Device>()
            {
                new DeployableShield(position.x, position.y),
                new BulletProofCamera(position.x, position.y)
            };

            bodyLocation = "Sprites/Operators/frost.png";
            headLocation = "Sprites/Operators/frostHead.png";
            handLocation = "Sprites/Operators/fbiGloves.png";

            injureScream = "SFX/Characters/ScreamAsh.wav";

            Knife = new Knife(position.x, position.y);
            MainDevice = new Beartrap(position.x, position.y);
            Phone = new Phone(position.x, position.y);
            drone = new Drone(position.x, position.y) { UsageCount = 0 };

            Armor = 2;
            Speed = 2;
            team = "Def";
            female = true;

            SetSprites();
            operatorID = 40;

            female = true;
        }
    }
}
