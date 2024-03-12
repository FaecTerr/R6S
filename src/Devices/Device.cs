using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    public class Device : Holdable_plus, ICloneable, IDrawToDifferentLayers
    {
        public Device mainDevice;

        public float _shakeVal;
        public float setting;

        public int DeviceCost = 25;
        public string descriptionPoints = "Device set";
        public string destroyedPoints = "Device destroyed";
        public int placings;

        public bool zeroSpeed = false; //after throwing, removes horizontal speed
        public bool opened; //certainly for shields, but can used just as extra bool value
        public bool setted = false; //deployed or ready to using
        public bool placeable = true; //need to deploy before using

        public bool breakable = false; //can be destroyed by bullets and smth else
        public bool bulletproof = false; //to break it, you need explode it
        public bool destructingByHands = true; // can be destroyed by operator punch
        public bool destructingByElectricity = false; // can be destroyed by electricity
        public bool connectable = true; //used for cameras, to avoid removing cameras getting in available list
        //public bool ignoreDefaultDestruction = false; // allows to override destruction
        public bool cantProne; //While holding, 'prone' will be unable
        public bool cantCrouch; //While holding, 'crouch' will be unable
        public bool cantStand; //While holding, 'stand' will be unable

        public bool ShowCounter;
        public bool ShowCooldown;
        public float Cooldown;
        public float CooldownTime; 
        public float MinimalCooldownValue = 0.1f; //10%, percent to activation
        public float CooldownTakeoffOnActivation = 0.05f; //5%
        public float CooldownRestorationModifier = 1; //100%, lowering will be equal of restoration

        public bool electricible = false; //can be electrified

        public bool needsToBeGentle = true;

        public string team; //Def and Att
        public float setTime; //Time needed to deploy
        public string text = ""; 
        public int ElectroFrames;
        public bool wallSet;
        public bool scannable = true; //Can be scanned by Detector of Electronic Devices
        public bool jammed;
        public int jammedFrames;
        public bool canPlace = true;
        public bool lockSprint = true;
        public bool jammResistance; 
        public Vec2 barrelOffset = default(Vec2);

        public bool catchableByADS;
        public int unableToTake = 30;

        public float pickUp;
        public bool canPick;
        public float pickUpTime = 1;
        public int switchOffTime = 0;

        public bool oneHand;
        public bool mainHand;

        public int health = 1;
        public int index;
        public int reticle = -1;

        public int UsageCount = 1;

        public SpriteMap _sprite;
        public bool UIHD;
        public bool isSecondary;

        public string placeSound = "SFX/DevicePut.wav";
        public string pickupSound = "";

        public bool dontRotate;
        
        public Device(float xpos, float ypos) : base(xpos, ypos)
        {
            graphic = _sprite;
            thickness = 0f;
            _canFlip = false;
            weight = 0f;
            _canRaise = false;
            canPickUp = false;
        }

        public object Clone()
        {
            return new Device(position.x, position.y);
        }

        public virtual void GetDamage(int amount, bool melee = false)
        {
            if(this is ObservationThing && !breakable)
            {
                if(!bulletproof || (destructingByHands && melee))
                {
                    health -= amount;
                }
            }
            if(amount > 0 && breakable && (!bulletproof || (destructingByHands && melee)))
            {
                health -= amount;
            }

            if(health <= 0)
            {
                Break();
            }
        }

        public virtual void HittedFrom(Operators operators)
        {
            
        }

        public virtual void DeviceActive(OPEQ d)
        {
            
        }

        public override void Initialize()
        {
            base.Initialize();
            placings = UsageCount;
        }
        
        public virtual void Electrice()
        {
            if (setted)
            {
                if (ElectroFrames <= 0) //visual effect of electricity
                {
                    Level.Add(new Electricity(x, y, 32f, alpha));
                }
                if (ElectroFrames == 4)
                {
                    Level.Add(new Electricity(topRight.x, topRight.y, 32f, alpha));
                    Level.Add(new Electricity(bottomLeft.x, bottomLeft.y, 32f, alpha));
                }
                if (ElectroFrames == 9)
                {
                    Level.Add(new Electricity(topLeft.x, topLeft.y, 32f, alpha));
                    Level.Add(new Electricity(bottomRight.x, bottomRight.y, 32f, alpha));
                }

                foreach (Operators d in Level.CheckRectAll<Operators>(topLeft, bottomRight)) //electricing operators
                {
                    if (d.elecFrames <= 0 && d.team != "Def")
                    {
                        d.elecFrames = 25;
                    }
                }

                foreach (Device d in Level.CheckRectAll<Device>(topLeft, bottomRight)) //breaking by electricity
                {
                    if (d.breakable == true && d.setted == true && d.destructingByElectricity && ElectroFrames > 0 && d.oper == null)
                    {
                        d.Break();
                    }
                }
            }
        }

        public virtual void Break() 
        {
            if (oper != null)
            {
                if (oper.holdObject != this)
                {
                    float dist = 0f;
                    float dir = 60f + Rando.Float(-10f, 10f);
                    ExplosionPart ins = new ExplosionPart(position.x + (float)(Math.Cos(Maths.DegToRad(dir)) * dist), position.y - (float)(Math.Sin(Maths.DegToRad(dir)) * dist), true);
                    Level.Add(ins);
                    for (int i = 0; i < 8; i++)
                    {
                        Level.Add(new Electricity(x, y, 6f));
                    }
                    //SFX.Play("explode", 0.2f, 0f, 0f, false);

                    Level.Add(new SoundSource(position.x, position.y, 200, "SFX/Devices/DeviceBreak.wav", "J"));
                    DuckNetwork.SendToEveryone(new NMSoundSource(position, 200, "SFX/Devices/DeviceBreak.wav", "J"));

                    DuckNetwork.SendToEveryone(new NMBreakDevice(this));

                    Level.Add(SmallSmoke.New(position.x, position.y, 0.2f));
                    canPick = false;

                    connectable = false;

                    if (this is ObservationThing)
                    {
                        foreach (Phone p in Level.current.things[typeof(Phone)])
                        {
                            p.GetAvailibleCameras();
                        }
                    }
                    Level.Remove(this);
                }
            }
            else
            {
                if (owner == null)
                {
                    float dist = 0f;
                    float dir = 60f + Rando.Float(-10f, 10f);
                    ExplosionPart ins = new ExplosionPart(position.x + (float)(Math.Cos(Maths.DegToRad(dir)) * dist), position.y - (float)(Math.Sin(Maths.DegToRad(dir)) * dist), true);
                    Level.Add(ins);
                    for (int i = 0; i < 8; i++)
                    {
                        Level.Add(new Electricity(x, y, 6f));
                    }
                    //SFX.Play("explode", 0.2f, 0f, 0f, false);

                    Level.Add(new SoundSource(position.x, position.y, 200, "SFX/Devices/DeviceBreak.wav", "J"));
                    DuckNetwork.SendToEveryone(new NMSoundSource(position, 200, "SFX/Devices/DeviceBreak.wav", "J"));

                    DuckNetwork.SendToEveryone(new NMBreakDevice(this));

                    Level.Add(SmallSmoke.New(position.x, position.y, 0.2f));
                    canPick = false;

                    connectable = false;

                    if (this is ObservationThing)
                    {
                        foreach (Phone p in Level.current.things[typeof(Phone)])
                        {
                            p.GetAvailibleCameras();
                        }
                    }
                    Level.Remove(this);
                }
            }
        }

        public override bool DoHit(Bullet bullet, Vec2 hitPos)
        {
            if (breakable && !bulletproof)
            {
                GetDamage(1);
            }
            return base.DoHit(bullet, hitPos);
        }

        public override bool Hit(Bullet bullet, Vec2 hitPos)
        {
            if(breakable && !bulletproof)
            {
                GetDamage(1);
            }
            return base.Hit(bullet, hitPos);
        }

        public override bool Hurt(float points) 
        {
            if(breakable == true && setted == true)
            {
                //Break();
                return false;
            }
            return false;
        }

        public virtual void Set() //Once, after device set
        {
            if(oper != null)
            {
                if (oper.local && needsToBeGentle)
                {
                    if(oper.inventory.Count > oper.lastGunIndex && oper.inventory[oper.lastGunIndex] != null)
                    {
                        if(oper.inventory[oper.lastGunIndex] is GunDev)
                        {
                            (oper.inventory[oper.lastGunIndex] as GunDev).lockUntilReleaseAction = true;
                        }
                    }
                }
                if(mainDevice == null && oper.local && placings > 0)
                {
                    placings--;
                    PlayerStats.renown += DeviceCost;
                    PlayerStats.Save();
                    Level.Add(new RenownGained() { description = descriptionPoints, amount = DeviceCost});
                }
            }
        }

        public virtual void Activation()
        {

        }

        public virtual void PocketActivation() 
        { 

        }

        public override void Update()
        {
            base.Update();

            if(Cooldown < 0)
            {
                Cooldown = 0;
            }
            if(Cooldown > 1)
            {
                Cooldown = 1;
            }

            if(ElectroFrames > 0)
            {
                ElectroFrames--;
                Electrice();
            }

            text = "";

            if (jammedFrames > 0)
            {
                jammedFrames--;
                jammed = true;

                if (!jammResistance && health > 0)
                {
                    if (Rando.Float(1f) > 0.85f)
                    {
                        Level.Add(new JammedParticle(position.x, position.y, 64, alpha));
                        Level.Add(new JammedParticle(position.x, position.y, 64, alpha));
                    }
                }
            }
            if (jammedFrames <= 0)
            {
                jammed = false;
            }
            if(jammResistance && jammed)
            {
                jammed = false;
                jammedFrames = 0;
            }

            if(oper != null)
            {
                if(oper.holdObject == this)
                {
                    if (cantStand)
                    {
                        oper.unableToStand = 10;
                    }
                    if (cantCrouch)
                    {
                        oper.unableToCrouch = 10;
                    }
                    if (cantProne)
                    {
                        oper.unableToProne = 10;
                    }
                }
            }


            if (setted && mainDevice != null && oper != null)
            {
                //DevConsole.Log(Convert.ToString(canPick), Color.White);
                if (canPick && !oper.observing)
                {
                    if (oper.controller && oper.genericController != null)
                    {
                        if (oper.local && (oper.position - position).length < 32f && (Level.CheckLine<Block>(position, oper.position) == null || (this is CastleBarricadeAP)) && oper.priorityTaken <= 1.2)
                        {
                            oper.priorityTaken = 1.2f;
                            if (oper.duckOwner.inputProfile.genericController.MapDown(268435456, false))
                            {
                                pickingUp();
                            }
                            if (oper.duckOwner.inputProfile.genericController.MapReleased(268435456))
                            {
                                CanceledPickUP();
                            }
                        }
                    }
                    else
                    {
                        if (Mouse.positionScreen.x > topLeft.x && Mouse.positionScreen.y > topLeft.y && Mouse.positionScreen.x < bottomRight.x && Mouse.positionScreen.y < bottomRight.y)
                        {
                            if (oper.local && (oper.position - position).length < 32f && (Level.CheckLine<Block>(position, oper.position) == null || (this is CastleBarricadeAP)) && oper.priorityTaken <= 1.2f)
                            {
                                oper.priorityTaken = 1.2f;
                                if (Keyboard.Down(PlayerStats.keyBindings[4]) || Keyboard.Down(PlayerStats.keyBindingsAlternate[4]))
                                {
                                    pickingUp();
                                }
                                if (!(Keyboard.Down(PlayerStats.keyBindings[4]) || Keyboard.Down(PlayerStats.keyBindingsAlternate[4])))
                                {
                                    CanceledPickUP();
                                }
                            }
                        }
                    }
                }
            }
            
            if(zeroSpeed == true)
                hSpeed = 0f;        
        }
        
        public virtual void pickingUp()
        {
            if(pickUp <= 0 && pickupSound != "")
            {
                Level.Add(new SoundSource(position.x, position.y, 240, pickupSound, "J"));
                DuckNetwork.SendToEveryone(new NMSoundSource(position, 240, pickupSound, "J"));
            }
            pickUp += 0.01666666f;
            if (pickUp > pickUpTime)
            {
                if (mainDevice is ObservationThing)
                {
                    foreach (Phone p in Level.current.things[typeof(Phone)])
                    {
                        while (p.camIndex >= p.ConnectedCameras() && p.ConnectedCameras() > 0)
                        {
                            p.camIndex--;
                            if (p.camIndex < 0)
                            {
                                p.camIndex = 0;
                            }
                        }
                        p.DisconnectObservable(this as ObservationThing);
                    }
                }
                if (mainDevice is Throwable)
                {
                    if ((mainDevice as Throwable).inPlace && (mainDevice as Throwable).doubleActivation)
                    {
                        (mainDevice as Throwable).inPlace = false;
                        mainDevice.UsageCount -= 1;
                    }
                }
                OnPickUp();
                mainDevice.UsageCount += 1;

                connectable = false;

                if (this is ObservationThing)
                {
                    foreach (Phone p in Level.current.things[typeof(Phone)])
                    {
                        p.GetAvailibleCameras();
                    }
                }

                Level.Remove(this);
            }
        }

        public virtual void CanceledPickUP()
        {
            pickUp = 0f;

            foreach (SoundSource s in Level.CheckRectAll<SoundSource>(topLeft, bottomRight))
            {
                if(s.sound == pickupSound)
                {
                    s.Cancel();
                }
            }
        }

        public virtual void OnPickUp()
        {
            Level.Add(new SoundSource(position.x, position.y, 200, "SFX/Devices/PickUpDevice.wav", "J"));
            DuckNetwork.SendToEveryone(new NMSoundSource(position, 200, "SFX/Devices/PickUpDevice.wav", "J"));
        }

        public virtual void OnDrawLayer(Layer layer)
        {
            if(layer == Layer.Foreground)
            {
                if (oper != null)
                {
                    if (canPick && (oper.position - position).length < 32f && (Level.CheckLine<Block>(position, oper.position) == null || (this is CastleBarricadeAP)))
                    {
                        if (oper.controller && oper.genericController != null && oper.priorityTaken <= 1 && mainDevice != null)
                        {
                            Vec2 pos = Level.current.camera.position;
                            Vec2 cameraSize = Level.current.camera.size;
                            Vec2 Unit = cameraSize / new Vec2(320, 180);

                            string text = "Hold   to pick up";
                            Graphics.DrawString(text, pos + new Vec2(cameraSize.x / 320 * 160 - text.Length / 2 * 9, cameraSize.y / 180 * 70) * Unit, Color.White, 1f, null, 1f * Unit.x);

                            SpriteMap _button = new SpriteMap(GetPath("Sprites/Keys.png"), 17, 17);
                            _button.CenterOrigin();
                            _button.scale = new Vec2(0.8f * Unit.x, 0.8f * Unit.x);

                            _button.frame = PlayerStats.GetFrameOfButtonGP("LStickUP");
                            Graphics.Draw(_button, pos.x + (160 - text.Length / 2 * 9 + 44) * Unit.x, pos.y + 73 * Unit.x, 1);
                            

                            if (pickUp > 0)
                            {
                                Graphics.DrawLine(pos + new Vec2(cameraSize.x / 320 * 60, cameraSize.y / 180 * 120), pos + new Vec2(cameraSize.x / 320 * 60 + 200 * pickUp / pickUpTime, cameraSize.y / 180 * 120), Color.White, 1f, 1f);
                            }
                        }
                        else
                        {
                            if (Mouse.positionScreen.x > topLeft.x && Mouse.positionScreen.y > topLeft.y && Mouse.positionScreen.x < bottomRight.x && Mouse.positionScreen.y < bottomRight.y)
                            {
                                Vec2 pos = Level.current.camera.position;
                                Vec2 cameraSize = Level.current.camera.size;
                                Vec2 Unit = cameraSize / new Vec2(320, 180);

                                string text = "Hold   to pick up";
                                Graphics.DrawString(text, pos + new Vec2(cameraSize.x / 320 * 160 - text.Length / 2 * 9, cameraSize.y / 180 * 70), Color.White, 1f, null, 1f * Unit.x);

                                SpriteMap _widebutton = new SpriteMap(GetPath("Sprites/Keys.png"), 34, 17);
                                _widebutton.CenterOrigin();
                                _widebutton.scale = new Vec2(0.8f * Unit.x, 0.8f * Unit.x);

                                SpriteMap _button = new SpriteMap(GetPath("Sprites/Keys.png"), 17, 17);
                                _button.CenterOrigin();
                                _button.scale = new Vec2(0.8f * Unit.x, 0.8f * Unit.x);

                                if (PlayerStats.GetSizeOfButton(PlayerStats.keyBindings[4]))
                                {
                                    _widebutton.frame = PlayerStats.GetFrameOfButton(PlayerStats.keyBindings[4]);
                                    Graphics.Draw(_widebutton, pos.x + (160 - text.Length / 2 * 9 + 44) * Unit.x, pos.y + 73 * Unit.x, 1);
                                }
                                else
                                {
                                    _button.frame = PlayerStats.GetFrameOfButton(PlayerStats.keyBindings[4]);
                                    Graphics.Draw(_button, pos.x + (160 - text.Length / 2 * 9 + 44) * Unit.x, pos.y + 73 * Unit.x, 1);
                                }

                                if (pickUp > 0)
                                {
                                    Graphics.DrawLine(pos + new Vec2(cameraSize.x / 320 * 60, cameraSize.y / 180 * 120), pos + new Vec2(cameraSize.x / 320 * 60 + 200 * pickUp / pickUpTime, cameraSize.y / 180 * 120), Color.White, 1f, 1f);
                                    Graphics.DrawLine(pos + new Vec2(cameraSize.x / 320 * 60, cameraSize.y / 180 * 120), pos + new Vec2(cameraSize.x / 320 * 60 + 200, cameraSize.y / 180 * 120), Color.Gray, 2f, 0.99f);
                                }
                            }
                        }
                    }
                    if (!setted && setting > 0 && setTime > 0.1f)
                    {
                        Vec2 pos = Level.current.camera.position;
                        Vec2 cameraSize = Level.current.camera.size;

                        if (setting > 0)
                        {
                            Graphics.DrawLine(pos + new Vec2(cameraSize.x / 320 * 60, cameraSize.y / 180 * 120), pos + new Vec2(cameraSize.x / 320 * 60 + 200, cameraSize.y / 180 * 120), Color.Gray, 2f, 0.9f);
                            Graphics.DrawLine(pos + new Vec2(cameraSize.x / 320 * 60, cameraSize.y / 180 * 120), pos + new Vec2(cameraSize.x / 320 * 60 + 200 * (setting / setTime), cameraSize.y / 180 * 120), Color.White, 1f, 1f);
                        }
                    }
                }
            }
        }

        public override void Draw()
        {
            base.Draw();
            /*if (jammed && !destroyed)
            {
                SpriteMap _jam = new SpriteMap(Mod.GetPath<R6S>("Sprites/Devices/JammedLocation.png"), 32, 32, false);

                _jam.CenterOrigin();
                _jam.frame = Rando.Int(2);
                _jam.scale = new Vec2(0.4f, 0.4f);

                _jam.alpha = 0.4f;

                Graphics.Draw(_jam, position.x, position.y);
            }*/
        }
    }
}
