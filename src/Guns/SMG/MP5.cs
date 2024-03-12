using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Weapon|SMG")]
    public class MP5 : GunDev
    {
        public MP5(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Guns/MP5.png"), 32, 32, false);
            graphic = _sprite;
            _sprite.frame = 0;

            center = new Vec2(16f, 16f);
            collisionSize = new Vec2(30, 8);
            collisionOffset = new Vec2(-15f, -4f);
            barrel = new Vec2(15, -0.5f);

            ammo = 31;
            maxAmmo = 30;
            magazine = 150;
            timeToReload = 3.5f;
            timeToTacticalReload = 2.6f;
            accuracy = 0.65f;
            xScope = 1.5f;
            hRecoil = 3.8f;
            uRecoil = -6.2f;
            dRecoil = 4.8f;
            fireRate = 0.07498f;
            damage = 27;

            canBeTacticallyReloaded = true;
            name = "MP5";
            reticle = 9;

            penetration = 1;

            yStability = 0.65f;
            xStability = 0.85f;

            fireSound = "SFX/guns/mp5_03.wav";

            weaponClass = "SMG";
        }
    }
}
