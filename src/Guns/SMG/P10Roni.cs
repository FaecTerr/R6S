using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Weapon|SMG")]
    public class P10Roni : GunDev
    {
        public P10Roni(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Guns/P10Roni.png"), 32, 32, false);
            graphic = _sprite;
            _sprite.frame = 0;

            center = new Vec2(16f, 16f);
            collisionSize = new Vec2(30, 8);
            collisionOffset = new Vec2(-15f, -4f);
            barrel = new Vec2(12, -0.5f);

            ammo = 20;
            maxAmmo = 19;
            magazine = 120;
            timeToReload = 3f;
            timeToTacticalReload = 2.1f;
            accuracy = 0.5f;
            xScope = 1.25f;
            hRecoil = 0.8f;
            uRecoil = -4.5f;
            dRecoil = 4.7f;
            fireRate = 0.06315f;
            damage = 26;

            canBeTacticallyReloaded = true;
            name = "P10 Roni";
            penetration = 1;

            grip = 2;
            reticle = 7;

            yStability = 0.85f;
            xStability = 0.95f;
            fireSound = "SFX/guns/MP7_02.wav";

            weaponClass = "SMG";
        }
    }
}
