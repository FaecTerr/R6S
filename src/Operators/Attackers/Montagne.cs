using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Operators|Attack")]
    public class MontagneOPEQ : OPEQ
    {
        public MontagneOPEQ(float xpos, float ypos) : base(xpos, ypos)
        {
            description =
"The Le Roc growth shield will \n\n" +
"help Montagne protect the \n\n" +
"allies from enemy fire and \n\n" +
"break through the defense. \n\n" +
" \n\n" +
" \n\n" +
"        ";
            name = "MONTAGNE";
            oper = new Montagne(position.x, position.y);
            _sprite = new SpriteMap(GetPath("Sprites/OperatorIcons.png"), 24, 24);
            _sprite.frame = 13;
            graphic = _sprite;
        }
    }
    public class Montagne : Attacker
    {
        public Montagne(float xpos, float ypos) : base(xpos, ypos)
        {
            Primary = new List<GunDev>()
            {
                new HandShield(position.x, position.y)
            };

            Secondary = new List<GunDev>()
            {
                new P9(position.x, position.y),
                new LFP(position.x, position.y)
            };

            Devices = new List<Device>()
            {
                new SmokeGrenade(position.x, position.y),
                new Breach(position.x, position.y)
            };

            bodyLocation = "Sprites/Operators/gign.png";
            headLocation = "Sprites/Operators/montyHat.png";
            handLocation = "Sprites/Operators/GIGNGloves.png";

            injureScream = "SFX/Characters/ScreamMonty.wav";

            Knife = new Knife(position.x, position.y);
            MainDevice = new MontagneFull(position.x, position.y);
            Phone = new WGPhone(position.x, position.y);
            drone = new Drone(position.x, position.y) { UsageCount = 2 };

            Armor = 3;
            Speed = 1;
            team = "Att";


            SetSprites();

            operatorID = 13;
        }
    }
}
