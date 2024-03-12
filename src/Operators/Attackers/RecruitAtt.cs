using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Operators")]
    public class RecruitAtt : Operators
    {
        public RecruitAtt(float xpos, float ypos) : base(xpos, ypos)
        {
            Primary = new List<GunDev>()
            {
                new L85A2(position.x, position.y),
                new Mk14(position.x, position.y)
            };

            Secondary = new List<GunDev>()
            {
                new P12(position.x, position.y),
                new SuperShorty(position.x, position.y)
            };

            Devices = new List<Device>()
            {
                new SmokeGrenade(position.x, position.y),
                new FlashbangGrenade(position.x, position.y)
            };
            _sprite = new SpriteMap(GetPath("Sprites/Operators/mute.png"), 32, 32, false);
            _head = new SpriteMap(GetPath("Sprites/Operators/recruitHat.png"), 32, 32, false);
            _hand = new SpriteMap(GetPath("Sprites/Operators/SASGloves.png"), 8, 8, false);

            Knife = new Knife(position.x, position.y);
            MainDevice = new FragGrenade(position.x, position.y) { UsageCount = 3};
            Phone = new Phone(position.x, position.y);
            drone = new Drone(position.x, position.y) { UsageCount = 2 };

            Armor = 2;
            Speed = 2;
            team = "Att";

            operatorID = -1;

            SetSprites();
        }
    }    
    //[EditorGroup("Faecterr's|Operators|Defense")]
    public class RecruitAttOPEQ : OPEQ
    {
        public RecruitAttOPEQ(float xpos, float ypos) : base(xpos, ypos)
        {
            description =
"L85A2 / Mk14 EBR \n\n" +
" \n\n" +
"P12 / Super shorty \n\n" +
" \n\n" +
"Smoke grenade / Flashbang grenade \n\n" +
" \n\n" +
"Main device : Frag grenade (x2) ";

            name = "RECRUIT";
            oper = new RecruitAtt(position.x, position.y);
            _sprite = new SpriteMap(GetPath("Sprites/OperatorIcons.png"), 24, 24);
            _sprite.frame = 78;
            graphic = _sprite;
        }
    }
}
