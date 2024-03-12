using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Weapon|DMR")]
    public class AA12 : GunDev
    {
        public AA12(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Guns/AA12.png"), 32, 32, false);
            graphic = _sprite;
            _sprite.frame = 0;

            center = new Vec2(16f, 16f);
            collisionSize = new Vec2(30, 8);
            collisionOffset = new Vec2(-15f, -4f);
            barrel = new Vec2(15, -0.5f);

            ammo = 31;
            maxAmmo = 30;
            magazine = 60;
            timeToReload = 3.4f;
            timeToTacticalReload = 2.2f;
            accuracy = 0.65f;
            xScope = 1.75f;
            hRecoil = 8.5f;
            uRecoil = -14.5f;
            dRecoil = 12f;
            fireRate = 0.2f;
            damage = 69;
            penetration = 1;

            semiAuto = false;
            canBeTacticallyReloaded = true;
            name = "ACS12";
            reticle = 8;
            highPowered = true;

            yStability = 0.94f;
            xStability = 0.96f;
            fireSound = "SFX/guns/slug_01.wav";

            weaponClass = "Slug";
            arc = 22;
        }
    }
}
