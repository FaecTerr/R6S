using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Weapon|SMG")]
    public class C1 : GunDev
    {
        public C1(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Guns/9mmC1.png"), 32, 32, false);
            graphic = _sprite;
            _sprite.frame = 0;

            center = new Vec2(16f, 16f);
            collisionSize = new Vec2(30, 8);
            collisionOffset = new Vec2(-15f, -4f);
            barrel = new Vec2(15, -0.5f);

            ammo = 41;
            maxAmmo = 40;
            magazine = 120;
            timeToReload = 2.9f;
            timeToTacticalReload = 2.2f;
            accuracy = 0.7f;
            xScope = 1.4f;
            hRecoil = 3.4f;
            uRecoil = -8.1f;
            dRecoil = 8.7f;
            fireRate = 0.104348f;
            damage = 45;

            canBeTacticallyReloaded = true;
            name = "9mm C1";
            reticle = 9;

            penetration = 1;

            yStability = 0.85f;
            xStability = 0.95f;

            fireSound = "SFX/guns/mp5_03.wav";

            weaponClass = "SMG";
            arc = 20;
        }
    }
}
