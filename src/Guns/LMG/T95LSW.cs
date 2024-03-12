using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Weapon|LMG")]
    public class T95LSW : GunDev
    {
        public T95LSW(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Guns/T95LSW.png"), 32, 32, false);
            graphic = _sprite;
            _sprite.frame = 0;

            center = new Vec2(16f, 16f);
            collisionSize = new Vec2(30, 8);
            collisionOffset = new Vec2(-15f, -4f);
            barrel = new Vec2(22, -0.5f);

            ammo = 81;
            maxAmmo = 80;
            magazine = 80;
            timeToReload = 2.4f;
            timeToTacticalReload = 1.7f;
            accuracy = 0.65f;
            xScope = 1.25f;
            hRecoil = 6.3f;
            uRecoil = -7.4f;
            dRecoil = 8f;
            fireRate = 0.092307f;
            damage = 46;
            penetration = 1;

            canBeTacticallyReloaded = true;
            name = "T-95 LSW";


            yStability = 0.85f;
            xStability = 0.85f;
            fireSound = "SFX/guns/Mx4.wav";

            weaponClass = "LMG";
            arc = 40;
        }
    }
}
