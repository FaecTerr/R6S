using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Operators|Defense")]
    public class ElaOPEQ : OPEQ
    {
        public ElaOPEQ(float xpos, float ypos) : base(xpos, ypos)
        {
            description =
"The sticky Grzmot mines can \n\n" +
"be anchored on surfaces, \n\n" +
"impairing hearing and causing \n\n" +
"a dizzying effect. These \n\n" +
"concussion mines are triggered \n\n" +
"upon proximity, affecting \n\n" +
"anyone within its radius.      ";
            name = "ELA";
            oper = new Ela(position.x, position.y);
            _sprite = new SpriteMap(GetPath("Sprites/OperatorIcons.png"), 24, 24);
            _sprite.frame = 26;
            graphic = _sprite;
        }
    }
    public class Ela : Defender
    {
        public Ela(float xpos, float ypos) : base(xpos, ypos)
        {
            Primary = new List<GunDev>()
            {
                new Scorpion(position.x, position.y),
                new SASG11(position.x, position.y)
            };

            Secondary = new List<GunDev>()
            {
                new RG15(position.x, position.y)
            };

            Devices = new List<Device>()
            {
                new DeployableShield(position.x, position.y),
                new BulletProofCamera(position.x, position.y)
            };

            bodyLocation = "Sprites/Operators/grom.png";
            headLocation = "Sprites/Operators/elaHead.png";
            handLocation = "Sprites/Operators/GromHand.png";

            injureScream = "SFX/Characters/ScreamAsh.wav";

            Knife = new Knife(position.x, position.y);
            MainDevice = new Grzmot(position.x, position.y);
            Phone = new NHPhone(position.x, position.y);
            drone = new Drone(position.x, position.y);

            Armor = 1;
            Speed = 3;
            team = "Def"; 
            female = true;


            SetSprites();

            operatorID = 26;
        }
    }
}
