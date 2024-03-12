using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Weapon|Rifles")]
    public class AR33 : GunDev
    {
        public AR33(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Guns/AR33.png"), 32, 32, false);
            graphic = _sprite;
            _sprite.frame = 0;

            center = new Vec2(16f, 16f);
            collisionSize = new Vec2(30, 8);
            collisionOffset = new Vec2(-15f, -4f);
            barrel = new Vec2(15, -0.5f);

            ammo = 26;
            maxAmmo = 25;
            magazine = 125;
            timeToReload = 1.8f;
            timeToTacticalReload = 1.3f;
            accuracy = 0.58f;
            xScope = 2f;
            hRecoil = 8f;
            uRecoil = -9f;
            dRecoil = 11.5f;
            fireRate = 0.08f;
            damage = 41;
            penetration = 1;

            canBeTacticallyReloaded = true;
            name = "AR33";
            reticle = 10;

            muzzle = 3;

            yStability = 0.66f;
            xStability = 0.96f;
            fireSound = "SFX/guns/mcx300_01.wav";

            weaponClass = "AR";
            arc = 26;
        }
    }
}
