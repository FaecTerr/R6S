using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Operators|Defense")]
    public class KaidOPEQ : OPEQ
    {
        public KaidOPEQ(float xpos, float ypos) : base(xpos, ypos)
        {
            description =
" \n\n" +
" \n\n" +
" \n\n" +
" \n\n" +
" \n\n" +
" \n\n" +
"       ";
            name = "KAID";
            oper = new Kaid(position.x, position.y);
            _sprite = new SpriteMap(GetPath("Sprites/OperatorIcons.png"), 24, 24);
            _sprite.frame = 32;
            graphic = _sprite;
        }
    }
    public class Kaid : Defender
    {
        public Kaid(float xpos, float ypos) : base(xpos, ypos)
        {
            Primary = new List<GunDev>()
            {
                new AUGa2(position.x, position.y) { muzzle = 1 },
                new TCSG12(position.x, position.y)
            };

            Secondary = new List<GunDev>()
            {
                new Mag44(position.x, position.y),
                new LFP(position.x, position.y)
            };

            Devices = new List<Device>()
            {
                new C4(position.x, position.y),
                new BarbedWire(position.x, position.y)
            };

            bodyLocation = "Sprites/Operators/gigr.png";
            headLocation = "Sprites/Operators/KaidHead.png";
            handLocation = "Sprites/Operators/KaidHand.png";

            injureScream = "SFX/Characters/ScreamMute.wav";

            Knife = new Knife(position.x, position.y);
            MainDevice = new RTILA(position.x, position.y);
            Phone = new Phone(position.x, position.y);
            drone = new Drone(position.x, position.y) { UsageCount = 0 };

            Armor = 3;
            Speed = 1;
            team = "Def";


            SetSprites();

            operatorID = 32;
        }
    }
}