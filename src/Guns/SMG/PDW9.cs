using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Weapon|SMG")]
    public class PDW9 : GunDev
    {
        public PDW9(float xpos, float ypos) : base(xpos, ypos)
        {
            this._sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Guns/PDW9.png"), 32, 32, false);
            this.graphic = this._sprite;
            this._sprite.frame = 0;

            center = new Vec2(16f, 16f);
            collisionSize = new Vec2(30, 8);
            collisionOffset = new Vec2(-15f, -4f);
            barrel = new Vec2(12, -0.5f);

            ammo = 51;
            maxAmmo = 50;
            magazine = 150;
            timeToReload = 3.2f;
            timeToTacticalReload = 2.5f;
            accuracy = 0.74f;
            xScope = 1.25f;
            hRecoil = 3.5f;
            uRecoil = -5.0f;
            dRecoil = 6.2f;
            fireRate = 0.075f;
            damage = 34;

            grip = 2;
            muzzle = 2;

            penetration = 1;

            canBeTacticallyReloaded = true;
            name = "PDW9";
            reticle = 7;

            yStability = 0.945f;
            xStability = 0.92f;
            fireSound = "SFX/guns/PDW9.wav";

            weaponClass = "SMG";
        }
    }
}
