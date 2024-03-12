using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Weapon|DMR")]
    public class HK417 : GunDev
    {
        public HK417(float xpos, float ypos) : base(xpos, ypos)
        {
            this._sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Guns/HK417.png"), 32, 32, false);
            this.graphic = this._sprite;
            this._sprite.frame = 0;

            center = new Vec2(16f, 16f);
            collisionSize = new Vec2(30, 8);
            collisionOffset = new Vec2(-15f, -4f);
            barrel = new Vec2(14, -0.5f);

            ammo = 18;
            maxAmmo = 17;
            magazine = 51;
            timeToReload = 3.5f;
            timeToTacticalReload = 2.9f;
            accuracy = 0.8f;
            xScope = 3f;
            hRecoil = 5.8f;
            uRecoil = -24.5f;
            dRecoil = 26.6f;
            fireRate = 0.09f;
            damage = 65;

            gunMobility = 0.9f;
            gunADSMobility = 0.35f;

            semiAuto = false;
            canBeTacticallyReloaded = true;
            name = "HK 417";
            reticle = 8;
            penetration = 1;

            muzzle = 2;
            highPowered = true;

            yStability = 0.85f;
            xStability = 0.45f;
            fireSound = "SFX/guns/mcx300_01.wav";


            weaponClass = "DMR";
            arc = 20;
        }
    }
}
