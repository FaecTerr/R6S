using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Weapon|SMG")]
    public class P90 : GunDev
    {
        public P90(float xpos, float ypos) : base(xpos, ypos)
        {
            this._sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Guns/P90.png"), 32, 32, false);
            this.graphic = this._sprite;
            this._sprite.frame = 0;

            center = new Vec2(16f, 16f);
            collisionSize = new Vec2(30f, 16f);
            collisionOffset = new Vec2(-15f, -8f);
            barrel = new Vec2(15, -0.5f);

            ammo = 51;
            maxAmmo = 50;
            magazine = 150;
            timeToReload = 3.8f;
            timeToTacticalReload = 3f;
            accuracy = 0.9f;
            xScope = 2f;
            hRecoil = 3.8f;
            uRecoil = -6.2f;
            dRecoil = 8.7f;
            fireRate = 0.06185f;
            damage = 22;

            canBeTacticallyReloaded = true;
            name = "P90";
            reticle = 7;
            penetration = 1;

            underGrip = 2;

            yStability = 0.92f;
            xStability = 0.92f;
            fireSound = "SFX/guns/p90_01.wav";

            weaponClass = "SMG";
        }
    }
}
