using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Operators|Defense")]
    public class TachankinOPEQ : OPEQ
    {
        public TachankinOPEQ(float xpos, float ypos) : base(xpos, ypos)
        {
            description =
"Tachankin it is able to put \n\n" +
"pressure on the enemy, \n\n" +
"forcing him to either wait \n\n" +
"for the right moment, or \n\n" +
"start acting \n\n" +
" \n\n" +
"        ";

            name = "TACHANKIN";
            oper = new Tachankin(position.x, position.y);
            _sprite = new SpriteMap(GetPath("Sprites/OperatorIcons.png"), 24, 24);
            _sprite.frame = 4;
            graphic = _sprite;
        }
    }
    public class Tachankin : Defender
    {
        public Tachankin(float xpos, float ypos) : base(xpos, ypos)
        {
            Primary = new List<GunDev>()
            {
                new VSN(position.x, position.y),
                new DP27(position.x, position.y)
            };

            Secondary = new List<GunDev>()
            {
                new PMM(position.x, position.y),
                new GSH(position.x, position.y)
            };

            Devices = new List<Device>()
            {
                new DeployableShield(position.x, position.y),
                new BarbedWire(position.x, position.y)
            };

            bodyLocation = "Sprites/Operators/spetsnazHard.png";
            headLocation = "Sprites/Operators/tachankaHat.png";
            handLocation = "Sprites/Operators/SpetsnazTachankaHand.png";

            injureScream = "SFX/Characters/ScreamTachanka.wav";

            Knife = new Knife(position.x, position.y);
            MainDevice = new Shumikha(position.x, position.y);
            Phone = new Phone(position.x, position.y);
            drone = new Drone(position.x, position.y) { UsageCount = 0 };

            Armor = 3;
            Speed = 1;
            team = "Def";

            SetSprites();

            operatorID = 4;
        }
    }
}
