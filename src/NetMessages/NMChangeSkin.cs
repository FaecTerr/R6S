using System;
using System.Collections.Generic;


namespace DuckGame.R6S
{
    public class NMChangeSkin : NMEvent
    {
        public ulong netindex;
        public string n;
        public int i;
        public Operators oper;

        public NMChangeSkin()
        {
        }

        public NMChangeSkin(Operators op, string name, int index)
        {
            oper = op;
            n = name;
            i = index;
        }

        public override void Activate()
        {
            if (oper != null)
            {
                string s = "";
                foreach (SkinElement skin in SkinsCollectionManager.allCollection)
                {
                    if (skin.name == n)
                    {
                        s = "Sprites/Guns/skins/" + skin.location + ".png";
                    }
                }

                if (s != "" && oper.inventory.Count > i)
                {
                    (oper.inventory[i] as GunDev)._sprite = new SpriteMap(Mod.GetPath<R6S>(s), 32, 32);
                    (oper.inventory[i] as GunDev).graphic = (oper.inventory[i] as GunDev)._sprite;
                    (oper.inventory[i] as GunDev).CorrectSprite();
                }

            }
        }
    }
}