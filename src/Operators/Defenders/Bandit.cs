using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Operators|Defense")]
    public class BanditOPEQ : OPEQ
    {
        public BanditOPEQ(float xpos, float ypos) : base(xpos, ypos)
        {
            description =
       "Bandit is capable of\n\n" +
       "electrifying a variety of \n\n" +
       "surfaces thanks to his \n\n" +
       "special ability: Crude \n\n" +
       "Electrical Device or CED-1 \n\n" +
       "Shock Wire. \n\n" +
       "        ";
            name = "BANDIT";
            oper = new Bandit(this.position.x, this.position.y);
            _sprite = new SpriteMap(GetPath("Sprites/OperatorIcons.png"), 24, 24);
            _sprite.frame = 2;
            graphic = _sprite;
        }
    }
    public class Bandit : Operators
    {
        public Bandit(float xpos, float ypos) : base(xpos, ypos)
        {
            Primary = new List<GunDev>()
            {
                new MP7(this.position.x, this.position.y),
                new M870(this.position.x, this.position.y)
            };

            Secondary = new List<GunDev>()
            {
                new P12(this.position.x, this.position.y)
            };

            Devices = new List<Device>()
            {
                new C4(this.position.x, this.position.y),
                new BarbedWire(this.position.x, this.position.y)
            };

            bodyLocation = "Sprites/Operators/bandit.png";
            headLocation = "Sprites/Operators/banditHat.png";
            handLocation = "Sprites/Operators/GSGHand.png";

            injureScream = "SFX/Characters/ScreamBandit.wav";

            _head.CenterOrigin();
            Knife = new Knife(position.x, position.y);
            MainDevice = new CED(position.x, position.y);
            Phone = new Phone(position.x, position.y);
            drone = new Drone(position.x, position.y) { UsageCount = 0 };

            Armor = 1;
            Speed = 3;
            team = "Def";

            SetSprites();

            operatorID = 2;
        }
    }
}
