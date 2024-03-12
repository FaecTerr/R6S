using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Weapon|LMG")]
    public class DP27 : GunDev
    {
        public DP27(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Guns/DP27.png"), 48, 32, false);
            graphic = _sprite;
            _sprite.frame = 0;

            center = new Vec2(24f, 16f);
            collisionSize = new Vec2(46, 8);
            collisionOffset = new Vec2(-23f, -4f);
            barrel = new Vec2(22, -0.5f);

            ammo = 70;
            maxAmmo = 69;
            magazine = 138;
            timeToReload = 5.3f;
            timeToTacticalReload = 4.6f;
            accuracy = 0.57f;
            xScope = 1.25f;
            hRecoil = 14f;
            uRecoil = -16f;
            dRecoil = 20f;
            fireRate = 0.10501f;
            damage = 45;

            gunMobility = 0.8f;
            gunADSMobility = 0.3f;

            highPowered = true;
            penetration = 1;

            canBeTacticallyReloaded = false;
            name = "DP27";

            yStability = 0.92f;
            xStability = 0.85f;
            fireSound = "SFX/guns/slug_01.wav";


            weaponClass = "LMG";
            arc = 40;
        }
    }
}
