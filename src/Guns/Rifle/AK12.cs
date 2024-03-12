using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Weapon|Rifles")]
    public class AK12 : GunDev
    {
        public AK12(float xpos, float ypos) : base(xpos, ypos)
        {
            this._sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Guns/AK12.png"), 32, 32, false);
            this.graphic = this._sprite;
            this._sprite.frame = 0;

            center = new Vec2(16f, 16f);
            collisionSize = new Vec2(30, 8);
            collisionOffset = new Vec2(-15f, -4f);
            barrel = new Vec2(15, -0.5f);

            ammo = 31;
            maxAmmo = 30;
            magazine = 150;
            timeToReload = 3.3f;
            timeToTacticalReload = 2.5f;
            accuracy = 0.35f;
            xScope = 2f;
            hRecoil = 10.5f;
            uRecoil = -15.5f;
            dRecoil = 17.5f;
            fireRate = 0.0705f;
            damage = 45;
            penetration = 1;

            canBeTacticallyReloaded = true;
            name = "AK-12";
            reticle = 17;

            yStability = 0.7f;
            xStability = 0.5f;
            fireSound = "SFX/guns/ak47_01.wav";


            weaponClass = "AR";
            arc = 26;
        }
    }
}
