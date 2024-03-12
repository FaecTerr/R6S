using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Operators|Attack")]
    public class GridlockOPEQ : OPEQ
    {
        public GridlockOPEQ(float xpos, float ypos) : base(xpos, ypos)
        {
            description =
"Fuze's APM-6 cluster charge\n\n" +
"propels a group of explosive \n\n" +
"cluster grenades through any \n\n" +
"soft breach surface. \n\n" +
" \n\n" +
" \n\n" +
"        ";

            name = "GRIDLOCK";
            oper = new Gridlock(position.x, position.y);
            _sprite = new SpriteMap(GetPath("Sprites/OperatorIcons.png"), 24, 24);
            _sprite.frame = 35;
            graphic = _sprite;
        }
    }
    public class Gridlock : Attacker
    {
        public Gridlock(float xpos, float ypos) : base(xpos, ypos)
        {
            Primary = new List<GunDev>()
            {
                new P90(position.x, position.y),
                new LMGE105(position.x, position.y)
            };

            Secondary = new List<GunDev>()
            {
                new SuperShorty(position.x, position.y),
                new P229(position.x, position.y)
            };

            Devices = new List<Device>()
            {
                new HardBreach(position.x, position.y),
                new SmokeGrenade(position.x, position.y)
            };

            bodyLocation = "Sprites/Operators/spetsnazHard.png";
            headLocation = "Sprites/Operators/gridlockHead.png";
            handLocation = "Sprites/Operators/SpetsnazHand.png";

            injureScream = "SFX/Characters/ScreamGridlock.wav";

            Knife = new Knife(position.x, position.y);
            MainDevice = new TRAX(position.x, position.y);
            Phone = new Phone(position.x, position.y);
            drone = new Drone(position.x, position.y) { UsageCount = 2 };

            Armor = 3;
            Speed = 1;
            team = "Att";

            female = true;
            SetSprites();
            operatorID = 35;
        }
    }
}
