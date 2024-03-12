using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Operators|Defense")]
    public class SolisOPEQ : OPEQ
    {
        public SolisOPEQ(float xpos, float ypos) : base(xpos, ypos)
        {
            description =
       " \n\n" +
       " \n\n" +
       " \n\n" +
       " \n\n" +
       " \n\n" +
       " \n\n" +
       "        ";
            name = "SOLIS";
            oper = new Solis(position.x, position.y);
            _sprite = new SpriteMap(GetPath("Sprites/OperatorIcons.png"), 24, 24);
            _sprite.frame = 64;
            graphic = _sprite;
        }
    }
    public class Solis : Defender
    {
        public Solis(float xpos, float ypos) : base(xpos, ypos)
        {
            Primary = new List<GunDev>()
            {
                new P90(position.x, position.y),
                new M870(position.x, position.y)
            };

            Secondary = new List<GunDev>()
            {
                new SMG11(position.x, position.y)
            };

            Devices = new List<Device>()
            {
                new BulletProofCamera(position.x, position.y),
                new Impact(position.x, position.y)
            };

            //bodyLocation = "Sprites/Operators/majima.png";
            //headLocation = "Sprites/Operators/majimaHead.png";
            bodyLocation = "Sprites/Operators/echo.png";
            headLocation = "Sprites/Operators/solisHead.png";
            handLocation = "Sprites/Operators/SASGloves.png";

            injureScream = "SFX/Characters/ScreamEcho.wav";

            Knife = new Knife(position.x, position.y);
            MainDevice = new SpecIO(position.x, position.y);
            Phone = new HandPhone(position.x, position.y);
            drone = new Drone(position.x, position.y) { UsageCount = 0 };

            Armor = 1;
            Speed = 3;
            team = "Def";
            female = true;

            SetSprites();

            operatorID = 64;
        }
    }
}
