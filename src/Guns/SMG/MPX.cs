using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Weapon|SMG")]
    public class MPX : GunDev
    {
        public MPX(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Guns/MPX.png"), 32, 32, false);
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
            accuracy = 0.55f;
            xScope = 1.5f;
            hRecoil = 5.3f;
            uRecoil = -8.5f;
            dRecoil = 6.1f;
            fireRate = 0.0714f;
            damage = 26;

            grip = 1;
            muzzle = 2;

            penetration = 1;

            canBeTacticallyReloaded = true;
            name = "MPX";
            reticle = 11;

            yStability = 0.97f;
            xStability = 0.97f;
            fireSound = "SFX/guns/MPX.wav";

            weaponClass = "SMG";
        }
    }
}
