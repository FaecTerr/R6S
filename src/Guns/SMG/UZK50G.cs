using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Weapon|SMG")]
    public class UZK50G : GunDev
    {
        public UZK50G(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Guns/UZK.png"), 32, 32, false);
            graphic = _sprite;
            _sprite.frame = 0;

            center = new Vec2(16f, 16f);
            collisionSize = new Vec2(30, 8);
            collisionOffset = new Vec2(-15f, -4f);
            barrel = new Vec2(15, -0.5f);

            ammo = 23;
            maxAmmo = 22;
            magazine = 132;
            timeToReload = 2.8f;
            timeToTacticalReload = 2f;
            accuracy = 0.6f;
            xScope = 1.25f;
            hRecoil = 5.8f;
            uRecoil = -4.2f;
            dRecoil = 3f;
            fireRate = 0.0857f;
            damage = 44;

            horizontalPattern = new List<float>()
            {
            0, 0.2f, -0.1f, -0.2f, -0.4f, -0.3f,
            -0.4f, 0.2f, 0.3f, 0.2f, 0.3f,
            0.2f, -0.4f, -0.3f, -0.2f, -0.1f,
            0.1f, 0.3f, 0.1f, 0.1f, 0.2f,
            0.1f, -0.6f, -0.4f, -0.7f, -0.4f,
            -0.1f, -0.4f, 0.1f, -0.5f, -0.6f
            };

            verticalPattern = new List<float>()
            {
            0, 0.45f, 0.55f, 0.45f, 0.45f, 0.6f,
            0.6f, 0.6f, 0.55f, 0.45f, 0.55f,
            0.6f, 0.6f, 0.55f, 0.65f, 0.6f,
            0.65f, 0.7f, 0.7f, 0.75f, 0.75f,
            0.7f, 0.8f, 0.85f, 0.8f, 0.8f,
            0.8f, 0.9f, 0.9f, 0.9f, 0.9f
            };

            canBeTacticallyReloaded = true;
            name = "UZK50GI";
            reticle = 10;
            penetration = 1;

            grip = 1;

            yStability = 0.86f;
            xStability = 0.86f;

            fireSound = "SFX/guns/ump45_01.wav";

            weaponClass = "SMG";
        }
    }
}
