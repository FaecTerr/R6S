using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Operators|Defense")]
    public class ValkyrieOPEQ : OPEQ
    {
        public ValkyrieOPEQ(float xpos, float ypos) : base(xpos, ypos)
        {
            description =
       "The ability to install \n\n" +
       "3 cameras ('Black eye')\n\n" +
       "anywhere is the main \n\n" +
       "feature of Valkirye \n\n" +
       " \n\n" +
       " \n\n" +
       "        ";
            name = "VALKYRIE";
            oper = new Valkyrie(this.position.x, this.position.y);
            _sprite = new SpriteMap(GetPath("Sprites/OperatorIcons.png"), 24, 24);
            _sprite.frame = 20;
            graphic = _sprite;
        }
    }
    public class Valkyrie : Defender
    {
        public Valkyrie(float xpos, float ypos) : base(xpos, ypos)
        {
            Primary = new List<GunDev>()
            {
                new MPX(position.x, position.y),
                new M1014(position.x, position.y)
            };

            Secondary = new List<GunDev>()
            {
                new D50(position.x, position.y)
            };

            Devices = new List<Device>()
            {
                new C4(position.x, position.y),
                new Impact(position.x, position.y)
            };

            bodyLocation = "Sprites/Operators/valkyrie.png";
            headLocation = "Sprites/Operators/valkyrieHat.png";
            handLocation = "Sprites/Operators/ValkHand.png";

            injureScream = "SFX/Characters/ScreamValkyrie.wav";

            Knife = new Knife(position.x, position.y);
            MainDevice = new BlackEye(position.x, position.y);
            Phone = new Phone(position.x, position.y);
            drone = new Drone(position.x, position.y) { UsageCount = 0 };

            Armor = 2;
            Speed = 2;
            team = "Def"; 
            female = true;

            SetSprites();

            operatorID = 20;
        }
    }
}
