using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Operators|Attack")]
    public class NomadOPEQ : OPEQ
    {
        public NomadOPEQ(float xpos, float ypos) : base(xpos, ypos)
        {
            description =
" \n\n" +
" \n\n" +
" \n\n" +
" \n\n" +
" \n\n" +
" \n\n" +
"        ";

            name = "NOMAD";
            oper = new Nomad(position.x, position.y);
            _sprite = new SpriteMap(GetPath("Sprites/OperatorIcons.png"), 24, 24);
            _sprite.frame = 33;
            graphic = _sprite;
        }
    }
    public class Nomad : Attacker
    {
        public Nomad(float xpos, float ypos) : base(xpos, ypos)
        {
            Primary = new List<GunDev>()
            {
                new ARX200(position.x, position.y),
                new AK12(position.x, position.y)
            };

            Secondary = new List<GunDev>()
            {
                new Mag44(position.x, position.y),
                new Meusoc(position.x, position.y)
            };

            Devices = new List<Device>()
            {
                new FlashbangGrenade(position.x, position.y),
                new Breach(position.x, position.y)
            };

            bodyLocation = "Sprites/Operators/gigr.png";
            headLocation = "Sprites/Operators/nomadHead.png";
            handLocation = "Sprites/Operators/NomadHand.png";

            injureScream = "SFX/Characters/ScreamAsh.wav";

            Knife = new Knife(position.x, position.y);
            MainDevice = new AirjabLauncher(position.x, position.y);
            Phone = new Phone(position.x, position.y);
            drone = new Drone(position.x, position.y) { UsageCount = 2 };

            Armor = 2;
            Speed = 2;
            team = "Att"; 
            female = true;


            SetSprites();

            operatorID = 33;
        }
    }
}
