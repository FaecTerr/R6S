using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Weapon|Rifles")]
    public class C7E : GunDev
    {
        public C7E(float xpos, float ypos) : base(xpos, ypos)
        {
            this._sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Guns/C7E.png"), 48, 32, false);
            this.graphic = this._sprite;
            this._sprite.frame = 0;

            center = new Vec2(16f, 16f);
            collisionSize = new Vec2(30, 8);
            collisionOffset = new Vec2(-15f, -4f);
            barrel = new Vec2(22, -0.5f);

            ammo = 26;
            maxAmmo = 25;
            magazine = 125;
            timeToReload = 3.6f;
            timeToTacticalReload = 2.8f;
            accuracy = 0.6f;
            xScope = 1.5f;
            hRecoil = 4.8f;
            uRecoil = -11f;
            dRecoil = 9f;
            fireRate = 0.075f;
            damage = 46;
            penetration = 1;
            
            grip = 1;

            canBeTacticallyReloaded = true;
            name = "C7E";
            reticle = 11;

            yStability = 0.94f;
            xStability = 0.76f;
            fireSound = "SFX/guns/s12k_01.wav";


            weaponClass = "AR";
            arc = 26;
        }
    }
}
