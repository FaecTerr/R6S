using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Operators|Defense")]
    public class AlibiOPEQ : OPEQ
    {
        public AlibiOPEQ(float xpos, float ypos) : base(xpos, ypos)
        {
            description =
" \n\n" +
" \n\n" +
" \n\n" +
" \n\n" +
" \n\n" +
" \n\n" +
"       ";
            name = "ALIBI";
            oper = new Alibi(position.x, position.y);
            _sprite = new SpriteMap(GetPath("Sprites/OperatorIcons.png"), 24, 24);
            _sprite.frame = 30;
            graphic = _sprite;
        }
    }
    public class Alibi : Defender
    {
        public Alibi(float xpos, float ypos) : base(xpos, ypos)
        {
            Primary = new List<GunDev>()
            {
                new G36(position.x, position.y),
                new AA12(position.x, position.y)
            };

            Secondary = new List<GunDev>()
            {
                new LFP(position.x, position.y),
                new Bailiff(position.x, position.y)
            };

            Devices = new List<Device>()
            {
                new Impact(position.x, position.y),
                new DeployableShield(position.x, position.y)
            };

            bodyLocation = "Sprites/Operators/gis.png";
            headLocation = "Sprites/Operators/alibiHead.png";
            handLocation = "Sprites/Operators/AlibiHand.png";

            injureScream = "SFX/Characters/ScreamEla.wav";

            Knife = new Knife(position.x, position.y);
            MainDevice = new Prism(position.x, position.y);
            Phone = new Phone(position.x, position.y);
            drone = new Drone(position.x, position.y) { UsageCount = 0};

            Armor = 1;
            Speed = 3;
            team = "Def"; 
            female = true;


            SetSprites();

            operatorID = 30;
        }
    }
}
