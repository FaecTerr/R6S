using System;
using System.Collections.Generic;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Devices|Weapon")]
    public class GrenadeCannon : Launchers
    {
        public GrenadeCannon(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Devices/GrenadeCannon.png"), 22, 14, false);
            graphic = _sprite;
            _sprite.frame = 0;
            center = new Vec2(11f, 7f);
            collisionSize = new Vec2(20f, 12f);
            collisionOffset = new Vec2(-10f, -6f);
            _canRaise = true;
            weight = 0.9f;
            thickness = 0.1f;
            placeable = false;
            breakable = false;
            zeroSpeed = false;
            scannable = false;

            Missiles1 = 3;

            DeviceCost = 20;
            descriptionPoints = "Breach charge";
            
            index = 17;
        }

        public override void SetMissile()
        {
            Vec2 pos = new Vec2(position.x + (float)(Math.Cos(angle) * 10 * offDir), position.y + (float)(Math.Sin(angle) * 2));
            missile = new GrenadeCharge(pos.x, pos.y);
            base.SetMissile();
        }
    }

    [BaggedProperty("isFatal", false)]
    public class GrenadeCharge : Device
    {
        public List<Bullet> firedBullets = new List<Bullet>();
        public float timer = 2f;
        public int degr;

        public GrenadeCharge(float xval, float yval) : base(xval, yval)
        {
            _canRaise = false;
            _sprite = new SpriteMap(GetPath("Sprites/Devices/Charge.png"), 16, 16, false);
            graphic = _sprite;
            center = new Vec2(8f, 8f);
            collisionOffset = new Vec2(-4f, -6f);
            collisionSize = new Vec2(8f, 12f);
            gravMultiplier = 0.2f;
            weight = 0.9f;
            thickness = 0.1f;
            _sprite.AddAnimation("idle", 0.2f, true, new int[] { 0, 1, 2 });
            _sprite.AddAnimation("breach", 0.2f, false, new int[] { 3 });
            _sprite.SetAnimation("idle");

            canPickUp = false;
            zeroSpeed = false;

            destructingByElectricity = false;
            catchableByADS = true;

            bulletproof = true;
            destructingByHands = false;
        }

        public override void Update()
        {
            if (setted && timer > 0)
            {
                timer -= 0.01666666f;
            }
            if (setted && timer <= 0)
            {
                Explode();
            }
            base.Update();
        }

        public virtual void Explode()
        {
            Level.Add(new Explosion(position.x, position.y, 28, 10, "S") { shootedBy = oper });
            Level.Add(new Explosion(position.x, position.y, 48, 60, "N") { shootedBy = oper });

            DuckNetwork.SendToEveryone(new NMExplosion(position, 28, 10, "S", oper));
            DuckNetwork.SendToEveryone(new NMExplosion(position, 48, 60, "N", oper));

            Level.Add(new SoundSource(position.x, position.y, 400, "SFX/explo/explosion_barrel.wav", "J"));
            DuckNetwork.SendToEveryone(new NMSoundSource(position, 400, "SFX/explo/explosion_barrel.wav", "J"));

            Level.Add(new ExplosionPart(position.x, position.y, true));
            int num = 6;
            if (Graphics.effectsLevel < 2)
            {
                num = 3;
            }
            for (int i = 0; i < num; i++)
            {
                float dir = i * 60f + Rando.Float(-10f, 10f);
                float dist = Rando.Float(20f, 20f);
                ExplosionPart ins = new ExplosionPart(position.x + (float)(Math.Cos(Maths.DegToRad(dir)) * dist), position.y - (float)(Math.Sin(Maths.DegToRad(dir)) * dist), true);
                Level.Add(ins);
            }
            Graphics.FlashScreen();
            //SFX.Play("explode", 1f, 0f, 0f, false);

            for (int i = 0; i < 40; i++)
            {
                float dir = i * 9f - 5f;
                ATShrapnel shrap = new ATShrapnel();
                shrap.range = 48f;
                Bullet bullet = new Bullet(position.x + (float)(Math.Cos(Maths.DegToRad(dir)) * 6.0), position.y - (float)(Math.Sin(Maths.DegToRad(dir)) * 6.0), shrap, dir, null, false, -1f, false, true);
                Level.Add(bullet);
                firedBullets.Add(bullet);
                if (Network.isActive)
                {
                    NMFireGun gunEvent = new NMFireGun(null, firedBullets, 20, false, 4, false);
                    Send.Message(gunEvent, NetMessagePriority.ReliableOrdered);
                    firedBullets.Clear();
                }
                Level.Remove(this);
            }

        }

        public override void OnSolidImpact(MaterialThing with, ImpactedFrom from)
        {
            base.OnSolidImpact(with, from);
            if (with != null)
            {
                if ((with is Block || with is DeployableShieldAP) && setted == false)
                {
                    Level.Add(new SoundSource(position.x, position.y, 400, "SFX/Devices/ASHCharge.wav", "J"));
                    DuckNetwork.SendToEveryone(new NMSoundSource(position, 400, "SFX/Devices/ASHCharge.wav", "J"));

                    _sprite.SetAnimation("breach");
                    _enablePhysics = false;
                    grounded = true;
                    _hSpeed = 0f;
                    _vSpeed = 0f;
                    gravMultiplier = 0f;
                    setted = true;
                    frame = 2;
                    if (from == ImpactedFrom.Right)
                    {
                        angleDegrees = 0;
                        degr = 0;
                    }
                    if (from == ImpactedFrom.Left)
                    {
                        angleDegrees = 180;
                        degr = 180;
                    }
                    if (from == ImpactedFrom.Top)
                    {
                        angleDegrees = 270;
                        degr = 270;
                    }
                    if (from == ImpactedFrom.Bottom)
                    {
                        angleDegrees = 90;
                        degr = 90;
                    }
                }
            }
        }
    }
}
