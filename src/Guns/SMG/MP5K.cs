using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Weapon|SMG")]
    public class MP5K : GunDev
    {
        public MP5K(float xpos, float ypos) : base(xpos, ypos)
        {
            this._sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Guns/MP5K.png"), 32, 32, false);
            this.graphic = this._sprite;
            this._sprite.frame = 0;

            center = new Vec2(16f, 16f);
            collisionSize = new Vec2(30, 8);
            collisionOffset = new Vec2(-15f, -4f);
            barrel = new Vec2(14, -0.5f);

            ammo = 26;
            maxAmmo = 25;
            magazine = 175;
            timeToReload = 2.8f;
            timeToTacticalReload = 2.1f;
            accuracy = 0.6f;
            xScope = 1.25f;
            hRecoil = 2.7f;
            uRecoil = -6.6f;
            dRecoil = 4.6f;
            fireRate = 0.07498f;
            damage = 30;

            canBeTacticallyReloaded = true;
            name = "MP5K";
            reticle = 11;

            penetration = 1;
            grip = 1;


            yStability = 0.83f;
            xStability = 0.89f;
            fireSound = "SFX/guns/mp5SD_03.wav";

            weaponClass = "SMG";
        }
    }
}
