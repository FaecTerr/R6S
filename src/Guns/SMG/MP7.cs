using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Weapon|SMG")]
    public class MP7 : GunDev
    {
        public MP7(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Guns/MP7.png"), 32, 32, false);
            graphic = this._sprite;
            _sprite.frame = 0;

            center = new Vec2(16f, 16f);
            collisionSize = new Vec2(30, 8);
            collisionOffset = new Vec2(-15f, -4f);
            barrel = new Vec2(12, -0.5f);

            ammo = 31;
            maxAmmo = 30;
            magazine = 150;
            timeToReload = 3f;
            timeToTacticalReload = 2.1f;
            accuracy = 0.6f;
            xScope = 1.25f;
            hRecoil = 4.2f;
            uRecoil = -7.5f;
            dRecoil = 6.6f;
            fireRate = 0.06668f;
            damage = 30;

            canBeTacticallyReloaded = true;
            name = "MP7";
            penetration = 1;

            yStability = 0.85f;
            xStability = 0.95f;
            fireSound = "SFX/guns/MP7_02.wav";

            weaponClass = "SMG";
        }
    }
}
