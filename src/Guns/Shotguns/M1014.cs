namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Weapon|Shotguns")]
    public class M1014 : GunDev
    {
        public M1014(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Guns/M1014.png"), 32, 32, false);
            graphic = _sprite;
            _sprite.frame = 0;

            center = new Vec2(16f, 16f);
            collisionSize = new Vec2(30, 8);
            collisionOffset = new Vec2(-15f, -4f);
            barrel = new Vec2(15, -0.5f);

            ammo = 8;
            maxAmmo = 7;
            magazine = 28;
            timeToReload = 0.7f;
            timeToTacticalReload = 0.4f;
            accuracy = 0.775f;
            xScope = 1.25f;
            hRecoil = 15f;
            uRecoil = -34f;
            dRecoil = 36f;
            fireRate = 0.62f;
            damage = 17;

            gunMobility = 0.85f;
            gunADSMobility = 0.75f;

            isShotgun = true;
            semiAuto = false;
            bulletsPerShot = 8;
            name = "M1014";
            manuallyReload = true;

            yStability = 0.85f;
            xStability = 0.55f;
            fireSound = "SFX/guns/mossberg_590a_01_pump.wav";

            weaponClass = "Shotgun";
            arc = 16;
        }
    }
}
