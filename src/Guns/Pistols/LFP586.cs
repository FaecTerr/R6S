using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Weapon|Pistols")]
    public class LFP : GunDev
    {
        public LFP(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Guns/LFP586.png"), 32, 32, false);
            graphic = _sprite;
            _sprite.frame = 0;

            center = new Vec2(16f, 16f);
            collisionSize = new Vec2(14, 8);
            collisionOffset = new Vec2(-7f, -4f);
            barrel = new Vec2(10, -0.5f);

            ammo = 6;
            maxAmmo = 6;
            magazine = 30;
            timeToReload = 6f;
            accuracy = 0.3f;
            xScope = 1.25f;
            hRecoil = 6.8f;
            uRecoil = -55.2f;
            dRecoil = 56.8f;
            fireRate = 0.13333f;
            damage = 78;

            gunMobility = 1f;
            gunADSMobility = 0.7f;

            canBeTacticallyReloaded = false;
            name = "LFP586";
            semiAuto = false;
            oneHand = true;

            highPowered = true;
            penetration = 0.8f;

            yStability = 0.97f;
            xStability = 0.97f;
            fireSound = "SFX/guns/magnum_357_02.wav";


            weaponClass = "Pistol";
        }
    }
}
