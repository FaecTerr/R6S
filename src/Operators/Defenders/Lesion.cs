using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Operators|Defense")]
    public class LesionOPEQ : OPEQ
    {
        public LesionOPEQ(float xpos, float ypos) : base(xpos, ypos)
        {
            description =
"G. U. mines that Lesion \n\n" +
"uses are not only a way \n\n" +
"to harm the enemy, but \n\n" +
"also an important source \n\n" +
"of information, since they \n\n" +
"can be seen even through walls. \n\n" +
"       ";
            name = "LESION";
            oper = new Lesion(position.x, position.y);
            _sprite = new SpriteMap(GetPath("Sprites/OperatorIcons.png"), 24, 24);
            _sprite.frame = 24;
            graphic = _sprite;
        }
    }
    public class Lesion : Operators
    {
        public Lesion(float xpos, float ypos) : base(xpos, ypos)
        {
            Primary = new List<GunDev>()
            {
                new T5SMG(position.x,position.y),
                new SIX12SD(position.x, position.y)

            };

            Secondary = new List<GunDev>()
            {
                new P9(position.x, position.y)
            };

            Devices = new List<Device>()
            {
                new Impact(position.x, position.y),
                new BulletProofCamera(position.x, position.y)
            };

            bodyLocation = "Sprites/Operators/lesion.png";
            headLocation = "Sprites/Operators/LesionHead.png";
            handLocation = "Sprites/Operators/LesionGloves.png";

            Knife = new Knife(position.x, position.y);
            MainDevice = new GuMine(position.x, position.y);
            Phone = new Phone(position.x, position.y);
            drone = new Drone(position.x, position.y) { UsageCount = 0 };

            Armor = 2;
            Speed = 2;
            team = "Def";


            SetSprites();

            operatorID = 24;
        }
    }
}
