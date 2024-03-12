using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Weapon|Pistols")]
    public class GSH : GunDev
    {
        public GSH (float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Guns/GSH.png"), 32, 32, false);
            graphic = _sprite;
            _sprite.frame = 0;

            center = new Vec2(16f, 16f);
            collisionSize = new Vec2(14, 8);
            collisionOffset = new Vec2(-7f, -4f);
            barrel = new Vec2(10, -0.5f);

            ammo = 19;
            maxAmmo = 18;
            magazine = 90;
            timeToReload = 2.5f;
            timeToTacticalReload = 1.8f;
            accuracy = 0.55f;
            hRecoil = 5.8f;
            uRecoil = -12.2f;
            dRecoil = 24.9f;
            fireRate = 0.13333f;
            damage = 24;

            gunMobility = 1f;
            gunADSMobility = 0.7f;

            xScope = 1.25f;

            canBeTacticallyReloaded = true;
            name = "GSH-18";
            semiAuto = false;
            oneHand = true;

            yStability = 0.75f;
            xStability = 0.75f;


            weaponClass = "Pistol";
            arc = 20;
        }
    }
}
