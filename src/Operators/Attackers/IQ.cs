using System.Collections.Generic;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Operators|Attack")]
    public class IQOPEQ : OPEQ
    {
        public IQOPEQ(float xpos, float ypos) : base(xpos, ypos)
        {
            description =
"IQ has an electronic detector at its \n\n" +
"disposal, thanks to which the attack \n\n" +
"team receives information on the \n\n" +
"situation at the location \n\n" +
" \n\n" +
" \n\n" +
"        ";

            name = "IQ";
            oper = new IQ(position.x, position.y);
            _sprite = new SpriteMap(GetPath("Sprites/OperatorIcons.png"), 24, 24);
            _sprite.frame = 3;
            graphic = _sprite;
        }
    }
    public class IQ : Attacker
    {
        public IQ(float xpos, float ypos) : base(xpos, ypos)
        {
            Primary = new List<GunDev>()
            {
                new COMMANDO552(position.x, position.y),
                new G8A1(position.x, position.y)
            };

            Secondary = new List<GunDev>()
            {
                new P12(position.x, position.y) { muzzle = 1, fireSound = "SFX/guns/P12SD.wav" }
            };

            Devices = new List<Device>()
            {
                new Claymore(position.x, position.y),
                new Breach(position.x, position.y)
            };

            bodyLocation = "Sprites/Operators/gsg9.png";
            headLocation = "Sprites/Operators/iqHat.png";
            handLocation = "Sprites/Operators/GSGHand.png";

            injureScream = "SFX/Characters/ScreamIQ.wav";

            Knife = new Knife(position.x, position.y);
            MainDevice = new ElectronicsDetector(position.x, position.y);
            Phone = new NHPhone(position.x, position.y);
            drone = new Drone(position.x, position.y) { UsageCount = 2 };

            Armor = 1;
            Speed = 3;
            team = "Att"; 
            female = true;

            SetSprites();

            operatorID = 3;
        }
    }
}
