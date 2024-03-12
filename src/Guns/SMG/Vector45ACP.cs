using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Weapon|SMG")]
    public class Vector45ACP : GunDev
    {
        public Vector45ACP(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Guns/Vector45ACP.png"), 32, 32, false);
            graphic = _sprite;
            _sprite.frame = 0;

            center = new Vec2(16f, 16f);
            collisionSize = new Vec2(30, 8);
            collisionOffset = new Vec2(-15f, -4f);
            barrel = new Vec2(12, -0.5f);

            ammo = 26;
            maxAmmo = 25;
            magazine = 125;
            timeToReload = 2.6f;
            timeToTacticalReload = 2.1f;
            accuracy = 0.65f;
            xScope = 1.5f;
            hRecoil = 1.4f;
            uRecoil = -3.3f;
            dRecoil = 3.8f;
            fireRate = 0.0499998f;
            damage = 23;

            muzzle = 2;

            penetration = 1;

            canBeTacticallyReloaded = true;
            name = "Vector.45 ACP";
            reticle = 11;

            yStability = 0.98f;
            xStability = 0.98f;
            fireSound = "SFX/guns/mp9SD_03.wav";

            weaponClass = "SMG";
            arc = 16;
        }
    }
}
