using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Weapon|SMG")]
    public class K1A : GunDev
    {
        public K1A(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Guns/K1A.png"), 32, 32, false);
            graphic = _sprite;
            _sprite.frame = 0;

            center = new Vec2(16f, 16f);
            collisionSize = new Vec2(30, 8);
            collisionOffset = new Vec2(-15f, -4f);
            barrel = new Vec2(15, -0.5f);

            ammo = 31;
            maxAmmo = 30;
            magazine = 150;
            timeToReload = 2.3f;
            timeToTacticalReload = 1.8f;
            accuracy = 0.5f;
            xScope = 1.6f;
            hRecoil = 0.7f;
            uRecoil = -4.5f;
            dRecoil = 5.5f;
            fireRate = 0.08333f;
            damage = 36;
            penetration = 1;
            grip = 2;

            horizontalPattern = new List<float>()
            {
            0, 0.6f, -0.1f, -0.5f, 0.05f, -0.05f,
            0.1f, 0.4f, 0.3f, 0.4f, -0.2f,
            -0.05f, -0.05f, -0.05f, -0.1f, 0.05f,
            0.05f, -0.05f, -0.05f, -0.1f, 0.05f,
            0.05f, -0.05f, 0.05f, -0.1f, -0.3f,
            -0.5f, -0.6f, -0.7f, -0.7f, -0.7f
            };

            verticalPattern = new List<float>()
            {
            0, 0.75f, 0.75f, 0.75f, 0.75f, 0.75f,
            0.75f, 0.75f, 0.8f, 0.8f, 0.8f,
            0.8f, 0.8f, 0.95f, 0.85f, 0.9f,
            0.85f, 0.9f, 0.9f, 0.9f, 0.85f,
            0.9f, 0.95f, 0.95f, 0.95f, 0.95f,
            1.05f, 1f, 1.05f, 1.05f, 1.05f
            };

            canBeTacticallyReloaded = true;
            name = "K1A";
            reticle = 11;

            yStability = 0.75f;
            xStability = 0.96f;
            fireSound = "SFX/guns/K1A.wav";
            reloadSound = "SFX/guns/K1AReload.wav";


            weaponClass = "SMG";
            arc = 10;
        }
    }
}
