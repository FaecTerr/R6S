using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Operators|Defense")]
    public class EchoOPEQ : OPEQ
    {
        public EchoOPEQ(float xpos, float ypos) : base(xpos, ypos)
        {
            description =
       "Echo's unique drone \n\n" +
       "'Yokai' is good scouting \n\n" +
       "tool. With ability to \n\n" +
       "stun enemies he appears \n\n" +
       "as a good anchor and \n\n" +
       "intel gatherer \n\n" +
       "        ";
            name = "ECHO";
            oper = new Echo(position.x, position.y);
            _sprite = new SpriteMap(GetPath("Sprites/OperatorIcons.png"), 24, 24);
            _sprite.frame = 22;
            graphic = _sprite;
        }
    }
    public class Echo : Defender
    {
        public Echo(float xpos, float ypos) : base(xpos, ypos)
        {
            Primary = new List<GunDev>()
            {
                new MP5(position.x, position.y) { muzzle = 1},
                new M870(position.x, position.y)
            };

            Secondary = new List<GunDev>()
            {
                new Bearing9(position.x, position.y),
                new P229(position.x, position.y)
            };

            Devices = new List<Device>()
            {
                new DeployableShield(position.x, position.y),
                new Impact(position.x, position.y) 
            };

            //bodyLocation = "Sprites/Operators/majima.png";
            //headLocation = "Sprites/Operators/majimaHead.png";
            bodyLocation = "Sprites/Operators/echo.png";
            headLocation = "Sprites/Operators/echoHead.png";
            handLocation = "Sprites/Operators/SASGloves.png";
            
            injureScream = "SFX/Characters/ScreamEcho.wav";

            Knife = new Knife(position.x, position.y);
            MainDevice = new Yokai(position.x, position.y);
            Phone = new HandPhone(position.x, position.y);
            drone = new Drone(position.x, position.y) { UsageCount = 0 };

            Armor = 3;
            Speed = 1;
            team = "Def";

            SetSprites();

            operatorID = 22;
        }
    }
}
