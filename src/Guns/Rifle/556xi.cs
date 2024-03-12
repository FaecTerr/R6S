using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Weapon|Rifles")]
    public class xi556 : GunDev
    {
        public xi556(float xpos, float ypos) : base(xpos, ypos)
        {
            this._sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Guns/xi556.png"), 32, 32, false);
            this.graphic = this._sprite;
            this._sprite.frame = 0;

            center = new Vec2(16f, 16f);
            collisionSize = new Vec2(30, 8);
            collisionOffset = new Vec2(-15f, -4f);
            barrel = new Vec2(14, -0.5f);

            ammo = 31;
            maxAmmo = 30;
            magazine = 150;
            timeToReload = 3.3f;
            timeToTacticalReload = 2.5f;
            accuracy = 0.4f;
            xScope = 2.5f;
            hRecoil = 9.5f;
            uRecoil = -9.5f;
            dRecoil = 11.5f;
            fireRate = 0.087f;
            damage = 47;
            penetration = 1;

            canBeTacticallyReloaded = true;
            name = "556xi";
            reticle = 11;

            yStability = 0.9f;
            xStability = 0.4f;

            fireSound = "SFX/guns/mcx300_01.wav";


            weaponClass = "AR";
            arc = 26;
        }
    }
}
