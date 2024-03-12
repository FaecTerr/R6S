using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Operators|Defense")]
    public class GoyoOPEQ : OPEQ
    {
        public GoyoOPEQ(float xpos, float ypos) : base(xpos, ypos)
        {
            description =
" \n\n" +
" \n\n" +
" \n\n" +
" \n\n" +
" \n\n" +
" \n\n" +
"       ";
            name = "GOYO";
            oper = new Goyo(position.x, position.y);
            _sprite = new SpriteMap(GetPath("Sprites/OperatorIcons.png"), 24, 24);
            _sprite.frame = 36;
            graphic = _sprite;
        }
    }
    public class Goyo : Operators
    {
        public Goyo(float xpos, float ypos) : base(xpos, ypos)
        {
            Primary = new List<GunDev>()
            {
                new Vector45ACP(position.x, position.y),
                new TCSG12(position.x,position.y)

            };

            Secondary = new List<GunDev>()
            {
                new P229(position.x, position.y)
            };

            Devices = new List<Device>()
            {
                new C4(position.x, position.y),
                new DeployableShield(position.x, position.y)
            };

            bodyLocation = "Sprites/Operators/lesion.png";
            headLocation = "Sprites/Operators/GoyoHead.png";
            handLocation = "Sprites/Operators/LesionGloves.png";

            Knife = new Knife(position.x, position.y);
            MainDevice = new Vulkan(position.x, position.y);
            Phone = new Phone(position.x, position.y);
            drone = new Drone(position.x, position.y) { UsageCount = 0 };

            Armor = 2;
            Speed = 2;
            team = "Def";


            SetSprites();

            operatorID = 36;
        }
    }
}
