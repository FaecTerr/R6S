using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Devices|Weapon")]
    public class Luison : GunDev
    {
        public Luison(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Guns/Luison.png"), 32, 32, false);
            graphic = _sprite;

            center = new Vec2(17f, 17f);
            collisionSize = new Vec2(30, 8);
            collisionOffset = new Vec2(-15f, -4f);

            ammo = 13;
            maxAmmo = 12;
            magazine = 128;
            timeToReload = 3f;
            timeToTacticalReload = 1.3f;
            accuracy = 0.85f;
            xScope = 1.3f;
            hRecoil = 15.8f;
            uRecoil = -38f;
            dRecoil = 39f;
            fireRate = 60;
            damage = 65;

            canBeTacticallyReloaded = true;
            name = "Luison";
            semiAuto = false;
            oneHand = true;

            yStability = 0.85f;
            xStability = 0.85f;
        }


        /*public override void Update()
        {
            base.Update();
            recoil *= 0.95f;
            this._visual.xscale = Level.current.camera.width / 32;
            this._visual.yscale = Level.current.camera.height / 32;
            if (_visual.alpha < 1)
            {
                _visual.alpha += 0.02f;
            }
            if (ammo <= 0 && reloading == false)
            {
                reload = 2.1f;
                reloading = true;
            }
            if (reloading == true)
            {
                reload -= 0.01666666f;
                if (reload <= 0f)
                {
                    ammo = 13;
                    reloading = false;
                }
            }
            if (this.owner != null)
            {
                OPEQ own = owner as OPEQ;
                ownDuck = own.duck;
                Operators f = own.oper as Operators;
                if (f != null)
                {
                    f.silentStep = true;
                }
                float dir = 0f;
                Level.current.camera.position = own.position - (own.position - Mouse.positionScreen) * 0.1f - new Vec2(Level.current.camera.width / 2, Level.current.camera.height / 2);
                if (f.dead == false)
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
                    shotFrames--;
                }
                this._sprite.angleDegrees = 0;
                if ((Mouse.left == InputState.Pressed) && ammo > 0 && shotFrames <= 0)
                {
                    ammo--;
                    shotFrames = 1;
                    recoil += new Vec2(Rando.Float(-15, 15), Rando.Float(-30, -50));
                    List<Bullet> firedBullets = new List<Bullet>();
                    ATShotgun shrap = new ATShotgun();
                    shrap.accuracy = 0.95f;
                    Bullet bullet = new Bullet(this.position.x + 12 * offDir + (float)(Math.Cos((double)Maths.DegToRad(dir)) * 6.0), this.position.y - 2f - (float)(Math.Sin((double)Maths.DegToRad(dir)) * 6.0), shrap, dir, null, false, -1f, false, true);
                    bullet.owner = own;
                    Level.Add(bullet);
                    firedBullets.Add(bullet);
                    if (Network.isActive)
                    {
                        NMFireGun gunEvent = new NMFireGun(null, firedBullets, 20, false, 4, false);
                        Send.Message(gunEvent, NetMessagePriority.ReliableOrdered);
                        firedBullets.Clear();
                    }
                }
            }
            else
            {
                ownDuck = null;
                _visual.alpha = 0;
            }
        }
        public override void Draw()
        {
            base.Draw();
            if (ownDuck != null && ownDuck.profile.localPlayer)
            {
                Graphics.Draw(_aim, Mouse.positionScreen.x + recoil.x, Mouse.positionScreen.y + recoil.y);
                Graphics.Draw(_visual, Level.current.camera.position.x, Level.current.camera.position.y, 1f);
            }
        }*/
    }
}
