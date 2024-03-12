using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Weapon|Mini SMG")]
    public class SMG11 : GunDev
    {
        public SMG11(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Guns/SMG11.png"), 32, 32, false);
            graphic = _sprite;
            _sprite.frame = 0;

            center = new Vec2(16f, 16f);
            collisionSize = new Vec2(24, 8);
            collisionOffset = new Vec2(-12f, -4f);
            barrel = new Vec2(12, -0.5f);

            ammo = 17;
            maxAmmo = 16;
            magazine = 80;
            timeToReload = 3.5f;
            timeToTacticalReload = 3.2f;
            accuracy = 0.35f;
            xScope = 1.25f;
            hRecoil = 8.7f;
            uRecoil = -12.7f;
            dRecoil = 11.2f;
            fireRate = 0.04685f;
            damage = 33;

            gunMobility = 0.95f;
            gunADSMobility = 0.55f;

            canBeTacticallyReloaded = true;
            name = "SMG-11";
            oneHand = true;
            penetration = 1;

            yStability = 0.68f;
            xStability = 0.85f;
            fireSound = "SFX/guns/mac10_01.wav";

            horizontalPattern = new List<float>()
            {
            0, -1, -0.9f, 0.8f, -0.7f, -0.6f,
            -0.5f, 0.4f, -0.3f, -0.3f, -0.2f,
            -0.3f, -0.2f, -0.2f, -0.3f, -0.2f,
            -0.2f, -0.2f
            };

            verticalPattern = new List<float>()
            {
            0, 1, 1, 1, 0.85f, 0.75f,
            0.85f, 1f, 0.8f, 0.95f, 0.8f,
            0.85f, 0.8f, 0.9f, 0.85f, 0.75f,
            0.75f, 0.7f
            };

            weaponClass = "SMG";
        }
    }
}
