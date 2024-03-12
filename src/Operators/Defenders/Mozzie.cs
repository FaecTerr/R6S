using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Operators|Defense")]
    public class MozzieOPEQ : OPEQ
    {
        public MozzieOPEQ(float xpos, float ypos) : base(xpos, ypos)
        {
            description =
" \n\n" +
" \n\n" +
" \n\n" +
" \n\n" +
" \n\n" +
" \n\n" +
"       ";
            name = "MOZZIE";
            oper = new Mozzie(position.x, position.y);
            _sprite = new SpriteMap(GetPath("Sprites/OperatorIcons.png"), 24, 24);
            _sprite.frame = 34;
            graphic = _sprite;
        }
    }
    public class Mozzie : Operators
    {
        public Mozzie(float xpos, float ypos) : base(xpos, ypos)
        {
            Primary = new List<GunDev>()
            {
                new P10Roni(position.x, position.y),
                new F2(position.x,position.y)

            };

            Secondary = new List<GunDev>()
            {
                new P229(position.x, position.y)
            };

            Devices = new List<Device>()
            {
                new C4(position.x, position.y),
                new BarbedWire(position.x, position.y)
            };

            bodyLocation = "Sprites/Operators/lesion.png";
            headLocation = "Sprites/Operators/mozzieHead.png";
            handLocation = "Sprites/Operators/LesionGloves.png";

            Knife = new Knife(position.x, position.y);
            MainDevice = new ParasyteLauncher(position.x, position.y);
            Phone = new Phone(position.x, position.y);
            drone = new Drone(position.x, position.y) { UsageCount = 0 };

            Armor = 1;
            Speed = 3;
            team = "Def";


            SetSprites();

            operatorID = 34;
        }
    }
}
