using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Weapon|Rifles")]
    public class COMMANDO552 : GunDev
    {
        public COMMANDO552(float xpos, float ypos) : base(xpos, ypos)
        {
            this._sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Guns/Commando.png"), 32, 32, false);
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
            timeToTacticalReload = 2.5f;
            accuracy = 0.45f;
            xScope = 2f;
            hRecoil = 8.5f;
            uRecoil = -14.5f;
            dRecoil = 10.5f;
            fireRate = 0.08696f;
            damage = 48;
            penetration = 1;

            canBeTacticallyReloaded = true;
            name = "552 Commando";

            yStability = 0.85f;
            xStability = 0.9f;
            fireSound = "SFX/guns/s12k_01.wav";


            weaponClass = "AR";
            arc = 26;
        }
    }
}
