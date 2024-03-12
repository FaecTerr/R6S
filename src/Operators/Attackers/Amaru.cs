using System.Collections.Generic;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Operators|Attack")]
    public class AmaruOPEQ : OPEQ
    {
        public AmaruOPEQ(float xpos, float ypos) : base(xpos, ypos)
        {
            description =
"Amaru has an electronic detector at its \n\n" +
"disposal, thanks to which the attack \n\n" +
"team receives information on the \n\n" +
"situation at the location \n\n" +
" \n\n" +
" \n\n" +
"        ";

            name = "AMARU";
            oper = new Amaru(position.x, position.y);
            _sprite = new SpriteMap(GetPath("Sprites/OperatorIcons.png"), 24, 24);
            _sprite.frame = 49;
            graphic = _sprite;
        }
    }
    public class Amaru : Attacker
    {
        public Amaru(float xpos, float ypos) : base(xpos, ypos)
        {
            Primary = new List<GunDev>()
            {
                new G8A1(position.x, position.y),
                new M590A1(position.x, position.y)
            };

            Secondary = new List<GunDev>()
            {
                new SMG11(position.x, position.y),
                new ITA12S(position.x, position.y)
            };

            Devices = new List<Device>()
            {
                new Claymore(position.x, position.y),
                new Breach(position.x, position.y)
            };

            bodyLocation = "Sprites/Operators/gsg9.png";
            headLocation = "Sprites/Operators/AmaruHead.png";
            handLocation = "Sprites/Operators/GSGHand.png";

            injureScream = "SFX/Characters/ScreamAmaru.wav";

            Knife = new Knife(position.x, position.y);
            MainDevice = new Garra(position.x, position.y);
            Phone = new NHPhone(position.x, position.y);
            drone = new Drone(position.x, position.y) { UsageCount = 2 };

            Armor = 2;
            Speed = 2;
            team = "Att"; 
            female = true;

            SetSprites();

            operatorID = 49;
        }
    }
}
