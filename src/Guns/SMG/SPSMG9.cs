using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Weapon|Mini SMG")]
    public class SPSMG9 : GunDev
    {
        public SPSMG9(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Guns/SPSMG9.png"), 32, 32, false);
            graphic = _sprite;
            _sprite.frame = 0;

            center = new Vec2(16f, 16f);
            collisionSize = new Vec2(24, 8);
            collisionOffset = new Vec2(-12f, -4f);
            barrel = new Vec2(12, -0.5f);

            ammo = 21;
            maxAmmo = 20;
            magazine = 100;
            timeToReload = 3.5f;
            timeToTacticalReload = 3.2f;
            accuracy = 0.35f;
            xScope = 1.25f;
            hRecoil = 8.5f;
            uRecoil = -5.2f;
            dRecoil = 6.7f;
            fireRate = 0.06122f;
            damage = 33;

            gunMobility = 0.95f;
            gunADSMobility = 0.55f;

            canBeTacticallyReloaded = true;
            name = "SPSMG9";
            oneHand = true;
            penetration = 1;

            yStability = 0.88f;
            xStability = 0.92f;
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
