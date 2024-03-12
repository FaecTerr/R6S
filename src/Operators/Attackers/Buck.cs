using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Operators|Attack")]
    public class BuckOPEQ : OPEQ
    {
        public BuckOPEQ(float xpos, float ypos) : base(xpos, ypos)
        {
            description =
"\n\n" +
" \n\n" +
" \n\n" +
" \n\n" +
" \n\n" +
" \n\n" +
"        ";
            name = "BUCK";
            oper = new Buck(position.x, position.y);
            _sprite = new SpriteMap(GetPath("Sprites/OperatorIcons.png"), 24, 24);
            _sprite.frame = 41;
            graphic = _sprite;
        }
    }
    public class Buck : Attacker
    {
        public Buck(float xpos, float ypos) : base(xpos, ypos)
        {

            Primary = new List<GunDev>()
            {
                new R4C(position.x, position.y),
                new AR1550(position.x, position.y)
            };

            Secondary = new List<GunDev>()
            {
                new PMM(position.x, position.y)
            };

            Devices = new List<Device>()
            {
                new FlashbangGrenade(position.x, position.y),
                new HardBreach(position.x, position.y)
            };

            bodyLocation = "Sprites/Operators/frost.png";
            headLocation = "Sprites/Operators/BuckHead.png";
            handLocation = "Sprites/Operators/fbiGloves.png";

            injureScream = "SFX/Characters/ScreamJager.wav";

            Knife = new Knife(position.x, position.y);
            MainDevice = new SkeletonKey(position.x, position.y);
            Phone = new Phone(position.x, position.y);
            drone = new Drone(position.x, position.y) { UsageCount = 2 };

            Armor = 2;
            Speed = 2;
            team = "Att";

            SetSprites();
            operatorID = 41;
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
