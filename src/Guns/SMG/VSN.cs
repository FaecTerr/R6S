using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Weapon|SMG")]
    public class VSN : GunDev
    {
        public VSN(float xpos, float ypos) : base(xpos, ypos)
        {
            this._sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Guns/VSN.png"), 32, 32, false);
            this.graphic = this._sprite;
            this._sprite.frame = 0;

            center = new Vec2(16f, 16f);
            collisionSize = new Vec2(30, 8);
            collisionOffset = new Vec2(-15f, -4f);
            barrel = new Vec2(14, -0.5f);

            ammo = 31;
            maxAmmo = 30;
            magazine = 150;
            timeToReload = 2.6f;
            timeToTacticalReload = 1.8f;
            accuracy = 0.45f;
            xScope = 1.25f;
            hRecoil = 3.2f;
            uRecoil = -7.2f;
            dRecoil = 8.2f;
            fireRate = 0.08f;
            damage = 34;

            canBeTacticallyReloaded = true;
            name = "9x19 VSN";
            reticle = 17;
            penetration = 1;

            yStability = 0.65f;
            xStability = 0.86f;

            fireSound = "SFX/guns/MP40_01.wav";
            horizontalPattern = new List<float>()
            {
            0, 0.6f, -0.1f, 1, 0.9f, 0.8f,
            0.6f, 0.2f, -0.3f, -0.6f, 0.6f,
            0.5f, 0.4f, 0.3f, 0.3f, -0.2f,
            -0.2f, -0.3f, -0.1f, 0.1f, 0.2f,
            0.4f, 0.2f, 0.5f, 0.6f, 0.4f,
            0.1f, -0.4f, 0.1f, 0.2f, 0.3f
            };

            verticalPattern = new List<float>()
            {
            0, 0.6f, 0.55f, 0.5f, 0.55f, 0.6f,
            0.7f, 0.6f, 0.6f, 0.65f, 0.65f,
            0.6f, 0.7f, 0.65f, 0.75f, 0.7f,
            0.75f, 0.7f, 0.8f, 0.75f, 0.85f,
            0.8f, 0.9f, 0.85f, 0.8f, 0.9f,
            1.05f, 1f, 1.1f, 1.2f, 1.3f
            };

            weaponClass = "SMG";
        }
    }
}