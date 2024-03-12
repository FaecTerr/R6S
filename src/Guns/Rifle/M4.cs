using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Weapon|Rifles")]
    public class M4 : GunDev
    {
        public M4(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Guns/M4.png"), 32, 32, false);
            graphic = _sprite;
            _sprite.frame = 0;

            center = new Vec2(16f, 16f);
            collisionSize = new Vec2(30, 8);
            collisionOffset = new Vec2(-15f, -4f);
            barrel = new Vec2(15, -0.5f);

            ammo = 31;
            maxAmmo = 30;
            magazine = 150;
            timeToReload = 3.4f;
            timeToTacticalReload = 2.6f;
            accuracy = 0.6f;
            xScope = 1.25f;
            hRecoil = -3.8f;
            uRecoil = -8.5f;
            dRecoil = 10.5f;
            fireRate = 0.08f;
            damage = 44;
            penetration = 1;

            grip = 2;

            canBeTacticallyReloaded = true;
            name = "M4";
            reticle = 11;

            yStability = 0.83f;
            xStability = 0.96f;
            fireSound = "SFX/guns/s12k_01.wav";


            weaponClass = "AR";
            arc = 26;
        }
    }
}
