using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Devices|Weapon")]
    public class BuckShotgun : Device
    {
        public List<Bullet> firedBullets = new List<Bullet>();
        public Duck ownDuck;
        public int ammo = 14;
        public float reload;
        public int ammo2 = 3;
        public float reload2;
        public int shotgunAmmo = 18;
        public float ang;
        public SpriteMap _aim;
        public int shotFrames;
        public int shotFrames2;
        public bool reloading;
        public bool reloading2;
        public Vec2 recoil;
        public BuckShotgun(float xpos, float ypos) : base(xpos, ypos)
        {
            this._sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Devices/BuckRifle.png"), 32, 32, false);
            this.graphic = this._sprite;
            this._sprite.frame = 0;
            this._aim = new SpriteMap(Mod.GetPath<R6S>("Sprites/Aim.png"), 17, 17);
            this._aim.CenterOrigin();
            this._aim.frame = 2;
            this.center = new Vec2(16f, 16f);
            this.collisionSize = new Vec2(32, 14f);
            this.collisionOffset = new Vec2(-16f, -7f);
            this.setTime = 1f;
            this._canRaise = true;
            this.placeable = false;
            this.scannable = false;
            this.zeroSpeed = false;
        }
        public override void Update()
        {
            base.Update();
            recoil *= 0.95f;
            if (ammo <= 0 && reloading == false)
            {
                reload = 3.3f;
                reloading = true;
            }
            if (reloading == true)
            {
                reload -= 0.01666666f;
                if (reload <= 0f)
                {
                    ammo = 14;
                    reloading = false;
                }
            }
            if (ammo2 <= 0 && reloading2 == false)
            {
                reload2 = 4.5f;
                reloading2 = true;
            }
            if (reloading2 == true)
            {
                reload2 -= 0.01666666f;
                if (reload2 <= 0f)
                {
                    ammo2 = 3;
                    shotgunAmmo -= 3;
                    reloading2 = false;
                }
            }
            if (this.owner != null)
            {
                Duck own = owner as Duck;
                ownDuck = own;
                float dir = 0f;
                if (own.profile.localPlayer)
                    Level.current.camera.position = own.position - (own.position - Mouse.positionScreen) * 0.1f - new Vec2(Level.current.camera.width / 2, Level.current.camera.height / 2);
                if (own.profile.localPlayer && own.dead == false)
                {
                    this._aim.scale = new Vec2(Level.current.camera.height / 128, Level.current.camera.height / 128);
                    Vec2 vec2 = Mouse.positionScreen - own.position;
                    vec2 += recoil;
                    Vec2 vec3 = new Vec2(vec2.x, vec2.y * -1f);
                    float num2 = (float)Math.Atan((double)(vec3.y / vec3.x));
                    if (num2 > 1f)
                    {
                        num2 = 1f;
                    }
                    if (num2 < -1f)
                    {
                        num2 = -1f;
                    }
                    this.angle = 6.28318548f - num2;
                    if (offDir > 0)
                    {
                        dir = 360 - this.angleDegrees;
                    }
                    else
                    {
                        dir = 180f - this.angleDegrees;
                    }
                    shotFrames--; shotFrames2--;
                }
                this._sprite.angleDegrees = 0;
                if ((Mouse.left == InputState.Down || own.profile.inputProfile.Pressed("SHOOT")) && ammo > 0 && shotFrames <= 0)
                {
                    ammo--;
                    shotFrames = 2;
                    shotFrames2 = 40;
                    recoil += new Vec2(Rando.Float(-25, 25), Rando.Float(-35, -75));
                    List<Bullet> firedBullets = new List<Bullet>();
                    AT9mm shrap = new AT9mm();
                    shrap.accuracy = 0.95f;
                    Bullet bullet = new Bullet(this.position.x + 12 * offDir + (float)(Math.Cos((double)Maths.DegToRad(dir)) * 6.0), this.position.y - 2f - (float)(Math.Sin((double)Maths.DegToRad(dir)) * 6.0), shrap, dir, null, false, -1f, false, true);
                    bullet.owner = own;
                    bullet.ammo.range = 80f;
                    bullet.ammo.rangeVariation = 5f;
                    Level.Add(bullet);
                    SFX.Play("pistolFire", 1f, 1f, 0f, false);
                    firedBullets.Add(bullet);
                    if (Network.isActive)
                    {
                        NMFireGun gunEvent = new NMFireGun(null, firedBullets, 20, false, 4, false);
                        Send.Message(gunEvent, NetMessagePriority.ReliableOrdered);
                        firedBullets.Clear();
                    }
                }
                if ((Mouse.right == InputState.Pressed) && ammo2 > 0 && shotFrames2 <= 0)
                {
                    ammo2--;
                    shotFrames2 = 19;
                    shotFrames = 40;
                    if (shotgunAmmo > 0)
                    {
                        recoil += new Vec2(Rando.Float(-35, 35), Rando.Float(-120, -190));
                        List<Bullet> firedBullets = new List<Bullet>();
                        for (int i = 0; i < 6; i++)
                        {
                            ATShotgun shrap = new ATShotgun();
                            shrap.accuracy = 0.55f;
                            Bullet bullet = new Bullet(this.position.x + 12 * offDir + (float)(Math.Cos((double)Maths.DegToRad(dir)) * 6.0), this.position.y + 2f - (float)(Math.Sin((double)Maths.DegToRad(dir)) * 6.0), shrap, dir, null, false, -1f, false, true);
                            bullet.owner = own;
                            Level.Add(bullet);
                            SFX.Play("shotgunFire2", 1f, 1f, 0f, false);
                            firedBullets.Add(bullet);
                        }
                        if (Network.isActive)
                        {
                            NMFireGun gunEvent = new NMFireGun(null, firedBullets, 20, false, 4, false);
                            Send.Message(gunEvent, NetMessagePriority.ReliableOrdered);
                            firedBullets.Clear();
                        }
                    }
                    else
                    {
                        SFX.Play("click", 1f);
                        for (int i = 0; i < 2; i++)
                        {
                            SmallSmoke s = SmallSmoke.New(this.position.x + 3f * offDir, this.position.y - 2f);
                            s.scale = new Vec2(0.3f, 0.3f);
                            s.hSpeed = Rando.Float(-0.1f, 0.1f);
                            s.vSpeed = -Rando.Float(0.05f, 0.2f);
                            s.alpha = 0.6f;
                            Level.Add(s);
                        }
                    }
                }

            }            else
            {
                ownDuck = null;
            }

        }
        public override void Draw()
        {
            base.Draw();
            if (ownDuck != null && ownDuck.profile.localPlayer)
                Graphics.Draw(_aim, Mouse.positionScreen.x+recoil.x, Mouse.positionScreen.y+recoil.y);
        }
    }
}
