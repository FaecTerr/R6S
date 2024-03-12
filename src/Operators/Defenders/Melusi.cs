using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Operators|Defense")]
    public class MelusiOPEQ : OPEQ
    {
        public MelusiOPEQ(float xpos, float ypos) : base(xpos, ypos)
        {
            description =
" \n\n" +
" \n\n" +
" \n\n" +
" \n\n" +
" \n\n" +
" \n\n" +
"       ";
            name = "MELUSI";
            oper = new Melusi(position.x, position.y);
            _sprite = new SpriteMap(GetPath("Sprites/OperatorIcons.png"), 24, 24);
            _sprite.frame = 50;
            graphic = _sprite;
        }
    }
    public class Melusi : Operators
    {
        public Melusi(float xpos, float ypos) : base(xpos, ypos)
        {
            Primary = new List<GunDev>()
            {
                new MP5(position.x, position.y),
                new M1014(position.x,position.y)
            };

            Secondary = new List<GunDev>()
            {
                new RG15(position.x, position.y)
            };

            Devices = new List<Device>()
            {
                new Impact(position.x, position.y),
                new BarbedWire(position.x, position.y)
            };

            bodyLocation = "Sprites/Operators/lesion.png";
            headLocation = "Sprites/Operators/melusiHead.png";
            handLocation = "Sprites/Operators/LesionGloves.png";

            Knife = new Knife(position.x, position.y);
            MainDevice = new Banshee(position.x, position.y);
            Phone = new Phone(position.x, position.y);
            drone = new Drone(position.x, position.y) { UsageCount = 0 };

            Armor = 3;
            Speed = 1;
            team = "Def";
            female = true;

            SetSprites();

            operatorID = 50;
        }
    }
}
