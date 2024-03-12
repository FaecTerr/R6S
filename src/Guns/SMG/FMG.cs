using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Weapon|SMG")]
    public class FMG : GunDev
    {
        public FMG(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Guns/FMG.png"), 32, 32, false);
            graphic = _sprite;
            _sprite.frame = 0;

            center = new Vec2(16f, 16f);
            collisionSize = new Vec2(30, 8);
            collisionOffset = new Vec2(-15f, -4f);
            barrel = new Vec2(12, -0.5f);

            ammo = 31;
            maxAmmo = 30;
            magazine = 150;
            timeToReload = 3f;
            timeToTacticalReload = 2.2f;
            accuracy = 0.65f;
            xScope = 1.5f;
            hRecoil = 4.4f;
            uRecoil = -4.3f;
            dRecoil = 5.8f;
            fireRate = 0.07498f;
            damage = 30;

            grip = 2;
            muzzle = 1;

            penetration = 1;

            canBeTacticallyReloaded = true;
            name = "FMG-9";
            reticle = 11;

            yStability = 0.98f;
            xStability = 0.98f;
            fireSound = "SFX/guns/mp9SD_03.wav";

            weaponClass = "SMG";
        }
    }
}
