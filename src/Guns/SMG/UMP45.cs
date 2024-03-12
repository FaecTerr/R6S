using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Weapon|SMG")]
    public class UMP45 : GunDev
    {
        public UMP45(float xpos, float ypos) : base(xpos, ypos)
        {
            this._sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Guns/UMP45.png"), 32, 32, false);
            this.graphic = this._sprite;
            this._sprite.frame = 0;

            center = new Vec2(16f, 16f);
            collisionSize = new Vec2(30, 8);
            collisionOffset = new Vec2(-15f, -4f);
            barrel = new Vec2(15, -0.5f);

            ammo = 26;
            maxAmmo = 25;
            magazine = 125;
            timeToReload = 3.8f;
            timeToTacticalReload = 3f;
            accuracy = 0.6f;
            xScope = 1.25f;
            hRecoil = 7.8f;
            uRecoil = -9.2f;
            dRecoil = 12f;
            fireRate = 0.1f;
            damage = 38;

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
            name = "UMP45";
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
