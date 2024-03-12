using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Operators|Attack")]
    public class FuzeOPEQ : OPEQ
    {
        public FuzeOPEQ(float xpos, float ypos) : base(xpos, ypos)
        {
            description =
"Fuze's APM-6 cluster charge\n\n" +
"propels a group of explosive \n\n" +
"cluster grenades through any \n\n" +
"soft breach surface. \n\n" +
" \n\n" +
" \n\n" +
"        ";

            name = "FUZE";
            oper = new Fuze(position.x, position.y);
            _sprite = new SpriteMap(GetPath("Sprites/OperatorIcons.png"), 24, 24);
            _sprite.frame = 7;
            graphic = _sprite;
        }
    }
    public class Fuze : Attacker
    {
        public Fuze(float xpos, float ypos) : base(xpos, ypos)
        {
            Primary = new List<GunDev>()
            {
                new AK12(position.x, position.y),
                new BShield(position.x, position.y)
            };

            Secondary = new List<GunDev>()
            {
                new PMM(position.x, position.y),
                new GSH(position.x, position.y)
            };

            Devices = new List<Device>()
            {
                new HardBreach(position.x, position.y),
                new Breach(position.x, position.y)
            };

            bodyLocation = "Sprites/Operators/spetsnazHard.png";
            headLocation = "Sprites/Operators/fuzeHat.png";
            handLocation = "Sprites/Operators/SpetsnazHand.png";

            injureScream = "SFX/Characters/ScreamFuze.wav";

            Knife = new Knife(position.x, position.y);
            MainDevice = new ClusterCharge(position.x, position.y);
            Phone = new Phone(position.x, position.y);
            drone = new Drone(position.x, position.y) { UsageCount = 2 };

            Armor = 3;
            Speed = 1;
            team = "Att";

            SetSprites();
            operatorID = 7;
        }
    }
}
