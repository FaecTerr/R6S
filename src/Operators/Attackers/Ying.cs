using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Operators|Attack")]
    public class YingOPEQ : OPEQ
    {
        public YingOPEQ(float xpos, float ypos) : base(xpos, ypos)
        {
            description =
"Glaz is capable of ranged\n\n" +
"shooting through his unique \n\n" +
"cope ability: HDS Flipsight. \n\n" +
"Glaz can see through all smokes \n\n" +
" \n\n" +
" \n\n" +
"        ";
            name = "YING";
            oper = new Ying(position.x, position.y);
            _sprite = new SpriteMap(GetPath("Sprites/OperatorIcons.png"), 24, 24);
            _sprite.frame = 37;
            graphic = _sprite;
        }
    }
    public class Ying : Attacker
    {
        public Ying(float xpos, float ypos) : base(xpos, ypos)
        {

            Primary = new List<GunDev>()
            {
                new SIX12SD(position.x, position.y),
                new T95LSW(position.x, position.y)
            };

            Secondary = new List<GunDev>()
            {
                new PMM(position.x, position.y)
            };

            Devices = new List<Device>()
            {
                new SmokeGrenade(position.x, position.y),
                new HardBreach(position.x, position.y)
            };

            bodyLocation = "Sprites/Operators/fbi.png";
            headLocation = "Sprites/Operators/YingHead.png";
            handLocation = "Sprites/Operators/SpetsnazGlazHand.png";

            injureScream = "SFX/Characters/ScreamTwitch.wav";

            Knife = new Knife(position.x, position.y);
            MainDevice = new Candela(position.x, position.y);
            Phone = new WGPhone(position.x, position.y);
            drone = new Drone(position.x, position.y) { UsageCount = 2 };

            Armor = 2;
            Speed = 2;
            team = "Att";
            female = true;

            SetSprites();
            operatorID = 37;
        }

        public override void Update()
        {
            base.Update();
            flashImmuneFrames = 60;
        }
    }
}
