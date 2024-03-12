using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Weapon|Rifles")]
    public class Spear308 : GunDev
    {
        public Spear308(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Guns/Spear308.png"), 32, 32, false);
            graphic = _sprite;
            _sprite.frame = 0;

            center = new Vec2(16f, 16f);
            collisionSize = new Vec2(30, 8);
            collisionOffset = new Vec2(-15f, -4f);
            barrel = new Vec2(14, -0.5f);

            ammo = 31;
            maxAmmo = 30;
            magazine = 150;
            timeToReload = 3.1f;
            timeToTacticalReload = 2.4f;
            accuracy = 0.4f;
            xScope = 1.5f;
            hRecoil = 5.5f;
            uRecoil = -7.5f;
            dRecoil = 6.5f;
            fireRate = 0.08571f;
            damage = 40;
            penetration = 1;

            canBeTacticallyReloaded = true;
            name = "Spear .308";
            reticle = 11;

            yStability = 0.9f;
            xStability = 0.4f;

            fireSound = "SFX/guns/mcx300_01.wav";


            weaponClass = "AR";
            arc = 26;
        }
    }
}
