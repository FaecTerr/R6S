namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Weapon|Shotguns")]
    public class SuperShorty : GunDev
    {
        public SuperShorty(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Guns/SuperShorty.png"), 32, 32, false);
            graphic = _sprite;
            _sprite.frame = 0;

            center = new Vec2(16f, 16f);
            collisionSize = new Vec2(30, 8);
            collisionOffset = new Vec2(-15f, -4f);
            barrel = new Vec2(12, -0.5f);

            ammo = 3;
            maxAmmo = 2;
            magazine = 18;
            timeToReload = 0.5f;
            timeToTacticalReload = 0.3f;
            accuracy = 0.8f;
            xScope = 1.1f;
            hRecoil = 7.4f;
            uRecoil = -22.3f;
            dRecoil = 25.3f;
            fireRate = 0.46f;
            damage = 16;

            gunMobility = 0.85f;
            gunADSMobility = 0.75f;

            isShotgun = true;
            semiAuto = false;
            bulletsPerShot = 8;
            name = "Super shorty";
            manuallyReload = true;

            yStability = 0.95f;
            xStability = 0.35f;

            weaponClass = "Shotgun";
            arc = 24;
        }
    }
}
