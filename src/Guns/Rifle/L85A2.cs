using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Weapon|Rifles")]
    public class L85A2 : GunDev
    {
        public L85A2(float xpos, float ypos) : base(xpos, ypos)
        {
            this._sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Guns/L85A2.png"), 32, 32, false);
            this.graphic = this._sprite;
            this._sprite.frame = 0;

            center = new Vec2(16f, 16f);
            collisionSize = new Vec2(30, 8);
            collisionOffset = new Vec2(-15f, -4f);
            barrel = new Vec2(15, -0.5f);

            ammo = 31;
            maxAmmo = 30;
            magazine = 150;
            timeToReload = 3f;
            timeToTacticalReload = 1.9f;
            accuracy = 0.65f;
            xScope = 2.5f;
            hRecoil = 7.2f;
            uRecoil = -9.7f;
            dRecoil = 12.7f;
            fireRate = 0.0896f;
            damage = 47;
            penetration = 1;

            canBeTacticallyReloaded = true;
            name = "L85A2";
            reticle = 10;

            yStability = 0.66f;
            xStability = 0.96f;
            fireSound = "SFX/guns/mcx300_01.wav";


            weaponClass = "AR";
            arc = 26;
        }
    }
}
