using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class SkeletonKey : Device
    {
        public bool enabled;
        public bool disabled;

        public int magazine = 16;
        public int ammo = 6;
        public int maxAmmo = 5;

        public float reload;
        public float reloadTime = 2.4f;
        public bool canBeTactReloaded = true;
        public float tactReloadTime = 2;

        public float hRecoil = 8f;
        public float uRecoil = -9f;
        public float dRecoil = 8.5f; 
        public float accuracy = 0.58f;
        public bool highPower = true;

        public float fireRate = 0.13333f;
        public float xScope = 1.25f;
        public int damage = 24;
        public float penetration = 1;
        public int bulletsPerShot = 12;

        public string weaponClass = "Shotgun";
        public string weaponName = "Skeleton key";
        public string fireSound = "";

        public float xStability = 0.95f;
        public float yStability = 0.95f;
        public int muzzle = 0;
        public int grip = 0;

        public SkeletonKey(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Devices/JackalTracker.png"), 32, 32, false);
            graphic = _sprite;

            _sprite.frame = 0;
            center = new Vec2(16f, 16f);
            index = 41;
            UsageCount = 26;
            team = "Att";
            ShowCounter = true;
        }

        public void Swap()
        {
            if (oper != null && oper.MainGun != null)
            {
                int tempMagazine = magazine;
                magazine = oper.MainGun.magazine;
                oper.MainGun.magazine = tempMagazine;
                int tempMaxAmmo = maxAmmo;
                maxAmmo = oper.MainGun.maxAmmo;
                oper.MainGun.maxAmmo = tempMaxAmmo;
                int tempAmmo = ammo;
                ammo = oper.MainGun.ammo;
                oper.MainGun.ammo = tempAmmo;

                float tempTaReload = tactReloadTime;
                tactReloadTime = oper.MainGun.timeToTacticalReload;
                oper.MainGun.timeToTacticalReload = tempTaReload;
                float tempReload = reloadTime;
                reloadTime = oper.MainGun.timeToReload;
                oper.MainGun.timeToReload = tempReload;
                float tempRel = reload;
                reload = oper.MainGun.reload;
                oper.MainGun.reload = tempRel;
                bool tcbtr = canBeTactReloaded;
                canBeTactReloaded = oper.MainGun.canBeTacticallyReloaded;
                oper.MainGun.canBeTacticallyReloaded = tcbtr;

                float temphr = hRecoil;
                hRecoil = oper.MainGun.hRecoil;
                oper.MainGun.hRecoil = temphr;
                float tempur = uRecoil;
                uRecoil = oper.MainGun.uRecoil;
                oper.MainGun.uRecoil = tempur;
                float tempdr = dRecoil;
                dRecoil = oper.MainGun.dRecoil;
                oper.MainGun.dRecoil = tempdr;
                float tacc = accuracy;
                accuracy = oper.MainGun.accuracy;
                oper.MainGun.accuracy = tacc;

                float tfr = fireRate;
                fireRate = oper.MainGun.fireRate;
                oper.MainGun.fireRate = tfr;
                float txs = xScope;
                xScope = oper.MainGun.xScope;
                oper.MainGun.xScope = txs;
                int dmg = damage;
                damage = oper.MainGun.damage;
                oper.MainGun.damage = dmg;
                float pen = penetration;
                penetration = oper.MainGun.penetration;
                oper.MainGun.penetration = pen;
                int bps = bulletsPerShot;
                bulletsPerShot = oper.MainGun.bulletsPerShot;
                oper.MainGun.bulletsPerShot = bps;
                bool hipwr = highPower;
                highPower = oper.MainGun.highPowered;
                oper.MainGun.highPowered = hipwr;

                string wpc = weaponClass;
                weaponClass = oper.MainGun.weaponClass;
                oper.MainGun.weaponClass = wpc;
                string wpn = weaponName;
                weaponName = oper.MainGun.name;
                oper.MainGun.name = wpn;
                string fs = fireSound;
                fireSound = oper.MainGun.fireSound;
                oper.MainGun.fireSound = fs;

                float xs = xStability;
                xStability = oper.MainGun.xStability;
                oper.MainGun.xStability = xs;
                float ys = yStability;
                yStability = oper.MainGun.yStability;
                oper.MainGun.yStability = ys;
                int mzl = muzzle;
                muzzle = oper.MainGun.muzzle;
                oper.MainGun.muzzle = mzl;
                int grp = grip;
                grip = oper.MainGun.grip;
                oper.MainGun.grip = grp;
            }
        }

        public override void Update()
        {
            base.Update();
            if (oper != null && oper.MainGun != null)
            {
                enabled = !enabled;

                Swap();

                if (enabled)
                {
                    //Level.Add(new SoundSource(position.x, position.y, 80, "SFX/Devices/TrackerActivate.wav", "J"));
                    //DuckNetwork.SendToEveryone(new NMSoundSource(position, 80, "SFX/Devices/TrackerActivate.wav", "J"));
                }
                else
                {
                    //Level.Add(new SoundSource(position.x, position.y, 80, "SFX/Devices/TrackerDeactivate.wav", "J"));
                    //DuckNetwork.SendToEveryone(new NMSoundSource(position, 80, "SFX/Devices/TrackerDeactivate.wav", "J"));
                }

                if (user == null)
                {
                    user = oper;
                }
                oper.ChangeWeapon(10, 1);
            }


            if (UsageCount <= 0 && !disabled)
            {
                disabled = true;
                UsageCount = 1;
                ShowCounter = false;
            }

            if(user != null && user.MainGun != null)
            {
                if (user.MainGun.ammo + user.MainGun.magazine <= 0)
                {
                    Swap();
                }
                if (enabled)
                {
                    UsageCount = user.MainGun.magazine + user.MainGun.ammo;
                }
            }
        }

        public override void OnDrawLayer(Layer layer)
        {
            if (layer == Layer.Foreground)
            {
                if (enabled && user != null)
                {
                    if (!user.observing && user.local)
                    {
                        Vec2 Unit = Level.current.camera.size / new Vec2(320, 180);
                        Vec2 pos = Level.current.camera.position;
                        Vec2 cameraSize = Level.current.camera.size;
                    }
                }
            }
            base.OnDrawLayer(layer);
        }
    }
}
