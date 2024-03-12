using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Operators|Attack")]
    public class SledgeOPEQ : OPEQ
    {
        public SledgeOPEQ(float xpos, float ypos) : base(xpos, ypos)
        {
            description =
"Sledge is capable of breaching \n\n" +
"non-reinforced surfaces thanks \n\n" +
"to his tactical breaching \n\n" +
"hammer: The Caber. \n\n" +
" \n\n" +
" \n\n" +
"        ";
            name = "SLEDGE";
            oper = new Sledge(this.position.x, this.position.y);
            _sprite = new SpriteMap(GetPath("Sprites/OperatorIcons.png"), 24, 24);
            _sprite.frame = 11;
            graphic = _sprite;
        }
    }
    public class Sledge : Attacker
    {
        public Sledge(float xpos, float ypos) : base(xpos, ypos)
        {
            Primary = new List<GunDev>()
            {
                new L85A2(this.position.x, this.position.y),
                new M590A1(this.position.x, this.position.y)
            };

            Secondary = new List<GunDev>()
            {
                new P226(this.position.x, this.position.y),
                new SMG11(this.position.x, this.position.y)
            };

            Devices = new List<Device>()
            {
                new FragGrenade(this.position.x, this.position.y),
                new Breach(this.position.x, this.position.y)
            };

            bodyLocation = "Sprites/Operators/mute.png";
            headLocation = "Sprites/Operators/sledgeHat.png";
            handLocation = "Sprites/Operators/SASGloves.png";

            injureScream = "SFX/Characters/ScreamSledge.wav";


            Knife = new Knife(this.position.x, this.position.y);
            MainDevice = new Hammer(this.position.x, this.position.y);
            Phone = new Phone(this.position.x, this.position.y);
            drone = new Drone(position.x, position.y) { UsageCount = 2 };

            this.Armor = 2;
            this.Speed = 2;
            this.team = "Att";


            SetSprites();

            operatorID = 11;
        }
    }
}
