using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Operators|Attack")]
    public class TwitchOPEQ : OPEQ
    {
        public TwitchOPEQ(float xpos, float ypos) : base(xpos, ypos)
        {
            description =
"Twitch is a disabler and intel \n\n" +
"Operator.  \n\n" +
"Twitch is capable of remote \n\n" +
"disabling through her unique \n\n" +
"ability: RSD MODEL 1 \n\n" +
"SHOCK DRONE \n\n" +
"        ";

            name = "TWITCH";
            oper = new Twitch(position.x, position.y);
            _sprite = new SpriteMap(GetPath("Sprites/OperatorIcons.png"), 24, 24);
            _sprite.frame = 15;
            graphic = _sprite;
        }
    }
    public class Twitch : Attacker
    {
        public Twitch(float xpos, float ypos) : base(xpos, ypos)
        {
            Primary = new List<GunDev>()
            {
                new F2(position.x, position.y),
                new HK417(position.x, position.y)
            };

            Secondary = new List<GunDev>()
            {
                new P9(position.x, position.y),
                new LFP(position.x, position.y)
            };

            Devices = new List<Device>()
            {
                new HardBreach(position.x, position.y),
                new Claymore(position.x, position.y)
            };

            bodyLocation = "Sprites/Operators/gign.png";
            headLocation = "Sprites/Operators/twitchHat.png";
            handLocation = "Sprites/Operators/GIGNTwitchHand.png";

            injureScream = "SFX/Characters/ScreamTwitch.wav";

            Knife = new Knife(position.x, position.y);
            MainDevice = new TwitchDrone(position.x, position.y);
            Phone = new WGPhone(position.x, position.y);
            drone = new TwitchDrone(position.x, position.y) { UsageCount = 0 };

            Armor = 2;
            Speed = 2;
            team = "Att"; 
            female = true;

            SetSprites();

            operatorID = 15;
        }
    }
}
