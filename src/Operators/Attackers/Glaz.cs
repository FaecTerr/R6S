using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Operators|Attack")]
    public class GlazOPEQ : OPEQ
    {
        public GlazOPEQ(float xpos, float ypos) : base(xpos, ypos)
        {
            description =
"Glaz is capable of ranged\n\n" +
"shooting through his unique \n\n" +
"cope ability: HDS Flipsight. \n\n" +
"Glaz can see through all smokes \n\n" +
" \n\n" +
" \n\n" +
"        ";
            name = "GLAZ";
            oper = new Glaz(position.x, position.y);
            _sprite = new SpriteMap(GetPath("Sprites/OperatorIcons.png"), 24, 24);
            _sprite.frame = 5;
            graphic = _sprite;
        }
    }
    public class Glaz : Attacker
    {
        public Glaz(float xpos, float ypos) : base(xpos, ypos)
        {

            Primary = new List<GunDev>()
            {
                new OTs11(position.x, position.y),
            };

            Secondary = new List<GunDev>()
            {
                new PMM(position.x, position.y),
                new GSH(position.x, position.y)
            };

            Devices = new List<Device>()
            {
                new SmokeGrenade(position.x, position.y),
                new FragGrenade(position.x, position.y)
            };
            
            bodyLocation = "Sprites/Operators/spetsnaz.png";
            headLocation = "Sprites/Operators/glazHat.png";
            handLocation = "Sprites/Operators/SpetsnazGlazHand.png";

            injureScream = "SFX/Characters/ScreamGlaz.wav";
            roundStartPhrase = "SFX/Characters/GlazOSOK.wav";

            Knife = new Knife(position.x, position.y);
            MainDevice = new ThermalScope(position.x, position.y);
            Phone = new Phone(position.x, position.y);
            drone = new Drone(position.x, position.y) { UsageCount = 2 };

            Armor = 2;
            Speed = 2;
            team = "Att";


            SetSprites();
            operatorID = 5;
        }
    }
}
