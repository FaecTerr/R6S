using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Operators|Defense")]
    public class MaestroOPEQ : OPEQ
    {
        public MaestroOPEQ(float xpos, float ypos) : base(xpos, ypos)
        {
            description =
" \n\n" +
" \n\n" +
" \n\n" +
" \n\n" +
" \n\n" +
" \n\n" +
"       ";
            name = "MAESTRO";
            oper = new Maestro(position.x, position.y);
            _sprite = new SpriteMap(GetPath("Sprites/OperatorIcons.png"), 24, 24);
            _sprite.frame = 46;
            graphic = _sprite;
        }
    }
    public class Maestro : Operators
    {
        public Maestro(float xpos, float ypos) : base(xpos, ypos)
        {
            Primary = new List<GunDev>()
            {
                new DP27(position.x, position.y),
                new AA12(position.x,position.y)
            };

            Secondary = new List<GunDev>()
            {
                new Bailiff(position.x, position.y),
                new LFP(position.x, position.y)
            };

            Devices = new List<Device>()
            {
                new Impact(position.x, position.y),
                new BarbedWire(position.x, position.y)
            };

            bodyLocation = "Sprites/Operators/lesion.png";
            headLocation = "Sprites/Operators/maestroHead.png";
            handLocation = "Sprites/Operators/LesionGloves.png";

            Knife = new Knife(position.x, position.y);
            MainDevice = new EvilEye(position.x, position.y);
            Phone = new Phone(position.x, position.y);
            drone = new Drone(position.x, position.y) { UsageCount = 0 };

            Armor = 3;
            Speed = 1;
            team = "Def";


            SetSprites();

            operatorID = 46;
        }
    }
}
