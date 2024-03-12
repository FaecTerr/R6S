using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Weapon|Pistols")]
    public class P9 : GunDev
    {
        public P9(float xpos, float ypos) : base(xpos, ypos)
        {
            this._sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Guns/P9.png"), 32, 32, false);
            this.graphic = this._sprite;
            this._sprite.frame = 0;

            center = new Vec2(16f, 16f);
            collisionSize = new Vec2(14, 8);
            collisionOffset = new Vec2(-7f, -4f);
            barrel = new Vec2(10, -0.5f);

            ammo = 19;
            maxAmmo = 18;
            magazine = 80;
            timeToReload = 2.7f;
            timeToTacticalReload = 1.9f;
            accuracy = 0.45f;
            xScope = 1.25f;
            hRecoil = 3.4f;
            uRecoil = -6.5f;
            dRecoil = 7.6f;
            fireRate = 0.13333f;
            damage = 45;

            underGrip = 1;

            gunMobility = 1f;
            gunADSMobility = 0.7f;

            canBeTacticallyReloaded = true;
            name = "P9";
            semiAuto = false;
            oneHand = true;

            yStability = 0.95f;
            xStability = 0.85f;
            fireSound = "SFX/guns/p226_01.wav";


            weaponClass = "Pistol";

            arc = 24;
        }
    }
}
